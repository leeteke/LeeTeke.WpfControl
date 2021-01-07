using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
   public class CheckBoxManager
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
            DependencyProperty.RegisterAttached("RippleBrush", typeof(Brush), typeof(CheckBoxManager));

        #endregion


        #region CheckBoxSize
        /// <summary>
        /// 选择框大小
        /// </summary>
        public static double GetCheckBoxSize(DependencyObject obj)
        {
            return (double)obj.GetValue(CheckBoxSizeProperty);
        }

        public static void SetCheckBoxSize(DependencyObject obj, double value)
        {
            obj.SetValue(CheckBoxSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckBoxSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckBoxSizeProperty =
            DependencyProperty.RegisterAttached("CheckBoxSize", typeof(double), typeof(CheckBoxManager));
        #endregion



    }
}
