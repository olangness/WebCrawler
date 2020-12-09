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

namespace WebCrawler.ViewModel.Crawling
{
    /// <summary>
    /// Interaction logic for TopicPill.xaml
    /// </summary>
    public partial class TopicPillView : UserControl
    {
        private readonly TopicPillViewModel _viewModel;

        public TopicPillView()
        {
            InitializeComponent();
            _viewModel = new TopicPillViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }
    }
}
