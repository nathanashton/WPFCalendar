using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomCalendar.Converters
{
    public class BoolColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (bool) value;
            if (result)
            {
                if (parameter as string == "Today")
                    return new SolidColorBrush(Colors.Orange);
                return new SolidColorBrush(Colors.LightCyan);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}