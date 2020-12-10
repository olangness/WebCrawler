using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Model.Data_Storage
{
    public interface ILog
    {
        string MessageText { get; set; }
        DateTime MessageTime { get; set; }
    }
}
