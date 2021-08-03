using LeeTeke.WpfControl.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;

namespace LeeTeke.WpfControl.Dependencies
{
    public class TabViewManager 
    {

        public static bool GetItemCanClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(ItemCanCloseProperty);
        }

        public static void SetItemCanClose(DependencyObject obj, bool value)
        {
            obj.SetValue(ItemCanCloseProperty, value);
        }

        // Using a DependencyProperty as the backing store for ItemCanClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCanCloseProperty =
            DependencyProperty.RegisterAttached("ItemCanClose", typeof(bool), typeof(TabViewManager), new PropertyMetadata(true));        
    }
}
