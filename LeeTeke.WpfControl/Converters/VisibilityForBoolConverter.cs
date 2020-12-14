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
   public class VisibilityForBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool vb)
                return vb ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility vb)
            {
                switch (vb)
                {
                    case Visibility.Visible:
                        return true;
                    case Visibility.Hidden:
                    case Visibility.Collapsed:
                        return false;
                    default:
                        return false;
                }
            }
            return false;
        }
    }
}
