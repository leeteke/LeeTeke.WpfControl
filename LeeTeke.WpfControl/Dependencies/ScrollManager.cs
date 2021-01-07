using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ScrollManager 
    {
        #region TrackBrush
        public static Brush GetTrackBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TrackBrushProperty);
        }

        public static void SetTrackBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(TrackBrushProperty, value);
        }

        public static readonly DependencyProperty TrackBrushProperty =
            DependencyProperty.RegisterAttached("TrackBrush", typeof(Brush), typeof(ScrollManager));
        #endregion

        #region ThumbBrush
        public static Brush GetThumbBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ThumbBrushProperty);
        }

        public static void SetThumbBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(ThumbBrushProperty, value);
        }

        public static readonly DependencyProperty ThumbBrushProperty =
            DependencyProperty.RegisterAttached("ThumbBrush", typeof(Brush), typeof(ScrollManager));
        #endregion

        #region ScrollBarCornerRadius
        public static CornerRadius GetScrollBarCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(ScrollBarCornerRadiusProperty);
        }

        public static void SetScrollBarCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(ScrollBarCornerRadiusProperty, value);
        }

        public static readonly DependencyProperty ScrollBarCornerRadiusProperty =
            DependencyProperty.RegisterAttached("ScrollBarCornerRadius", typeof(CornerRadius), typeof(ScrollManager));

        #endregion

        #region ScrollBarShadow


        public static DropShadowEffect GetScrollBarShadowr(DependencyObject obj)
        {
            return (DropShadowEffect)obj.GetValue(ScrollBarShadowProperty);
        }

        public static void SetScrollBarShadow(DependencyObject obj, DropShadowEffect value)
        {
            obj.SetValue(ScrollBarShadowProperty, value);
        }

        // Using a DependencyProperty as the backing store for ScrollBarShadowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollBarShadowProperty =
            DependencyProperty.RegisterAttached("ScrollBarShadow", typeof(DropShadowEffect), typeof(ScrollManager));




        #endregion

        #region ScrollViewerHook
        public static bool GetScrollViewerHook(DependencyObject obj)
        {
            return (bool)obj.GetValue(ScrollViewerHookProperty);
        }

        public static void SetScrollViewerHook(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollViewerHookProperty, value);
        }

        public static readonly DependencyProperty ScrollViewerHookProperty =
            DependencyProperty.RegisterAttached("ScrollViewerHook", typeof(bool), typeof(ScrollManager), new PropertyMetadata(OnScrollViewerHookChanged));

        private static void OnScrollViewerHookChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;
            if ((bool)e.NewValue)
            {
                scrollViewer.PreviewMouseWheel += ScrollViewer_PreviewMouseWheel;

            }
            else
            {
                scrollViewer.PreviewMouseWheel -= ScrollViewer_PreviewMouseWheel;
            }
        }

        private static void ScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            var handle = true;

            if (e.Delta > 0)
            {
                if (scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                {
                  
                    scrollViewer.LineUp();
                }
                else if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
                {
                   scrollViewer.LineLeft();
                }
                else if (scrollViewer.VerticalScrollBarVisibility != ScrollBarVisibility.Disabled)
                {
                    scrollViewer.LineUp();
                }
                else if (scrollViewer.HorizontalScrollBarVisibility != ScrollBarVisibility.Disabled)
                    scrollViewer.LineLeft();
                else
                    return;
            }
            else
            {
                if (scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                    scrollViewer.LineDown();
                else if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
                    scrollViewer.LineRight();
                else if (scrollViewer.VerticalScrollBarVisibility != ScrollBarVisibility.Disabled)
                    scrollViewer.LineDown();
                else if (scrollViewer.HorizontalScrollBarVisibility != ScrollBarVisibility.Disabled)
                    scrollViewer.LineRight();
                else
                    return;
            }


            if (handle)
                e.Handled = true;
        }
        #endregion
    }
}
