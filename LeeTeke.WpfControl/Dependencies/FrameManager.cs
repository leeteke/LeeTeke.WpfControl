using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LeeTeke.WpfControl.Dependencies
{
    public class FrameManager
    {


        #region DisableNavigation
        public static bool GetDisableNavigation(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisableNavigationProperty);
        }

        public static void SetDisableNavigation(DependencyObject obj, bool value)
        {
            obj.SetValue(DisableNavigationProperty, value);
        }

        // Using a DependencyProperty as the backing store for DisableNavigation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisableNavigationProperty =
            DependencyProperty.RegisterAttached("DisableNavigation", typeof(bool), typeof(FrameManager), new PropertyMetadata(DisableNavigationChanged));

        private static void DisableNavigationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Frame frame)
            {
                if (e.NewValue is bool disable && disable)
                {
                    frame.Navigated += Frame_Navigated;
                }
                else
                {
                    frame.Navigated -= Frame_Navigated;
                }
            }
        }

        private static void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ((Frame)sender).NavigationService.RemoveBackEntry();
        }
        #endregion


        #region SwitchAnimationEnabled
        public static bool GetSwitchAnimationEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(SwitchAnimationEnabledProperty);
        }

        public static void SetSwitchAnimationEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(SwitchAnimationEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for SwitchAnimationEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SwitchAnimationEnabledProperty =
            DependencyProperty.RegisterAttached("SwitchAnimationEnabled", typeof(bool), typeof(FrameManager), new PropertyMetadata(OnSwitchAnimationEnabledChanged));

        private static void OnSwitchAnimationEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Frame frame)
            {
                frame.Navigating -= Frame_Navigating;
                if ((bool)e.NewValue == true)
                {
                    frame.Navigating += Frame_Navigating;
                }

            }
        }

        private static void Frame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (sender is Frame frame&& e.Content is UIElement element)
            {
                var func = GetAnimationCustom(frame);
                if (func != null)
                {
                    func(element)?.Begin();
                }
                else
                {
                    var sb = AnimationHelper.GetStoryboard(element, GetAnimationMode(frame), GetAnimationEasingFunction(frame), GetAnimationDuration(frame),GetAnimationRenderTransformOrigin(frame));
                    sb.Begin();
                }
            }
        }
        #endregion


        #region AnimationMode
        public static AnimationMode GetAnimationMode(DependencyObject obj)
        {
            return (AnimationMode)obj.GetValue(AnimationModeProperty);
        }

        public static void SetAnimationMode(DependencyObject obj, AnimationMode value)
        {
            obj.SetValue(AnimationModeProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnimationMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationModeProperty =
            DependencyProperty.RegisterAttached("AnimationMode", typeof(AnimationMode), typeof(FrameManager),new PropertyMetadata(AnimationMode.Fade| AnimationMode.FromBottom| AnimationMode.Grade_90));
        #endregion


        #region AnimationEasingFunction
        public static IEasingFunction GetAnimationEasingFunction(DependencyObject obj)
        {
            return (IEasingFunction)obj.GetValue(AnimationEasingFunctionProperty);
        }

        public static void SetAnimationEasingFunction(DependencyObject obj, IEasingFunction value)
        {
            obj.SetValue(AnimationEasingFunctionProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnimationEasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationEasingFunctionProperty =
            DependencyProperty.RegisterAttached("AnimationEasingFunction", typeof(IEasingFunction), typeof(FrameManager),new PropertyMetadata(AnimationHelper.DefaultEasingFunction));
        #endregion


        #region AnimationDuration
        public static Duration GetAnimationDuration(DependencyObject obj)
        {
            return (Duration)obj.GetValue(AnimationDurationProperty);
        }

        public static void SetAnimationDuration(DependencyObject obj, Duration value)
        {
            obj.SetValue(AnimationDurationProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.RegisterAttached("AnimationDuration", typeof(Duration), typeof(FrameManager),new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(120))));
        #endregion


        #region AnimationRenderTransformOrigin
        public static Point GetAnimationRenderTransformOrigin(DependencyObject obj)
        {
            return (Point)obj.GetValue(AnimationRenderTransformOriginProperty);
        }

        public static void SetAnimationRenderTransformOrigin(DependencyObject obj, Point value)
        {
            obj.SetValue(AnimationRenderTransformOriginProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnimationRenderTransformOrigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationRenderTransformOriginProperty =
            DependencyProperty.RegisterAttached("AnimationRenderTransformOrigin", typeof(Point), typeof(FrameManager),new PropertyMetadata(AnimationHelper.DefaultRenderTransformOrigin));
        #endregion


        #region AnimationCustom
        public static Func<UIElement, Storyboard> GetAnimationCustom(DependencyObject obj)
        {
            return (Func<UIElement, Storyboard>)obj.GetValue(AnimationCustomProperty);
        }

        public static void SetAnimationCustom(DependencyObject obj, Func<UIElement, Storyboard> value)
        {
            obj.SetValue(AnimationCustomProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnimationCustom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationCustomProperty =
            DependencyProperty.RegisterAttached("AnimationCustom", typeof(Func<UIElement, Storyboard>), typeof(FrameManager));
        #endregion


  
    }
}
