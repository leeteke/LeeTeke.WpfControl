using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ScrollViewerManager
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
            DependencyProperty.RegisterAttached("TrackBrush", typeof(Brush), typeof(ScrollViewerManager));
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
            DependencyProperty.RegisterAttached("ThumbBrush", typeof(Brush), typeof(ScrollViewerManager));
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
            DependencyProperty.RegisterAttached("ScrollBarCornerRadius", typeof(CornerRadius), typeof(ScrollViewerManager));

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
            DependencyProperty.RegisterAttached("ScrollBarShadow", typeof(DropShadowEffect), typeof(ScrollViewerManager));




        #endregion

        #region SlideEnabled
        public static bool GetSlideEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(SlideEnabledProperty);
        }

        public static void SetSlideEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(SlideEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for SlideEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SlideEnabledProperty =
            DependencyProperty.RegisterAttached("SlideEnabled", typeof(bool), typeof(ScrollViewerManager), new PropertyMetadata(OnSlideEnabledChanged));

        private static void OnSlideEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer scroll && e.NewValue is bool v)
            {
                if (v)
                {

                    scroll.PreviewMouseWheel += Scroll_PreviewMouseWheel;
                }
                else
                {
                    scroll.PreviewMouseWheel -= Scroll_PreviewMouseWheel;
                }
            }
        }

        private static void Scroll_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            var mm = GetSlideMonitorMode(scrollViewer);
            if (mm != ScrollViewerSlideMonitorMode.Both)
            {
                var ps = Helper.FindVisualParent<ScrollViewer>(e.OriginalSource as DependencyObject);
                if (ps != null && ps != scrollViewer && CanScoolViewerHandled(mm, ps, e.Delta))
                {
                    return;
                }
            }

            e.Handled = true;
            if (scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                ScrollVerticalOffsetAdd(scrollViewer, e.Delta);
            else if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
                ScrollHorizontalOffsetAdd(scrollViewer, e.Delta);
            else if (scrollViewer.VerticalScrollBarVisibility != ScrollBarVisibility.Disabled)
                ScrollVerticalOffsetAdd(scrollViewer, e.Delta);
            else if (scrollViewer.HorizontalScrollBarVisibility != ScrollBarVisibility.Disabled)
                ScrollHorizontalOffsetAdd(scrollViewer, e.Delta);
            else
                return;

        }

        private static bool CanScoolViewerHandled(ScrollViewerSlideMonitorMode mode, ScrollViewer scrollViewer, int offset)
        {
            try
            {

                if (mode != ScrollViewerSlideMonitorMode.OnlyHorizontal && scrollViewer.ScrollableHeight > 0 && scrollViewer.VerticalScrollBarVisibility != ScrollBarVisibility.Disabled)
                {
                    if (offset > 0 ? scrollViewer.VerticalOffset == 0 : scrollViewer.ScrollableHeight == scrollViewer.VerticalOffset)
                        return false;
                    return true;
                }
                else if (mode != ScrollViewerSlideMonitorMode.OnlyVertical && scrollViewer.ScrollableWidth > 0 && scrollViewer.HorizontalScrollBarVisibility != ScrollBarVisibility.Disabled)
                {
                    if (offset > 0 ? scrollViewer.HorizontalOffset == 0 : scrollViewer.ScrollableWidth == scrollViewer.HorizontalOffset)
                        return false;
                    return true;
                }
                else
                    return false;

            }
            catch
            {

                return false;
            }
        }

        #endregion

        #region SlideMonitorMode
        public static ScrollViewerSlideMonitorMode GetSlideMonitorMode(DependencyObject obj)
        {
            return (ScrollViewerSlideMonitorMode)obj.GetValue(SlideMonitorModeProperty);
        }

        public static void SetSlideMonitorMode(DependencyObject obj, ScrollViewerSlideMonitorMode value)
        {
            obj.SetValue(SlideMonitorModeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SlideMonitorMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SlideMonitorModeProperty =
            DependencyProperty.RegisterAttached("SlideMonitorMode", typeof(ScrollViewerSlideMonitorMode), typeof(ScrollViewerManager), new PropertyMetadata(ScrollViewerSlideMonitorMode.Auto));
        #endregion

        #region SlideEasingFunction
        public static IEasingFunction GetSlideEasingFunction(DependencyObject obj)
        {
            return (IEasingFunction)obj.GetValue(SlideEasingFunctionProperty);
        }

        public static void SetSlideEasingFunction(DependencyObject obj, IEasingFunction value)
        {
            obj.SetValue(SlideEasingFunctionProperty, value);
        }

        // Using a DependencyProperty as the backing store for SlideEasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SlideEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SlideEasingFunction", typeof(IEasingFunction), typeof(ScrollViewerManager), new PropertyMetadata(new CubicEase { EasingMode = EasingMode.EaseOut }));
        #endregion

        #region SlideDuration
        public static Duration GetSlideDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(SlideDurationProperty);
        }

        public static void SetSlideDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(SlideDurationProperty, value);
        }

        // Using a DependencyProperty as the backing store for SlideDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SlideDurationProperty =
            DependencyProperty.RegisterAttached("SlideDuration", typeof(Duration), typeof(ScrollViewerManager), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300))));
        #endregion


    


        #region TopContent
        public static object GetTopContent(DependencyObject obj)
        {
            return (object)obj.GetValue(TopContentProperty);
        }

        public static void SetTopContent(DependencyObject obj, object value)
        {
            obj.SetValue(TopContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for TopContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopContentProperty =
            DependencyProperty.RegisterAttached("TopContent", typeof(object), typeof(ScrollViewerManager));
        #endregion


        #region BottomContent
        public static object GetBottomContent(DependencyObject obj)
        {
            return (object)obj.GetValue(BottomContentProperty);
        }

        public static void SetBottomContent(DependencyObject obj, object value)
        {
            obj.SetValue(BottomContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for BottomContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.RegisterAttached("BottomContent", typeof(object), typeof(ScrollViewerManager));
        #endregion

        #region LeftContent
        public static object GetLeftContent(DependencyObject obj)
        {
            return (object)obj.GetValue(LeftContentProperty);
        }

        public static void SetLeftContent(DependencyObject obj, object value)
        {
            obj.SetValue(LeftContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for LeftContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftContentProperty =
            DependencyProperty.RegisterAttached("LeftContent", typeof(object), typeof(ScrollViewerManager));
        #endregion

        #region RightContent
        public static object GetRightContent(DependencyObject obj)
        {
            return (object)obj.GetValue(RightContentProperty);
        }

        public static void SetRightContent(DependencyObject obj, object value)
        {
            obj.SetValue(RightContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for RightContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.RegisterAttached("RightContent", typeof(object), typeof(ScrollViewerManager));
        #endregion


        #region 公共

        /// <summary>
        /// 垂直滚动条增加
        /// </summary>
        /// <param name="scrollViewr"></param>
        /// <param name="data"></param>
        public static void ScrollVerticalOffsetAdd(ScrollViewer scrollViewr, double data)
        {
            if (!GetScrollRunning(scrollViewr))
            {
                SetTotalVerticalOffset(scrollViewr, scrollViewr.VerticalOffset);
                SetCurrentVerticalOffset(scrollViewr, scrollViewr.VerticalOffset);
            }
            var totalValue = Math.Min(Math.Max(0, GetTotalVerticalOffset(scrollViewr) - data), scrollViewr.ScrollableHeight);
            SetTotalVerticalOffset(scrollViewr, totalValue);
            ScrollToVerticalOffset(scrollViewr, totalValue);
        }

        /// <summary>
        /// 垂直滚动条滚动到
        /// </summary>
        /// <param name="scrollViewr"></param>
        /// <param name="offset"></param>
        public static void ScrollToVerticalOffset(ScrollViewer scrollViewr, double offset)
        {
            if (!GetSlideEnabled(scrollViewr))
            {
                SetCurrentVerticalOffset(scrollViewr, offset);
                return;
            }
            var animation = new DoubleAnimation(offset, GetSlideDuration(scrollViewr))
            {
                EasingFunction = GetSlideEasingFunction(scrollViewr),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, e1) =>
            {
                SetCurrentVerticalOffset(scrollViewr, offset);
                SetScrollRunning(scrollViewr, false);
            };
            SetScrollRunning(scrollViewr, true);

            scrollViewr.BeginAnimation(CurrentVerticalOffsetProperty, animation, HandoffBehavior.Compose);
        }

        /// <summary>
        /// 水平滚动条增加
        /// </summary>
        /// <param name="scrollViewr"></param>
        /// <param name="data"></param>
        public static void ScrollHorizontalOffsetAdd(ScrollViewer scrollViewr, double data)
        {
            if (!GetScrollRunning(scrollViewr))
            {
                SetTotalHorizontalOffset(scrollViewr, scrollViewr.HorizontalOffset);
                SetCurrentHorizontalOffset(scrollViewr, scrollViewr.HorizontalOffset);
            }
            var totalValue = Math.Min(Math.Max(0, GetTotalHorizontalOffset(scrollViewr) - data), scrollViewr.ScrollableWidth);
            SetTotalHorizontalOffset(scrollViewr, totalValue);
            ScrollToHorizontalOffset(scrollViewr, totalValue);

        }

        /// <summary>
        /// 水平滚动条滚动到
        /// </summary>
        /// <param name="scrollViewr"></param>
        /// <param name="offset"></param>
        public static void ScrollToHorizontalOffset(ScrollViewer scrollViewr, double offset)
        {
            if (!GetSlideEnabled(scrollViewr))
            {
                SetCurrentHorizontalOffset(scrollViewr, offset);
                return;
            }
            var animation = new DoubleAnimation(offset, GetSlideDuration(scrollViewr))
            {
                EasingFunction = GetSlideEasingFunction(scrollViewr),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, e1) =>
            {
                SetCurrentHorizontalOffset(scrollViewr, offset);
                SetScrollRunning(scrollViewr, false);
            };
            SetScrollRunning(scrollViewr, true);

            scrollViewr.BeginAnimation(CurrentHorizontalOffsetProperty, animation, HandoffBehavior.Compose);
        }
        #endregion

        #region 私有



        #region TotalVerticalOffset
        private static double GetTotalVerticalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(TotalVerticalOffsetProperty);
        }

        private static void SetTotalVerticalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(TotalVerticalOffsetProperty, value);
        }

        // Using a DependencyProperty as the backing store for TotalVerticalOffset.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TotalVerticalOffsetProperty =
            DependencyProperty.RegisterAttached("TotalVerticalOffset", typeof(double), typeof(ScrollViewerManager));
        #endregion



        #region TotalHorizontalOffset
        private static double GetTotalHorizontalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(TotalHorizontalOffsetProperty);
        }

        private static void SetTotalHorizontalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(TotalHorizontalOffsetProperty, value);
        }

        // Using a DependencyProperty as the backing store for TotalHorizontalOffset.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TotalHorizontalOffsetProperty =
            DependencyProperty.RegisterAttached("TotalHorizontalOffset", typeof(double), typeof(ScrollViewerManager));
        #endregion


        #region CurrentVerticalOffset
        private static double GetCurrentVerticalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(CurrentVerticalOffsetProperty);
        }

        private static void SetCurrentVerticalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(CurrentVerticalOffsetProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentVerticalOffsetProperty.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty CurrentVerticalOffsetProperty =
           DependencyProperty.RegisterAttached("CurrentVerticalOffset", typeof(double), typeof(ScrollViewerManager), new PropertyMetadata(OnCurrentVerticalOffsetChanged));

        private static void OnCurrentVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer sv && e.NewValue is double v)
            {
                sv.ScrollToVerticalOffset(v);
            }
        }
        #endregion


        #region CurrentHorizontalOffset
        private static double GetCurrentHorizontalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(CurrentHorizontalOffsetProperty);
        }

        private static void SetCurrentHorizontalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(CurrentHorizontalOffsetProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentHorizontalOffset.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty CurrentHorizontalOffsetProperty =
           DependencyProperty.RegisterAttached("CurrentHorizontalOffset", typeof(double), typeof(ScrollViewerManager), new PropertyMetadata(OnCurrentHorizontalOffsetChanged));

        private static void OnCurrentHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer ctl && e.NewValue is double v)
            {
                ctl.ScrollToHorizontalOffset(v);
            }
        }
        #endregion


        #region ScrollRunning
        private static bool GetScrollRunning(DependencyObject obj)
        {
            return (bool)obj.GetValue(ScrollRunningProperty);
        }

        private static void SetScrollRunning(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollRunningProperty, value);
        }

        // Using a DependencyProperty as the backing store for ScrollRunning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollRunningProperty =
            DependencyProperty.RegisterAttached("ScrollRunning", typeof(bool), typeof(ScrollViewerManager));
        #endregion


        #endregion
    }

}
