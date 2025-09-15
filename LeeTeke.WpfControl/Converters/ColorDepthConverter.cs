using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Converters
{
    public class ColorDepthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color && float.TryParse($"{parameter}", out float depth))
            {
                return color == Colors.Transparent ? (depth > 0 ? Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255) : Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0)) : Helper.ChangeColorDepth(color, depth);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color && float.TryParse($"{parameter}", out float depth))
            {
                return Helper.ChangeColorDepth(color, Math.Abs(depth));
            }
            return value;
        }
    }
}
