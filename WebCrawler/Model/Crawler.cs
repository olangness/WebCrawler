using System;
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
using InfinityCrawler.Processing.Content;
using TurnerSoftware.RobotsExclusionTools;

namespace WebCrawler.Model
{
    class Crawler
    {
        private IRepos _externalUrlRepository;
        private IRepos _otherUrlRepository;
        private IRepos _failedUrlRepository;
        private IRepos _currentPageUrlRepository;
        private static List<Page> _pages = new List<Page>();
        private static List<string> _exceptions = new List<string>();
        private bool isCurrentPage = true;
        //public List<String> Disallows = new List<String>();
        private static List<string> urlsWithTopics = new List<string>();

        //Constructor
        public Crawler(IRepos externalUrlRepository, IRepos otherUrlRepository, IRepos failedUrlRepository, IRepos currentPageUrlRepository)
        {
            _externalUrlRepository = externalUrlRepository;

            _otherUrlRepository = otherUrlRepository;

            _failedUrlRepository = failedUrlRepository;

            _currentPageUrlRepository = currentPageUrlRepository;
        }

        //Initializing the crawling process.
        public void InitializeCrawl(string url)
        {
            CrawlPage(url);
        }

        //Initialisting the reporting
        public void InitilizeCreateReport()
        {
            //var stringBuilder = Reporting.CreateReport(_externalUrlRepository, _otherUrlRepository, _failedUrlRepository, _currentPageUrlRepository, _pages, _exceptions);

            //Logging.Logging.WriteReportToDisk(stringBuilder.ToString());

            //System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["logTextFileName"].ToString());

            //Environment.Exit(0);
        }

        private void CrawlPage(string url)
        {
            if (!PageHasBeenCrawled(url))
            {
                var htmlText = GetWebText(url);

                var linkParser = new LinkParser();

                var page = new Page();
                page.Text = htmlText;
                page.Url = url;

                _pages.Add(page);

                //AddUrlToList(topic);

                linkParser.ParseLinks(page, url);

                //Add data to main data lists
                if (isCurrentPage)
                {
                    AddRangeButNoDuplicates(_currentPageUrlRepository.List, linkParser.ExternalUrls);
                }

                AddRangeButNoDuplicates(_externalUrlRepository.List, linkParser.ExternalUrls);
                AddRangeButNoDuplicates(_otherUrlRepository.List, linkParser.OtherUrls);
                AddRangeButNoDuplicates(_failedUrlRepository.List, linkParser.BadUrls);

                foreach (string exception in linkParser.Exceptions)
                    _exceptions.Add(exception);

                isCurrentPage = false;

                /*//For each extracted URL
                //• Obey robots.txt (freshness caveat)
                //c. Check that not already in frontier
                var paths = page.OutLinks.Where(x => page.RobotsAreObeyed(x)
                                        && b.Contains(x) == false);*/

                //Crawl all the links found on the page.
                foreach (string link in _externalUrlRepository.List)
                {
                    string formattedLink = link;
                    try
                    {
                        formattedLink = FixPath(url, formattedLink);

                        if (formattedLink != String.Empty)
                        {
                            CrawlPage(formattedLink);
                        }
                    }
                    catch (Exception exc)
                    {
                        _failedUrlRepository.List.Add(formattedLink + " (on page at url " + url + ") - " + exc.Message);
                    }
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

        // Adds url containing keyword(s) to a list
        private static void AddUrlToList(string topic)
        {
            foreach (Page page in _pages)
            {
                if (page.ToString().Contains(topic))
                {
                    urlsWithTopics.Add(page.Url);
                }
            }
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


