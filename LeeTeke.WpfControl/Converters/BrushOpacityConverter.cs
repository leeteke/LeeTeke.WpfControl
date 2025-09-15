using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Converters
{
    public class BrushOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Brush brush && double.TryParse(parameter.ToString(), out double opacity))
            {
                var newBrush= brush.Clone();
                newBrush.Opacity = opacity;
                return newBrush;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Brush brush)
            {
                brush.Opacity = 1;
                return brush;
            }
            return value;
        }
    }
}
