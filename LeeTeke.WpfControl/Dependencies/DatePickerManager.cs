using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace LeeTeke.WpfControl.Dependencies
{
    public class DatePickerManager
    {





        #region PopupAnimation
        public static PopupAnimation GetPopupAnimation(DependencyObject obj)
        {
            return (PopupAnimation)obj.GetValue(PopupAnimationProperty);
        }

        public static void SetPopupAnimation(DependencyObject obj, PopupAnimation value)
        {
            obj.SetValue(PopupAnimationProperty, value);
        }

        // Using a DependencyProperty as the backing store for PopupAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupAnimationProperty =
            DependencyProperty.RegisterAttached("PopupAnimation", typeof(PopupAnimation), typeof(DatePickerManager));
        #endregion


        #region ButtonWidth
        public static double GetButtonWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ButtonWidthProperty);
        }

        public static void SetButtonWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ButtonWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.RegisterAttached("ButtonWidth", typeof(double), typeof(DatePickerManager));
        #endregion


        #region ButtonFontSize
        public static double GetButtonFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ButtonFontSizeProperty);
        }

        public static void SetButtonFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(ButtonFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonFontSizeProperty =
            DependencyProperty.RegisterAttached("ButtonFontSize", typeof(double), typeof(DatePickerManager),new PropertyMetadata(DependencyConst.FontSize));
        #endregion


    }
}
