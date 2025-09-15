using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Converters
{
    public class BrushDepthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (float.TryParse($"{parameter}", out float depth))
            {
                return value switch
                {
                    SolidColorBrush scbrush => scbrush.Color == Colors.Transparent ? (depth > 0 ? new SolidColorBrush(Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255)) : new SolidColorBrush(Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0))) : new SolidColorBrush(Helper.ChangeColorDepth(scbrush.Color, depth)),
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
                    SolidColorBrush scbrush => new SolidColorBrush(Helper.ChangeColorDepth(scbrush.Color, Math.Abs(depth))),
                    _ => value,
                };
            }
            return value;
        }
    }
}
