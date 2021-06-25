using LeeTeke.WpfControl.Converters;
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
        private double _displacement = 200;

        /// <summary>
        /// 界面的stackpanel
        /// </summary>
        private StackPanel _stackPanel;


        /// <summary>
        /// 通知窗口
        /// </summary>
        private Window _notifyWindow;

        private bool _isLoded = false;

        private ResourceDictionary _buttonResource;
        public NotifyBanner()
        {
            _buttonResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/LeeTeke.WpfControl;component/Themes/Button.xaml") };
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            _stackPanel = this.Template.FindName("PART_StackPanel", this) as StackPanel;
            if (_stackPanel != null)
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

        #region ShowMode
        /// <summary>
        /// 展示模式
        /// </summary>
        public NotifyBannerShowMode ShowMode
        {
            get { return (NotifyBannerShowMode)GetValue(ShowModeProperty); }
            set { SetValue(ShowModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowModeProperty =
            DependencyProperty.Register("ShowMode", typeof(NotifyBannerShowMode), typeof(NotifyBanner));
        #endregion


        #region OnDesktopMargin
        /// <summary>
        /// 桌面模式下的Margin
        /// </summary>
        public Thickness OnDesktopMargin
        {
            get { return (Thickness)GetValue(OnDesktopMarginProperty); }
            set { SetValue(OnDesktopMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OnDesktopMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OnDesktopMarginProperty =
            DependencyProperty.Register("OnDesktopMargin", typeof(Thickness), typeof(NotifyBanner));
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
            GenerateSB(data);

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
        private void GenerateSB(NotifybannerDataContext data)
        {


            if (data.Data == null)
                return;

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
                    TextWrapping = TextWrapping.Wrap
                };
            }



            var show = new Grid() { Background = new SolidColorBrush(Colors.Transparent) };

            show.Children.Add(new Border()
            {
                Padding = this.Padding,
                Background = data.Data.Background ?? this.Background,
                Effect = this.Effect,
                MinHeight = this.MinHeight,
                MinWidth = this.MinWidth,
                CornerRadius = this.CornerRadius,
                Child = element,
                IsHitTestVisible = false,
            });

            var closeButton = new Button()
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = "╳",
                Width = 20,
                Height = 20,
                Style = _buttonResource["TextButton"] as Style
            };
            closeButton.Click += CloseButton_Click;
            show.Children.Add(closeButton);
            data.Control = show;
            data.BannerPath = this.BannerPath;
            show.Opacity = 1;
            show.DataContext = data;
            show.MouseUp += Show_MouseUp;
            show.Margin = new Thickness(0, 0, 0, 5);

            switch (BannerPath)
            {
                case BannerPathMode.RightBottom:
                case BannerPathMode.RightCenter:
                case BannerPathMode.RightTop:
                    show.RenderTransform = new TranslateTransform() { X = _displacement };
                    show.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
                case BannerPathMode.LeftBottom:
                case BannerPathMode.LeftCenter:
                case BannerPathMode.LeftTop:
                    show.RenderTransform = new TranslateTransform() { X = -_displacement };
                    show.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                default:
                    break;
            }

            var sb = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };

            DoubleAnimationUsingKeyFrames xDA = new DoubleAnimationUsingKeyFrames();
            xDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                Value = 0,
                KeyTime = KeyTime.FromTimeSpan(this.EasingDuration.TimeSpan),
                EasingFunction = this.EasingFunction,
            });

            DoubleAnimationUsingKeyFrames oDA = new DoubleAnimationUsingKeyFrames();
            oDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {

                Value = 1,
                KeyTime = KeyTime.FromTimeSpan(this.EasingDuration.TimeSpan),
                EasingFunction = this.EasingFunction,
            });
            sb.Children.Add(xDA);

            sb.Children.Add(oDA);


            Storyboard.SetTarget(xDA, show);
            Storyboard.SetTargetProperty(xDA, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            Storyboard.SetTarget(oDA, show);
            Storyboard.SetTargetProperty(oDA, new PropertyPath(Border.OpacityProperty));

            ///添加
            if (UXAdd(show))
                sb.Begin();
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is NotifybannerDataContext data && !data.IsClicked)
            {
                BackSB(data);
            }

        }

        private void Show_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is NotifybannerDataContext data && !data.IsClicked)
            {
                BackSB(data);
                RaiseNotifyBannerClicked(data.Data.Value);
            }
        }

        private void BackSB(NotifybannerDataContext data)
        {
            data.IsClicked = true;
            var sb = new Storyboard() { FillBehavior = FillBehavior.Stop };
            DoubleAnimationUsingKeyFrames xDA = new DoubleAnimationUsingKeyFrames();
            xDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {

                Value = BannerPath switch
                {
                    BannerPathMode.LeftTop or BannerPathMode.LeftCenter or BannerPathMode.LeftBottom => -_displacement,
                    _ => _displacement
                },
                KeyTime = KeyTime.FromTimeSpan(this.EasingDuration.TimeSpan),
                EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut },
            });

            DoubleAnimationUsingKeyFrames oDA = new DoubleAnimationUsingKeyFrames();
            oDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {

                Value = 0,
                KeyTime = KeyTime.FromTimeSpan(this.EasingDuration.TimeSpan),
                EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut },
            });
            sb.Children.Add(xDA);
            sb.Children.Add(oDA);

            Storyboard.SetTarget(xDA, data.Control);
            Storyboard.SetTargetProperty(xDA, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            Storyboard.SetTarget(oDA, data.Control);
            Storyboard.SetTargetProperty(oDA, new PropertyPath(Border.OpacityProperty));

            sb.Completed += (e, s) =>
            {
                UXRemove(data.Control);
            };
            sb.Begin();
        }


        /// <summary>
        /// 通知添加UI
        /// </summary>
        /// <param name="element"></param>
        private bool UXAdd(UIElement element)
        {
            switch (ShowMode)
            {
                case NotifyBannerShowMode.InApp:
                    ///这是 
                    if (_stackPanel == null)
                    {
                        return false;
                    }
                    else
                    {
                        switch (BannerPath)
                        {
                            case BannerPathMode.RightTop:
                            case BannerPathMode.RightCenter:
                            case BannerPathMode.LeftTop:
                            case BannerPathMode.LeftCenter:
                                _stackPanel?.Children.Insert(0, element);
                                break;
                            case BannerPathMode.RightBottom:
                            case BannerPathMode.LeftBottom:
                            default:
                                _stackPanel.Children.Add(element);
                                break;
                        }

                        return true;
                    }
                case NotifyBannerShowMode.OnDesktop:
                    var win = GenerateWindow();
                    if (win.Content is StackPanel stack)
                    {

                        switch (BannerPath)
                        {
                            case BannerPathMode.RightTop:
                            case BannerPathMode.RightCenter:
                            case BannerPathMode.LeftTop:
                            case BannerPathMode.LeftCenter:
                                stack.Children.Insert(0, element);
                                break;
                            case BannerPathMode.RightBottom:
                            case BannerPathMode.LeftBottom:
                            default:
                                stack.Children.Add(element);
                                break;
                        }

                        win.Show();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return false;
            }



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool UXRemove(UIElement element)
        {
            if (VisualTreeHelper.GetParent(element) is StackPanel sp)
            {
                sp.Children.Remove(element);
                if (sp.Children.Count < 1 && ShowMode == NotifyBannerShowMode.OnDesktop)
                {
                    _notifyWindow.Close();
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// 生成通知
        /// </summary>
        /// <returns></returns>
        private Window GenerateWindow()
        {
            if (_notifyWindow == null)
            {

                var spE = new StackPanel()
                {
                    Background = null
                };
                spE.SetBinding(StackPanel.MarginProperty, new Binding
                {
                    Source = this,
                    Path = new PropertyPath(NotifyBanner.OnDesktopMarginProperty),
                    Mode = BindingMode.OneWay
                });

                spE.SetBinding(StackPanel.VerticalAlignmentProperty, new Binding
                {
                    Source = this,
                    Path = new PropertyPath(NotifyBanner.BannerPathProperty),
                    Mode = BindingMode.OneWay,
                    Converter= new NotifyConverter()
                });
                _notifyWindow = new()
                {
                    Title = "notifyWindowService",
                    AllowsTransparency = true,
                    WindowStyle = WindowStyle.None,
                    Background = null,
                    WindowState = WindowState.Maximized,
                    Topmost = true,
                    ShowInTaskbar = false,
                    Content = spE
                };
                _notifyWindow.Closed += (cx, ce) =>
                {
                    _notifyWindow = null;
                };
            }

            return _notifyWindow;

        }
        #endregion


    }


    class NotifybannerDataContext
    {
        public bool IsClicked { get; set; }

        public NotifyBannerShowModel Data { get; set; }

        public Grid Control { get; set; }

        public BannerPathMode BannerPath { get; set; }

    }
}
