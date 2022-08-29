using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class TreeViewItemManager
    {


        #region MinHeight
        public static double GetMinHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(MinHeightProperty);
        }

        public static void SetMinHeight(DependencyObject obj, double value)
        {
            obj.SetValue(MinHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinHeightProperty =
            DependencyProperty.RegisterAttached("MinHeight", typeof(double), typeof(TreeViewItemManager));
        #endregion

        #region Margin
        public static Thickness GetMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginProperty);
        }

        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for Margin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(TreeViewItemManager));
        #endregion

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
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(TreeViewItemManager));
        #endregion

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


        #region SwitchWidth
        public static double GetSwitchWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(SwitchWidthProperty);
        }

        public static void SetSwitchWidth(DependencyObject obj, double value)
        {
            obj.SetValue(SwitchWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for SwitchWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SwitchWidthProperty =
            DependencyProperty.RegisterAttached("SwitchWidth", typeof(double), typeof(TreeViewItemManager));
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

        #region NoItemsModeParentSign
        public static bool GetNoItemsModeParentSign(DependencyObject obj)
        {
            return (bool)obj.GetValue(NoItemsModeParentSignProperty);
        }

        public static void SetNoItemsModeParentSign(DependencyObject obj, bool value)
        {
            obj.SetValue(NoItemsModeParentSignProperty, value);
        }

        // Using a DependencyProperty as the backing store for NoItemsModeParentSign.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoItemsModeParentSignProperty =
            DependencyProperty.RegisterAttached("NoItemsModeParentSign", typeof(bool), typeof(TreeViewItemManager));
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

        #region MouseOverBackground
        public static Brush GetMouseOverBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBackgroundProperty);
        }

        public static void SetMouseOverBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(TreeViewItemManager));
        #endregion

        #region MouseOverForeground
        public static Brush GetMouseOverForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverForegroundProperty);
        }

        public static void SetMouseOverForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.RegisterAttached("MouseOverForeground", typeof(Brush), typeof(TreeViewItemManager));
        #endregion

        #region MouseOverFontSize
        public static double GetMouseOverFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(MouseOverFontSizeProperty);
        }

        public static void SetMouseOverFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(MouseOverFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverFontSizeProperty =
            DependencyProperty.RegisterAttached("MouseOverFontSize", typeof(double), typeof(TreeViewItemManager), new PropertyMetadata(DependencyConst.FontSize));
        #endregion

        #region MouseOverFontWeight
        public static FontWeight GetMouseOverFontWeight(DependencyObject obj)
        {
            return (FontWeight)obj.GetValue(MouseOverFontWeightProperty);
        }

        public static void SetMouseOverFontWeight(DependencyObject obj, FontWeight value)
        {
            obj.SetValue(MouseOverFontWeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverFontWeightProperty =
            DependencyProperty.RegisterAttached("MouseOverFontWeight", typeof(FontWeight), typeof(TreeViewItemManager));
        #endregion

        #region SelectedBackground
        public static Brush GetSelectedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedBackgroundProperty);
        }

        public static void SetSelectedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.RegisterAttached("SelectedBackground", typeof(Brush), typeof(TreeViewItemManager));
        #endregion

        #region SelectedForeground
        public static Brush GetSelectedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedForegroundProperty);
        }

        public static void SetSelectedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.RegisterAttached("SelectedForeground", typeof(Brush), typeof(TreeViewItemManager));
        #endregion

        #region SelectedFontSize
        public static double GetSelectedFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SelectedFontSizeProperty);
        }

        public static void SetSelectedFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(SelectedFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFontSizeProperty =
            DependencyProperty.RegisterAttached("SelectedFontSize", typeof(double), typeof(TreeViewItemManager), new PropertyMetadata(DependencyConst.FontSize));
        #endregion

        #region SelectedFontWeight
        public static FontWeight GetSelectedFontWeight(DependencyObject obj)
        {
            return (FontWeight)obj.GetValue(SelectedFontWeightProperty);
        }

        public static void SetSelectedFontWeight(DependencyObject obj, FontWeight value)
        {
            obj.SetValue(SelectedFontWeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFontWeightProperty =
            DependencyProperty.RegisterAttached("SelectedFontWeight", typeof(FontWeight), typeof(TreeViewItemManager));
        #endregion


        #region NoItemsModeParentSignBrush
        public static Brush GetNoItemsModeParentSignBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(NoItemsModeParentSignBrushProperty);
        }

        public static void SetNoItemsModeParentSignBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(NoItemsModeParentSignBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for NoItemsModeParentSignBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoItemsModeParentSignBrushProperty =
            DependencyProperty.RegisterAttached("NoItemsModeParentSignBrush", typeof(Brush), typeof(TreeViewItemManager));
        #endregion


        #region MarkShow
        public static bool GetMarkShow(DependencyObject obj)
        {
            return (bool)obj.GetValue(MarkShowProperty);
        }

        public static void SetMarkShow(DependencyObject obj, bool value)
        {
            obj.SetValue(MarkShowProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkShowProperty =
            DependencyProperty.RegisterAttached("MarkShow", typeof(bool), typeof(TreeViewItemManager));
        #endregion

        #region MarkSize

        public static double GetMarkSize(DependencyObject obj)
        {
            return (double)obj.GetValue(MarkSizeProperty);
        }

        public static void SetMarkSize(DependencyObject obj, double value)
        {
            obj.SetValue(MarkSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkSizeProperty =
            DependencyProperty.RegisterAttached("MarkSize", typeof(double), typeof(TreeViewItemManager));
        #endregion

        #region MarkBrush
        public static Brush GetMarkBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MarkBrushProperty);
        }

        public static void SetMarkBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MarkBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkBrushProperty =
            DependencyProperty.RegisterAttached("MarkBrush", typeof(Brush), typeof(TreeViewItemManager));
        #endregion

        #region MarkMargin

        public static Thickness GetMarkMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarkMarginProperty);
        }

        public static void SetMarkMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarkMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkMarginProperty =
            DependencyProperty.RegisterAttached("MarkMargin", typeof(Thickness), typeof(TreeViewItemManager));
        #endregion

        #region MarkCornerRadius
        public static CornerRadius GetMarkCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(MarkCornerRadiusProperty);
        }

        public static void SetMarkCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(MarkCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkCornerRadiusProperty =
            DependencyProperty.RegisterAttached("MarkCornerRadius", typeof(CornerRadius), typeof(TreeViewItemManager));
        #endregion

        #region MarkMouseOverBrush
        public static Brush GetMarkMouseOverBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MarkMouseOverBrushProperty);
        }

        public static void SetMarkMouseOverBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MarkMouseOverBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkMouseOverBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkMouseOverBrushProperty =
            DependencyProperty.RegisterAttached("MarkMouseOverBrush", typeof(Brush), typeof(TreeViewItemManager));
        #endregion

        #region MarkSelectedBrush
        public static Brush GetMarkSelectedBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MarkSelectedBrushProperty);
        }

        public static void SetMarkSelectedBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MarkSelectedBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkSelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkSelectedBrushProperty =
            DependencyProperty.RegisterAttached("MarkSelectedBrush", typeof(Brush), typeof(TreeViewItemManager));
        #endregion
    }
}
