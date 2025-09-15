using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace LeeTeke.WpfControl.Converters
{
    public class StringToCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {


                if (value == null)
                    return new CornerRadius();
                var input = value.ToString()?.Split(',');
                return (input?.Length) switch
                {
                    1 => new CornerRadius(double.Parse(input[0])),
                    2 => new CornerRadius(double.Parse(input[0]), double.Parse(input[1]), double.Parse(input[0]), double.Parse(input[1])),
                    3 => new CornerRadius(double.Parse(input[0]), double.Parse(input[1]), double.Parse(input[2]), double.Parse(input[1])),
                    4 => new CornerRadius(double.Parse(input[0]), double.Parse(input[1]), double.Parse(input[2]), double.Parse(input[4])),
                    _ => (object)new CornerRadius(),
                };
            }
            catch (Exception)
            {
                return new CornerRadius();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CornerRadius cr)
                return $"{cr.TopLeft},{cr.TopRight},{cr.BottomRight},{cr.BottomLeft}";
            return "";
        }
    }
}
