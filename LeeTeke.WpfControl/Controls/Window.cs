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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#if NET40
using Microsoft.Windows.Shell;
#else
using System.Windows.Shell;
#endif
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
    ///     <MyNamespace:Window/>
    ///
    /// </summary>
    public class Window : System.Windows.Window
    {
        static Window()
        {
        }

        public Window()
        {
            this.Style = Application.Current.Resources["BaseWindow"] as Style;
#if NET40
            var chrome = new WindowChrome
            {
                CornerRadius = new CornerRadius(),
                GlassFrameThickness = new Thickness(0, 0, 0, 1),
                                NonClientFrameEdges= NonClientFrameEdges.Right| NonClientFrameEdges.Bottom| NonClientFrameEdges.Left
                
            };
#else
            var chrome = new WindowChrome
            {
                CornerRadius = new CornerRadius(),
                GlassFrameThickness = new Thickness(0, 0, 0, 1),
                UseAeroCaptionButtons = false,
            };
#endif

            BindingOperations.SetBinding(chrome, WindowChrome.CaptionHeightProperty, new Binding(nameof(TitleHeight)) { Source = this });
            WindowChrome.SetWindowChrome(this, chrome);
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (SizeToContent != SizeToContent.WidthAndHeight)
                return;


            SizeToContent = SizeToContent.Height;
            Dispatcher.BeginInvoke(new Action(() => { SizeToContent = SizeToContent.WidthAndHeight; }));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ViewInit();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (SizeToContent == SizeToContent.WidthAndHeight)
                InvalidateMeasure();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (WindowState == WindowState.Maximized)
            {
                BorderThickness = new Thickness();
            }
        }


        #region DeactivatedEffect
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Effect DeactivatedEffect
        {
            get { return (Effect)GetValue(DeactivatedEffectProperty); }
            set { SetValue(DeactivatedEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeactivatedEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeactivatedEffectProperty =
            DependencyProperty.Register("DeactivatedEffect", typeof(Effect), typeof(Window));
        #endregion


        #region DeactivatedBorderBrush
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush DeactivatedBorderBrush
        {
            get { return (Brush)GetValue(DeactivatedBorderBrushProperty); }
            set { SetValue(DeactivatedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeactivatedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeactivatedBorderBrushProperty =
            DependencyProperty.Register("DeactivatedBorderBrush", typeof(Brush), typeof(Window));
        #endregion


        #region DeactivatedBorderThickness
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness DeactivatedBorderThickness
        {
            get { return (Thickness)GetValue(DeactivatedBorderThicknessProperty); }
            set { SetValue(DeactivatedBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeactivatedBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeactivatedBorderThicknessProperty =
            DependencyProperty.Register("DeactivatedBorderThickness", typeof(Thickness), typeof(Window));
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Window));
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
            DependencyProperty.Register("IsClip", typeof(bool), typeof(Window));
        #endregion


        #region WinButtonPanelBackground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush WinButtonPanelBackground
        {
            get { return (Brush)GetValue(WinButtonPanelBackgroundProperty); }
            set { SetValue(WinButtonPanelBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WinButtonPanelBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WinButtonPanelBackgroundProperty =
            DependencyProperty.Register("WinButtonPanelBackground", typeof(Brush), typeof(Window));
        #endregion


        #region WinButtonPanelVerticalAlignment
        /// <summary>
        /// 请添加描述
        /// </summary>
        public VerticalAlignment WinButtonPanelVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(WinButtonPanelVerticalAlignmentProperty); }
            set { SetValue(WinButtonPanelVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WinButtonPanelVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WinButtonPanelVerticalAlignmentProperty =
            DependencyProperty.Register("WinButtonPanelVerticalAlignment", typeof(VerticalAlignment), typeof(Window));
        #endregion


        #region WinTitlePanelBackground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush WinTitlePanelBackground
        {
            get { return (Brush)GetValue(WinTitlePanelBackgroundProperty); }
            set { SetValue(WinTitlePanelBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WinTitlePanelBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WinTitlePanelBackgroundProperty =
            DependencyProperty.Register("WinTitlePanelBackground", typeof(Brush), typeof(Window));
        #endregion


        #region TitleBackground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush TitleBackground
        {
            get { return (Brush)GetValue(TitleBackgroundProperty); }
            set { SetValue(TitleBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleBackgroundProperty =
            DependencyProperty.Register("TitleBackground", typeof(Brush), typeof(Window));
        #endregion


        #region WinTitlePanelVerticalAlignment
        /// <summary>
        /// 请添加描述
        /// </summary>
        public VerticalAlignment WinTitlePanelVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(WinTitlePanelVerticalAlignmentProperty); }
            set { SetValue(WinTitlePanelVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WinTitlePanelVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WinTitlePanelVerticalAlignmentProperty =
            DependencyProperty.Register("WinTitlePanelVerticalAlignment", typeof(VerticalAlignment), typeof(Window));
        #endregion


        #region TitleContent
        /// <summary>
        /// 请添加描述
        /// </summary>
        public object TitleContent
        {
            get { return (object)GetValue(TitleContentProperty); }
            set { SetValue(TitleContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleContentProperty =
            DependencyProperty.Register("TitleContent", typeof(object), typeof(Window));
        #endregion

        #region TitleHeight
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double TitleHeight
        {
            get { return (double)GetValue(TitleHeightProperty); }
            set { SetValue(TitleHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleHeightProperty =
            DependencyProperty.Register("TitleHeight", typeof(double), typeof(Window),new PropertyMetadata(30.0));
        #endregion


        #region CloseButtonVisibility
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonVisibilityProperty =
            DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(Window));
        #endregion


        #region CloseButtonIsEnable
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool CloseButtonIsEnable
        {
            get { return (bool)GetValue(CloseButtonIsEnableProperty); }
            set { SetValue(CloseButtonIsEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonIsEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonIsEnableProperty =
            DependencyProperty.Register("CloseButtonIsEnable", typeof(bool), typeof(Window));
        #endregion


        #region ClientFull
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool ClientFull
        {
            get { return (bool)GetValue(ClientFullProperty); }
            set { SetValue(ClientFullProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClientFull.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientFullProperty =
            DependencyProperty.Register("ClientFull", typeof(bool), typeof(Window));
        #endregion



        #region TitleButtonWidth
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double TitleButtonWidth
        {
            get { return (double)GetValue(TitleButtonWidthProperty); }
            set { SetValue(TitleButtonWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleButtonWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleButtonWidthProperty =
            DependencyProperty.Register("TitleButtonWidth", typeof(double), typeof(Window));
        #endregion


        #region TitleForeground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(Window));
        #endregion



        #region MyRegion

        private void ViewInit()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand,
              (s, e) => WindowState = WindowState.Minimized));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand,
                (s, e) => WindowState = WindowState.Maximized));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand,
                (s, e) => WindowState = WindowState.Normal));
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (s, e) => Close()));
            CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, ShowSystemMenu));

        }


        private void ShowSystemMenu(object sender, ExecutedRoutedEventArgs e)
        {
            var point = WindowState == WindowState.Maximized
                ? new Point(0, TitleHeight)
                : new Point(Left, Top + TitleHeight);
            SystemCommands.ShowSystemMenu(this, point);
        }
        #endregion

    }
}
