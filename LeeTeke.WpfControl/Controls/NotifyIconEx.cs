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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using NotifyIcon = System.Windows.Forms.NotifyIcon;
using ContextMenuStrip = System.Windows.Forms.ContextMenuStrip;
using System.Windows.Threading;
using System.IO;
using System.Windows.Controls.Primitives;
using LeeTeke.WpfControl.Converters;

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
    ///     <MyNamespace:NotifyIcon/>
    ///
    /// </summary>
    public class NotifyIconEx : ContentControl, IDisposable
    {
        static NotifyIconEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotifyIconEx), new FrameworkPropertyMetadata(typeof(NotifyIconEx)));
        }
        #region 字段
        private NotifyIcon _notify;
        private Icon? _icon;
        private Icon _nullInon;

        private Popup _popup;
        private Border _border;


        private DispatcherTimer _flashTimer;
        #endregion


        public NotifyIconEx()
        {
            Application.Current.Exit += (es, ec) => this.Dispose();
            AppDomain.CurrentDomain.ProcessExit += (es, ec) => this.Dispose();
            #region 初始化组件
            _border = new Border();
            {
                _border.Resources = Application.Current.Resources;
            }

            BindingOperations.SetBinding(_border, Border.MinHeightProperty, new Binding() { Source = this, Path = new PropertyPath("MinHeight"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.MinWidthProperty, new Binding() { Source = this, Path = new PropertyPath("MinWidth"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.CornerRadiusProperty, new Binding() { Source = this, Path = new PropertyPath("CornerRadius"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.BackgroundProperty, new Binding() { Source = this, Path = new PropertyPath("Background"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.BorderBrushProperty, new Binding() { Source = this, Path = new PropertyPath("BorderBrush"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.BorderThicknessProperty, new Binding() { Source = this, Path = new PropertyPath("BorderThickness"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.PaddingProperty, new Binding() { Source = this, Path = new PropertyPath("Padding"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.MarginProperty, new Binding() { Source = this, Path = new PropertyPath("Margin"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.EffectProperty, new Binding() { Source = this, Path = new PropertyPath("Effect"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(_border, Border.DataContextProperty, new Binding() { Source = this, Path = new PropertyPath("DataContext"), Mode = BindingMode.OneWay });
            _popup = new Popup()
            {
                AllowsTransparency = true,
                Placement = PlacementMode.MousePoint,
                StaysOpen = false,
                PopupAnimation = PopupAnimation.Fade,
                Child = _border,
            };
            _border.PreviewMouseUp += _border_PreviewMouseUp;
            #endregion

            #region 初始化图片
            _notify = new NotifyIcon();
            _notify.DoubleClick += NotifyIcon_DoubleClick;
            _notify.MouseClick += NotifyIcon_MouseClick;
            _notify.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            _notify.ContextMenuStrip = new ContextMenuStrip
            {
                AutoSize = false,
                Size = new System.Drawing.Size(0, 0)
            };
            _notify.ContextMenuStrip.Items.Add("");
            _notify.ContextMenuStrip.Closed += ContextMenuStrip_Closed;
            _flashTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(400)
            };
            _flashTimer.Tick += FlashTimer_Tick;
            using MemoryStream memory = new MemoryStream(Convert.FromBase64String(@"AAABAAEAICAAAAEAGACoDAAAFgAAACgAAAAgAAAAQAAAAAEAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////w=="));
            _nullInon = new Icon(memory, 30, 30);
            #endregion
        }



        protected override void OnContentChanged(object oldContent, object newContent)
        {


            if (newContent == null)
            {
                _border.Child = null;
                return;
            }
            else if (newContent is FrameworkElement element)
            {
                AddRightContent(element);
            }
            else
            {
                AddRightContent(new TextBlock() { Text = newContent.ToString() });
            }
        }

        #region 依赖属性

        #region Icon
        /// <summary>
        /// Icon
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NotifyIconEx), new PropertyMetadata(null, new PropertyChangedCallback(IconChanged)));

        private static void IconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotifyIconEx notifyIconEx && e.NewValue != null && e.NewValue is ImageSource source)
            {
                try
                {
                    using var bitmap = Helper.ImageSourceToBitmap(source);
                    if (bitmap != null)
                    {
                        notifyIconEx._icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());
                        notifyIconEx._notify.Icon = notifyIconEx._icon;
                        notifyIconEx._notify.Visible = true;
                    }
                }
                catch
                {
                }
            }
        }

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NotifyIconEx), new PropertyMetadata(new CornerRadius(2)));

        #endregion



        #region BalloonTipData
        /// <summary>
        /// BalloonTipModel
        /// </summary>
        public BalloonTipModel BalloonTipData
        {
            get { return (BalloonTipModel)GetValue(BalloonTipDataProperty); }
            set { SetValue(BalloonTipDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BalloonTipData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BalloonTipDataProperty =
            DependencyProperty.Register("BalloonTipData", typeof(BalloonTipModel), typeof(NotifyIconEx), new PropertyMetadata(null, new PropertyChangedCallback(BalloonTipDataChanged)));

        private static void BalloonTipDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotifyIconEx leeIcon && e.NewValue != null && e.NewValue is BalloonTipModel balloon)
            {
                leeIcon.ShowBalloonTip(balloon.Time, balloon.Title, balloon.Content, balloon.Icon);
            }
        }

        #endregion

        #region Visible



        public bool Visible
        {
            get { return (bool)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Visble.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.Register("Visible", typeof(bool), typeof(NotifyIconEx), new PropertyMetadata(true, new PropertyChangedCallback(VisibleChanged)));

        private static void VisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotifyIconEx leeNotify)
            {
                var value = (bool)e.NewValue;
                leeNotify._notify.Visible = value;
            }
        }



        #endregion

        #region Text
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NotifyIconEx), new PropertyMetadata(null, new PropertyChangedCallback(TextChanged)));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotifyIconEx leeNotify && e.NewValue != null)
            {
                leeNotify._notify.Text = e.NewValue.ToString();
            }
        }
        #endregion

        #region Flashing


        public bool Flashing
        {
            get { return (bool)GetValue(FlashingProperty); }
            set { SetValue(FlashingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Flashing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlashingProperty =
            DependencyProperty.Register("Flashing", typeof(bool), typeof(NotifyIconEx), new PropertyMetadata(false, new PropertyChangedCallback(FlashingChanged)));

        private static void FlashingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotifyIconEx leeNotify)
            {
                var value = (bool)e.NewValue;
                if (value)
                {
                    leeNotify._flashTimer.Start();
                }
                else
                {
                    leeNotify._flashTimer.Stop();
                    leeNotify._notify.Icon = leeNotify._icon;
                }
            }
        }




        #endregion

        #region ClickedThenClosing
        /// <summary>
        /// 点击后关闭
        /// </summary>
        public bool ClickedThenClosing
        {
            get { return (bool)GetValue(ClickedThenClosingProperty); }
            set { SetValue(ClickedThenClosingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClickedThenClosing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickedThenClosingProperty =
            DependencyProperty.Register("ClickedThenClosing", typeof(bool), typeof(NotifyIconEx), new PropertyMetadata(true));

        #endregion

        #endregion


        #region Event



        #region MouseLeftClick
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event RoutedEventHandler MouseLeftClick
        {
            add { AddHandler(MouseLeftClickEvent, value); }
            remove { RemoveHandler(MouseLeftClickEvent, value); }
        }

        public static readonly RoutedEvent MouseLeftClickEvent = EventManager.RegisterRoutedEvent(
        "MouseLeftClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotifyIconEx));


        private void RaiseMouseLeftClick()
        {
            var arg = new RoutedEventArgs(MouseLeftClickEvent);
            RaiseEvent(arg);
        }

        #endregion

        #region MouseRightClick
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event RoutedEventHandler MouseRightClick
        {
            add { AddHandler(MouseRightClickEvent, value); }
            remove { RemoveHandler(MouseRightClickEvent, value); }
        }

        public static readonly RoutedEvent MouseRightClickEvent = EventManager.RegisterRoutedEvent(
        "MouseRightClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotifyIconEx));


        private void RaiseMouseRightClick()
        {
            var arg = new RoutedEventArgs(MouseRightClickEvent);
            RaiseEvent(arg);
        }

        #endregion

        #region MouseDoubleClick
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new event RoutedEventHandler MouseDoubleClick
        {
            add { AddHandler(MouseDoubleClickEvent, value); }
            remove { RemoveHandler(MouseDoubleClickEvent, value); }
        }

        public static new readonly RoutedEvent MouseDoubleClickEvent = EventManager.RegisterRoutedEvent(
        "MouseDoubleClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotifyIconEx));


        private void RaiseMouseDoubleClick()
        {
            var arg = new RoutedEventArgs(MouseDoubleClickEvent);
            RaiseEvent(arg);
        }

        #endregion


        #region BalloonTipClicked
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event RoutedEventHandler BalloonTipClicked
        {
            add { AddHandler(BalloonTipClickedEvent, value); }
            remove { RemoveHandler(BalloonTipClickedEvent, value); }
        }

        public static readonly RoutedEvent BalloonTipClickedEvent = EventManager.RegisterRoutedEvent(
        "BalloonTipClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotifyIconEx));


        private void RaiseBalloonTipClicked()
        {
            var arg = new RoutedEventArgs(BalloonTipClickedEvent);
            RaiseEvent(arg);
        }

        #endregion
        #endregion

        #region 公共方法

        /// <summary>
        /// 冒泡显示
        /// </summary>
        /// <param name="timeout">毫秒</param>
        /// <param name="tipTitle">标题</param>
        /// <param name="tipText">内容</param>
        /// <param name="tipIcon">Icon</param>
        public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            _notify.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon switch
            {
                ToolTipIcon.Error => System.Windows.Forms.ToolTipIcon.Error,
                ToolTipIcon.Info => System.Windows.Forms.ToolTipIcon.Info,
                ToolTipIcon.Warning => System.Windows.Forms.ToolTipIcon.Warning,
                _ => System.Windows.Forms.ToolTipIcon.None,
            });
        }
        #endregion


        #region Command


        #region MouseMoveCommand
        /// <summary>
        /// MouseMoveCommand
        /// </summary>
        public ICommand MouseMoveCommand
        {
            get { return (ICommand)GetValue(MouseMoveCommandProperty); }
            set { SetValue(MouseMoveCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseMoveCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseMoveCommandProperty =
            DependencyProperty.Register("MouseMoveCommand", typeof(ICommand), typeof(NotifyIconEx));

        #endregion


        #region MouseLeftButtonClickCommand
        /// <summary>
        /// MouseLeftButtonClickCommand
        /// </summary>
        public ICommand MouseLeftButtonClickCommand
        {
            get { return (ICommand)GetValue(MouseLeftButtonClickCommandProperty); }
            set { SetValue(MouseLeftButtonClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseLeftButtonClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseLeftButtonClickCommandProperty =
            DependencyProperty.Register("MouseLeftButtonClickCommand", typeof(ICommand), typeof(NotifyIconEx));

        #endregion


        #region MouseRightButtonClickCommand
        /// <summary>
        /// MouseRightButtonClickCommand
        /// </summary>
        public ICommand MouseRightButtonClickCommand
        {
            get { return (ICommand)GetValue(MouseRightButtonClickCommandProperty); }
            set { SetValue(MouseRightButtonClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseRightButtonClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseRightButtonClickCommandProperty =
            DependencyProperty.Register("MouseRightButtonClickCommand", typeof(ICommand), typeof(NotifyIconEx));

        #endregion


        #region MouseDoubleClickCommand
        /// <summary>
        /// MouseDoubleClickCommand
        /// </summary>
        public ICommand MouseDoubleClickCommand
        {
            get { return (ICommand)GetValue(MouseDoubleClickCommandProperty); }
            set { SetValue(MouseDoubleClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseDoubleClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseDoubleClickCommandProperty =
            DependencyProperty.Register("MouseDoubleClickCommand", typeof(ICommand), typeof(NotifyIconEx));

        #endregion


        #region BalloonTipClickedCommand
        /// <summary>
        /// BalloonTipClickedCommand
        /// </summary>
        public ICommand BalloonTipClickedCommand
        {
            get { return (ICommand)GetValue(BalloonTipClickedCommandProperty); }
            set { SetValue(BalloonTipClickedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BalloonTipClickedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BalloonTipClickedCommandProperty =
            DependencyProperty.Register("BalloonTipClickedCommand", typeof(ICommand), typeof(NotifyIconEx));

        #endregion


        #endregion

        #region 私有逻辑

        private void FlashTimer_Tick(object? sender, EventArgs e)
        {
            if (_notify.Icon == _nullInon)
            {
                _notify.Icon = _icon;
            }
            else
            {
                _notify.Icon = _nullInon;
            }
        }


        private void ContextMenuStrip_Closed(object? sender, System.Windows.Forms.ToolStripDropDownClosedEventArgs e)
        {
            if (_popup.IsOpen && !_popup.IsMouseOver)
                _popup.IsOpen = false;
        }

        private void NotifyIcon_BalloonTipClicked(object? sender, EventArgs e)
        {
            if (sender is NotifyIconEx)
            {
                RaiseBalloonTipClicked();
                try
                {
                    MouseDoubleClickCommand?.Execute(null);
                }
                catch
                {
                }
            }
        }

        private void NotifyIcon_MouseClick(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    RaiseMouseLeftClick();
                    try
                    {
                        MouseLeftButtonClickCommand?.Execute(null);
                    }
                    catch
                    {
                    }
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    RaiseMouseRightClick();
                    try
                    {
                        MouseRightButtonClickCommand?.Execute(null);
                    }
                    catch
                    {
                    }
                    if (_popup != null)
                    {
                        _popup.IsOpen = true;
                    }
                    break;
            }
        }

        private void NotifyIcon_DoubleClick(object? sender, EventArgs e)
        {
            RaiseMouseDoubleClick();
            try
            {
                MouseDoubleClickCommand?.Execute(null);
            }
            catch
            {
            }
        }



        private void AddRightContent(FrameworkElement element)
        {
            var multiBinding = new MultiBinding()
            {
                Converter = new MultiValueToClipConverter(),
            };
            multiBinding.Bindings.Add(new Binding() { Source = _border, Path = new PropertyPath("ActualWidth"), Mode = BindingMode.OneWay });
            multiBinding.Bindings.Add(new Binding() { Source = _border, Path = new PropertyPath("ActualHeight"), Mode = BindingMode.OneWay });
            multiBinding.Bindings.Add(new Binding() { Source = _border, Path = new PropertyPath("CornerRadius"), Mode = BindingMode.OneWay });
            multiBinding.Bindings.Add(new Binding() { Source = _border, Path = new PropertyPath("BorderThickness"), Mode = BindingMode.OneWay });
            BindingOperations.SetBinding(element, FrameworkElement.ClipProperty, multiBinding);
            _border.Child = null;
            _border.Child = element;
        }

        private void _border_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_popup.IsOpen && ClickedThenClosing)
            {
                _popup.IsOpen = false;
            }
        }


        #endregion


        #region IDisposable
        /// <summary>
        /// 析构
        /// </summary>
        ~NotifyIconEx()
        {
            Dispose(false);
        }
        bool _disposed; //是否回收完毕
        /// <summary>
        /// IDisposable接口
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing">是否需要释放那些实现IDisposable接口的托管对象</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return; //如果已经被回收，就中断执行
            if (disposing)
            {
                //TODO:释放那些实现IDisposable接口的托管对象
                _flashTimer.Stop();
                _notify.Dispose();
            }
            //TODO:释放非托管资源，设置对象为null
            _disposed = true;
        }
        #endregion



    }
}
