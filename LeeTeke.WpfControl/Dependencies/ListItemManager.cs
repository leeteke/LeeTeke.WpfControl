using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ListItemManager
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
            DependencyProperty.RegisterAttached("MinHeight", typeof(double), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("MinWidth", typeof(double), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("BorderThickness", typeof(Thickness), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("Marigin", typeof(Thickness), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("Padding", typeof(Thickness), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("Effect", typeof(Effect), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("MouseOverBorderThickness", typeof(Thickness), typeof(ListItemManager));
        #endregion

        #region MouseOverMarigin
        public static Thickness GetMouseOverMarigin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MouseOverMariginProperty);
        }

        public static void SetMouseOverMarigin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MouseOverMariginProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverMarigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverMariginProperty =
            DependencyProperty.RegisterAttached("MouseOverMarigin", typeof(Thickness), typeof(ListItemManager));
        #endregion

        #region MouseOverPadding
        public static Thickness GetMouseOverPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MouseOverPaddingProperty);
        }

        public static void SetMouseOverPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MouseOverPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverPaddingProperty =
            DependencyProperty.RegisterAttached("MouseOverPadding", typeof(Thickness), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("MouseOverForeground", typeof(Brush), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("MouseOverFontSize", typeof(double), typeof(ListItemManager), new PropertyMetadata(DependencyConst.FontSize));
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
            DependencyProperty.RegisterAttached("MouseOverFontWeight", typeof(FontWeight), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("MouseOverEffect", typeof(Effect), typeof(ListItemManager));
        #endregion


        #region ActivateSelectedStyle
        public static bool GetActivateSelectedStyle(DependencyObject obj)
        {
            return (bool)obj.GetValue(ActivateSelectedStyleProperty);
        }

        public static void SetActivateSelectedStyle(DependencyObject obj, bool value)
        {
            obj.SetValue(ActivateSelectedStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for ActivateSelectedStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActivateSelectedStyleProperty =
            DependencyProperty.RegisterAttached("ActivateSelectedStyle", typeof(bool), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("SelectedEffect", typeof(Effect), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("SelectedBackground", typeof(Brush), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("SelectedBorderBrush", typeof(Brush), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("SelectedBorderThickness", typeof(Thickness), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("SelectedForeground", typeof(Brush), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("SelectedFontSize", typeof(double), typeof(ListItemManager), new PropertyMetadata(DependencyConst.FontSize));
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
            DependencyProperty.RegisterAttached("SelectedFontWeight", typeof(FontWeight), typeof(ListItemManager));
        #endregion

        #region SelectedMarigin
        public static Thickness GetSelectedMarigin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SelectedMariginProperty);
        }

        public static void SetSelectedMarigin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SelectedMariginProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedMarigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMariginProperty =
            DependencyProperty.RegisterAttached("SelectedMarigin", typeof(Thickness), typeof(ListItemManager));
        #endregion

        #region SelectedPadding
        public static Thickness GetSelectedPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SelectedPaddingProperty);
        }

        public static void SetSelectedPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SelectedPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPaddingProperty =
            DependencyProperty.RegisterAttached("SelectedPadding", typeof(Thickness), typeof(ListItemManager));
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
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ListItemManager));
        #endregion

        #region IsClip
        public static bool GetIsClip(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsClipProperty);
        }

        public static void SetIsClip(DependencyObject obj, bool value)
        {
            obj.SetValue(IsClipProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClipProperty =
            DependencyProperty.RegisterAttached("IsClip", typeof(bool), typeof(ListItemManager));
        #endregion

    }
}
