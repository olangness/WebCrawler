using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WebCrawler.View.Crawling
{
    public class ListToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in (List<string>)value) sb.AppendLine(s);
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] lines = ((string)value).Split(new string[] { @"\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return lines.ToList<String>();
        }
    }
}
