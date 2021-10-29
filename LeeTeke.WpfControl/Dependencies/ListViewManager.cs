using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ListViewManager:ListManager
    {

        #region HeaderFreeze
        public static bool GetHeaderFreeze(DependencyObject obj)
        {
            return (bool)obj.GetValue(HeaderFreezeProperty);
        }

        public static void SetHeaderFreeze(DependencyObject obj, bool value)
        {
            obj.SetValue(HeaderFreezeProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderFreeze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFreezeProperty =
            DependencyProperty.RegisterAttached("HeaderFreeze", typeof(bool), typeof(ListViewManager));
        #endregion

        #region HeaderBackground
        public static Brush GetHeaderBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderBackgroundProperty);
        }

        public static void SetHeaderBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.RegisterAttached("HeaderBackground", typeof(Brush), typeof(ListViewManager));
        #endregion

        #region HeaderBorderThickness
        public static Thickness GetHeaderBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HeaderBorderThicknessProperty);
        }

        public static void SetHeaderBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HeaderBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBorderThicknessProperty =
            DependencyProperty.RegisterAttached("HeaderBorderThickness", typeof(Thickness), typeof(ListViewManager));
        #endregion

        #region HeaderBorderBursh
        public static Brush GetHeaderBorderBursh(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderBorderBurshProperty);
        }

        public static void SetHeaderBorderBursh(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderBorderBurshProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderBorderBursh.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBorderBurshProperty =
            DependencyProperty.RegisterAttached("HeaderBorderBursh", typeof(Brush), typeof(ListViewManager));
        #endregion

        #region HeaderMargin
        public static Thickness GetHeaderMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HeaderMarginProperty);
        }

        public static void SetHeaderMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HeaderMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.RegisterAttached("HeaderMargin", typeof(Thickness), typeof(ListViewManager));
        #endregion

        #region HeaderPadding
        public static Thickness GetHeaderPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HeaderPaddingProperty);
        }

        public static void SetHeaderPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HeaderPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderPaddingProperty =
            DependencyProperty.RegisterAttached("HeaderPadding", typeof(Thickness), typeof(ListViewManager));
        #endregion

        #region HeaderCornerRadius
        public static CornerRadius GetHeaderCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(HeaderCornerRadiusProperty);
        }

        public static void SetHeaderCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(HeaderCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderCornerRadiusProperty =
            DependencyProperty.RegisterAttached("HeaderCornerRadius", typeof(CornerRadius), typeof(ListViewManager));
        #endregion

        #region HeaderIsClip
        public static bool GetHeaderIsClip(DependencyObject obj)
        {
            return (bool)obj.GetValue(HeaderIsClipProperty);
        }

        public static void SetHeaderIsClip(DependencyObject obj, bool value)
        {
            obj.SetValue(HeaderIsClipProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderIsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderIsClipProperty =
            DependencyProperty.RegisterAttached("HeaderIsClip", typeof(bool), typeof(ListViewManager));
        #endregion


        #region HeaderHorizontalAlignment
        public static HorizontalAlignment GetHeaderHorizontalAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(HeaderHorizontalAlignmentProperty);
        }

        public static void SetHeaderHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(HeaderHorizontalAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("HeaderHorizontalAlignment", typeof(HorizontalAlignment), typeof(ListViewManager));
        #endregion


        #region HeaderVerticalAlignment
        public static VerticalAlignment GetHeaderVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(HeaderVerticalAlignmentProperty);
        }

        public static void SetHeaderVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(HeaderVerticalAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("HeaderVerticalAlignment", typeof(VerticalAlignment), typeof(ListViewManager));
        #endregion


    }
}
