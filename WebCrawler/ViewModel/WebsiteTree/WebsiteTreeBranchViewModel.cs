using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebCrawler.ViewModel.WebsiteTree
{
    class WebsiteTreeBranchViewModel : INotifyPropertyChanged
    {
        private string _pageTitle = "pagetitle";
        public string PageTitle
        {
            get { return _pageTitle; }

            set
            {
                if (value != _pageTitle)
                {
                    _pageTitle = value;
                }
            }
        }

        private string _pageUrl = "pageurl";
        public string PageUrl
        {
            get { return _pageUrl; }

            set
            {
                if (value != _pageUrl)
                {
                    _pageUrl = value;
                }
            }
        }

        public string Indentation { get; set; }

        public WebsiteTreeBranchViewModel(string title, string url, int indentation)
        {
            PageTitle = title;
            PageUrl = url;
            Indentation = $"{indentation * 50}, 0, 0, 0";
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
