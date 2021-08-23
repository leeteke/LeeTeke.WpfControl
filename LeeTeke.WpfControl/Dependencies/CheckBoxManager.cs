using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

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

        #region CheckedContent
        public static object GetCheckedContent(DependencyObject obj)
        {
            return (object)obj.GetValue(CheckedContentProperty);
        }

        public static void SetCheckedContent(DependencyObject obj, object value)
        {
            obj.SetValue(CheckedContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedContentProperty =
            DependencyProperty.RegisterAttached("CheckedContent", typeof(object), typeof(CheckBoxManager), new PropertyMetadata(CheckedContentChanged));

        private static void CheckedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CheckBox toggle)
            {
                SetHaveCheckedContent(toggle, e.NewValue != null);
            }
        }
        #endregion

        #region HaveCheckedContent
        public static bool GetHaveCheckedContent(DependencyObject obj)
        {
            return (bool)obj.GetValue(HaveCheckedContentProperty);
        }

        private static void SetHaveCheckedContent(DependencyObject obj, bool value)
        {
            obj.SetValue(HaveCheckedContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for HaveCheckedContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HaveCheckedContentProperty =
            DependencyProperty.RegisterAttached("HaveCheckedContent", typeof(bool), typeof(CheckBoxManager));
        #endregion


        #region SwtichFill
        public static Brush GetSwtichFill(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SwtichFillProperty);
        }

        public static void SetSwtichFill(DependencyObject obj, Brush value)
        {
            obj.SetValue(SwtichFillProperty, value);
        }

        // Using a DependencyProperty as the backing store for SwtichFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SwtichFillProperty =
            DependencyProperty.RegisterAttached("SwtichFill", typeof(Brush), typeof(CheckBoxManager));
        #endregion


    }
}
