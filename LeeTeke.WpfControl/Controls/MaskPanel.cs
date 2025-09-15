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
    ///     <MyNamespace:MaskPanel/>
    ///
    /// </summary>
    [TemplatePart(Name = ElementBorder, Type = typeof(Border))]
    [TemplatePart(Name = ElementRectangle, Type = typeof(Rectangle))]
    [TemplatePart(Name = ElementPanel, Type = typeof(Panel))]
    [TemplatePart(Name = ElementClose, Type = typeof(Button))]
    [TemplatePart(Name = ElementFull, Type = typeof(Button))]
    [TemplatePart(Name = ElementTitlePanel, Type = typeof(Panel))]
    public class MaskPanel : System.Windows.Controls.Control
    {
        static MaskPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaskPanel), new FrameworkPropertyMetadata(typeof(MaskPanel)));
        }

        #region consts
        private const string ElementBorder = "PART_Border";
        private const string ElementRectangle = "PART_Rectangle";
        private const string ElementPanel = "PART_Panel";
        private const string ElementClose = "PART_Close";
        private const string ElementFull = "PART_Full";
        private const string ElementTitlePanel = "PART_TitlePanel";
        #endregion

        private Border? _border;
        private Rectangle? _rectangle;
        private Panel? _panel;
        private Button? _btnClose;
        private Button? _btnFull;
        private Panel? _titlePanel;

        private Point _movePoint;
        private bool _moveTarget;

        private Point _sizePoint;
        private bool _sizeTarget;

        private bool _isPanelClick = false;//是否是面板点击
        private bool _isRenderEnd = false; //渲染完成
        private string _defaultContent = "未加载内容";
        private MaskPanelData? _maskData = null;//当前数据

        public MaskPanel()
        {
            this.MouseMove += MaskPanel_MouseMove;
            this.SizeChanged += MaskPanel_SizeChanged;
            this.IsVisibleChanged += MaskPanel_IsVisibleChanged;
        }


        #region Event

        private void MaskPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsVisible)
            {
                if (_border != null && _border.RenderTransform is TranslateTransform tt)
                {
                    tt.X = 0;
                    tt.Y = 0;
                }
            }
        }

        private void MaskPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_border != null && IsFull)
            {
                _border.Width = this.ActualWidth;
                _border.Height = this.ActualHeight;
            }

        }

        private void MaskPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_sizeTarget && e.LeftButton == MouseButtonState.Pressed && _border != null)
            {
                this.Cursor = Cursors.SizeNWSE;
                var nowP = e.GetPosition(this);
                var movwP = nowP - _sizePoint;
                var sizeWidth = _border.ActualWidth + movwP.X;
                var sizeHeight = _border.ActualHeight + movwP.Y;
                if (sizeWidth < 0)
                    sizeWidth = 0;
                if (sizeHeight < 0)
                    sizeHeight = 0;
                if (sizeWidth > this.ActualWidth)
                    sizeWidth = this.ActualWidth;
                if (sizeHeight > this.ActualHeight)
                    sizeHeight = this.ActualHeight;
                _border.Width = sizeWidth;
                _border.Height = sizeHeight;
                _sizePoint = nowP;

            }
            else
            {
                _sizeTarget = false;
                this.Cursor = Cursors.Arrow;
            }
        }

        #endregion

        #region override

        public override void OnApplyTemplate()
        {
            if (_border != null)
                _border.SizeChanged -= _border_SizeChanged;

            if (_rectangle != null)
                _rectangle.MouseLeftButtonDown -= _rectangle_MouseLeftButtonDown;

            if (_panel != null)
            {
                _panel.MouseLeftButtonDown -= _panel_MouseLeftButtonDown;
                _panel.MouseLeave -= _panel_MouseLeave;
                _panel.MouseLeftButtonUp -= _panel_MouseLeftButtonUp;
            }

            if (_btnFull != null)
                _btnFull.Click -= _btnFull_Click;

            if (_btnClose != null)
                _btnClose.Click -= _btnClose_Click;

            if (_titlePanel != null)
            {
                _titlePanel.MouseLeftButtonDown -= _titlePanel_MouseLeftButtonDown;
                _titlePanel.MouseMove -= _titlePanel_MouseMove;
            }

            base.OnApplyTemplate();

            _border = GetTemplateChild(ElementBorder) as Border;
            _rectangle = GetTemplateChild(ElementRectangle) as Rectangle;
            _panel = GetTemplateChild(ElementPanel) as Panel;
            _titlePanel = GetTemplateChild(ElementTitlePanel) as Panel;
            _btnFull = GetTemplateChild(ElementFull) as Button;
            _btnClose = GetTemplateChild(ElementClose) as Button;

            if (_border != null)
                _border.SizeChanged += _border_SizeChanged;

            if (_rectangle != null)
                _rectangle.MouseLeftButtonDown += _rectangle_MouseLeftButtonDown;

            if (_panel != null)
            {
                _panel.MouseLeftButtonDown += _panel_MouseLeftButtonDown;
                _panel.MouseLeave += _panel_MouseLeave;
                _panel.MouseLeftButtonUp += _panel_MouseLeftButtonUp;
            }


            if (_btnFull != null)
                _btnFull.Click += _btnFull_Click;

            if (_btnClose != null)
                _btnClose.Click += _btnClose_Click;

            if (_titlePanel != null)
            {
                _titlePanel.MouseLeftButtonDown += _titlePanel_MouseLeftButtonDown;
                _titlePanel.MouseMove += _titlePanel_MouseMove;
            }

        }

        private void _border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_border?.RenderSize.IsEmpty == false && !_isRenderEnd)
            {
                _isRenderEnd = true;
                IsShowChanged(true);
            }
        }

        private void _btnClose_Click(object sender, RoutedEventArgs e)
        {
            IsShow = false;
        }

        private void _titlePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanMove && !IsFull)
            {
                _movePoint = e.GetPosition(this);
                _moveTarget = true;
            }
        }

        private void _titlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _moveTarget && _border != null)
            {
                var nowP = e.GetPosition(this);
                var moveP = nowP - _movePoint;

                if (_border.RenderTransform is TranslateTransform tt)
                {
                    var moveX = tt.X + moveP.X;
                    var moveY = tt.Y + moveP.Y;
                    #region 范围限定


                    if (moveX > ((this.ActualWidth + _border.ActualWidth) / 2) - 35)
                        moveX = ((this.ActualWidth + _border.ActualWidth) / 2) - 35;
                    if (moveX < -((this.ActualWidth + _border.ActualWidth) / 2) + 35)
                        moveX = -((this.ActualWidth + _border.ActualWidth) / 2) + 35;

                    if (moveY > ((this.ActualHeight + _border.ActualHeight) / 2) - TitleHeight)
                        moveY = ((this.ActualHeight + _border.ActualHeight) / 2) - TitleHeight;
                    if (moveY < -((this.ActualHeight - _border.ActualHeight) / 2))
                        moveY = -((this.ActualHeight - _border.ActualHeight) / 2);
                    #endregion
                    tt.X = moveX;
                    tt.Y = moveY;
                }
                else
                {
                    var newTT = new TranslateTransform();
                    _border.RenderTransform = newTT;
                    newTT.X += moveP.X;
                    newTT.Y += moveP.Y;
                }
                _movePoint = nowP;
            }
            else
            {
                _moveTarget = false;
            }
        }

        private void _btnFull_Click(object sender, RoutedEventArgs e)
        {
            IsFull = !IsFull;

        }

        private void _panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isPanelClick = true;
        }

        private void _panel_MouseLeave(object sender, MouseEventArgs e)
        {
            _isPanelClick = false;
        }
        private void _panel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource == sender && BackgroundClickToClose && _isPanelClick)
            {
                if (_maskData != null && _maskData.BlockClose)
                {
                    return;
                }
                IsShow = false;
            }
        }

        private void _rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanResize && !IsFull)
            {
                _sizePoint = e.GetPosition(this);
                _sizeTarget = true;
            }
        }


        #endregion

        #region 依赖属性

        #region Content
        /// <summary>
        /// 请添加描述
        /// </summary>
        public object? Content
        {
            get { return (object?)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(MaskPanel));
        #endregion

        #region ContentData
        /// <summary>
        /// 请添加描述
        /// </summary>
        public MaskPanelData ContentData
        {
            get { return (MaskPanelData)GetValue(ContentDataProperty); }
            set { SetValue(ContentDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentDataProperty =
            DependencyProperty.Register("ContentData", typeof(MaskPanelData), typeof(MaskPanel), new PropertyMetadata(null, OnContentDataChanged));

        private static void OnContentDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MaskPanel mask)
            {
                mask.DataChanged(e.NewValue as MaskPanelData);
            }
        }
        #endregion

        #region IsShow
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(MaskPanel), new PropertyMetadata(false, OnIsShowChanged));

        private static void OnIsShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MaskPanel mask)
            {
                mask.IsShowChanged((bool)e.NewValue);
            }
        }



        #region Width
        /// <summary>
        /// 请添加描述
        /// </summary>
        public new double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(MaskPanel));
        #endregion

        #region Height
        /// <summary>
        /// 请添加描述
        /// </summary>
        public new double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(MaskPanel));
        #endregion

        #region MaxHeight
        /// <summary>
        /// 请添加描述
        /// </summary>
        public new double MaxHeight
        {
            get { return (double)GetValue(MaxHeightProperty); }
            set { SetValue(MaxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxHeight.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty MaxHeightProperty =
            DependencyProperty.Register("MaxHeight", typeof(double), typeof(MaskPanel));
        #endregion

        #region MaxWidth
        /// <summary>
        /// 请添加描述
        /// </summary>
        public new double MaxWidth
        {
            get { return (double)GetValue(MaxWidthProperty); }
            set { SetValue(MaxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxWidth.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.Register("MaxWidth", typeof(double), typeof(MaskPanel));
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MaskPanel));
        #endregion

        #region PanelBackground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush PanelBackground
        {
            get { return (Brush)GetValue(PanelBackgroundProperty); }
            set { SetValue(PanelBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PanelBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PanelBackgroundProperty =
            DependencyProperty.Register("PanelBackground", typeof(Brush), typeof(MaskPanel));
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
            DependencyProperty.Register("IsClip", typeof(bool), typeof(MaskPanel));
        #endregion


        #endregion

        #region PanelMargin
        /// <summary>
        /// PanelMargin
        /// </summary>
        public Thickness PanelMargin
        {
            get { return (Thickness)GetValue(PanelMarginProperty); }
            set { SetValue(PanelMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PanelMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PanelMarginProperty =
            DependencyProperty.Register("PanelMargin", typeof(Thickness), typeof(MaskPanel));
        #endregion

        #region IsFull
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsFull
        {
            get { return (bool)GetValue(IsFullProperty); }
            set { SetValue(IsFullProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFull.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFullProperty =
            DependencyProperty.Register("IsFull", typeof(bool), typeof(MaskPanel), new PropertyMetadata(OnIsFullChanged));

        private static void OnIsFullChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MaskPanel mask)
            {
                mask.IsFullChanged((bool)e.NewValue);
            }
        }
        #endregion

        #region CanMove
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool CanMove
        {
            get { return (bool)GetValue(CanMoveProperty); }
            set { SetValue(CanMoveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanMove.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanMoveProperty =
            DependencyProperty.Register("CanMove", typeof(bool), typeof(MaskPanel));
        #endregion

        #region CanResize
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanReSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanResizeProperty =
            DependencyProperty.Register("CanResize", typeof(bool), typeof(MaskPanel));
        #endregion

        #region Title
        /// <summary>
        /// 请添加描述
        /// </summary>
        public object? Title
        {
            get { return (object?)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(MaskPanel));
        #endregion

        #region TitleVerticalAlignment
        /// <summary>
        /// 请添加描述
        /// </summary>
        public VerticalAlignment TitleVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(TitleVerticalAlignmentProperty); }
            set { SetValue(TitleVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleVerticalAlignmentProperty =
            DependencyProperty.Register("TitleVerticalAlignment", typeof(VerticalAlignment), typeof(MaskPanel));
        #endregion

        #region TitleHorizontalAlignment
        /// <summary>
        /// 请添加描述
        /// </summary>
        public HorizontalAlignment TitleHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TitleHorizontalAlignmentProperty); }
            set { SetValue(TitleHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleHorizontalAlignmentProperty =
            DependencyProperty.Register("TitleHorizontalAlignment", typeof(HorizontalAlignment), typeof(MaskPanel));
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
            DependencyProperty.Register("TitleHeight", typeof(double), typeof(MaskPanel));
        #endregion

        #region TitleMargin
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness TitleMargin
        {
            get { return (Thickness)GetValue(TitleMarginProperty); }
            set { SetValue(TitleMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleMarginProperty =
            DependencyProperty.Register("TitleMargin", typeof(Thickness), typeof(MaskPanel));
        #endregion

        #region ShowClose
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool ShowClose
        {
            get { return (bool)GetValue(ShowCloseProperty); }
            set { SetValue(ShowCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowCloseProperty =
            DependencyProperty.Register("ShowClose", typeof(bool), typeof(MaskPanel));
        #endregion

        #region BackgroundClickToClose
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool BackgroundClickToClose
        {
            get { return (bool)GetValue(BackgroundClickToCloseProperty); }
            set { SetValue(BackgroundClickToCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundClickToClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundClickToCloseProperty =
            DependencyProperty.Register("BackgroundClickToClose", typeof(bool), typeof(MaskPanel));
        #endregion

        #region AnimationEnabled
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool AnimationEnabled
        {
            get { return (bool)GetValue(AnimationEnabledProperty); }
            set { SetValue(AnimationEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationEnabledProperty =
            DependencyProperty.Register("AnimationEnabled", typeof(bool), typeof(MaskPanel));
        #endregion

        #region ShowAnimationMode
        /// <summary>
        /// 请添加描述
        /// </summary>
        public AnimationMode ShowAnimationMode
        {
            get { return (AnimationMode)GetValue(ShowAnimationModeProperty); }
            set { SetValue(ShowAnimationModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAnimationModeProperty =
            DependencyProperty.Register("ShowAnimationMode", typeof(AnimationMode), typeof(MaskPanel));
        #endregion

        #region ShowAnimationEasingFunction
        /// <summary>
        /// AnimationEasingFunction
        /// </summary>
        public IEasingFunction ShowAnimationEasingFunction
        {
            get { return (IEasingFunction)GetValue(ShowAnimationEasingFunctionProperty); }
            set { SetValue(ShowAnimationEasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationEasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAnimationEasingFunctionProperty =
            DependencyProperty.Register("ShowAnimationEasingFunction", typeof(IEasingFunction), typeof(MaskPanel));
        #endregion

        #region ShowAnimationDuration
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Duration ShowAnimationDuration
        {
            get { return (Duration)GetValue(ShowAnimationDurationProperty); }
            set { SetValue(ShowAnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAnimationDurationProperty =
            DependencyProperty.Register("ShowAnimationDuration", typeof(Duration), typeof(MaskPanel));
        #endregion

        #region ShowAnimationCustom
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Func<UIElement, Storyboard> ShowAnimationCustom
        {
            get { return (Func<UIElement, Storyboard>)GetValue(ShowAnimationCustomProperty); }
            set { SetValue(ShowAnimationCustomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationCustom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAnimationCustomProperty =
            DependencyProperty.Register("ShowAnimationCustom", typeof(Func<UIElement, Storyboard>), typeof(MaskPanel));
        #endregion

        #region ShowAnimationRenderTransformOrigin
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Point ShowAnimationRenderTransformOrigin
        {
            get { return (Point)GetValue(ShowAnimationRenderTransformOriginProperty); }
            set { SetValue(ShowAnimationRenderTransformOriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowAnimationRenderTransformOrigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAnimationRenderTransformOriginProperty =
            DependencyProperty.Register("ShowAnimationRenderTransformOrigin", typeof(Point), typeof(MaskPanel));
        #endregion

        #region CloseAnimationMode
        /// <summary>
        /// 请添加描述
        /// </summary>
        public AnimationMode CloseAnimationMode
        {
            get { return (AnimationMode)GetValue(CloseAnimationModeProperty); }
            set { SetValue(CloseAnimationModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseAnimationModeProperty =
            DependencyProperty.Register("CloseAnimationMode", typeof(AnimationMode), typeof(MaskPanel));
        #endregion

        #region CloseAnimationEasingFunction
        /// <summary>
        /// AnimationEasingFunction
        /// </summary>
        public IEasingFunction CloseAnimationEasingFunction
        {
            get { return (IEasingFunction)GetValue(CloseAnimationEasingFunctionProperty); }
            set { SetValue(CloseAnimationEasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationEasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseAnimationEasingFunctionProperty =
            DependencyProperty.Register("CloseAnimationEasingFunction", typeof(IEasingFunction), typeof(MaskPanel));
        #endregion

        #region CloseAnimationDuration
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Duration CloseAnimationDuration
        {
            get { return (Duration)GetValue(CloseAnimationDurationProperty); }
            set { SetValue(CloseAnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseAnimationDurationProperty =
            DependencyProperty.Register("CloseAnimationDuration", typeof(Duration), typeof(MaskPanel));
        #endregion

        #region CloseAnimationCustom
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Func<UIElement, Storyboard> CloseAnimationCustom
        {
            get { return (Func<UIElement, Storyboard>)GetValue(CloseAnimationCustomProperty); }
            set { SetValue(CloseAnimationCustomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationCustom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseAnimationCustomProperty =
            DependencyProperty.Register("CloseAnimationCustom", typeof(Func<UIElement, Storyboard>), typeof(MaskPanel));
        #endregion

        #region CloseAnimationRenderTransformOrigin
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Point CloseAnimationRenderTransformOrigin
        {
            get { return (Point)GetValue(CloseAnimationRenderTransformOriginProperty); }
            set { SetValue(CloseAnimationRenderTransformOriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseAnimationRenderTransformOrigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseAnimationRenderTransformOriginProperty =
            DependencyProperty.Register("CloseAnimationRenderTransformOrigin", typeof(Point), typeof(MaskPanel));
        #endregion


        #endregion

        #region Private

        private void IsFullChanged(bool isFull)
        {
            if (IsFull)
            {
                if (_border != null)
                {
                    _border.Width = this.ActualWidth - PanelMargin.Left - PanelMargin.Right;
                    _border.Height = this.ActualHeight - PanelMargin.Top - PanelMargin.Bottom;
                    if (_border.RenderTransform is TranslateTransform tt)
                    {
                        tt.X = 0;
                        tt.Y = 0;
                    }
                }
                if (_btnFull != null)
                    _btnFull.Content = "\xe73f";
            }
            else
            {
                //修改border尺寸
                if (_border != null)
                    ChangedBorderSize();
                if (_btnFull != null)
                    _btnFull.Content = "\xe740";
            }
        }

        /// <summary>
        /// 显示改变
        /// </summary>
        /// <param name="isShow"></param>
        private void IsShowChanged(bool isShow)
        {
            if (_border == null)//是否加载完毕
                return;
            if (isShow)
            {
                if (_panel != null)
                    _panel.Visibility = Visibility.Visible;
                if (!_isRenderEnd)
                    return;

                if (_maskData != null)
                {
                    Title = _maskData.Title;
                    if (_btnClose != null)
                        _btnClose.Visibility = _maskData.BlockClose ? Visibility.Collapsed : Visibility.Visible;

                    //修改border尺寸
                    ChangedBorderSize();

                    if (_maskData.Content is Page)
                    {
                        Content = new Frame()
                        {
                            Content = _maskData.Content,
                            NavigationUIVisibility = NavigationUIVisibility.Hidden,
                            Background = null,
                            BorderThickness = new Thickness(0)
                        };
                    }
                    else
                    {
                        Content = _maskData.Content;

                    }
                    _maskData.ClosePanel = () =>
                    {
                        if (_maskData != null)
                        {
                            _maskData.IsActiveClose = true;
                        }
                        IsShow = false;
                    };
                }

                ///动画选项
                if (AnimationEnabled && ShowAnimationMode != 0)
                {
                    BeginShow();
                }
                else
                {
                    //直接调用显示
                    _maskData?.ViewDisplayAction?.Invoke(true);
                }
            }
            else
            {
                _maskData?.ViewDisplayAction?.Invoke(false);
                if (AnimationEnabled && CloseAnimationMode != 0)
                {
                    BeginClose();
                }
                else
                {
                    Close();
                }
            }
        }


        /// <summary>
        /// 开始动画
        /// </summary>
        private void BeginShow()
        {
            //这里如果为空则直接触发
            if (_border == null)
            {
                _maskData?.ViewDisplayAction?.Invoke(true);
                return;
            }
            if (ShowAnimationCustom != null)
            {
                var func = ShowAnimationCustom(_border);
                func.Completed += Sb_Completed;
                func.Begin();
            }
            else
            {

                var sb = AnimationHelper.GetStoryboard(_border, ShowAnimationMode, ShowAnimationEasingFunction, ShowAnimationDuration, ShowAnimationRenderTransformOrigin);
                sb.Completed += Sb_Completed;
                sb.Begin();
            }
        }


        private void Sb_Completed(object? sender, EventArgs e)
        {
            _maskData?.ViewDisplayAction?.Invoke(true);
        }


        /// <summary>
        /// 开始关闭
        /// </summary>
        private void BeginClose()
        {
            if (_border == null)
            {
                Close();
                return;
            }

            if (CloseAnimationCustom != null)
            {
                var func = CloseAnimationCustom(_border);
                func.Completed += Close_Completed;
                func.Begin();
            }
            else
            {
                var sb = AnimationHelper.GetStoryboard(_border, CloseAnimationMode, CloseAnimationEasingFunction, CloseAnimationDuration, CloseAnimationRenderTransformOrigin);
                sb.Completed += Close_Completed;
                sb.Begin();
            }
        }
        /// <summary>
        /// 关闭动画完成时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Completed(object? sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 关闭显示
        /// </summary>
        private void Close()
        {
            ///关闭视图显示
            if (_panel != null)
                _panel.Visibility = Visibility.Collapsed;
            Title = _defaultContent;
            Content = _defaultContent;
            IsFull = false;
            ///调用关闭内容
            if (_maskData != null)
            {
                _maskData.ClosePanel = null;
                var closeData = _maskData;
                Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        closeData.CloseCallback?.Invoke(closeData.IsActiveClose ? MaskPanelCloseStatus.Self : MaskPanelCloseStatus.Trigger);
                    });
                });


                _maskData = null;
            }
        }
        /// <summary>
        /// Data改变
        /// </summary>
        private void DataChanged(MaskPanelData? newData)
        {
            if (newData == null)
            {
                IsShow = false;
                return;
            }

            ///初始化的参数
            newData.IsActiveClose = false;

            if (_maskData == null)
            {
                _maskData = newData;
                IsShow = true;
                return;
            }

            if (newData != _maskData)
            {
                newData.CloseCallback?.Invoke(MaskPanelCloseStatus.BlockDisplay);
                return;
            }
        }

        /// <summary>
        /// 该表BorderSize
        /// </summary>
        private void ChangedBorderSize()
        {
            try
            {
                ///当panelMargin不为空时无法修改相应大小
                if (_maskData != null &&_border!=null)
                {
                    _border.Width = Width == 0 ? double.NaN : Width;
                    _border.Height = Height == 0 ? double.NaN : Height;
                    if (!_maskData.ContentSize.IsEmpty)
                    {
                        if (HorizontalContentAlignment != HorizontalAlignment.Stretch)
                        {
                            _border.Width = BoderWidthCalculation(_maskData.ContentSize.Width);
                        }

                        if (VerticalContentAlignment != VerticalAlignment.Stretch)
                            _border.Height = BoderHeightCalculation(_maskData.ContentSize.Height);
                    }
                    else if (_maskData.Content is FrameworkElement element)
                    {
                        if (!double.IsNaN(element.Width) && HorizontalContentAlignment != HorizontalAlignment.Stretch)
                            _border.Width = BoderWidthCalculation(element.Width);

                        if (!double.IsNaN(element.Height) && VerticalContentAlignment != VerticalAlignment.Stretch)
                            _border.Height = BoderHeightCalculation(element.Height);
                    }
                }
            }
            catch
            {
            }
        }


        private double BoderWidthCalculation(double changeSize)
        {
            var result = changeSize + BorderThickness.Left + BorderThickness.Right + Padding.Left + Padding.Right;
            var aWidht = this.ActualWidth - PanelMargin.Left - PanelMargin.Right;
            return result > aWidht ? aWidht : result;
        }

        private double BoderHeightCalculation(double changeSize)
        {
            var result = changeSize + TitleHeight + BorderThickness.Top + BorderThickness.Bottom + Padding.Top + Padding.Bottom;
            var aHeight = this.ActualHeight - PanelMargin.Top - PanelMargin.Bottom;
            return result > aHeight ? aHeight : result;
        }


        #endregion


    }
}
