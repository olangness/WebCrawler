using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebCrawler.View.WebsiteTree;

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

        public List<WebsiteTreeBranchView> TreeBranchViewModels { get; set; }

        public WebsiteTreeBrowserViewModel()
        {
            List<WebsiteTreeBranchView> treeBranches = new List<WebsiteTreeBranchView>();

            WebsiteTreeBranchView branch = new WebsiteTreeBranchView("Title of page", "Da url of page", 0);

            treeBranches.Add(branch);

            WebsiteTreeBranchView branch1 = new WebsiteTreeBranchView("Title of page", "Da url of page", 1);

            treeBranches.Add(branch1);

            WebsiteTreeBranchView branch2 = new WebsiteTreeBranchView("Title of page", "Da url of page", 2);

            treeBranches.Add(branch2);

            TreeBranchViewModels = treeBranches;
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
