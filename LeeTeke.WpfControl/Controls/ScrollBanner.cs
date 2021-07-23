using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeeTeke.WpfControl.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LeeTeke.WpfControl.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LeeTeke.WpfControl.Controls;assembly=LeeTeke.WpfControl.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:ScrollBanner/>
    ///
    /// </summary>
    public class ScrollBanner : ContentControl
    {


        static ScrollBanner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollBanner), new FrameworkPropertyMetadata(typeof(ScrollBanner)));
        }

        private Storyboard _scrollingSB;
        public ScrollBanner()
        {
            SizeChanged += ScrollBanner_SizeChanged;

        }



        #region override
        protected override void OnContentChanged(object oldContent, object newContent)
        {

            if (newContent != null && newContent is not FrameworkElement)
            {
                this.Content = new TextBlock() { Text = newContent.ToString() };
                return;
            }

            base.OnContentChanged(oldContent, newContent);

            if (IsEnabled == true && Visibility != Visibility.Visible && newContent != null)
            {
                Visibility = Visibility.Visible;
                return;
            }

            ScrollingAnimationWork();
        }



        #endregion

        #region 依赖属性


        #region IsScrolling
        /// <summary>
        /// 是否正在滚动
        /// </summary>
        public bool IsScrolling
        {
            get { return (bool)GetValue(IsScrollingProperty); }
            set { SetValue(IsScrollingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsScrolling.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsScrollingProperty =
            DependencyProperty.Register("IsScrolling", typeof(bool), typeof(ScrollBanner));
        #endregion

        #region AutoScrolling
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool AutoScrolling
        {
            get { return (bool)GetValue(AutoScrollingProperty); }
            set { SetValue(AutoScrollingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoScrolling.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoScrollingProperty =
            DependencyProperty.Register("AutoScrolling", typeof(bool), typeof(ScrollBanner));
        #endregion

        #region Orientation
        /// <summary>
        /// Orientation
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ScrollBanner));
        #endregion

        #region CornerRadius
        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ScrollBanner));
        #endregion

        #region IsEnabled
        /// <summary>
        /// 请添加描述
        /// </summary>
        public new bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(ScrollBanner), new PropertyMetadata(true, EnabledChanged));

        private static void EnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollBanner banner)
            {
                if (e.NewValue is bool @bool)
                {
                    banner.Visibility = @bool ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
        #endregion

        #endregion

        #region private


        private void ScrollBanner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollingAnimationWork();
        }

        private void ScrollingAnimationWork()
        {
            ///如果等于空
            if (Content == null || Content is not FrameworkElement || this.ActualHeight == 0 || this.ActualWidth == 0 || !IsEnabled)
            {
                _scrollingSB?.Stop();
                return;
            }
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    HScrollingAnimationWork();
                    break;
                case Orientation.Vertical:
                    VScrollingAnimationWork();
                    break;
                default:
                    break;
            }
        }

        private void HScrollingAnimationWork()
        {
            ///停止现在的动画
            _scrollingSB?.Stop();

            if (Content is FrameworkElement element)
            {
                element.SizeChanged += (es, ex) => ScrollingAnimationWork();
                ///是否自动判断
                if (AutoScrolling)
                {
                    if (element.ActualWidth < this.ActualWidth)
                        return;
                }

                element.RenderTransformOrigin = new Point(0, 0.5);
                element.RenderTransform = new TranslateTransform() { X = this.ActualWidth + 5 };
                _scrollingSB = new Storyboard()
                {
                    FillBehavior = FillBehavior.Stop,
                    RepeatBehavior = RepeatBehavior.Forever
                };
                _scrollingSB.Children.Add(new DoubleAnimation()
                {
                    To = -(element.ActualWidth + 5),
                    Duration = TimeSpan.FromSeconds(10 * this.ActualWidth / 600)
                });
                Storyboard.SetTarget(_scrollingSB, element);
                Storyboard.SetTargetProperty(_scrollingSB, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.XProperty }));

                _scrollingSB.Begin();
            }
        }

        private void VScrollingAnimationWork()
        {
            ///停止现在的动画
            _scrollingSB?.Stop();
            if (Content is FrameworkElement element)
            {
                element.SizeChanged += (es, ex) => ScrollingAnimationWork();
                ///是否自动判断
                if (AutoScrolling)
                {
                    if (element.ActualHeight < this.ActualHeight)
                        return;
                }

                element.RenderTransformOrigin = new Point(0.5, 0);
                element.RenderTransform = new TranslateTransform() { Y = this.ActualHeight + 5 };
                _scrollingSB = new Storyboard()
                {
                    FillBehavior = FillBehavior.Stop,
                    RepeatBehavior = RepeatBehavior.Forever
                };
                _scrollingSB.Children.Add(new DoubleAnimation()
                {
                    To = -(element.ActualHeight + 5),
                    Duration = TimeSpan.FromSeconds(4 * this.ActualHeight / 35)
                });
                Storyboard.SetTarget(_scrollingSB, element);
                Storyboard.SetTargetProperty(_scrollingSB, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.YProperty }));

                _scrollingSB.Begin();
            }
        }
        #endregion

    }
}
