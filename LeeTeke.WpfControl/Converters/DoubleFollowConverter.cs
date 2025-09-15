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
            if (double.TryParse($"{value}", out double lord)&& parameter is string pValue)
            {
                if (double.TryParse(pValue, out double change))
                {
                    return lord += change;
                }
                else if (pValue.StartsWith("*") && double.TryParse(pValue.TrimStart('*'), out double product))
                {
                    return lord *= product;
                }
                else if (pValue.StartsWith("/") && double.TryParse(pValue.TrimStart('/'), out double divisor))
                {
                    return lord /= divisor;
                }
            }
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse($"{value}", out double lord)&& parameter is string pValue)
            {
                if (double.TryParse(pValue, out double change))
                {
                    return lord -= change;
                }
                else if (pValue.StartsWith("*") && double.TryParse(pValue.TrimStart('*'), out double product))
                {
                    return lord /= product;
                }
                else if (pValue.StartsWith("/") && double.TryParse(pValue.TrimStart('/'), out double divisor))
                {
                    return lord *= divisor;
                }

            }
            return value;
        }
    }
}
