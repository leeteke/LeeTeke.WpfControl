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
    public class DoubleComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse($"{value}", out double _value))
                _value = 0;
            var _parameter = $"{parameter}".Split('_');
            ///比较器
            if (_parameter.Length < 2)
                return false;

            return _parameter[0] switch
            {
                "LessEqual" => double.TryParse(_parameter[1], out double result_1) && _value <= result_1,
                "GreaterEqual" => double.TryParse(_parameter[1], out double result_2) && _value >= result_2,
                "Greater" => double.TryParse(_parameter[1], out double result_3) && _value > result_3,
                "Less" => double.TryParse(_parameter[1], out double result_4) && _value < result_4,
                "Equal" => double.TryParse(_parameter[1], out double result_5) && _value == result_5,
                "NotEqual" => double.TryParse(_parameter[1], out double result_6) && _value != result_6,
                _ => false,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
