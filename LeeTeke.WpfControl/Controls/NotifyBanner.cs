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
using System.Windows.Shell;

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
    /// 
    [TemplatePart (Name =ElementStackPanel,Type = typeof(StackPanel))]
    public class NotifyBanner : System.Windows.Controls.Control
    {
        static NotifyBanner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotifyBanner), new FrameworkPropertyMetadata(typeof(NotifyBanner)));
        }

        private const string ElementStackPanel = "PART_StackPanel";


        private StackPanel? _stackPanel;
        /// <summary>
        /// 通知窗口
        /// </summary>
        private Window? _notifyWindow;


        public NotifyBanner()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _stackPanel = GetTemplateChild(ElementStackPanel) as StackPanel;
        }

        #region 依赖



        #region NotifyMargin
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness NotifyMargin
        {
            get { return (Thickness)GetValue(NotifyMarginProperty); }
            set { SetValue(NotifyMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NotifyMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotifyMarginProperty =
            DependencyProperty.Register("NotifyMargin", typeof(Thickness), typeof(NotifyBanner));
        #endregion

        #region NotifyPadding
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness NotifyPadding
        {
            get { return (Thickness)GetValue(NotifyPaddingProperty); }
            set { SetValue(NotifyPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NotifyPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotifyPaddingProperty =
            DependencyProperty.Register("NotifyPadding", typeof(Thickness), typeof(NotifyBanner));
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
            DependencyProperty.Register("CloseVisibly", typeof(ShowMode), typeof(NotifyBanner));
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
            DependencyProperty.Register("AutoClose", typeof(bool), typeof(NotifyBanner));
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


        #region NotifySite
        /// <summary>
        /// 请添加描述
        /// </summary>
        public NotifySite NotifySite
        {
            get { return (NotifySite)GetValue(NotifySiteProperty); }
            set { SetValue(NotifySiteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NotifySite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotifySiteProperty =
            DependencyProperty.Register("NotifySite", typeof(NotifySite), typeof(NotifyBanner));
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
            if (d is NotifyBanner banner && e.NewValue is NotifyBannerShowData model )
            {
                banner.HaveContent(model);
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
            DependencyProperty.Register("IsClip", typeof(bool), typeof(NotifyBanner));
        #endregion




        #endregion


        #region ICommand


        #region ClickedCommand
        /// <summary>
        /// 请添加描述
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


        #region ClosedCommand
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ICommand ClosedCommand
        {
            get { return (ICommand)GetValue(ClosedCommandProperty); }
            set { SetValue(ClosedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClosedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClosedCommandProperty =
            DependencyProperty.Register("ClosedCommand", typeof(ICommand), typeof(NotifyBanner));
        #endregion



        #endregion

        #region RouteEvent




        #region NotifyBannerClosed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event NotifyClosedEventHandler Closed
        {
            add { AddHandler(NotifyBannerClosedEvent, value); }
            remove { RemoveHandler(NotifyBannerClosedEvent, value); }
        }

        public static readonly RoutedEvent NotifyBannerClosedEvent = EventManager.RegisterRoutedEvent(
        "Closed", RoutingStrategy.Bubble, typeof(NotifyClosedEventHandler), typeof(NotifyBanner));


        private void RaiseNotifyBannerClosed(object? newValue)
        {
            var arg = new NotifyClosedEventArgs(newValue, NotifyBannerClosedEvent);
            RaiseEvent(arg);
            ClosedCommand?.Execute(newValue);
        }


        #endregion


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


        private void RaiseNotifyBannerClicked(object? newValue)
        {
            var arg = new NotifyBannerClickedEventArgs(newValue, NotifyBannerClickedEvent);
            RaiseEvent(arg);
            ClickedCommand?.Execute(newValue);
        }

        #endregion


        #endregion



        #region 私有逻辑

        private void HaveContent(NotifyBannerShowData data)
        {
            var control = new NotifyBannerItem(data, NotifySite);
            ItemBiding(control);
            var add = UXAdd(control);
            if (add)
            {
                control.Clicked += (es, ex) =>
                {
                    RaiseNotifyBannerClicked(ex?.Value);
                };
                control.Closed += (es, ex) =>
                {
                    UXRemove(control);
                    RaiseNotifyBannerClosed(ex?.Value);
                };
            }
        }
        /// <summary>
        /// 通知添加UI
        /// </summary>
        /// <param name="element"></param>
        private bool UXAdd(NotifyBannerItem item)
        {
            try
            {
                var panel = ShowMode switch
                {
                    NotifyBannerShowMode.InApp => _stackPanel,
                    NotifyBannerShowMode.OnDesktop => GenerateWindow().Content as StackPanel,
                    _ => null,
                };
                if (panel==null)
                    return false;

                switch (item.Site)
                {
                    case NotifySite.TopRight:
                    case NotifySite.TopCenter:
                    case NotifySite.TopLeft:
                        panel.Children.Insert(0, item);
                        break;
                    case NotifySite.BottomRight:
                    case NotifySite.BottomCenter:
                    case NotifySite.BottomLeft:
                        panel.Children.Add(item);
                        break;
                    default:
                        return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool UXRemove(NotifyBannerItem item)
        {
            if (VisualTreeHelper.GetParent(item) is StackPanel sp)
            {
                sp.Children.Remove(item);
                if (sp.Children.Count < 1 && ShowMode == NotifyBannerShowMode.OnDesktop)
                {
                    _notifyWindow?.Close();
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
                    Background = null,
                    ClipToBounds=true
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
                    Path = new PropertyPath(NotifyBanner.NotifySiteProperty),
                    Mode = BindingMode.OneWay,
                    Converter = new NotifyConverter(),
                    ConverterParameter="V"
                });
                spE.SetBinding(StackPanel.HorizontalAlignmentProperty, new Binding
                {
                    Source = this,
                    Path = new PropertyPath(NotifyBanner.NotifySiteProperty),
                    Mode = BindingMode.OneWay,
                    Converter = new NotifyConverter(),
                    ConverterParameter="H"
                });
                _notifyWindow = new Window()
                {
                    Title = "NotifyWindowService",
                    WindowStyle = WindowStyle.None,
                    AllowsTransparency = true,
                    Background = null,
                    WindowState = WindowState.Maximized,
                   ResizeMode= ResizeMode.NoResize,
                    Topmost = true,
                    ShowInTaskbar = false,
                    Content = spE
                };
           //  WindowChrome.SetWindowChrome(_notifyWindow, new WindowChrome() {  GlassFrameThickness= new Thickness(-1), CaptionHeight=0});
                _notifyWindow.Closed += (cx, ce) =>
                {
                    _notifyWindow = null;
                    LeeTeke.WpfControl.Helper.ClearMemory();
                };
            }
            _notifyWindow.Show();
            return _notifyWindow;

        }


        private void ItemBiding(NotifyBannerItem item)
        {
            item.SetBinding(NotifyBannerItem.BackgroundProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.BackgroundProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.BorderBrushProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.BorderBrushProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.EffectProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.EffectProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.BorderThicknessProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.BorderThicknessProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.CornerRadiusProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.CornerRadiusProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.ForegroundProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.ForegroundProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.FontSizeProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.FontSizeProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.FontStretchProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.FontStretchProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.MarginProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.NotifyMarginProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.PaddingProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.NotifyPaddingProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.MinHeightProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.MinHeightProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.MinWidthProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.MinWidthProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.DurationProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.ShowDurationProperty),
                Mode = BindingMode.OneWay
            });


            item.SetBinding(NotifyBannerItem.EasingDurationProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.EasingDurationProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.CloseVisiblyProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.CloseVisiblyProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.EasingFunctionProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.EasingFunctionProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.VerticalContentAlignmentProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.VerticalContentAlignmentProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.HorizontalContentAlignmentProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.HorizontalContentAlignmentProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.AutoCloseProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.AutoCloseProperty),
                Mode = BindingMode.OneWay
            });

            item.SetBinding(NotifyBannerItem.IsClipProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(NotifyBanner.IsClipProperty),
                Mode = BindingMode.OneWay
            });
        }
        #endregion


    }

}
