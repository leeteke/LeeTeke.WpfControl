using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        #region ShowPreviewText
        public static bool GetShowPreviewText(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowPreviewTextProperty);
        }

        public static void SetShowPreviewText(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowPreviewTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowPreviewText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowPreviewTextProperty =
            DependencyProperty.RegisterAttached("ShowPreviewText", typeof(bool), typeof(ComboBoxManager));
        #endregion

        #region PreviewText
        public static string GetPreviewText(DependencyObject obj)
        {
            return (string)obj.GetValue(PreviewTextProperty);
        }

        public static void SetPreviewText(DependencyObject obj, string value)
        {
            obj.SetValue(PreviewTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewTextProperty =
            DependencyProperty.RegisterAttached("PreviewText", typeof(string), typeof(ComboBoxManager));
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


        #region BanQuick
        public static bool GetBanQuick(DependencyObject obj)
        {
            return (bool)obj.GetValue(BanQuickProperty);
        }

        public static void SetBanQuick(DependencyObject obj, bool value)
        {
            obj.SetValue(BanQuickProperty, value);
        }

        // Using a DependencyProperty as the backing store for BanQuick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BanQuickProperty =
            DependencyProperty.RegisterAttached("BanQuick", typeof(bool), typeof(ComboBoxManager), new PropertyMetadata(BanQuickChanged));

        private static void BanQuickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ComboBox box)
            {
                if (e.NewValue is bool _value && _value)
                {
                    box.PreviewKeyDown += Box_PreviewKeyDown;
                    box.PreviewMouseWheel += Box_PreviewMouseWheel; 
                }
                else
                {
                    box.PreviewKeyDown -= Box_PreviewKeyDown;
                    box.PreviewMouseWheel -= Box_PreviewMouseWheel;
                }
            }
        }

        private static void Box_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private static void Box_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Right:
                case Key.Up:
                case Key.Down:
                        e.Handled = true;
                    return;
                default:
                    break;
            }
        }
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

    }
}
