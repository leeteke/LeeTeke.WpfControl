using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LeeTeke.WpfControl.Converters
{
    public class MultiValueToCenterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 1)
            {
                double value = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    value += double.Parse($"{values[i]}");
                }
                if (double.TryParse(parameter.ToString(), out double change))
                {
                    return value += change;
                }
                else if (parameter.ToString().StartsWith("*") && double.TryParse(parameter.ToString().TrimStart('*'), out double product))
                {
                    return value *= product;
                }
                else if (parameter.ToString().StartsWith("/") && double.TryParse(parameter.ToString().TrimStart('/'), out double divisor))
                {
                    return value /= divisor;
                }
            }
            return default;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
