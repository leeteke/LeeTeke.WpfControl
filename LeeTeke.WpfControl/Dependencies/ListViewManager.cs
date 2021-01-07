using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ListViewManager
    {
        #region SelectedItemBackground



        public static Brush GetSelectedItemBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedItemBackgroundProperty);
        }

        public static void SetSelectedItemBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedItemBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItemBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemBackgroundProperty =
            DependencyProperty.RegisterAttached("SelectedItemBackground", typeof(Brush), typeof(ListViewManager));








        #endregion

        #region  SelectedItemBorderBrush



        public static Brush GetSelectedItemBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedItemBorderBrushProperty);
        }

        public static void SetSelectedItemBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedItemBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItemBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemBorderBrushProperty =
            DependencyProperty.RegisterAttached("SelectedItemBorderBrush", typeof(Brush), typeof(ListViewManager));



        #endregion

        #region SelectedItemBorderThickness


        public static Thickness GetSelectedItemBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SelectedItemBorderThicknessProperty);
        }

        public static void SetSelectedItemBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SelectedItemBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItemBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemBorderThicknessProperty =
            DependencyProperty.RegisterAttached("SelectedItemBorderThickness", typeof(Thickness), typeof(ListViewManager));



        #endregion

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
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(ListViewManager));



        #endregion


        #region MarkSite


        public static ListViewItemMarkSite GetMarkSite(DependencyObject obj)
        {
            return (ListViewItemMarkSite)obj.GetValue(MarkSiteProperty);
        }

        public static void SetMarkSite(DependencyObject obj, ListViewItemMarkSite value)
        {
            obj.SetValue(MarkSiteProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkSite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkSiteProperty =
            DependencyProperty.RegisterAttached("MarkSite", typeof(ListViewItemMarkSite), typeof(ListViewManager), new PropertyMetadata(ListViewItemMarkSite.Left));




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
            DependencyProperty.RegisterAttached("MarkSize", typeof(double), typeof(ListViewManager), new PropertyMetadata(3.0));



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
            DependencyProperty.RegisterAttached("MarkMargin", typeof(Thickness), typeof(ListViewManager), new PropertyMetadata(new Thickness(0)));
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
            DependencyProperty.RegisterAttached("MarkBrush", typeof(Brush), typeof(ListViewManager));



        #endregion



        #region SelectedItemForeground
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static Brush GetSelectedItemForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedItemForegroundProperty);
        }

        public static void SetSelectedItemForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedItemForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItemForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemForegroundProperty =
            DependencyProperty.RegisterAttached("SelectedItemForeground", typeof(Brush), typeof(ListViewManager));
        #endregion

        #region SelectedItemFontSize
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static double GetSelectedItemFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SelectedItemFontSizeProperty);
        }

        public static void SetSelectedItemFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(SelectedItemFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItemFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemFontSizeProperty =
            DependencyProperty.RegisterAttached("SelectedItemFontSize", typeof(double), typeof(ListViewManager));
        #endregion

        #region SelectedItemFontWeight
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static FontWeight GetSelectedItemFontWeight(DependencyObject obj)
        {   
            return (FontWeight)obj.GetValue(SelectedItemFontWeightProperty);
        }

        public static void SetSelectedItemFontWeight(DependencyObject obj, FontWeight value)
        {
            obj.SetValue(SelectedItemFontWeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItemFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemFontWeightProperty =
            DependencyProperty.RegisterAttached("SelectedItemFontWeight", typeof(FontWeight), typeof(ListViewManager));
        #endregion

    }
    public enum ListViewItemMarkSite
    {
        Null,
        Top,
        Bottom,
        Left,
        Right
    }
}
