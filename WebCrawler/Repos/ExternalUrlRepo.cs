using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Interfaces;

namespace WebCrawler.Repositories
{
    public class ExternalUrlRepository : IRepos
    {
        //List of external Urls.
        List<string> _listOfExternalUrl;

        public ExternalUrlRepository()
        {
            _listOfExternalUrl = new List<string>();
        }

        //List to gather Urls.
        public List<string> List
        {
            get
            {
                return _listOfExternalUrl;
            }
        }

        //Method to add new Url.
        public void Add(string entity)
        {
            _listOfExternalUrl.Add(entity);
        }
    }
}
