using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Log
    {
        string messageText;
        DateTime messageTime;


        public Log() { }

        public Log(string message, DateTime time)
        {
            MessageText = message;
            
            MessageTime = time;
        }

        public string MessageText
        {
            get { return messageText; }

            set { messageText = value; }
        }

        public DateTime MessageTime
        {
            get { return messageTime; }

            set { messageTime = value; }
        }
    }
}
