using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
   public class MenuItemManager
    {
        #region RippleBrush
        public static Brush GetRippleBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RippleBrushProperty);
        }

        public static void SetRippleBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(RippleBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for RippleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RippleBrushProperty =
            DependencyProperty.RegisterAttached("RippleBrush", typeof(Brush), typeof(MenuItemManager));
        #endregion

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
            DependencyProperty.RegisterAttached("MinHeight", typeof(double), typeof(MenuItemManager));
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
            DependencyProperty.RegisterAttached("MinWidth", typeof(double), typeof(MenuItemManager));
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
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(MenuItemManager));
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
            DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(MenuItemManager));
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
            DependencyProperty.RegisterAttached("BorderThickness", typeof(Thickness), typeof(MenuItemManager));
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
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(MenuItemManager));
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
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(MenuItemManager));
        #endregion

        #region SubTagMinWidth
        public static double GetSubTagMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(SubTagMinWidthProperty);
        }

        public static void SetSubTagMinWidth(DependencyObject obj, double value)
        {
            obj.SetValue(SubTagMinWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubTagMinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubTagMinWidthProperty =
            DependencyProperty.RegisterAttached("SubTagMinWidth", typeof(double), typeof(MenuItemManager));
        #endregion

        #region SubCornerRadius
        public static CornerRadius GetSubCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(SubCornerRadiusProperty);
        }

        public static void SetSubCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(SubCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubCornerRadiusProperty =
            DependencyProperty.RegisterAttached("SubCornerRadius", typeof(CornerRadius), typeof( MenuItemManager));
        #endregion

        #region SubIsClip
        public static bool GetSubIsClip(DependencyObject obj)
        {
            return (bool)obj.GetValue(SubIsClipProperty);
        }

        public static void SetSubIsClip(DependencyObject obj, bool value)
        {
            obj.SetValue(SubIsClipProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubIsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubIsClipProperty =
            DependencyProperty.RegisterAttached("SubIsClip", typeof(bool), typeof(MenuItemManager));
        #endregion

        #region SubEffect
        public static Effect GetSubEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(SubEffectProperty);
        }

        public static void SetSubEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(SubEffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubEffectProperty =
            DependencyProperty.RegisterAttached("SubEffect", typeof(Effect), typeof(MenuItemManager));
        #endregion

        #region SubBackground
        public static Brush GetSubBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SubBackgroundProperty);
        }

        public static void SetSubBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SubBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubBackgroundProperty =
            DependencyProperty.RegisterAttached("SubBackground", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region SubPadding
        public static Thickness GetSubPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SubPaddingProperty);
        }

        public static void SetSubPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SubPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubPaddingProperty =
            DependencyProperty.RegisterAttached("SubPadding", typeof(Thickness), typeof(MenuItemManager));
        #endregion

        #region SubBorderThickness
        public static Thickness GetSubBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SubBorderThicknessProperty);
        }

        public static void SetSubBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SubBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubBorderThicknessProperty =
            DependencyProperty.RegisterAttached("SubBorderThickness", typeof(Thickness), typeof(MenuItemManager));
        #endregion

        #region SubBorderBrush
        public static Brush GetSubBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SubBorderBrushProperty);
        }

        public static void SetSubBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(SubBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubBorderBrushProperty =
            DependencyProperty.RegisterAttached("SubBorderBrush", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region SubScrollViewerStyle
        public static Style GetSubScrollViewerStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(SubScrollViewerStyleProperty);
        }

        public static void SetSubScrollViewerStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(SubScrollViewerStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for SubScrollViewerStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubScrollViewerStyleProperty =
            DependencyProperty.RegisterAttached("SubScrollViewerStyle", typeof(Style), typeof(MenuItemManager));
        #endregion

        #region HighlightedBackground
        public static Brush GetHighlightedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HighlightedBackgroundProperty);
        }

        public static void SetHighlightedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HighlightedBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HighlightedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightedBackgroundProperty =
            DependencyProperty.RegisterAttached("HighlightedBackground", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region HighlightedBorderBrush
        public static Brush GetHighlightedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HighlightedBorderBrushProperty);
        }

        public static void SetHighlightedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(HighlightedBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for HighlightedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightedBorderBrushProperty =
            DependencyProperty.RegisterAttached("HighlightedBorderBrush", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region HighlightedBorderThickness
        public static Thickness GetHighlightedBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HighlightedBorderThicknessProperty);
        }

        public static void SetHighlightedBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HighlightedBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for HighlightedBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("HighlightedBorderThickness", typeof(Thickness), typeof(MenuItemManager));
        #endregion

        #region HighlightedForeground
        public static Brush GetHighlightedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HighlightedForegroundProperty);
        }

        public static void SetHighlightedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HighlightedForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HighlightedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightedForegroundProperty =
            DependencyProperty.RegisterAttached("HighlightedForeground", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region CheckedBackground
        public static Brush GetCheckedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedBackgroundProperty);
        }

        public static void SetCheckedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.RegisterAttached("CheckedBackground", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region CheckedBorderBrush
        public static Brush GetCheckedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedBorderBrushProperty);
        }

        public static void SetCheckedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedBorderBrushProperty =
            DependencyProperty.RegisterAttached("CheckedBorderBrush", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region CheckedBorderThickness
        public static Thickness GetCheckedBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(CheckedBorderThicknessProperty);
        }

        public static void SetCheckedBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(CheckedBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("CheckedBorderThickness", typeof(Thickness), typeof(MenuItemManager));
        #endregion

        #region CheckedForeground
        public static Brush GetCheckedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedForegroundProperty);
        }

        public static void SetCheckedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.RegisterAttached("CheckedForeground", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region IconFontFamily
        public static FontFamily GetIconFontFamily(DependencyObject obj)
        {
            return (FontFamily)obj.GetValue(IconFontFamilyProperty);
        }

        public static void SetIconFontFamily(DependencyObject obj, FontFamily value)
        {
            obj.SetValue(IconFontFamilyProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontFamilyProperty =
            DependencyProperty.RegisterAttached("IconFontFamily", typeof(FontFamily), typeof(MenuItemManager),new PropertyMetadata(DependencyConst.FontFamily));
        #endregion

        #region IconForeground
        public static Brush GetIconForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(IconForegroundProperty);
        }

        public static void SetIconForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(IconForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.RegisterAttached("IconForeground", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region IconFontSize
        public static double GetIconFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(IconFontSizeProperty);
        }

        public static void SetIconFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(IconFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontSizeProperty =
            DependencyProperty.RegisterAttached("IconFontSize", typeof(double), typeof(MenuItemManager),new PropertyMetadata(DependencyConst.FontSize));
        #endregion

        #region IconFontWeight
        public static FontWeight GetIconFontWeight(DependencyObject obj)
        {
            return (FontWeight)obj.GetValue(IconFontWeightProperty);
        }

        public static void SetIconFontWeight(DependencyObject obj, FontWeight value)
        {
            obj.SetValue(IconFontWeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontWeightProperty =
            DependencyProperty.RegisterAttached("IconFontWeight", typeof(FontWeight), typeof(MenuItemManager));
        #endregion

        #region IconMinWidth
        public static double GetIconMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(IconMinWidthProperty);
        }

        public static void SetIconMinWidth(DependencyObject obj, double value)
        {
            obj.SetValue(IconMinWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconMinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconMinWidthProperty =
            DependencyProperty.RegisterAttached("IconMinWidth", typeof(double), typeof(MenuItemManager));
        #endregion

        #region IconVerticalAlignment
        public static VerticalAlignment GetIconVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(IconVerticalAlignmentProperty);
        }

        public static void SetIconVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(IconVerticalAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("IconVerticalAlignment", typeof(VerticalAlignment), typeof(MenuItemManager));
        #endregion

        #region IconHorizontalAlignment
        public static HorizontalAlignment GetIconHorizontalAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(IconHorizontalAlignmentProperty);
        }

        public static void SetIconHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(IconHorizontalAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconHorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("IconHorizontalAlignment", typeof(HorizontalAlignment), typeof(MenuItemManager));
        #endregion

        #region SeparatorMargin
        public static Thickness GetSeparatorMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SeparatorMarginProperty);
        }

        public static void SetSeparatorMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SeparatorMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for SeparatorMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorMarginProperty =
            DependencyProperty.RegisterAttached("SeparatorMargin", typeof(Thickness), typeof(MenuItemManager));
        #endregion

        #region SeparatorHeight
        public static double GetSeparatorHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(SeparatorHeightProperty);
        }

        public static void SetSeparatorHeight(DependencyObject obj, double value)
        {
            obj.SetValue(SeparatorHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for SeparatorHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorHeightProperty =
            DependencyProperty.RegisterAttached("SeparatorHeight", typeof(double), typeof(MenuItemManager));
        #endregion

        #region SeparatorFill
        public static Brush GetSeparatorFill(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SeparatorFillProperty);
        }

        public static void SetSeparatorFill(DependencyObject obj, Brush value)
        {
            obj.SetValue(SeparatorFillProperty, value);
        }

        // Using a DependencyProperty as the backing store for SeparatorFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorFillProperty =
            DependencyProperty.RegisterAttached("SeparatorFill", typeof(Brush), typeof(MenuItemManager));
        #endregion

        #region SeparatorContent
        public static object GetSeparatorContent(DependencyObject obj)
        {
            return (object)obj.GetValue(SeparatorContentProperty);
        }

        public static void SetSeparatorContent(DependencyObject obj, object value)
        {
            obj.SetValue(SeparatorContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for SeparatorContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorContentProperty =
            DependencyProperty.RegisterAttached("SeparatorContent", typeof(object), typeof(MenuItemManager));
        #endregion

    }
}
