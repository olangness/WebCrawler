using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Model
{
    public class Link : INotifyPropertyChanged
    {

        public Link()
        {

        }
        public Link(string name, string url)
        {
            LinkName = name;
            LinkURL = url;
        }

        private string _linkName;
        public string LinkName
        {
            get { return _linkName; }
            set
            {
                _linkName = value;
                NotifyPropertyChanged("LinkName");
            }
        }

        private string _linkUrl;
        public string LinkURL
        {
            get { return _linkUrl; }
            set
            {
                _linkUrl = value;
                NotifyPropertyChanged("LinkURL");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
