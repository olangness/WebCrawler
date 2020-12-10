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
using WebCrawler.ViewModel.Crawling;

namespace WebCrawler.View.Crawling
{
    /// <summary>
    /// Interaction logic for CrawlingTabView.xaml
    /// </summary>
    public partial class CrawlingTabView : UserControl
    {
        private readonly CrawlingTabViewModel _viewModel;

        public CrawlingTabView()
        {
            InitializeComponent();
            _viewModel = new CrawlingTabViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }

        public void ListViewLogsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
