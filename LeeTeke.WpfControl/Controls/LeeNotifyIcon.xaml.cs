using LeeTeke.WpfControl.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Brush = System.Windows.Media.Brush;
using Button = System.Windows.Controls.Button;
using Control = System.Windows.Forms.Control;


namespace LeeTeke.WpfControl.Controls
{
    /// <summary>
    /// LeeNotifyIcon.xaml 的交互逻辑
    /// </summary>
    public partial class LeeNotifyIcon : System.Windows.Controls.UserControl, IDisposable
    {
        private NotifyIcon notifyIcon = null;
        private DispatcherTimer flashTimer;
        private Icon icon;
        private Icon nullIncon;

        public LeeNotifyIcon()
        {
            InitializeComponent();
            _rightBorder.DataContext = this;
            EventManager.RegisterClassHandler(typeof(LeeNotifyIcon), Button.ClickEvent, new RoutedEventHandler(OnButtonClicked));
            notifyIcon = new NotifyIcon();
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            notifyIcon.ContextMenuStrip = new ContextMenuStrip
            {
                AutoSize = false,
                Size = new System.Drawing.Size(0, 0)
            };
            notifyIcon.ContextMenuStrip.Items.Add("");
            notifyIcon.ContextMenuStrip.Closed += ContextMenuStrip_Closed;
            notifyIcon.MouseMove += NotifyIcon_MouseMove;
            flashTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(400)
            };
            flashTimer.Tick += FlashTimer_Tick;
            using MemoryStream memory = new MemoryStream(Convert.FromBase64String(@"AAABAAEAICAAAAEAGACoDAAAFgAAACgAAAAgAAAAQAAAAAEAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////w=="));
            nullIncon = new Icon(memory, 30, 30);
        }




        #region 公共方法

        /// <summary>
        /// 冒泡显示
        /// </summary>
        /// <param name="timeout">毫秒</param>
        /// <param name="tipTitle">标题</param>
        /// <param name="tipText">内容</param>
        /// <param name="tipIcon">Icon</param>
        public void ShowBalloonTip(int timeout, string tipTitle, string tipText, Models.ToolTipIcon tipIcon)
        {
            System.Windows.Forms.ToolTipIcon toolTipIcon = tipIcon switch
            {
                Models.ToolTipIcon.Error => System.Windows.Forms.ToolTipIcon.Error,
                Models.ToolTipIcon.Info => System.Windows.Forms.ToolTipIcon.Info,
                Models.ToolTipIcon.Warning => System.Windows.Forms.ToolTipIcon.Warning,
                _ => System.Windows.Forms.ToolTipIcon.None,
            };
            notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, toolTipIcon);
        }
        #endregion

        #region 依赖属性



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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LeeNotifyIcon), new PropertyMetadata(new CornerRadius(3)));

        #endregion


        #region Background
        /// <summary>
        /// Background
        /// </summary>
        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(LeeNotifyIcon), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        #endregion


        #region BorderThickness
        /// <summary>
        /// BorderThickness
        /// </summary>
        public new Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderThickness.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(LeeNotifyIcon), new PropertyMetadata(new Thickness(0)));

        #endregion


        #region BorderBrush
        /// <summary>
        /// BorderBrush
        /// </summary>
        public new Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(LeeNotifyIcon));

        #endregion


        #region Padding
        /// <summary>
        /// Padding
        /// </summary>
        public new Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Padding.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register("Padding", typeof(Thickness), typeof(LeeNotifyIcon));

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
            DependencyProperty.Register("BalloonTipData", typeof(BalloonTipModel), typeof(LeeNotifyIcon), new PropertyMetadata(null, new PropertyChangedCallback(BalloonTipDataChanged)));

        private static void BalloonTipDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LeeNotifyIcon leeIcon && e.NewValue != null && e.NewValue is BalloonTipModel balloon)
            {
                leeIcon.ShowBalloonTip(balloon.Time, balloon.Title, balloon.Content, balloon.Icon);
            }
        }

        #endregion


        #region IconUri
        /// <summary>
        /// 图标地址
        /// </summary>
        public string IconUri
        {
            get { return (string)GetValue(IconUriProperty); }
            set { SetValue(IconUriProperty, value); }
        }
        // Using a DependencyProperty as the backing store for IconUri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconUriProperty =
            DependencyProperty.Register("IconUri", typeof(string), typeof(LeeNotifyIcon), new PropertyMetadata(null, new PropertyChangedCallback(IconUrlChanged)));

        private static void IconUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LeeNotifyIcon leeIcon)
            {
                var newUri = (string)e.NewValue;
                if (!string.IsNullOrEmpty(newUri))
                {
                    newUri = newUri.TrimStart('/');
                    try
                    {
                        leeIcon.icon = new Icon(System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/" + newUri)).Stream);//程序图标
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                    leeIcon.notifyIcon.Icon = leeIcon.icon;
                    leeIcon.notifyIcon.Visible = true;
                }
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
            DependencyProperty.Register("Visible", typeof(bool), typeof(LeeNotifyIcon), new PropertyMetadata(true, new PropertyChangedCallback(VisibleChanged)));

        private static void VisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LeeNotifyIcon leeNotify)
            {
                var value = (bool)e.NewValue;
                leeNotify.notifyIcon.Visible = value;
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
            DependencyProperty.Register("Text", typeof(string), typeof(LeeNotifyIcon), new PropertyMetadata(null, new PropertyChangedCallback(TextChanged)));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LeeNotifyIcon leeNotify && e.NewValue != null)
            {
                leeNotify.notifyIcon.Text = (string)e.NewValue;
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
            DependencyProperty.Register("Flashing", typeof(bool), typeof(LeeNotifyIcon), new PropertyMetadata(false, new PropertyChangedCallback(FlashingChanged)));

        private static void FlashingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LeeNotifyIcon leeNotify)
            {
                var value = (bool)e.NewValue;
                if (value)
                {
                    leeNotify.flashTimer.Start();
                }
                else
                {
                    leeNotify.flashTimer.Stop();
                    leeNotify.notifyIcon.Icon = leeNotify.icon;
                }
            }
        }




        #endregion

        #region Content
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(LeeNotifyIcon), new PropertyMetadata(null, new PropertyChangedCallback(ContentChanged)));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LeeNotifyIcon notifyIcon && e.NewValue != e.OldValue)
            {
                if (e.NewValue == null)
                {
                    notifyIcon._rightPopup = null;
                }
                else if (e.NewValue is FrameworkElement element)
                {
                    notifyIcon.AddRightContent(element);
                }
                else
                {
                    notifyIcon.AddRightContent(new TextBlock() { Text = e.NewValue.ToString() });
                }
            }
        }

        #endregion


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
            DependencyProperty.Register("MouseMoveCommand", typeof(ICommand), typeof(LeeNotifyIcon));

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
            DependencyProperty.Register("MouseLeftButtonClickCommand", typeof(ICommand), typeof(LeeNotifyIcon));

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
            DependencyProperty.Register("MouseRightButtonClickCommand", typeof(ICommand), typeof(LeeNotifyIcon));

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
            DependencyProperty.Register("MouseDoubleClickCommand", typeof(ICommand), typeof(LeeNotifyIcon));

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
            DependencyProperty.Register("BalloonTipClickedCommand", typeof(ICommand), typeof(LeeNotifyIcon));

        #endregion


        #endregion

        #region Event
        public new event EventHandler<MousePoint> MouseMoveEvent;
        public event EventHandler<MousePoint> MouseLeftButtonClickEvent;
        public event EventHandler<MousePoint> MouseRightButtonClickEvent;
        public new event EventHandler MouseDoubleClickEvent;
        public event EventHandler BalloonTipClicked;
        #endregion

        #region 内部逻辑

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_rightPopup.IsOpen)
                _rightPopup.IsOpen = false;
        }
        private void ContextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (_rightPopup.IsOpen && !_rightBorder.IsMouseOver)
                _rightPopup.IsOpen = false;

        }

        private void NotifyIcon_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseMoveEvent?.Invoke(this, new MousePoint() { X = Control.MousePosition.X, Y = Control.MousePosition.Y });
            try
            {
                MouseMoveCommand?.Execute(new MousePoint() { X = Control.MousePosition.X, Y = Control.MousePosition.Y });
            }
            catch
            {
            }
        }

        private void FlashTimer_Tick(object sender, EventArgs e)
        {
            if (notifyIcon.Icon == nullIncon)
            {
                notifyIcon.Icon = icon;
            }
            else
            {
                notifyIcon.Icon = nullIncon;
            }
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (sender is NotifyIcon notify)
            {
                BalloonTipClicked?.Invoke(this, new EventArgs());
                try
                {
                    MouseDoubleClickCommand?.Execute(null);
                }
                catch
                {
                }
            }
        }

        private void NotifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    ;
                    MouseLeftButtonClickEvent?.Invoke(this, new MousePoint() { X = Control.MousePosition.X, Y = Control.MousePosition.Y });
                    try
                    {
                        MouseLeftButtonClickCommand?.Execute(new MousePoint() { X = Control.MousePosition.X, Y = Control.MousePosition.Y });
                    }
                    catch
                    {
                    }
                    break;
                case MouseButtons.Right:
                    MouseRightButtonClickEvent?.Invoke(this, new MousePoint() { X = Control.MousePosition.X, Y = Control.MousePosition.Y });
                    try
                    {
                        MouseRightButtonClickCommand?.Execute(new MousePoint() { X = Control.MousePosition.X, Y = Control.MousePosition.Y });
                    }
                    catch
                    {
                    }
                    if (_rightPopup != null)
                    {
                        _rightPopup.IsOpen = true;

                    }
                    break;
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            MouseDoubleClickEvent?.Invoke(this, new EventArgs());
            try
            {
                MouseDoubleClickCommand?.Execute(null);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="element"></param>
        private void AddRightContent(FrameworkElement element)
        {
            element.DataContext = this.DataContext;
            _rightBorder.Child = null;
            _rightBorder.Child = element;
        }

        public void Dispose()
        {
            flashTimer.Stop();
            notifyIcon.Dispose();
        }
        #endregion


    }




}
