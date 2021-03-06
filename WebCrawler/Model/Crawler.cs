﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Configuration;
using WebCrawler.Interfaces;
using System.Text.RegularExpressions;
using WebCrawler.Repositories;
using System.Collections.ObjectModel;
using WebCrawler.Model.Data_Storage;

namespace WebCrawler.Model
{
    public class Crawler
    {
        private IRepos _externalUrlRepository;
        private IRepos _otherUrlRepository;
        private IRepos _failedUrlRepository;
        private IRepos _currentPageUrlRepository;
        private static List<Page> _pages = new List<Page>();
        private static List<string> _exceptions = new List<string>();
        private bool isCurrentPage = true;
        private static List<string> urlsWithTopics = new List<string>();
        private static ObservableCollection<Link> links = new ObservableCollection<Link>();
        private static ObservableCollection<Log> log = new ObservableCollection<Log>();

        private static Crawler instance = null;
        private static readonly object padlock = new object();

        Crawler()
        {
        }

        public static Crawler Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Crawler(new ExternalUrlRepository(), new OtherUrlRepository(), new FailedUrlRepository(), new CurrentPageUrlRepository());
                    }
                    return instance;
                }
            }
        }

        public ObservableCollection<Log> Log
        {
            get { return log; }
            set { log = value; }
        }

        public ObservableCollection<Link> Links
        {
            get { return links; }
            set { links = value; }
        }

        //Constructor
        public Crawler(IRepos externalUrlRepository, IRepos otherUrlRepository, IRepos failedUrlRepository, IRepos currentPageUrlRepository)
        {
            _externalUrlRepository = externalUrlRepository;

            _otherUrlRepository = otherUrlRepository;

            _failedUrlRepository = failedUrlRepository;

            _currentPageUrlRepository = currentPageUrlRepository;
        }

        //Initializing the crawling process.
        public void InitializeCrawl(string key, string url)
        {
            //var crawlUrl = ConfigurationManager.AppSettings["url"];
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            configuration.AppSettings.Settings["url"].Value = url;
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
            CrawlPage(ConfigurationManager.AppSettings["url"]);
        }

        /*//Initialisting the reporting
        public void InitilizeCreateReport()
        {
            //var stringBuilder = Reporting.CreateReport(_externalUrlRepository, _otherUrlRepository, _failedUrlRepository, _currentPageUrlRepository, _pages, _exceptions);

            //Logging.Logging.WriteReportToDisk(stringBuilder.ToString());

            //System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["logTextFileName"].ToString());

            //Environment.Exit(0);
        }*/
        int depthLimit = 0;
        private void CrawlPage(string url)
        {
            
            if (!PageHasBeenCrawled(url))
            {
                if(depthLimit++ >= 5)
                {
                    return;
                }

                var htmlText = GetWebText(url);

                var linkParser = new LinkParser();

                var page = new Page();
                page.Text = htmlText;
                page.Url = url;

                _pages.Add(page);
                links.Add(new Link("Page Title", url));
                log.Add(new Log($"New Entry: {url}", DateTime.Now));

                linkParser.ParseLinks(page, url);

                //Add data to main data lists
                if (isCurrentPage)
                {
                    AddRangeButNoDuplicates(_currentPageUrlRepository.List, linkParser.GoodUrls);
                }

                AddRangeButNoDuplicates(_externalUrlRepository.List, linkParser.ExternalUrls);
                AddRangeButNoDuplicates(_otherUrlRepository.List, linkParser.OtherUrls);
                AddRangeButNoDuplicates(_failedUrlRepository.List, linkParser.BadUrls);

                foreach(var urlToParse in linkParser.GoodUrls)
                {
                    links.Add(new Link("Page Title", urlToParse));
                    log.Add(new Log($"New Entry: {url}", DateTime.Now));
                    
                }
                if (linkParser.GoodUrls.Count == 0)
                {
                    links.Add(new Link("Page Title", "==============No New URLs From Page=============="));
                }

                foreach (string exception in linkParser.Exceptions)
                    _exceptions.Add(exception);

                isCurrentPage = false;
                Console.WriteLine(linkParser.GoodUrls);
                Console.WriteLine(_externalUrlRepository.List);
                //Crawl all the links found on the page.
                int loopBreak1 = 0;
                foreach (string link in _externalUrlRepository.List)
                {
                    string formattedLink = link;
                    loopBreak1++;
                    try
                    {
                        formattedLink = FixPath(url, formattedLink);

                        if (formattedLink != String.Empty)
                        {
                            links.Add(new Link(" ", "==============Crawling to new external page " + link + "...=============="));
                            CrawlPage(formattedLink);
                        }
                    }
                    catch (Exception exc)
                    {
                        _failedUrlRepository.List.Add(formattedLink + " (on page at url " + url + ") - " + exc.Message);
                    }
                    if (loopBreak1 >= 100)
                    {
                        break;
                    }
                }
                int loopBreak = 0;
                foreach (string iLink in linkParser.GoodUrls)
                {
                    string formattediLink = iLink;
                    loopBreak++;
                    try
                    {
                        formattediLink = FixPath(url, formattediLink);

                        if (formattediLink != String.Empty)
                        {

                            links.Add(new Link(" ", "==============Crawling to new internal page " + iLink + "...=============="));
                            CrawlPage(formattediLink);
                        }
                    }
                    catch (Exception exc)
                    {
                        _failedUrlRepository.List.Add(formattediLink + " (on page at url " + url + ") - " + exc.Message);
                    }
                    if (loopBreak >= 5)
                    {
                        break;
                    }
                    /*if (formattediLink == "/covid19")
                    {
                        break;
                    }*/
                }
            }
        }

        //Checks to see if the page has been crawled.
        public static bool PageHasBeenCrawled(string url)
        {
            foreach (Page page in _pages)
            {
                if (page.Url == url)
                    return true;
            }

            return false;
        }

        // Fixes a path. Makes sure it is a fully functional absolute url
        public static string FixPath(string originatingUrl, string link)
        {
            string formattedLink = String.Empty;

            if (link.IndexOf("../") > -1)
            {
                formattedLink = ResolveRelativePaths(link, originatingUrl);
            }
            else if (originatingUrl.IndexOf(ConfigurationManager.AppSettings["url"].ToString()) > -1
                && link.IndexOf(ConfigurationManager.AppSettings["url"].ToString()) == -1 && !link.Contains("http:"))
            {
                formattedLink = originatingUrl.Substring(0, originatingUrl.LastIndexOf("/") + 1) + link;
            }
            else if (link.IndexOf(ConfigurationManager.AppSettings["url"].ToString()) == -1)
            {
                formattedLink = link; //ConfigurationManager.AppSettings["url"].ToString() + 
            }

            return formattedLink;
        }

        //turn a relative path into an absolute path
        public static string ResolveRelativePaths(string relativeUrl, string originatingUrl)
        {
            string resolvedUrl = String.Empty;

            string[] relativeUrlArray = relativeUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string[] originatingUrlElements = originatingUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int indexOfFirstNonRelativePathElement = 0;
            for (int i = 0; i <= relativeUrlArray.Length - 1; i++)
            {
                if (relativeUrlArray[i] != "..")
                {
                    indexOfFirstNonRelativePathElement = i;
                    break;
                }
            }

            int countOfOriginatingUrlElementsToUse = originatingUrlElements.Length - indexOfFirstNonRelativePathElement - 1;
            for (int i = 0; i <= countOfOriginatingUrlElementsToUse - 1; i++)
            {
                if (originatingUrlElements[i] == "http:" || originatingUrlElements[i] == "https:")
                    resolvedUrl += originatingUrlElements[i] + "//";
                else
                    resolvedUrl += originatingUrlElements[i] + "/";
            }

            for (int i = 0; i <= relativeUrlArray.Length - 1; i++)
            {
                if (i >= indexOfFirstNonRelativePathElement)
                {
                    resolvedUrl += relativeUrlArray[i];

                    if (i < relativeUrlArray.Length - 1)
                        resolvedUrl += "/";
                }
            }

            return resolvedUrl;
        }

        // Merges a two lists of strings.
        private static void AddRangeButNoDuplicates(List<string> targetList, List<string> sourceList)
        {
            foreach (string str in sourceList)
            {
                if (!targetList.Contains(str))
                    targetList.Add(str);
            }
        }

        // Gets the response text for a given url.
        public static string GetWebText(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "A Web Crawler";

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();
            return htmlText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //topics.Add(textBox2.Text);
            //QueryTerms.Add(textBox2.Text);
        }
    }
}


