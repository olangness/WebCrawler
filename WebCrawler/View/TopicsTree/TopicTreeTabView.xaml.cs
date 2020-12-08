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
using WebCrawler.ViewModel.TopicsTree;

namespace WebCrawler.View.TopicsTree
{
    /// <summary>
    /// Interaction logic for TopicTreeTabView.xaml
    /// </summary>
    public partial class TopicTreeTabView : UserControl
    {
        private readonly TopicTreeTabViewModel _viewModel;

        public TopicTreeTabView()
        {
            InitializeComponent();
            _viewModel = new TopicTreeTabViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }
    }
}
