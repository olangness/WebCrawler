using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebCrawler.ViewModel.WebsiteTree;

namespace WebCrawler.View.WebsiteTree
{
    /// <summary>
    /// Interaction logic for WebsiteTreeTabView.xaml
    /// </summary>
    public partial class WebsiteTreeTabView : UserControl
    {
        private readonly WebsiteTreeTabViewModel _viewModel;

        public WebsiteTreeTabView()
        {
            InitializeComponent();
            _viewModel = new WebsiteTreeTabViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }

        public void ListViewLinkSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}

