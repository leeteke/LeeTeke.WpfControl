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
    public class MultiValueToCenterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 0)
            {
                double value = 0;
                foreach (var item in values)
                {
                    if (double.TryParse($"{item}", out double itemValue))
                    {
                        value += itemValue;
                    }
                }

                if (parameter is string pValue)
                {

                    if (double.TryParse(pValue, out double change))
                    {
                        return value += change;
                    }
                    else if (pValue.StartsWith("*") && double.TryParse(pValue.TrimStart('*'), out double product))
                    {
                        return value *= product;
                    }
                    else if (pValue.StartsWith("/") && double.TryParse(pValue.TrimStart('/'), out double divisor))
                    {
                        return value /= divisor;
                    }
                }
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return Array.Empty<object>();
        }
    }
}
