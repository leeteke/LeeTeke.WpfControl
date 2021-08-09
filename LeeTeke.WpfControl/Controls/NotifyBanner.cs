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

       
        public NotifyBanner()
        {
         
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
            DependencyProperty.Register("CloseVisibly", typeof(ShowMode), typeof(NotifyBanner));
        #endregion

        #region BannerPath
        /// <summary>
        /// 请填写描述
        /// </summary>
        public NotifyPath BannerPath
        {
            get { return (NotifyPath)GetValue(BannerPathProperty); }
            set { SetValue(BannerPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BannerPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BannerPathProperty =
            DependencyProperty.Register("BannerPath", typeof(NotifyPath), typeof(NotifyBanner));



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
        /// 请添加描述
        /// </summary>
        public Duration ShowDuration
        {
            get { return (Duration)GetValue(ShowDurationProperty); }
            set { SetValue(ShowDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDurationProperty =
            DependencyProperty.Register("ShowDuration", typeof(Duration), typeof(NotifyBanner));
        #endregion


        #region Content
        /// <summary>
        /// 内容
        /// </summary>
        public NotifyBannerShowData Content
        {
            get { return (NotifyBannerShowData)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(NotifyBannerShowData), typeof(NotifyBanner), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ContentChanged)));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotifyBanner banner && e.NewValue is NotifyBannerShowData model && banner._isLoded)
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

        private async void HaveContentAsync(NotifyBannerShowData data)
        {
            var control = new NotifyBannerItem(data);
            var add= UXAdd(control);
            if (add)
            {
                control.Closed += (es, ex) =>
                {
                    UXRemove(control);
                };
            }
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
                            case NotifyPath.RightTop:
                            case NotifyPath.RightCenter:
                            case NotifyPath.LeftTop:
                            case NotifyPath.LeftCenter:
                            case NotifyPath.TopCenter:
                                _stackPanel?.Children.Insert(0, element);
                                break;
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
                            case NotifyPath.RightTop:
                            case NotifyPath.RightCenter:
                            case NotifyPath.LeftTop:
                            case NotifyPath.LeftCenter:
                            case NotifyPath.TopCenter:
                                stack.Children.Insert(0, element);
                                break;
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

}
