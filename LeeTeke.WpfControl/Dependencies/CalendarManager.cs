using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class CalendarManager
    {

        #region TitleBackground
        public static Brush GetTitleBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TitleBackgroundProperty);
        }

        public static void SetTitleBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for TitleBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleBackgroundProperty =
            DependencyProperty.RegisterAttached("TitleBackground", typeof(Brush), typeof(CalendarManager));
        #endregion


        #region TitleTextBrush
        public static Brush GetTitleTextBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TitleTextBrushProperty);
        }

        public static void SetTitleTextBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleTextBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for TitleTextBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTextBrushProperty =
            DependencyProperty.RegisterAttached("TitleTextBrush", typeof(Brush), typeof(CalendarManager));
        #endregion


    }
}
