using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Model.Data_Storage;

namespace WebCrawler.Model
{
    public class Log : INotifyPropertyChanged
    {
        string messageText;
        DateTime messageTime;


        public Log() { }

        private DateTime _messageTime;
        public DateTime MessageTime
        {
            get { return _messageTime; }
            set
            {
                _messageTime = value;
                NotifyPropertyChanged("ProjectID");
            }
        }

        private string _messageText;
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                NotifyPropertyChanged("ProjectID");
            }
        }

        public Log(string message, DateTime time)
        {
            MessageTime = time;
            MessageText = message;
        }


        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
