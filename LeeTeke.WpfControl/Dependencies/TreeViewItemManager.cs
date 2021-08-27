using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class TreeViewItemManager
    {

        #region SwitchSize
        public static double GetSwitchSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SwitchSizeProperty);
        }

        public static void SetSwitchSize(DependencyObject obj, double value)
        {
            obj.SetValue(SwitchSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SwitchSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SwitchSizeProperty =
            DependencyProperty.RegisterAttached("SwitchSize", typeof(double), typeof(TreeViewItemManager));
        #endregion

        #region SelectedCloseOhterMode
        public static TreeViewSelectedCloseOtherMode GetSelectedCloseOhterMode(DependencyObject obj)
        {
            return (TreeViewSelectedCloseOtherMode)obj.GetValue(SelectedCloseOhterModeProperty);
        }

        public static void SetSelectedCloseOhterMode(DependencyObject obj, TreeViewSelectedCloseOtherMode value)
        {
            obj.SetValue(SelectedCloseOhterModeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedCloseOhterMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCloseOhterModeProperty =
            DependencyProperty.RegisterAttached("SelectedCloseOhterMode", typeof(TreeViewSelectedCloseOtherMode), typeof(TreeViewItemManager), new PropertyMetadata(OnTreeViewSelectedCloseOtherModeChanged));

        private static void OnTreeViewSelectedCloseOtherModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TreeViewItem item)
            {
                item.Expanded -= Item_Expanded;
                item.Selected -= Item_Expanded;
                if (e.NewValue is TreeViewSelectedCloseOtherMode value)
                {
                    item.Expanded += Item_Expanded;
                    item.Selected += Item_Expanded;
                }
            }
        }

        private static void Item_Expanded(object sender, RoutedEventArgs e)
        {
            if (sender is TreeViewItem item)
            {
                var mode = TreeViewItemManager.GetSelectedCloseOhterMode(item);
                if (mode== TreeViewSelectedCloseOtherMode.Sibling)
                {
                    var parent= VisualTreeHelper.GetParent(item);
                    if (parent is Panel panel)
                    {
                        foreach (var chlid in panel.Children)
                        {
                            if (chlid is TreeViewItem treeViewItem&& treeViewItem!=item)
                            {
                                treeViewItem.IsExpanded = false;
                            }
                        }
                    }
                }
             
            }
        }

        #endregion

        #region SelectedMode
        public static TreeViewSelectedMode GetSelectedMode(DependencyObject obj)
        {
            return (TreeViewSelectedMode)obj.GetValue(SelectedModeProperty);
        }

        public static void SetSelectedMode(DependencyObject obj, TreeViewSelectedMode value)
        {
            obj.SetValue(SelectedModeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedModeProperty =
            DependencyProperty.RegisterAttached("SelectedMode", typeof(TreeViewSelectedMode), typeof(TreeViewItemManager));
        #endregion


    }
}
