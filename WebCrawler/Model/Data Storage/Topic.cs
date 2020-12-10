using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    class Topic
    {
        string name;
        string[] listOfURLs;

        public Topic() { }
        public Topic(string name, string[] URLs)
        {
            Name = name;
            listOfURLs = URLs;
        }

        public string Name
        {
            get { return name; }

            set { name = value; }
        }

        public string[] ListOfURLs
        {
            get { return listOfURLs; }

            set { listOfURLs = value; }
        }

        public void addURl(string url)
        {
            for (int i = 0; i < listOfURLs.Length; i++)
            {
                if (listOfURLs[i] != null)
                {
                    listOfURLs[i] = url;

                }
                else if (i == listOfURLs.Length - 1)
                {
                    String[] temp = new String[listOfURLs.Length * 2];
                    for (int j = 0; j < listOfURLs.Length; j++)
                    {
                        temp[j] = listOfURLs[j];
                    }
                    listOfURLs = temp;
                    temp = null;

                    listOfURLs[i] = url;
                }

            }
        }
    }
}
