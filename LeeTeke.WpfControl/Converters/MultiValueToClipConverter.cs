using System;
using System.Collections.Generic;
using System.Globalization;
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
                    cornerRadius = radius;
                    if (values.Length > 3 && values[3] is Thickness thickness)
                    {
                        borderThickness = thickness;
                    }
                }

                var geometry = StaticMethods.GetRoundRectangle(new Rect(0, 0, width, height), borderThickness, cornerRadius);
                geometry.Freeze();

                return geometry;
            }

            return default;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
