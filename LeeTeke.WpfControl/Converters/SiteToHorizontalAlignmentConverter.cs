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
    public class SiteToHorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SiteMode site)
                return site switch
                {
                    SiteMode.Start => HorizontalAlignment.Left,
                    SiteMode.Center => HorizontalAlignment.Center,
                    SiteMode.End => HorizontalAlignment.Right,
                    SiteMode.Full => HorizontalAlignment.Stretch,
                    _ => HorizontalAlignment.Stretch,
                };
            return HorizontalAlignment.Stretch;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HorizontalAlignment site)
                return site switch
                {
                    HorizontalAlignment.Left => SiteMode.Start,
                    HorizontalAlignment.Center => SiteMode.Center,
                    HorizontalAlignment.Right => SiteMode.End,
                    HorizontalAlignment.Stretch => SiteMode.Full,
                    _ => SiteMode.Full,
                };
            return SiteMode.Full;
        }
    }
}
