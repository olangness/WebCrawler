using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.ViewModel.Crawling
{
    class CrawlingTabViewModel : INotifyPropertyChanged
    {
        public WebsiteCrawlerViewModel WebsiteCrawlerViewModel { get; set; } 

        public LogViewModel LogViewModel { get; set; }



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
