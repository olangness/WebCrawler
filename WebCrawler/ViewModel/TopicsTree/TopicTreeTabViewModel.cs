using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.ViewModel.Crawling;

namespace WebCrawler.ViewModel.TopicsTree
{
    class TopicTreeTabViewModel
    {
        public TopicKeywordViewModel TopicKeywordViewModel { get; set; }

        public List<string> Topics { get; set; }
    }
}
