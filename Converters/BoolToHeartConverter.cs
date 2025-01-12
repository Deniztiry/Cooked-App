using System;
using System.Globalization;

namespace Cooked_App.Converters
{
    public class BoolToHeartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isFavorite)
            {
                return isFavorite ? "heart_filled_orange.png" : "heart_outline_white.png";
            }
            return "heart_outline_white.png"; // Fallback
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
