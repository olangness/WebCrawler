using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebCrawler.Model;
using WebCrawler.Model.Data_Storage;
using WebCrawler.Repositories;
using WebCrawler.View.Crawling;

namespace WebCrawler.ViewModel.Crawling
{
    class CrawlingTabViewModel : INotifyPropertyChanged
    {
        public WebsiteCrawlerViewModel WebsiteCrawlerViewModel { get; set; }

        public ObservableCollection<Log> Logs { get; set; } = new ObservableCollection<Log>();

        public CrawlingTabViewModel()
        {
            Logs = Crawler.Instance.Log;
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
