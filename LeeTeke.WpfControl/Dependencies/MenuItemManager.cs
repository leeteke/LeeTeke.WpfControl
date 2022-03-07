using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
   public class MenuItemManager
    {

        #region RippleBrush
        public static Brush GetRippleBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RippleBrushProperty);
        }

        public static void SetRippleBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(RippleBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for RippleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RippleBrushProperty =
            DependencyProperty.RegisterAttached("RippleBrush", typeof(Brush), typeof(MenuItemManager));
        #endregion


        #region IconMinWidth
        public static double GetIconMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(IconMinWidthProperty);
        }

        public static void SetIconMinWidth(DependencyObject obj, double value)
        {
            obj.SetValue(IconMinWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconMinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconMinWidthProperty =
            DependencyProperty.RegisterAttached("IconMinWidth", typeof(double), typeof(MenuItemManager));
        #endregion


        #region NextTagMinWidth
        public static double GetNextTagMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(NextTagMinWidthProperty);
        }

        public static void SetNextTagMinWidth(DependencyObject obj, double value)
        {
            obj.SetValue(NextTagMinWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for NextTagMinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextTagMinWidthProperty =
            DependencyProperty.RegisterAttached("NextTagMinWidth", typeof(double), typeof(MenuItemManager));
        #endregion


    }
}
