using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Model
{
    class Data
    {
        Log[] logs = new Log[25];
        Log myLog = new Log();

        Topic[] topics = new Topic[25];
        Topic myTopic = new Topic();

        Link[] links = new Link[25];
        Link myLink = new Link();

        // log methods
        public void AddLog(string text, DateTime timeSent) // add a log to the list
        {
            myLog.MessageText = text;
            myLog.MessageTime = timeSent;
            for(int i = 0; i <logs.Length; i++)
            {
                if (logs[i] != null )
                {
                    logs[i] = myLog;
                    
                } else if(i == logs.Length - 1)
                {
                    Log[] temp = new Log[logs.Length * 2];
                    for(int j =0; j < logs.Length; j++)
                    {
                        temp[j] = logs[j];
                    }
                    logs = temp;
                    temp = null;

                    logs[i] = myLog;
                }

            }
        }

        public Log[] GetLogs() //get the list of the logs
        {
            return logs;
        }

        public Log GetSpecificLog(string text, DateTime timeSent) // get a specific Log based on the message and when the message was created
        {
            Log temp = new Log("no Log found", System.DateTime.Now);
            for(int i =0; i< logs.Length; i++)
            {
                if(logs[i].MessageText.Equals(text) && logs[i].MessageTime.Equals(timeSent))
                {
                    temp = logs[i];
                }
            }

            return temp;
        }


        //topic methods
        public void AddTopic(string linkName, string[] URLs) // add a topic to the list
        {
            myTopic.Name = linkName;
            myTopic.ListOfURLs = URLs;
            for (int i = 0; i < topics.Length; i++)
            {
                if (topics[i] != null)
                {
                    topics[i] = myTopic;

                }
                else if (i == topics.Length - 1)
                {
                    Topic[] temp = new Topic[topics.Length * 2];
                    for (int j = 0; j < topics.Length; j++)
                    {
                        temp[j] = topics[j];
                    }
                    topics = temp;
                    temp = null;

                    topics[i] = myTopic;
                }

            }
        }

        public Topic[] GetTopics() //get the list of topics
        {
            return topics;
        }

        public Topic GetSpecificTopic(string name, string[] urls) // get a specific topic based on the name of the topic and the list of URLs
        {
            string[] tempArray = new string[1];
            Topic temp = new Topic("no Topic found", tempArray );
            for (int i = 0; i < topics.Length; i++)
            {
                if (topics[i].Name.Equals(name) && topics[i].ListOfURLs.Equals(urls))
                {
                    temp = topics[i];
                }
            }

            return temp;
        }


        //link methods

        public void AddLink(string name, string url, int linkID, int parentID) // add a link to the list
        {
            myLink.Name = name;
            myLink.URL = url;
            myLink.LinkID = linkID;
            myLink.ParentID = parentID;

            for (int i = 0; i < links.Length; i++)
            {
                if (links[i] != null)
                {
                    links[i] = myLink;

                }
                else if (i == links.Length - 1)
                {
                    Link[] temp = new Link[links.Length * 2];
                    for (int j = 0; j < links.Length; j++)
                    {
                        temp[j] = links[j];
                    }
                    links = temp;
                    temp = null;

                    links[i] = myLink;
                }

            }
        }

        public Link[] GetLinks() // get the list of links
        {
            return links;
        }

        public Link GetSpecificLink(string name, string url, int linkID, int parentID)
        {
            Link temp = new Link("no Link found", "", 0, 0);
            for (int i = 0; i < links.Length; i++)
            {
                if (links[i].Name.Equals(name) && links[i].URL.Equals(url) && links[i].LinkID == linkID && links[i].ParentID == parentID)
                {
                    temp = links[i];
                }
            }

            return temp;
        }



    }
}
