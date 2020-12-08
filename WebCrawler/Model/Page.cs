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

        public int Size { get; }

        public string Text { get; set; }

        public string Url { get; set; }
    }
}
