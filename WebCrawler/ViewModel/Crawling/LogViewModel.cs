using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WebCrawler.Model;

namespace WebCrawler.ViewModel.Crawling
{
    public class LogViewModel
    {
        public List<string> LogModel { get; set; } = new List<string>();

        public LogViewModel(Crawler crawler)
        {
            LogModel = crawler.Log;
        }
        
    }
}
