using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LeeTeke.WpfControl
{
    public class AnimationHelper
    {
        /// <summary>
        /// 默认动画
        /// </summary>
        public static IEasingFunction DefaultEasingFunction { get; set; } = new CubicEase() { EasingMode = EasingMode.EaseOut };
        /// <summary>
        /// 默认事件
        /// </summary>
        public static Duration DefaultDuration { get; set; } = new Duration(TimeSpan.FromMilliseconds(300));

        public static Point DefaultRenderTransformOrigin { get; set; } = new Point(0.5, 0.5);

        public static Storyboard GetStoryboard(UIElement element, AnimationMode mode)
        {

            return GetStoryboard(element, mode, DefaultEasingFunction, DefaultDuration, DefaultRenderTransformOrigin);
        }

        public static Storyboard GetStoryboard(UIElement element, AnimationMode mode, IEasingFunction easingFunction)
        {
            return GetStoryboard(element, mode, easingFunction, DefaultDuration, DefaultRenderTransformOrigin);
        }

        public static Storyboard GetStoryboard(UIElement element, AnimationMode mode, IEasingFunction easingFunction, Duration duration)
        {
            return GetStoryboard(element, mode, easingFunction, duration, DefaultRenderTransformOrigin);

        }
        public static Storyboard GetStoryboard(UIElement element, AnimationMode mode, IEasingFunction easingFunction, Duration duration, Point RenderTransformOrigin)
        {
            element.RenderTransformOrigin = RenderTransformOrigin;
            var group = new TransformGroup();
            group.Children.Add(new TranslateTransform());
            group.Children.Add(new ScaleTransform());
            element.RenderTransform = group;

            var grade = GetGrade(mode);
            Storyboard storyboard = new Storyboard() { FillBehavior = FillBehavior.Stop };
            if (mode.HasFlag(AnimationMode.Fade))
            {
                mode = mode & ~AnimationMode.FadeOut;
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = 1 * (mode.HasFlag(AnimationMode.Grabe_NoFade) ? 0 : grade),
                    To = 1,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath(UIElement.OpacityProperty));
            }

            if (mode.HasFlag(AnimationMode.FadeOut))
            {
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = 1,
                    To = 1 * (mode.HasFlag(AnimationMode.Grabe_NoFade) ? 0 : grade),
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath(UIElement.OpacityProperty));
            }

            if (mode.HasFlag(AnimationMode.FromTop))
            {
                mode = mode & ~AnimationMode.ToTop & ~AnimationMode.FromBottom & ~AnimationMode.ToBottom;
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = -(element.RenderSize.Height - (element.RenderSize.Height * grade)),
                    To = 0,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[0].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.YProperty }));
            }

            if (mode.HasFlag(AnimationMode.ToTop))
            {
                mode = mode & ~AnimationMode.FromBottom & ~AnimationMode.ToBottom;
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = 0,
                    To = -(element.RenderSize.Height - (element.RenderSize.Height * grade)),
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[0].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.YProperty }));
            }

            if (mode.HasFlag(AnimationMode.FromBottom))
            {
                mode = mode & ~AnimationMode.ToBottom;
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = element.RenderSize.Height - (element.RenderSize.Height * grade),
                    To = 0,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[0].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.YProperty }));
            }

            if (mode.HasFlag(AnimationMode.ToBottom))
            {

                DoubleAnimation da = new DoubleAnimation()
                {
                    From = 0,
                    To = element.RenderSize.Height - (element.RenderSize.Height * grade),
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[0].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.YProperty }));
            }

            if (mode.HasFlag(AnimationMode.FromLeft))
            {
                mode = mode & ~AnimationMode.ToLeft & ~AnimationMode.FromRight & ~AnimationMode.ToRight;
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = -(element.RenderSize.Width - (element.RenderSize.Width * grade)),
                    To = 0,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[0].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.XProperty }));
            }

            if (mode.HasFlag(AnimationMode.ToLeft))
            {
                mode = mode & ~AnimationMode.FromRight & ~AnimationMode.ToRight;
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = 0,
                    To = -(element.RenderSize.Width - (element.RenderSize.Width * grade)),
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[0].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.XProperty }));
            }

            if (mode.HasFlag(AnimationMode.FromRight))
            {
                mode = mode & ~AnimationMode.ToRight;
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = element.RenderSize.Width - (element.RenderSize.Width * grade),
                    To = 0,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[0].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.XProperty }));
            }

            if (mode.HasFlag(AnimationMode.ToRight))
            {
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = 0,
                    To = element.RenderSize.Width - (element.RenderSize.Width * grade),
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[0].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.XProperty }));
            }


            if (mode.HasFlag(AnimationMode.Expansion))
            {
                mode = mode & ~AnimationMode.Shrink;
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = 1 * grade,
                    To = 1,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[1].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, ScaleTransform.ScaleXProperty }));

                DoubleAnimation db = new DoubleAnimation()
                {
                    From = 1 * grade,
                    To = 1,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(db);
                Storyboard.SetTarget(db, element);
                Storyboard.SetTargetProperty(db, new PropertyPath("(0).(1)[1].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, ScaleTransform.ScaleYProperty }));
            }

            if (mode.HasFlag(AnimationMode.Shrink))
            {
                DoubleAnimation da = new DoubleAnimation()
                {
                    From = 1,
                    To = 1 * grade,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(da);
                Storyboard.SetTarget(da, element);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)[1].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, ScaleTransform.ScaleXProperty }));

                DoubleAnimation db = new DoubleAnimation()
                {
                    From = 1,
                    To = 1 * grade,
                    EasingFunction = easingFunction,
                    Duration = duration,
                };

                storyboard.Children.Add(db);
                Storyboard.SetTarget(db, element);
                Storyboard.SetTargetProperty(db, new PropertyPath("(0).(1)[1].(2)", new DependencyProperty[] { UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, ScaleTransform.ScaleYProperty }));
            }

            return storyboard;
        }

        private static double GetGrade(AnimationMode mode)
        {

            if (mode.HasFlag(AnimationMode.Grade_90))
                return 0.9;
            if (mode.HasFlag(AnimationMode.Grade_80))
                return 0.8;
            if (mode.HasFlag(AnimationMode.Grade_70))
                return 0.7;
            if (mode.HasFlag(AnimationMode.Grade_60))
                return 0.6;
            if (mode.HasFlag(AnimationMode.Grade_50))
                return 0.5;
            if (mode.HasFlag(AnimationMode.Grade_40))
                return 0.4;
            if (mode.HasFlag(AnimationMode.Grade_30))
                return 0.3;
            if (mode.HasFlag(AnimationMode.Grade_20))
                return 0.2;
            if (mode.HasFlag(AnimationMode.Grade_10))
                return 0.1;

            return 0.0;

        }

    }
}
