using System;
using System.Globalization;
using Microsoft.Maui.Graphics;

namespace Cooked_App.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? Color.FromArgb("#FD9748") : Color.FromArgb("#E5E5E5");
            }
            return Color.FromArgb("#E5E5E5");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
