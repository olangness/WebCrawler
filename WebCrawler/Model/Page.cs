using System;

namespace WebCrawler
{
    public class Page
    {
        public Page() { }

        private int _size;
        private string _text;
        private string _url;
        private int _viewstateSize;

        public int Size
        {
            get { return _size; }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                _size = value.Length;
            }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}
