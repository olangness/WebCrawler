using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebCrawler.Model;
using WebCrawler.Repositories;

namespace WebCrawler.ViewModel.Crawling
{
    class WebsiteCrawlerViewModel : INotifyPropertyChanged
    {
        private Crawler _model = new Crawler(new ExternalUrlRepository(), new OtherUrlRepository(), new FailedUrlRepository(), new CurrentPageUrlRepository());

        private string _startingUrl;
        public string StartingURL
        {
            get { return _startingUrl; }
            set
            {
                if (_startingUrl != value)
                {
                    _startingUrl = value;
                    OnPropertyChange("StaringURL");
                }
            }
        }

        private string _topics;
        public string Topics
        {
            get { return _topics; }
            set
            {
                if (_topics != value)
                {
                    _topics = value;
                    OnPropertyChange("Topics");
                }
            }
        }

        public TopicKeywordViewModel TopicKeywordViewModel { get; set; }

        public ICommand StartCommand { get; private set; }

        public WebsiteCrawlerViewModel()
        {
            StartCommand = new RelayCommand<object>(_ => _model.InitializeCrawl(StartingURL));
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
