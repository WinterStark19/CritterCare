using Microsoft.Maui.Controls;
using System;
using System.IO;

namespace CritterCare
{
    public class ByteToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is byte[] byteArray && byteArray.Length > 0)
            {
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }

            // Return a default image if the byte array is empty
            return ImageSource.FromFile("default_dog.jpg");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}