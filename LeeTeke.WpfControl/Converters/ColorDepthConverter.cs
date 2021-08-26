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
            if (float.TryParse($"{parameter}", out float depth))
            {
                return value switch
                {
                    Color color => color == Colors.Transparent ? (depth > 0 ? Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255) : Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0)) : StaticMethods.ChangeColorDepth(color, depth),
                    SolidColorBrush scbrush => scbrush.Color == Colors.Transparent ? (depth > 0 ? new SolidColorBrush(Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255)) : new SolidColorBrush(Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0))) : new SolidColorBrush(StaticMethods.ChangeColorDepth(scbrush.Color, depth)),
                    _ => depth > 0 ? new SolidColorBrush(Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255)) : new SolidColorBrush(Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0)),
                };
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (float.TryParse($"{parameter}", out float depth))
            {
                return value switch
                {
                    Color color => StaticMethods.ChangeColorDepth(color, Math.Abs(depth)),
                    SolidColorBrush scbrush => new SolidColorBrush(StaticMethods.ChangeColorDepth(scbrush.Color, Math.Abs(depth))),
                    _ => value,
                };
            }
            return value;
        }
    }
}
