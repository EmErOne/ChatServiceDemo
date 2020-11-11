using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ChatService.Client.Converter
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class ImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string s))
                return null;

            BitmapImage bi = new BitmapImage();

            
            bi.BeginInit();
            bi.DecodePixelWidth = 300;
            bi.DecodePixelHeight = 200;
            bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(s));
            bi.EndInit();

            return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
