﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LeeTeke.WpfControl.Converters
{
   public class VisibilityForBoolReverseConverter : IValueConverter
    {

      
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool vb)
                return vb ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility vb)
            {
                return vb switch
                {
                    Visibility.Visible => false,
                    _ => true,
                };
            }
            return false;
        }
    }
}
