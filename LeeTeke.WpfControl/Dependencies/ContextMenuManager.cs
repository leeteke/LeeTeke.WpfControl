using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ContextMenuManager
    {
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

        #region Content
        public static object GetContent(DependencyObject obj)
        {
            return (object)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, object value)
        {
            obj.SetValue(ContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached("Content", typeof(object), typeof(ContextMenuManager));
        #endregion


        #region ContentDock
        public static Dock GetContentDock(DependencyObject obj)
        {
            return (Dock)obj.GetValue(ContentDockProperty);
        }

        public static void SetContentDock(DependencyObject obj, Dock value)
        {
            obj.SetValue(ContentDockProperty, value);
        }

        // Using a DependencyProperty as the backing store for ContentDock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentDockProperty =
            DependencyProperty.RegisterAttached("ContentDock", typeof(Dock), typeof(ContextMenuManager));
        #endregion


        #region SeparatorMargin
        public static Thickness GetSeparatorMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SeparatorMarginProperty);
        }

        public static void SetSeparatorMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SeparatorMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for SeparatorMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorMarginProperty =
            DependencyProperty.RegisterAttached("SeparatorMargin", typeof(Thickness), typeof(ContextMenuManager));
        #endregion

        #region SeparatorHeight
        public static double GetSeparatorHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(SeparatorHeightProperty);
        }

        public static void SetSeparatorHeight(DependencyObject obj, double value)
        {
            obj.SetValue(SeparatorHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for SeparatorHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorHeightProperty =
            DependencyProperty.RegisterAttached("SeparatorHeight", typeof(double), typeof(ContextMenuManager));
        #endregion

        #region SeparatorFill
        public static Brush GetSeparatorFill(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SeparatorFillProperty);
        }

        public static void SetSeparatorFill(DependencyObject obj, Brush value)
        {
            obj.SetValue(SeparatorFillProperty, value);
        }

        // Using a DependencyProperty as the backing store for SeparatorFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorFillProperty =
            DependencyProperty.RegisterAttached("SeparatorFill", typeof(Brush), typeof(ContextMenuManager));
        #endregion


        #region SeparatorContent
        public static object GetSeparatorContent(DependencyObject obj)
        {
            return (object)obj.GetValue(SeparatorContentProperty);
        }

        public static void SetSeparatorContent(DependencyObject obj, object value)
        {
            obj.SetValue(SeparatorContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for SeparatorContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorContentProperty =
            DependencyProperty.RegisterAttached("SeparatorContent", typeof(object), typeof(ContextMenuManager));
        #endregion


    }
}
