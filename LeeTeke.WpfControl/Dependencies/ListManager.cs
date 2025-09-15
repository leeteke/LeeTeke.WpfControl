using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ListManager
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
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(ListManager));



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
            DependencyProperty.RegisterAttached("NoItemsContent", typeof(object), typeof(ListManager));

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
            DependencyProperty.RegisterAttached("ListEndContent", typeof(object), typeof(ListManager));
        #endregion
    }
}
