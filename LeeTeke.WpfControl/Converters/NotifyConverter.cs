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
    internal class NotifyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ($"{parameter}" == "H")
            {
                return value is NotifySite site ? site switch
                {
                    NotifySite.BottomCenter or NotifySite.TopCenter => HorizontalAlignment.Center,
                    NotifySite.BottomLeft or NotifySite.TopLeft => HorizontalAlignment.Left,
                    NotifySite.BottomRight or NotifySite.TopRight => HorizontalAlignment.Right,
                    _ => HorizontalAlignment.Stretch
                } : HorizontalAlignment.Stretch;
            }

            if ($"{parameter}" == "V")
            {
                return value is NotifySite site ? site switch
                {
                    NotifySite.BottomCenter or NotifySite.BottomRight or NotifySite.BottomLeft => VerticalAlignment.Bottom,
                  NotifySite.TopCenter or NotifySite.TopRight or NotifySite.TopLeft=>VerticalAlignment.Top,
                    _ => VerticalAlignment.Stretch
                } : VerticalAlignment.Stretch;
            }

            return DependencyProperty.UnsetValue;

          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
