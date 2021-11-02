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


    }
}
