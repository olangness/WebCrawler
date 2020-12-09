using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.ViewModel.Crawling;

namespace WebCrawler.ViewModel.TopicsTree
{
    class TopicTreeTabViewModel : INotifyPropertyChanged
    {
        private List<string> _topics;
        public List<string> Topics
        {
            get { return _topics; }
            set { _topics = value; }
        }

        private string _selectedTopic;
        public string SelectedTopic
        {
            get { return _selectedTopic; }
            set
            {
                if (_selectedTopic != value)
                {
                    _selectedTopic = value;
                    OnPropertyChange("SelectedTopic");
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
