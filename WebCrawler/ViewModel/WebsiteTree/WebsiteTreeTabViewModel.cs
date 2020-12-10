using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebCrawler.Model;
using WebCrawler.View.WebsiteTree;
using static System.Windows.Forms.LinkLabel;

namespace WebCrawler.ViewModel.WebsiteTree
{
    class WebsiteTreeTabViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Model.Link> Links { get; set; } = new ObservableCollection<Model.Link>();

        public WebsiteTreeTabViewModel()
        {
            Links = Crawler.Instance.Links;
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
