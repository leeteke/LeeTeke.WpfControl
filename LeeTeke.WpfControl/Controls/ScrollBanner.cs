

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
    [TemplatePart(Name = ElementContent, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ElementCloseButton, Type = typeof(Button))]
    public class ScrollBanner : System.Windows.Controls.Control
    {


        static ScrollBanner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollBanner), new FrameworkPropertyMetadata(typeof(ScrollBanner)));
        }

        #region consts

        private const string ElementContent = "PART_Content";
        private const string ElementCloseButton = "PART_CloseButton";
        #endregion

        private Storyboard? _scrollingSB;
        private Button? _closeButton;
        private ContentPresenter? _content;

        public ScrollBanner()
        {
            SizeChanged += ScrollBanner_SizeChanged;
            IsEnabledChanged += ScrollBanner_IsEnabledChanged;
            IsVisibleChanged += ScrollBanner_IsVisibleChanged;

        }



        #region override

        public override void OnApplyTemplate()
        {
            if (_content != null)
                _content.SizeChanged -= Presenter_SizeChanged;
            if (_closeButton != null)
                _closeButton.Click -= CloseBtn_Click;

            base.OnApplyTemplate();

            _content = GetTemplateChild(ElementContent) as ContentPresenter;
            _closeButton = GetTemplateChild(ElementCloseButton) as Button;



            if (_content != null)
                _content.SizeChanged += Presenter_SizeChanged;
            if (_closeButton != null)
                _closeButton.Click += CloseBtn_Click;


        }

        //protected override void OnContentChanged(object oldContent, object newContent)
        //{
        //    base.OnContentChanged(oldContent, newContent);
        //    IsClosed = false;
        //    ScrollingAnimationWork();
        //}


        #endregion

        #region 依赖属性


        #region Content
        /// <summary>
        /// 请添加描述
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ScrollBanner), new PropertyMetadata(OnContentChanged));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollBanner sb && e.NewValue != e.OldValue)
            {
                sb.IsClosed = false;
                sb.ScrollingAnimationWork();
            }
        }
        #endregion



        #region IsClosed
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsClosed
        {
            get { return (bool)GetValue(IsClosedProperty); }
            private set { SetValue(IsClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClosed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClosedProperty =
            DependencyProperty.Register("IsClosed", typeof(bool), typeof(ScrollBanner));
        #endregion

        #region IsScrolling
        /// <summary>
        /// 是否正在滚动
        /// </summary>
        public bool IsScrolling
        {
            get { return (bool)GetValue(IsScrollingProperty); }
            private set { SetValue(IsScrollingProperty, value); }
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
            DependencyProperty.Register("AutoScrolling", typeof(bool), typeof(ScrollBanner), new PropertyMetadata(AutoScrollingChanged));

        private static void AutoScrollingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollBanner scrollBanner)
            {
                scrollBanner.ScrollingAnimationWork();
            }
        }
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

        #region Speed
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Speed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(ScrollBanner), new PropertyMetadata(SpeedChanged));

        private static void SpeedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollBanner scrollBanner)
            {
                scrollBanner._scrollingSB?.SetSpeedRatio((double)e.NewValue);
            }
        }
        #endregion

        #endregion

        #region private


        private void ScrollBanner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollingAnimationWork();
        }


        private void ScrollBanner_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            ScrollingAnimationWork();
        }

        private void ScrollBanner_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            ScrollingAnimationWork();

        }


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            IsClosed = true;
            this.Visibility = Visibility.Collapsed;
        }

        private void Presenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            ScrollingAnimationWork();
        }



        private void ScrollingAnimationWork()
        {

            _scrollingSB?.Stop();
            IsScrolling = false;

            if (_content != null && (Content == null || (Content is string str && string.IsNullOrWhiteSpace(str))))
            {
                IsClosed = true;
                this.Visibility = Visibility.Collapsed;
                return;
            }


            if (Content == null || !IsEnabled || _content == null || IsClosed)
            {
                if (_content != null)
                    _content.RenderTransform = new TranslateTransform() { X = 0, Y = 0 };
                return;
            }
            Visibility = Visibility.Visible;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    if (AutoScrolling && _content.ActualWidth < this.ActualWidth)
                    {
                        _content.RenderTransform = new TranslateTransform() { X = 0, Y = 0 };
                        return;
                    }
                    HScrollingAnimationWork();
                    break;
                case Orientation.Vertical:
                    if (AutoScrolling && _content.ActualHeight < this.ActualHeight)
                    {
                        _content.RenderTransform = new TranslateTransform() { X = 0, Y = 0 };
                        return;
                    }
                    VScrollingAnimationWork();
                    break;
                default:
                    break;
            }
        }

        private void HScrollingAnimationWork()
        {
            if (_content == null)
                return;

            _content.RenderTransformOrigin = new Point(0, 0.5);
            _content.RenderTransform = new TranslateTransform() { X = this.ActualWidth + 5 };
            _scrollingSB = new Storyboard()
            {
                FillBehavior = FillBehavior.Stop,
                RepeatBehavior = RepeatBehavior.Forever
            };
            _scrollingSB.Children.Add(new DoubleAnimation()
            {
                To = -(_content.ActualWidth + 5),
                Duration = TimeSpan.FromSeconds(10 * ((this.ActualWidth + _content.ActualWidth + 5) / 500))
            });
            Storyboard.SetTarget(_scrollingSB, _content);
            Storyboard.SetTargetProperty(_scrollingSB, new PropertyPath("(0).(1)", new DependencyProperty[] { ContentPresenter.RenderTransformProperty, TranslateTransform.XProperty }));

            _scrollingSB.Begin();

            _scrollingSB.SetSpeedRatio(Speed);


            IsScrolling = true;
        }

        private void VScrollingAnimationWork()
        {
            if (_content == null)
                return;

            _content.RenderTransformOrigin = new Point(0.5, 0);
            _content.RenderTransform = new TranslateTransform() { Y = this.ActualHeight + 5 };
            _scrollingSB = new Storyboard()
            {
                FillBehavior = FillBehavior.Stop,
                RepeatBehavior = RepeatBehavior.Forever
            };
            _scrollingSB.Children.Add(new DoubleAnimation()
            {
                To = -(_content.ActualHeight + 5),
                Duration = TimeSpan.FromSeconds(4 * ((_content.ActualHeight + 5) / 20))
            });
            Storyboard.SetTarget(_scrollingSB, _content);
            Storyboard.SetTargetProperty(_scrollingSB, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.YProperty }));

            _scrollingSB.SetSpeedRatio(Speed);
            _scrollingSB.Begin();
        }
    }
    #endregion


}
