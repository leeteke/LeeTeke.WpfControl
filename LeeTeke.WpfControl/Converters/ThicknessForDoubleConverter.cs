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
   public class ThicknessForDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse($"{value}", out double _value))
                _value = 0;
            var _parameter = $"{parameter}".Split('_');
            ///比较器
            if (_parameter.Length < 2)
                return new Thickness(_value);

     
         
                if (double.TryParse(_parameter[1].ToString(), out double change))
                {
                _value += change;
                }
                else if (_parameter[1].ToString().StartsWith("*") && double.TryParse(_parameter[1].ToString().TrimStart('*'), out double product))
                {
                _value *= product;
                }
                else if (_parameter[1].ToString().StartsWith("/") && double.TryParse(_parameter[1].ToString().TrimStart('/'), out double divisor))
                {
                _value /= divisor;
                }
       

            return _parameter[0] switch
            {
                "V" => new Thickness(0, _value, 0, _value),
                "H" => new Thickness(_value, 0, _value, 0),
                _ => new Thickness(_value)
            };

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
