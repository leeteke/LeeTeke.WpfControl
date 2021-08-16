using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ListBoxManager
    {

        #region Orientation


        public static Orientation GetOrientation(DependencyObject obj)
        {
            return (Orientation)obj.GetValue(OrientationProperty);
        }

        public static void SetOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(OrientationProperty, value);
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(ListBoxManager));



        #endregion

        #region NoItemsContent
        public static object GetNoItemsContent(DependencyObject obj)
        {
            return (object)obj.GetValue(NoItemsContentProperty);
        }

        public static void SetNoItemsContent(DependencyObject obj, object value)
        {
            obj.SetValue(NoItemsContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for NoItemsContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoItemsContentProperty =
            DependencyProperty.RegisterAttached("NoItemsContent", typeof(object), typeof(ListBoxManager));

        #endregion

        #region ListEndContent
        public static object GetListEndContent(DependencyObject obj)
        {
            return (object)obj.GetValue(ListEndContentProperty);
        }

        public static void SetListEndContent(DependencyObject obj, object value)
        {
            obj.SetValue(ListEndContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for ListEndContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListEndContentProperty =
            DependencyProperty.RegisterAttached("ListEndContent", typeof(object), typeof(ListBoxManager));
        #endregion

    }
   
}
