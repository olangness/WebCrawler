using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawler.Repositories;
using WebCrawler.View.Crawling;

namespace WebCrawler.ViewModel.Crawling
{
    class CrawlingTabViewModel : INotifyPropertyChanged
    {
        public WebsiteCrawlerView WebsiteCrawlerView { get; set; }

        public LogView LogView { get; set; }

        public CrawlingTabViewModel()
        {
            Crawler model = new Crawler(new ExternalUrlRepository(), new OtherUrlRepository(), new FailedUrlRepository(), new CurrentPageUrlRepository());
            WebsiteCrawlerView = new WebsiteCrawlerView(model);
            LogView = new LogView(model);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
