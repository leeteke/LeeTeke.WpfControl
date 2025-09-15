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
                    var left = Helper.ValueConver(args[0]);
                    thickness.TopLeft = left.Clear ? 0 : thickness.TopLeft + left.Result;
                    var top = Helper.ValueConver(args[1]);
                    thickness.TopRight = top.Clear ? 0 : thickness.TopRight + top.Result;
                    var right = Helper.ValueConver(args[2]);
                    thickness.BottomRight = right.Clear ? 0 : thickness.BottomRight + right.Result;
                    var bottom = Helper.ValueConver(args[3]);
                    thickness.BottomLeft = bottom.Clear ? 0 : thickness.BottomLeft + bottom.Result;
                    return thickness;
                }
            }
       
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
