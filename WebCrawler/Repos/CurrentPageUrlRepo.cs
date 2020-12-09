using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Interfaces;

namespace WebCrawler.Repositories
{
    public class CurrentPageUrlRepository : IRepos
    {
        List<string> _listOfCurrentPageUrl;

        public CurrentPageUrlRepository()
        {
            _listOfCurrentPageUrl = new List<string>();
        }

        //List to gather Urls.
        public List<string> List
        {
            get
            {
                return _listOfCurrentPageUrl;
            }
        }

        //Method to add new Url.
        public void Add(string entity)
        {
            _listOfCurrentPageUrl.Add(entity);
        }
    }
}
