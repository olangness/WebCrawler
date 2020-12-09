using System.Windows;
using WebCrawler.Repositories;
using WebCrawler.Model;

namespace WebCrawler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Crawler crawl = new Crawler(new ExternalUrlRepository(), new OtherUrlRepository(), new FailedUrlRepository(), new CurrentPageUrlRepository());
            crawl.InitializeCrawl();
            //InitializeComponent();
        }
    }
}


