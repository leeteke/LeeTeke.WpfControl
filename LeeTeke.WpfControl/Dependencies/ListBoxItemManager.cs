﻿using System;
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
    public class ListBoxItemManager
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
            DependencyProperty.RegisterAttached("MinHeight", typeof(double), typeof(ListBoxItemManager));
        #endregion


        #region MinWidth
        public static double GetMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MinWidthProperty);
        }

        public static void SetMinWidth(DependencyObject obj, double value)
        {
            obj.SetValue(MinWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinWidthProperty =
            DependencyProperty.RegisterAttached("MinWidth", typeof(double), typeof(ListBoxItemManager));
        #endregion


        #region Background
        public static Brush GetBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(ListBoxItemManager));
        #endregion

        #region BorderBrush
        public static Brush GetBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BorderBrushProperty);
        }

        public static void SetBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(ListBoxItemManager));
        #endregion

        #region BorderThickness
        public static Thickness GetBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(BorderThicknessProperty);
        }

        public static void SetBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(BorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for BorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.RegisterAttached("BorderThickness", typeof(Thickness), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(ListBoxItemManager));
        #endregion

        #region MouseOverBorderBrush
        public static Brush GetMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBorderBrushProperty);
        }

        public static void SetMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(ListBoxItemManager));
        #endregion

        #region MouseOverBorderThickness
        public static Thickness GetMouseOverBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MouseOverBorderThicknessProperty);
        }

        public static void SetMouseOverBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MouseOverBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBorderThicknessProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderThickness", typeof(Thickness), typeof(ListBoxItemManager));
        #endregion

        #region Marigin
        public static Thickness GetMarigin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MariginProperty);
        }

        public static void SetMarigin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MariginProperty, value);
        }

        // Using a DependencyProperty as the backing store for Marigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MariginProperty =
            DependencyProperty.RegisterAttached("Marigin", typeof(Thickness), typeof(ListBoxItemManager));
        #endregion

        #region Padding
        public static Thickness GetPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(PaddingProperty);
        }

        public static void SetPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(PaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for Padding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.RegisterAttached("Padding", typeof(Thickness), typeof(ListBoxItemManager));
        #endregion

        #region Effect
        public static Effect GetEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(EffectProperty);
        }

        public static void SetEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(EffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for Effect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EffectProperty =
            DependencyProperty.RegisterAttached("Effect", typeof(Effect), typeof(ListBoxItemManager));
        #endregion

        #region MouseOverEffect
        public static Effect GetMouseOverEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(MouseOverEffectProperty);
        }

        public static void SetMouseOverEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(MouseOverEffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverEffectProperty =
            DependencyProperty.RegisterAttached("MouseOverEffect", typeof(Effect), typeof(ListBoxItemManager));
        #endregion

        #region SelectedEffect
        public static Effect GetSelectedEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(SelectedEffectProperty);
        }

        public static void SetSelectedEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(SelectedEffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEffectProperty =
            DependencyProperty.RegisterAttached("SelectedEffect", typeof(Effect), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("SelectedBackground", typeof(Brush), typeof(ListBoxItemManager));
        #endregion

        #region SelectedBorderBrush
        public static Brush GetSelectedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedBorderBrushProperty);
        }

        public static void SetSelectedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBorderBrushProperty =
            DependencyProperty.RegisterAttached("SelectedBorderBrush", typeof(Brush), typeof(ListBoxItemManager));
        #endregion

        #region SelectedBorderThickness
        public static Thickness GetSelectedBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SelectedBorderThicknessProperty);
        }

        public static void SetSelectedBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SelectedBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("SelectedBorderThickness", typeof(Thickness), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("SelectedForeground", typeof(Brush), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("SelectedFontSize", typeof(double), typeof(ListBoxItemManager));
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
            DependencyProperty.RegisterAttached("SelectedFontWeight", typeof(FontWeight), typeof(ListBoxItemManager));
        #endregion

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