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
    public class ThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is Thickness thickness)
            {
                var args = $"{parameter}".Split(',');
                if (args.Length == 4)
                {
                    var left = StaticMethods.ValueConver(args[0]);
                    thickness.Left = left.clear ? 0 : thickness.Left + left.result;
                    var top = StaticMethods.ValueConver(args[1]);
                    thickness.Top = top.clear ? 0 : thickness.Top + top.result;
                    var right = StaticMethods.ValueConver(args[2]);
                    thickness.Right = right.clear ? 0 : thickness.Right + right.result;
                    var bottom = StaticMethods.ValueConver(args[3]);
                    thickness.Bottom = bottom.clear ? 0 : thickness.Bottom + bottom.result;
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
