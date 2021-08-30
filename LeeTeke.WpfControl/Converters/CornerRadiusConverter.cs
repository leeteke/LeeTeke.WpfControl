using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LeeTeke.WpfControl.Converters
{
   public class CornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CornerRadius thickness)
            {
                var args = $"{parameter}".Split(',');
                if (args.Length == 4)
                {
                    var left = StaticMethods.ValueConver(args[0]);
                    thickness.TopLeft = left.clear ? 0 : thickness.TopLeft + left.result;
                    var top = StaticMethods.ValueConver(args[1]);
                    thickness.TopRight = top.clear ? 0 : thickness.TopRight + top.result;
                    var right = StaticMethods.ValueConver(args[2]);
                    thickness.BottomRight = right.clear ? 0 : thickness.BottomRight + right.result;
                    var bottom = StaticMethods.ValueConver(args[3]);
                    thickness.BottomLeft = bottom.clear ? 0 : thickness.BottomLeft + bottom.result;
                    return thickness;
                }
            }
       
            return default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return default;
        }
    }
}
