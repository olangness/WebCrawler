using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Interfaces;

namespace WebCrawler.Repositories
{
    //Class for External Urls.
    public class OtherUrlRepository : IRepos
    {
        //List of external Urls.
        List<string> _listOfOtherUrl;

        //Constructor of the class.
        public OtherUrlRepository()
        {
            _listOfOtherUrl = new List<string>();
        }

        //List to gather Urls.
        public List<string> List
        {
            get
            {
                return _listOfOtherUrl;
            }
        }

        //Method to add new Url.
        public void Add(string entity)
        {
            _listOfOtherUrl.Add(entity);
        }
    }
}
