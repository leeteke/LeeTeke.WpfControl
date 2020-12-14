using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ContextMenuManager: DependencyObject
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
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ContextMenuManager));
        #endregion

        #region Shadow


        public static DropShadowEffect GetShadow(DependencyObject obj)
        {
            return (DropShadowEffect)obj.GetValue(ShadowProperty);
        }

        public static void SetShadow(DependencyObject obj, DropShadowEffect value)
        {
            obj.SetValue(ShadowProperty, value);
        }

        // Using a DependencyProperty as the backing store for Shadow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowProperty =
            DependencyProperty.RegisterAttached("Shadow", typeof(DropShadowEffect), typeof(ContextMenuManager));


        #endregion


    }
}
