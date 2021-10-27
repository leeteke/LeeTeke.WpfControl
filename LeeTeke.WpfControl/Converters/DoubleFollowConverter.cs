using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace LeeTeke.WpfControl.Converters
{
    public class DoubleFollowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse($"{value}",out double lord))
            {
                if (double.TryParse(parameter.ToString(), out double change))
                {
                    return lord += change;
                }
                else if (parameter.ToString().StartsWith("*") && double.TryParse(parameter.ToString().TrimStart('*'), out double product))
                {
                    return lord *= product;
                }
                else if (parameter.ToString().StartsWith("/") && double.TryParse(parameter.ToString().TrimStart('/'), out double divisor))
                {
                    return lord /= divisor;
                }
            }
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse($"{value}", out double lord))
            {
                if (double.TryParse(parameter.ToString(), out double change))
                {
                    return lord -= change;
                }
                else if (parameter.ToString().StartsWith("*") && double.TryParse(parameter.ToString().TrimStart('*'), out double product))
                {
                    return lord /= product;
                }
                else if (parameter.ToString().StartsWith("/") && double.TryParse(parameter.ToString().TrimStart('/'), out double divisor))
                {
                    return lord *= divisor;
                }
             
            }
            return value;
        }
    }
}
