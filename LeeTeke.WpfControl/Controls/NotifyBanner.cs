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
using System.Windows.Media.Effects;
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
    ///     <MyNamespace:NotifyBanner/>
    ///
    /// </summary>
    public class NotifyBanner : Control
    {
        static NotifyBanner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotifyBanner), new FrameworkPropertyMetadata(typeof(NotifyBanner)));
        }
        /// <summary>
        /// 位移距离
        /// </summary>
        private double _displacement = 100;
        private Canvas _canvas;
        private bool _isLoded = false;
        public NotifyBanner()
        {
              
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvas = this.Template.FindName("PART_Canvas", this) as Canvas;
            if (_canvas != null)
            {
                _isLoded = true;
            }
        }

        #region 依赖

        #region BannerPath
        /// <summary>
        /// 请填写描述
        /// </summary>
        public BannerPathMode BannerPath
        {
            get { return (BannerPathMode)GetValue(BannerPathProperty); }
            set { SetValue(BannerPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BannerPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BannerPathProperty =
            DependencyProperty.Register("BannerPath", typeof(BannerPathMode), typeof(NotifyBanner));

        #endregion

        #region EasingFunction
        /// <summary>
        /// 请填写描述
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(NotifyBanner));

        #endregion

        #region EasingDuration
        /// <summary>
        /// 请填写描述
        /// </summary>
        public Duration EasingDuration
        {
            get { return (Duration)GetValue(EasingDurationProperty); }
            set { SetValue(EasingDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingDurationProperty =
            DependencyProperty.Register("EasingDuration", typeof(Duration), typeof(NotifyBanner));

        #endregion

        #region IsKeepShow
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool IsKeepShow
        {
            get { return (bool)GetValue(IsKeepShowProperty); }
            set { SetValue(IsKeepShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsKeepShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsKeepShowProperty =
            DependencyProperty.Register("IsKeepShow", typeof(bool), typeof(NotifyBanner));

        #endregion

        #region ShowDuration
        /// <summary>
        /// 请填写描述
        /// </summary>
        public int ShowDuration
        {
            get { return (int)GetValue(ShowDurationProperty); }
            set { SetValue(ShowDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDurationProperty =
            DependencyProperty.Register("ShowDuration", typeof(int), typeof(NotifyBanner));

        #endregion

        #region Content
        /// <summary>
        /// 内容
        /// </summary>
        public NotifyBannerShowModel Content
        {
            get { return (NotifyBannerShowModel)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(NotifyBannerShowModel), typeof(NotifyBanner), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ContentChanged)));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotifyBanner banner && e.NewValue is NotifyBannerShowModel model && banner._isLoded)
            {
                banner.HaveContentAsync(model);
            }
        }

        #endregion

        #region Effect
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new Effect Effect
        {
            get { return (Effect)GetValue(EffectProperty); }
            set { SetValue(EffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Effect.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty EffectProperty =
            DependencyProperty.Register("Effect", typeof(Effect), typeof(NotifyBanner));

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NotifyBanner));

        #endregion


        #region ClickedCommand
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ICommand ClickedCommand
        {
            get { return (ICommand)GetValue(ClickedCommandProperty); }
            set { SetValue(ClickedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClickedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickedCommandProperty =
            DependencyProperty.Register("ClickedCommand", typeof(ICommand), typeof(NotifyBanner));

        #endregion


        #endregion

        #region RouteEvent


        #region NotifyBannerClicked
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event NotifyBannerClickedEventHandler Clicked
        {
            add { AddHandler(NotifyBannerClickedEvent, value); }
            remove { RemoveHandler(NotifyBannerClickedEvent, value); }
        }

        public static readonly RoutedEvent NotifyBannerClickedEvent = EventManager.RegisterRoutedEvent(
        "Clicked", RoutingStrategy.Bubble, typeof(NotifyBannerClickedEventHandler), typeof(NotifyBanner));


        private void RaiseNotifyBannerClicked(object newValue)
        {
            var arg = new NotifyBannerClickedEventArgs(newValue, NotifyBannerClickedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #endregion



        #region 私有逻辑

        private async void HaveContentAsync(NotifyBannerShowModel model)
        {
            var data = new NotifybannerDataContext() { Data = model };
            var sb = GenerateSB(data);
            if (sb == null)
                return;

            sb.Begin();
            if (model.Sound != null)
            {
                model.Sound.Play();
                model.Sound.Dispose();
            }

            if (IsKeepShow)
                return;

            ///等待持续时间
            await Task.Delay(model.Duration ?? this.ShowDuration);
            if (data.IsClicked)
                return;

             BackSB(data);
         
        }
        /// <summary>
        /// 生成动画
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Storyboard GenerateSB(NotifybannerDataContext data)
        {


            if (data.Data == null)
                return null;

            if (data.Data.Content is not FrameworkElement element)
            {
                element = new TextBlock()
                {
                    Text = data.Data.Content.ToString(),
                    Foreground = this.Foreground,
                    FontSize = this.FontSize,
                    FontWeight = this.FontWeight,
                    FontFamily = this.FontFamily,
                    FontStretch = this.FontStretch,
                    VerticalAlignment = this.VerticalContentAlignment,
                    HorizontalAlignment = this.HorizontalContentAlignment,
                };
            }

            var show = new Border()
            {
                Padding = this.Padding,
                Background = data.Data.Background ?? this.Background,
                Effect = this.Effect,
                MinHeight=this.MinHeight,
                MinWidth=this.MinWidth,
                CornerRadius=this.CornerRadius
            };

            data.Control = show;

            show.Child = element;
            show.Child.IsHitTestVisible = false;
            show.Opacity = 0;
            show.DataContext = data;
            show.MouseUp += Show_MouseUp;
            switch (BannerPath)
            {
                case BannerPathMode.RightToLeft:
                    if (_isLoded)
                    {
                        _canvas.Children.Add(show);
                        Canvas.SetRight(show, -_displacement);

                        var sb = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };
                        DoubleAnimationUsingKeyFrames xDA = new DoubleAnimationUsingKeyFrames();
                        xDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                        {

                            Value = 0,
                            KeyTime = KeyTime.FromTimeSpan(this.EasingDuration.TimeSpan),
                            EasingFunction = this.EasingFunction,
                        });

                        DoubleAnimationUsingKeyFrames yDA = new DoubleAnimationUsingKeyFrames();
                        yDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                        {

                            Value = 1,
                            KeyTime = KeyTime.FromTimeSpan(this.EasingDuration.TimeSpan),
                            EasingFunction = this.EasingFunction,
                        });
                        sb.Children.Add(xDA);
                        sb.Children.Add(yDA);

                        Storyboard.SetTarget(xDA, show);
                        Storyboard.SetTargetProperty(xDA, new PropertyPath(Canvas.RightProperty));

                        Storyboard.SetTarget(yDA, show);
                        Storyboard.SetTargetProperty(yDA, new PropertyPath(ContentControl.OpacityProperty));
                        return sb;
                    }
                    return null;
                case BannerPathMode.LeftToRight:
                    break;
                case BannerPathMode.TopToBottom:
                    break;
                case BannerPathMode.BottomToTop:
                    break;
                case BannerPathMode.Bloom:
                    break;
                default:
                    break;
            }


            return null;
        }

        private void Show_MouseEnter(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Show_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border&&border.DataContext is NotifybannerDataContext data&&!data.IsClicked)
            {
                data.IsClicked = true;
                BackSB(data);
                RaiseNotifyBannerClicked(data.Data.Value);
            }
        }

        private void BackSB(NotifybannerDataContext data)
        {
            var sb = new Storyboard() { FillBehavior = FillBehavior.Stop };
            DoubleAnimationUsingKeyFrames xDA = new DoubleAnimationUsingKeyFrames();
            xDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {

                Value = -_displacement,
                KeyTime = KeyTime.FromTimeSpan(this.EasingDuration.TimeSpan),
                EasingFunction = this.EasingFunction,
            });

            DoubleAnimationUsingKeyFrames yDA = new DoubleAnimationUsingKeyFrames();
            yDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {

                Value = 0,
                KeyTime = KeyTime.FromTimeSpan(this.EasingDuration.TimeSpan),
                EasingFunction = this.EasingFunction,
            });
            sb.Children.Add(xDA);
            sb.Children.Add(yDA);

            Storyboard.SetTarget(xDA, data.Control);
            Storyboard.SetTargetProperty(xDA, new PropertyPath(Canvas.RightProperty));

            Storyboard.SetTarget(yDA, data.Control);
            Storyboard.SetTargetProperty(yDA, new PropertyPath(ContentControl.OpacityProperty));

            sb.Completed += (e, s) =>
            {
                _canvas.Children.Remove(data.Control);
            };
            sb.Begin();
        }

        #endregion


    }


    class NotifybannerDataContext
    {
        public bool IsClicked { get; set; }

        public NotifyBannerShowModel Data { get; set; }

        public Border Control { get; set; }
    }
}
