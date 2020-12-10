using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebCrawler.View.WebsiteTree;
using static System.Windows.Forms.LinkLabel;

namespace WebCrawler.ViewModel.WebsiteTree
{
    class WebsiteTreeTabViewModel
    {
        public string WebsiteURL { get; set; }


        public ICommand Refresh { get; private set; }

        public WebsiteTreeTabViewModel()
        {
            Refresh = new RelayCommand<object>(_ => RefreshTree());
        }

        public void RefreshTree()
        {
            Link parent = new Link();
        }
    }
}
