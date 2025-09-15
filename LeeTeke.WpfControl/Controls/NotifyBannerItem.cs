using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
    ///     <MyNamespace:NotifyBannerItem/>
    ///
    /// </summary>
    [TemplatePart(Name = ElementCloseButton, Type = typeof(Button))]
    public class NotifyBannerItem : ContentControl
    {

        static NotifyBannerItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotifyBannerItem), new FrameworkPropertyMetadata(typeof(NotifyBannerItem)));
        }

        private const string ElementCloseButton = "PART_CloseButton";

        public NotifySite Site { get; }
        private DispatcherTimer? _timer;
        private bool _closing = false;
        private Button? _closeButton;



        public NotifyBannerItem(NotifyBannerShowData data, NotifySite site)
        {

            Site = site;
            this.Content = data.Content;
            this.DataContext = data.Value;
            if (data.Duration != null)
                this.Duration = (Duration)data.Duration;

            this.Sound = data.Sound;
            this.Status = data.Status;
            this.CanClick = data.CanClick;
            Loaded += NotifyBannerItem_Loaded;
            if (data.Status == NotifyStatus.Callback)
            {
                data.CloseAction = () => Close();
            }

            switch (Site)
            {
                case NotifySite.TopRight:
                case NotifySite.BottomRight:
                    this.RenderTransform = new TranslateTransform(200, 0);
                    break;

                case NotifySite.TopLeft:
                case NotifySite.BottomLeft:
                    this.RenderTransform = new TranslateTransform(-200, 0);
                    break;
                case NotifySite.TopCenter:
                    this.RenderTransform = new TranslateTransform(0, -50);
                    break;
                case NotifySite.BottomCenter:
                    this.RenderTransform = new TranslateTransform(0, 50);
                    break;
                default:
                    break;
            }

            this.MouseEnter += NotifyBannerItem_MouseEnter;
            this.MouseLeave += NotifyBannerItem_MouseLeave;
            this.MouseLeftButtonUp += NotifyBannerItem_MouseLeftButtonUp;

        }

        private void NotifyBannerItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RaiseClicked(this.DataContext);
        }

        private void NotifyBannerItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Status != NotifyStatus.Callback && AutoClose && !_closing && _timer != null)
            {

                _timer.Interval = Duration.TimeSpan;
                _timer.Start();
            }
        }

        private void NotifyBannerItem_MouseEnter(object sender, MouseEventArgs e)
        {
            _timer?.Stop();
        }

        private void NotifyBannerItem_Loaded(object sender, RoutedEventArgs e)
        {
            Show();

        }

        public override void OnApplyTemplate()
        {
            if (_closeButton != null)
            {
                _closeButton.Click -= CloseButton_Clicked;
            }

            base.OnApplyTemplate();

            _closeButton = GetTemplateChild(ElementCloseButton) as Button;
            if (_closeButton != null)
            {
                _closeButton.Click += CloseButton_Clicked;
            }

        }

        private void CloseButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }



        #region 依赖属性


        #region Effect
        /// <summary>
        /// 请添加描述
        /// </summary>
        public new Effect? Effect
        {
            get { return (Effect?)GetValue(EffectProperty); }
            set { SetValue(EffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Effect.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty EffectProperty =
            DependencyProperty.Register("Effect", typeof(Effect), typeof(NotifyBannerItem));
        #endregion


        #region CloseVisibly
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ShowMode CloseVisibly
        {
            get { return (ShowMode)GetValue(CloseVisiblyProperty); }
            set { SetValue(CloseVisiblyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseVisibly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseVisiblyProperty =
            DependencyProperty.Register("CloseVisibly", typeof(ShowMode), typeof(NotifyBannerItem));
        #endregion

        #region CornerRadius
        /// <summary>
        /// 请添加描述
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NotifyBannerItem));
        #endregion


        #region IsClip
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsClip
        {
            get { return (bool)GetValue(IsClipProperty); }
            set { SetValue(IsClipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClipProperty =
            DependencyProperty.Register("IsClip", typeof(bool), typeof(NotifyBannerItem));
        #endregion


        #region Status
        /// <summary>
        /// 请添加描述
        /// </summary>
        public NotifyStatus Status
        {
            get { return (NotifyStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(NotifyStatus), typeof(NotifyBannerItem));
        #endregion

        #region Sound
        /// <summary>
        /// 声音
        /// </summary>
        public Stream? Sound
        {
            get { return (Stream?)GetValue(SoundProperty); }
            set { SetValue(SoundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sound.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SoundProperty =
            DependencyProperty.Register("Sound", typeof(Stream), typeof(NotifyBannerItem));
        #endregion

        #region Duration
        /// <summary>
        /// 持续时间
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(NotifyBannerItem));
        #endregion


        #region AutoClose
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool AutoClose
        {
            get { return (bool)GetValue(AutoCloseProperty); }
            set { SetValue(AutoCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoCloseProperty =
            DependencyProperty.Register("AutoClose", typeof(bool), typeof(NotifyBannerItem));
        #endregion


        #region EasingDuration
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Duration EasingDuration
        {
            get { return (Duration)GetValue(EasingDurationProperty); }
            set { SetValue(EasingDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingDurationProperty =
            DependencyProperty.Register("EasingDuration", typeof(Duration), typeof(NotifyBannerItem));
        #endregion


        #region EasingFunction
        /// <summary>
        /// 动画效果
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(NotifyBannerItem));
        #endregion

        #region CanClick
        /// <summary>
        /// 是否可以点击
        /// </summary>
        public bool CanClick
        {
            get { return (bool)GetValue(CanClickProperty); }
            set { SetValue(CanClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanClickProperty =
            DependencyProperty.Register("CanClick", typeof(bool), typeof(NotifyBannerItem));
        #endregion


        #endregion

        #region Closed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event NotifyClosedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        "Closed", RoutingStrategy.Bubble, typeof(NotifyClosedEventHandler), typeof(NotifyBannerItem));


        private void RaiseClosed(object newValue)
        {
            var arg = new NotifyClosedEventArgs(newValue, ClosedEvent);
            RaiseEvent(arg);
        }

        #endregion

        #region Clicked
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event NotifyClickedEventHandler Clicked
        {
            add { AddHandler(ClickedEvent, value); }
            remove { RemoveHandler(ClickedEvent, value); }
        }

        public static readonly RoutedEvent ClickedEvent = EventManager.RegisterRoutedEvent(
        "Clicked", RoutingStrategy.Bubble, typeof(NotifyClickedEventHandler), typeof(NotifyBannerItem));


        private void RaiseClicked(object newValue)
        {
            if (!CanClick)
                return;
            var arg = new NotifyClickedEventArgs(newValue, ClickedEvent);
            RaiseEvent(arg);
            Close();
        }

        #endregion

        #region Prviate



        public void Show()
        {
            Effect? shadow = null;
            if (this.Effect != null)
            {
                shadow = Effect;
                this.Effect = null;
            }

            Storyboard storyboard = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };
            DoubleAnimationUsingKeyFrames oDA = new DoubleAnimationUsingKeyFrames();
            oDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                Value = 1,
                KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                EasingFunction = EasingFunction,
            });
            storyboard.Children.Add(oDA);
            Storyboard.SetTarget(oDA, this);
            Storyboard.SetTargetProperty(oDA, new PropertyPath(NotifyBannerItem.OpacityProperty));

            switch (Site)
            {
                case NotifySite.TopRight:
                case NotifySite.BottomRight:

                    DoubleAnimationUsingKeyFrames rDA = new DoubleAnimationUsingKeyFrames();

                    rDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = 0,
                        KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                        EasingFunction = EasingFunction,
                    });
                    storyboard.Children.Add(rDA);
                    Storyboard.SetTarget(rDA, this);
                    Storyboard.SetTargetProperty(rDA, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.XProperty }));
                    break;
                case NotifySite.TopLeft:
                case NotifySite.BottomLeft:

                    DoubleAnimationUsingKeyFrames lDA = new DoubleAnimationUsingKeyFrames();

                    lDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = 0,
                        KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                        EasingFunction = EasingFunction,
                    });
                    storyboard.Children.Add(lDA);
                    Storyboard.SetTarget(lDA, this);
                    Storyboard.SetTargetProperty(lDA, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.XProperty }));
                    break;
                case NotifySite.TopCenter:

                    DoubleAnimationUsingKeyFrames tDA = new DoubleAnimationUsingKeyFrames();

                    tDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = 0,
                        KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                        EasingFunction = EasingFunction,
                    });
                    storyboard.Children.Add(tDA);
                    Storyboard.SetTarget(tDA, this);
                    Storyboard.SetTargetProperty(tDA, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.YProperty }));
                    break;
                case NotifySite.BottomCenter:

                    DoubleAnimationUsingKeyFrames bDA = new DoubleAnimationUsingKeyFrames();

                    bDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = 0,
                        KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                        EasingFunction = EasingFunction,
                    });
                    storyboard.Children.Add(bDA);
                    Storyboard.SetTarget(bDA, this);
                    Storyboard.SetTargetProperty(bDA, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.YProperty }));
                    break;
                default:
                    break;
            }


            storyboard.Completed += (so, se) =>
            {
                if (Status != NotifyStatus.Callback && AutoClose)
                {
                    _timer = new DispatcherTimer();
                    _timer.Interval = Duration.TimeSpan;
                    _timer.Tick += (to, te) =>
                    {
                        Close();
                    };
                    _timer.Start();
                }
                this.Effect = shadow;
            };
            storyboard.Begin();

            if (Sound != null)
            {
                try
                {
                    using var sound = new SoundPlayer(Sound);
                    sound.Play();
                }
                catch (Exception)
                {
                }
            }

        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            _closing = true;
            _timer?.Stop();
            this.Effect = null;
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames oDA = new DoubleAnimationUsingKeyFrames();
            oDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                Value = 0,
                KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                EasingFunction = EasingFunction,
            });
            storyboard.Children.Add(oDA);
            Storyboard.SetTarget(oDA, this);
            Storyboard.SetTargetProperty(oDA, new PropertyPath(NotifyBannerItem.OpacityProperty));

            switch (Site)
            {
                case NotifySite.TopRight:
                case NotifySite.BottomRight:
                    DoubleAnimationUsingKeyFrames rDA = new DoubleAnimationUsingKeyFrames();
                    rDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = 200,
                        KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                        EasingFunction = EasingFunction,
                    });
                    storyboard.Children.Add(rDA);
                    Storyboard.SetTarget(rDA, this);
                    Storyboard.SetTargetProperty(rDA, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.XProperty }));
                    break;
                case NotifySite.TopLeft:
                case NotifySite.BottomLeft:
                    DoubleAnimationUsingKeyFrames lDA = new DoubleAnimationUsingKeyFrames();
                    lDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = -200,
                        KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                        EasingFunction = EasingFunction,
                    });
                    storyboard.Children.Add(lDA);
                    Storyboard.SetTarget(lDA, this);
                    Storyboard.SetTargetProperty(lDA, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.XProperty }));
                    break;
                case NotifySite.TopCenter:
                    DoubleAnimationUsingKeyFrames tDA = new DoubleAnimationUsingKeyFrames();
                    tDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = -50,
                        KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                        EasingFunction = EasingFunction,
                    });
                    storyboard.Children.Add(tDA);
                    Storyboard.SetTarget(tDA, this);
                    Storyboard.SetTargetProperty(tDA, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.YProperty }));
                    break;
                case NotifySite.BottomCenter:
                    DoubleAnimationUsingKeyFrames bDA = new DoubleAnimationUsingKeyFrames();
                    bDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = 50,
                        KeyTime = KeyTime.FromTimeSpan(EasingDuration.TimeSpan),
                        EasingFunction = EasingFunction,
                    });
                    storyboard.Children.Add(bDA);
                    Storyboard.SetTarget(bDA, this);
                    Storyboard.SetTargetProperty(bDA, new PropertyPath("(0).(1)", new DependencyProperty[] { FrameworkElement.RenderTransformProperty, TranslateTransform.YProperty }));
                    break;
                default:
                    break;
            }

            storyboard.Completed += (es, ex) => RaiseClosed(this.DataContext);
            storyboard.Begin();
        }


        #endregion

    }
}
