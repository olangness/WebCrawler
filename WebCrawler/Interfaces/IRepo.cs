using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Interfaces
{
    public interface IRepos
    {
        List<string> List { get; }
        void Add(string url);
    }
}
