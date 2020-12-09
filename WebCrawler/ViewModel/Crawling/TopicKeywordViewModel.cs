using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.ViewModel.Crawling
{
    class TopicKeywordViewModel : INotifyPropertyChanged
    {
        private List<string> _topicModel = new List<string>(new string[] { "element1", "element2", "element3" });
        public List<string> Topics
        {
            get { return _topicModel; }
            set
            {
                if (_topicModel != value)
                {
                    _topicModel = value;
                    OnPropertyChange("Topics");
                }
            }
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
