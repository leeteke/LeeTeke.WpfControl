using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using LeeTeke.WpfControl;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ListBoxItemManager
    {

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
            DependencyProperty.RegisterAttached("MarkShow", typeof(bool), typeof(ListBoxItemManager));
        #endregion

        #region MarkSite

        public static Dock GetMarkSite(DependencyObject obj)
        {
            return (Dock)obj.GetValue(MarkSiteProperty);
        }

        public static void SetMarkSite(DependencyObject obj, Dock value)
        {
            obj.SetValue(MarkSiteProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkSite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkSiteProperty =
            DependencyProperty.RegisterAttached("MarkSite", typeof(Dock), typeof(ListBoxItemManager));




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
            DependencyProperty.RegisterAttached("MarkSize", typeof(double), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("MarkBrush", typeof(Brush), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("MarkMargin", typeof(Thickness), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("MarkCornerRadius", typeof(CornerRadius), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("MarkMouseOverBrush", typeof(Brush), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("MarkSelectedBrush", typeof(Brush), typeof(ListBoxItemManager));
        #endregion

    }

}
