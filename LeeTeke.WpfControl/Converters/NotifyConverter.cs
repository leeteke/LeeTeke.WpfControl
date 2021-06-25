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
            return value is BannerPathMode mode
                ? mode switch
                {
                    BannerPathMode.RightTop or BannerPathMode.LeftTop => VerticalAlignment.Top,
                    BannerPathMode.RightBottom or BannerPathMode.LeftBottom => VerticalAlignment.Bottom,
                    BannerPathMode.LeftCenter or BannerPathMode.RightCenter => VerticalAlignment.Center,
                    _ => VerticalAlignment.Stretch,
                }
                : (object)VerticalAlignment.Stretch;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return default;
        }
    }
}
