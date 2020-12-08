using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.ViewModel.Crawling
{
    class CrawlingTabViewModel
    {
        public WebsiteCrawlerViewModel WebsiteCrawlerViewModel { get; set; }

        public TopicKeywordViewModel TopicKeywordViewModel { get; set; }

        public LogViewModel LogViewModel { get; set; }
    }
}
