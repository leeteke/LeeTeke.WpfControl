using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ToggleButtonManager
    {

        #region CornerRadius

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ToggleButtonManager));



        #endregion

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
            DependencyProperty.RegisterAttached("RippleBrush", typeof(Brush), typeof(ToggleButtonManager));
        #endregion

        #region MouseOverBackground

        public static Brush GetMouseOverBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBackgroundProperty);
        }

        public static void SetMouseOverBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseMoveBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(ToggleButtonManager));
        #endregion

        #region MouseOverBorderBrush

        public static Brush GetMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBorderBrushProperty);
        }

        public static void SetMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseMoveBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(ToggleButtonManager));
        #endregion

        #region CheckedBackground


        public static Brush GetCheckedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedBackgroundProperty);
        }

        public static void SetCheckedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.RegisterAttached("CheckedBackground", typeof(Brush), typeof(ToggleButtonManager));


        #endregion

        #region CheckedBorderBrush


        public static Brush GetCheckedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedBorderBrushProperty);
        }

        public static void SetCheckedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedBorderBrushProperty =
            DependencyProperty.RegisterAttached("CheckedBorderBrush", typeof(Brush), typeof(ToggleButtonManager));


        #endregion

        #region CheckedForeground



        public static Brush GetCheckedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedForegroundProperty);
        }

        public static void SetCheckedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.RegisterAttached("CheckedForeground", typeof(Brush), typeof(ToggleButtonManager));



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
            DependencyProperty.RegisterAttached("CheckedContent", typeof(object), typeof(ToggleButtonManager),new PropertyMetadata(CheckedContentChanged));

        private static void CheckedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleButton toggle)
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
            DependencyProperty.RegisterAttached("HaveCheckedContent", typeof(bool), typeof(ToggleButtonManager));
        #endregion




    }
}
