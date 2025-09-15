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
                    var left = Helper.ValueConver(args[0]);
                    thickness.Left = left.Clear ? 0 : thickness.Left + left.Result;
                    var top = Helper.ValueConver(args[1]);
                    thickness.Top = top.Clear ? 0 : thickness.Top + top.Result;
                    var right = Helper.ValueConver(args[2]);
                    thickness.Right = right.Clear ? 0 : thickness.Right + right.Result;
                    var bottom = Helper.ValueConver(args[3]);
                    thickness.Bottom = bottom.Clear ? 0 : thickness.Bottom + bottom.Result;
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
