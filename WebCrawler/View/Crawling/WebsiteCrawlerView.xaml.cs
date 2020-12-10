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
using WebCrawler.Model;
using WebCrawler.ViewModel.Crawling;

namespace WebCrawler.View.Crawling
{
    /// <summary>
    /// Interaction logic for WebsiteCrawlerView.xaml
    /// </summary>
    public partial class WebsiteCrawlerView : UserControl
    {
        private readonly WebsiteCrawlerViewModel _viewModel;

        public WebsiteCrawlerView(Crawler model)
        {
            InitializeComponent();
            _viewModel = new WebsiteCrawlerViewModel(model);
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }
    }
}
