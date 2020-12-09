using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using WebCrawler.Model;

namespace WebCrawler
{
    public class LinkParser
    {

        public LinkParser() { }


        private const string _LINK_REGEX = "href=\"[a-zA-Z./:&\\d_-]+\"";

        private List<string> _goodUrls = new List<string>();
        private List<string> _badUrls = new List<string>();
        private List<string> _otherUrls = new List<string>();
        private List<string> _externalUrls = new List<string>();
        private List<string> _exceptions = new List<string>();

        public List<string> GoodUrls { get; set; }
        public List<string> BadUrls { get; set; }
        public List<string> OtherUrls { get; set; }
        public List<string> ExternalUrls { get; set; }
        public List<string> Exceptions { get; set; }

        //Parses a page looking for links.
        public void ParseLinks(Page page, string sourceUrl)
        {
            MatchCollection matches = Regex.Matches(page.Text, _LINK_REGEX);

            for (int i = 0; i <= matches.Count - 1; i++)
            {
                Match anchorMatch = matches[i];

                if (anchorMatch.Value == String.Empty)
                {
                    BadUrls.Add("Blank url value on page " + sourceUrl);
                    continue;
                }

                string foundHref = null;
                try
                {
                    foundHref = anchorMatch.Value.Replace("href=\"", "");
                    foundHref = foundHref.Substring(0, foundHref.IndexOf("\""));
                }
                catch (Exception exc)
                {
                    Exceptions.Add("Error parsing matched href: " + exc.Message);
                }


                try
                {
                    if (!GoodUrls.Contains(foundHref))
                    {
                        if (foundHref != "/")
                        {
                            if (IsExternalUrl(foundHref))
                            {
                                _externalUrls.Add(foundHref);
                            }
                            else if (!IsAWebPage(foundHref))
                            {
                                foundHref = Crawler.FixPath(sourceUrl, foundHref);
                                _otherUrls.Add(foundHref);
                            }
                            else
                            {
                                GoodUrls.Add(foundHref);
                            }
                        }
                    }
                }
                catch (NullReferenceException nre)
                {
                    Console.WriteLine(nre + " no good url");
                    Console.WriteLine(GoodUrls.ToString());
                }

            }
        }


        //Is the url to an external site?
        private static bool IsExternalUrl(string url)
        {
            if (url.Length > 8 && (url.Substring(0, 7) == "http://" || url.Substring(0, 3) == "www" || url.Substring(0, 7) == "https://"))
            {
                return true;
            }

            return false;
        }

        //Is the value of the href pointing to a web page?
        private static bool IsAWebPage(string foundHref)
        {
            if (foundHref.IndexOf("javascript:") == 0)
                return false;

            string extension = foundHref.Substring(foundHref.LastIndexOf(".") + 1, foundHref.Length - foundHref.LastIndexOf(".") - 1);
            switch (extension)
            {
                case "jpg":
                case "css":
                    return false;
                default:
                    return true;
            }

        }
    }
}


