using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.ViewModel.WebsiteTree
{
    class WebsiteTreeBrowserViewModel : INotifyPropertyChanged
    {
        private string _treeText;
        public string TreeText
        {
            get { return _treeText; }
            set { _treeText = value; }
        }

        private string _selectedWebsite;
        public string SelectedWebsite
        {
            get { return _selectedWebsite; }
            set
            {
                if (_selectedWebsite != value)
                {
                    _selectedWebsite = value;
                    OnPropertyChange("SelectedWebsite");
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
