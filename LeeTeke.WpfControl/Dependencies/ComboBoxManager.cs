using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ComboBoxManager
    {

        #region EscToEmpty
        public static bool GetEscToEmpty(DependencyObject obj)
        {
            return (bool)obj.GetValue(EscToEmptyProperty);
        }

        public static void SetEscToEmpty(DependencyObject obj, bool value)
        {
            obj.SetValue(EscToEmptyProperty, value);
        }

        // Using a DependencyProperty as the backing store for EscToEmpty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EscToEmptyProperty =
            DependencyProperty.RegisterAttached("EscToEmpty", typeof(bool), typeof(ComboBoxManager));



        #endregion

        #region EnterCommand
        public static ICommand GetEnterCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(EnterCommandProperty);
        }

        public static void SetEnterCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(EnterCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnterCommandProperty =
            DependencyProperty.RegisterAttached("EnterCommand", typeof(ICommand), typeof(ComboBoxManager));




        #endregion


        #region PlaceholderVisibility
        public static bool GetPlaceholderVisibility(DependencyObject obj)
        {
            return (bool)obj.GetValue(PlaceholderVisibilityProperty);
        }

        public static void SetPlaceholderVisibility(DependencyObject obj, bool value)
        {
            obj.SetValue(PlaceholderVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for PlaceholderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderVisibilityProperty =
            DependencyProperty.RegisterAttached("PlaceholderVisibility", typeof(bool), typeof(ComboBoxManager));
        #endregion



        #region Placeholder
        public static object GetPlaceholder(DependencyObject obj)
        {
            return (object)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, object value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(object), typeof(ComboBoxManager));
        #endregion

        #region ItemHeight
        public static double GetItemHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(ItemHeightProperty);
        }

        public static void SetItemHeight(DependencyObject obj, double value)
        {
            obj.SetValue(ItemHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for ItemHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.RegisterAttached("ItemHeight", typeof(double), typeof(ComboBoxManager));
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
            DependencyProperty.RegisterAttached("MarkShow", typeof(bool), typeof(ComboBoxManager));
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
            DependencyProperty.RegisterAttached("MarkSite", typeof(Dock), typeof(ComboBoxManager));




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
            DependencyProperty.RegisterAttached("MarkSize", typeof(double), typeof(ComboBoxManager));
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
            DependencyProperty.RegisterAttached("MarkBrush", typeof(Brush), typeof(ComboBoxManager));
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
            DependencyProperty.RegisterAttached("MarkMargin", typeof(Thickness), typeof(ComboBoxManager));
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
            DependencyProperty.RegisterAttached("MarkCornerRadius", typeof(CornerRadius), typeof(ComboBoxManager));
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
            DependencyProperty.RegisterAttached("MarkMouseOverBrush", typeof(Brush), typeof(ComboBoxManager));
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
            DependencyProperty.RegisterAttached("MarkSelectedBrush", typeof(Brush), typeof(ComboBoxManager));
        #endregion

        #region IsDropCenterOnControl
        public static bool GetIsDropCenterOnControl(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDropCenterOnControlProperty);
        }

        public static void SetIsDropCenterOnControl(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDropCenterOnControlProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsDropCenterOnControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropCenterOnControlProperty =
            DependencyProperty.RegisterAttached("IsDropCenterOnControl", typeof(bool), typeof(ComboBoxManager));
        #endregion

        #region DropBackground
        public static Brush GetDropBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(DropBackgroundProperty);
        }

        public static void SetDropBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(DropBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropBackgroundProperty =
            DependencyProperty.RegisterAttached("DropBackground", typeof(Brush), typeof(ComboBoxManager));
        #endregion

        #region DropEffect
        public static Effect GetDropEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(DropEffectProperty);
        }

        public static void SetDropEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(DropEffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropEffectProperty =
            DependencyProperty.RegisterAttached("DropEffect", typeof(Effect), typeof(ComboBoxManager));
        #endregion

        #region DropBorderBrush
        public static Brush GetDropBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(DropBorderBrushProperty);
        }

        public static void SetDropBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(DropBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropBorderBrushProperty =
            DependencyProperty.RegisterAttached("DropBorderBrush", typeof(Brush), typeof(ComboBoxManager));
        #endregion

        #region DropBorderThinckness
        public static Thickness GetDropBorderThinckness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(DropBorderThincknessProperty);
        }

        public static void SetDropBorderThinckness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(DropBorderThincknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropBorderThinckness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropBorderThincknessProperty =
            DependencyProperty.RegisterAttached("DropBorderThinckness", typeof(Thickness), typeof(ComboBoxManager));
        #endregion

        #region DropCornerRadius
        public static CornerRadius GetDropCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(DropCornerRadiusProperty);
        }

        public static void SetDropCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(DropCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropCornerRadiusProperty =
            DependencyProperty.RegisterAttached("DropCornerRadius", typeof(CornerRadius), typeof(ComboBoxManager));
        #endregion

        #region DropMargin
        public static Thickness GetDropMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(DropMarginProperty);
        }

        public static void SetDropMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(DropMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropMarginProperty =
            DependencyProperty.RegisterAttached("DropMargin", typeof(Thickness), typeof(ComboBoxManager));
        #endregion

        #region DropPadding
        public static Thickness GetDropPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(DropPaddingProperty);
        }

        public static void SetDropPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(DropPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropPaddingProperty =
            DependencyProperty.RegisterAttached("DropPadding", typeof(Thickness), typeof(ComboBoxManager));
        #endregion
        #region DropIsClip
        public static bool GetDropIsClip(DependencyObject obj)
        {
            return (bool)obj.GetValue(DropIsClipProperty);
        }

        public static void SetDropIsClip(DependencyObject obj, bool value)
        {
            obj.SetValue(DropIsClipProperty, value);
        }

        // Using a DependencyProperty as the backing store for DropIsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropIsClipProperty =
            DependencyProperty.RegisterAttached("DropIsClip", typeof(bool), typeof(ComboBoxManager));
        #endregion



    }
}
