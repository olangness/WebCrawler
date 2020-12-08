using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebCrawler.Model
{
    class Crawler
    {
        private string Title { get; set; }
        private string Url { get; set; }
        //private string siteUrl = "";
        //public string[] QueryTerms { get; } = { "", "" };
        public List<String> QueryTerms { get; set; }

        String URL;
        private void button2_Click(object sender, EventArgs e)
        {
            ScrapeWebsite();
        }
        internal async void ScrapeWebsite()
        {
            if (!PageHasBeenCrawled(URL))
            {
                URL = textBox1.Text;
                CancellationTokenSource cancellationToken = new CancellationTokenSource();
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage request = await httpClient.GetAsync(URL);
                cancellationToken.Token.ThrowIfCancellationRequested();

                Stream response = await request.Content.ReadAsStreamAsync();
                cancellationToken.Token.ThrowIfCancellationRequested();

                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.ParseDocument(response);

                GetScrapeResults(document);
            }
        }
        private void GetScrapeResults(IHtmlDocument document)
        {
            IEnumerable<IElement> articleLink;

            foreach (var term in QueryTerms)
            {
                articleLink = document.All.Where(x => x.ClassName == "views-field views-field-nothing" &&
                (x.ParentElement.InnerHtml.Contains(term) || x.ParentElement.InnerHtml.Contains(term.ToLower())));
                if (articleLink.Any())
                {
                    PrintResults(articleLink);
                }
            }
        }

        public void PrintResults(IEnumerable<IElement> articleLink)
        {
            foreach (var element in articleLink)
            {
                CleanUpResults(element);
                dataGridView1.DataContext = $"{Title} - {Url}{Environment.NewLine}";
            }
        }

        private void CleanUpResults(IElement result)
        {
            string htmlResult = result.InnerHtml.ReplaceFirst("        <span class=\"field-content\"><div><a href=\"", "https://www.oceannetworks.ca");
            htmlResult = htmlResult.ReplaceFirst("\">", "*");
            htmlResult = htmlResult.ReplaceFirst("</a></div>\n<div class=\"article-title-top\">", "-");
            htmlResult = htmlResult.ReplaceFirst("</div>\n<hr></span>  ", "");

            SplitResults(htmlResult);
        }

        private void SplitResults(string htmlResult)
        {
            string[] splitResults = htmlResult.Split('*');
            Url = splitResults[0];
            Title = splitResults[1];
        }

        public static bool PageHasBeenCrawled(string url)
        {
            foreach (Page page in _pages)
            {
                if (page.Url == url)
                    return true;
            }

            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //topics.Add(textBox2.Text);
            QueryTerms.Add(textBox2.Text);
        }
    }
}
