using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Converters
{
   public  class MultiValueToClipConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 1 && values[0] is double width && values[1] is double height)
            {
                if (width < 1.0 || height < 1.0)
                {
                    return Geometry.Empty;
                }

                CornerRadius cornerRadius = default;
                Thickness borderThickness = default;
                if (values.Length > 2 && values[2] is CornerRadius radius)
                {
                    ///这里得修改一下
                    if (radius.BottomRight > 0)
                        radius.BottomRight -= 0.5;

                    if (radius.TopRight > 0)
                        radius.TopRight -= 0.5;

                    if (radius.TopLeft > 0)
                        radius.TopLeft -= 0.5;

                    if (radius.BottomLeft > 0)
                        radius.BottomLeft -= 0.5;

                    cornerRadius = radius;
                    if (values.Length > 3 && values[3] is Thickness thickness)
                    {
                        borderThickness = thickness;
                    }
                }

                var geometry = Helper.GetRoundRectangle(new Rect(0, 0, width, height), borderThickness, cornerRadius);
                geometry.Freeze();

                return geometry;
            }

            return Geometry.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return Array.Empty<object>();
        }

    }
}
