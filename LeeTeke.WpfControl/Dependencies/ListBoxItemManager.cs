using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using LeeTeke.WpfControl;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ListBoxItemManager: ListItemManager
    {

        #region MarkSite


        public static ListItemMarkSite GetMarkSite(DependencyObject obj)
        {
            return (ListItemMarkSite)obj.GetValue(MarkSiteProperty);
        }

        public static void SetMarkSite(DependencyObject obj, ListItemMarkSite value)
        {
            obj.SetValue(MarkSiteProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkSite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkSiteProperty =
            DependencyProperty.RegisterAttached("MarkSite", typeof(ListItemMarkSite), typeof(ListBoxItemManager), new PropertyMetadata(ListItemMarkSite.Left));




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
            DependencyProperty.RegisterAttached("MarkSize", typeof(double), typeof(ListBoxItemManager), new PropertyMetadata(3.0));



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
            DependencyProperty.RegisterAttached("MarkMargin", typeof(Thickness), typeof(ListBoxItemManager), new PropertyMetadata(new Thickness(0)));
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
