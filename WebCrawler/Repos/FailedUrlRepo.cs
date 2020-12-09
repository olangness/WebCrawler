using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Interfaces;

namespace WebCrawler.Repositories
{
    //Class for Failed Urls.
    public class FailedUrlRepository : IRepos
    {
        //List of failed Urls.
        List<string> _listOfFailedUrl;

        //Constructor of the class.
        public FailedUrlRepository()
        {
            _listOfFailedUrl = new List<string>();
        }

        //List to gather Urls.
        public List<string> List
        {
            get
            {
                return _listOfFailedUrl;
            }
        }

 
        ///Method to add new Url.
        public void Add(string entity)
        {
            _listOfFailedUrl.Add(entity);
        }
    }
}
