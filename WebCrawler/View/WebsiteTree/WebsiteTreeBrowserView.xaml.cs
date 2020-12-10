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
    /// Interaction logic for WebsiteTreeBrowserView.xaml
    /// </summary>
    public partial class WebsiteTreeBrowserView : UserControl
    {
        private readonly WebsiteTreeBrowserViewModel _viewModel;

        public WebsiteTreeBrowserView()
        {
            InitializeComponent();
            _viewModel = new WebsiteTreeBrowserViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
            TreeBranches.ItemsSource = _viewModel.TreeBranchViewModels;
        }
    }
}
