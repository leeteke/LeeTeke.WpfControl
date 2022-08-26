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
                if (value is NotifySite site)
                {
                    switch (site)
                    {

                        case NotifySite.BottomCenter:
                        case NotifySite.TopCenter:
                            return HorizontalAlignment.Center;
                        case NotifySite.BottomLeft:
                        case NotifySite.TopLeft:
                            return HorizontalAlignment.Left;
                        case NotifySite.BottomRight:
                        case NotifySite.TopRight:
                            return HorizontalAlignment.Right;
                        default:
                            return HorizontalAlignment.Stretch;
                    }
                }
                else
                {
                    return HorizontalAlignment.Stretch;
                }
            }

            if ($"{parameter}" == "V")
            {
                if (value is NotifySite site)
                {
                    switch (site)
                    {
                        case NotifySite.TopRight:
                        case NotifySite.TopCenter:
                        case NotifySite.TopLeft:
                            return VerticalAlignment.Top;
                        case NotifySite.BottomRight:
                        case NotifySite.BottomCenter:
                        case NotifySite.BottomLeft:

                            return VerticalAlignment.Bottom;
                        default:
                            return VerticalAlignment.Stretch;
                    }
                }
                else
                {
                    return VerticalAlignment.Stretch;
                }
            }

            return DependencyProperty.UnsetValue;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
