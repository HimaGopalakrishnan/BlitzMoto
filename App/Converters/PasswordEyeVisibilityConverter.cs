using System;
using System.Globalization;
using App.Constants;
using Xamarin.Forms;

namespace App.Converters
{
    public class PasswordEyeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isPassword = (bool)value;
            var image = isPassword ? Images.Visibility_On_Image : Images.Visibility_Off_Image;
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}