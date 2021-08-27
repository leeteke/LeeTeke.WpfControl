using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Converters
{
    public class ColorOpactiyConverter : IValueConverter
    {
     
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color && int.TryParse(parameter.ToString(), out int a))
            {
                return Color.FromArgb((byte)a, color.R, color.G, color.B);
            }
            if (value is SolidColorBrush brush && int.TryParse(parameter.ToString(), out int aa))
            {
                return Color.FromArgb((byte)aa, brush.Color.R, brush.Color.R, brush.Color.R);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
