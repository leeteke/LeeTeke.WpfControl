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
    #region NotAlignmentCenter
    public class NotAlignmentCenterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HorizontalAlignment)
            {
                var alignment = (HorizontalAlignment)value;
                return alignment != HorizontalAlignment.Center;
            }
            else if (value is VerticalAlignment)
            {
                var alignment = (VerticalAlignment)value;
                return alignment != VerticalAlignment.Center;
            }
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
    #endregion
}
