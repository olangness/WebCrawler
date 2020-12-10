using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Model
{
    class Link
    {
        string name;
        string url;
        int linkID;
        int parentID;

        public Link()
        {

        }
        public Link(string name, string url, int linkID, int parentID)
        {
            Name = name;
            URL = url;
            LinkID = linkID;
            ParentID = parentID; //if parentID == 0, then this is root URL
        }

        public string Name
        {
            get { return name; }

            set { name = value; }
        }

        public string URL
        {
            get { return url; }

            set { url = value; }
        }

        public int LinkID
        {
            get { return linkID; }
            set { linkID = value; }
        }

        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }




    }
}
