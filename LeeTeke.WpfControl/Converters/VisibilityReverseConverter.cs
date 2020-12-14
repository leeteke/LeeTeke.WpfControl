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
    public class VisibilityReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility vi)
            {
                switch (vi)
                {
                    case Visibility.Visible:
                        return Visibility.Collapsed;
                    case Visibility.Hidden:
                    case Visibility.Collapsed:
                    default:
                        return Visibility.Visible;
                }
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility vi)
            {
                switch (vi)
                {
                    case Visibility.Visible:
                        return Visibility.Collapsed;
                    case Visibility.Hidden:
                    case Visibility.Collapsed:
                    default:
                        return Visibility.Visible;
                }
            }

            return Visibility.Visible;
        }
    }
}
