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
   public class SiteToVerticalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SiteMode site)
                return site switch
                {
                    SiteMode.Start => VerticalAlignment.Top,
                    SiteMode.Center => VerticalAlignment.Center,
                    SiteMode.End => VerticalAlignment.Bottom,
                    SiteMode.Full => VerticalAlignment.Stretch,
                    _ => VerticalAlignment.Stretch,
                };
            return VerticalAlignment.Stretch;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is VerticalAlignment site)
                return site switch
                {
                     VerticalAlignment.Top=> SiteMode.Start,
                     VerticalAlignment.Center=> SiteMode.Center,
                    VerticalAlignment.Bottom => SiteMode.End ,
                     VerticalAlignment.Stretch=> SiteMode.Full,
                    _ => SiteMode.Full,
                };
            return SiteMode.Full;
        }
    }
}
