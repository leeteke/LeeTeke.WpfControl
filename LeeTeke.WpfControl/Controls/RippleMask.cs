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
    ///     <MyNamespace:RippleMask/>
    ///
    /// </summary>
    [TemplatePart(Name = ElementCanvas, Type = typeof(Canvas))]
    public class RippleMask : System.Windows.Controls.Control
    {
        static RippleMask()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleMask), new FrameworkPropertyMetadata(typeof(RippleMask)));
        }

        private const string ElementCanvas = "PART_Canvas";

        /// <summary>
        /// 鼠标当前再控件中的位置
        /// </summary>
        private Point _mouseSite;
        /// <summary>
        /// 最大半径
        /// </summary>
        public double _maxRadius;

        private Storyboard? _sbOpen;
        private Storyboard? _sbClose;
        private Path? _ripplePath;
        private Canvas? _canvas;
        /// <summary>
        /// 点击状态 0-未点击 1按下 2-按下并松开
        /// </summary>
        private int _clickStates = 0;
        private RoutedEventHandler _routedLeftUpEvent;
        private RoutedEventHandler _routedLeftDownEvent;
        private RoutedEventHandler _routedLeftLevelEvent;
        private RoutedEventHandler _routedMoveEvent;


        public RippleMask()
        {
            _routedLeftUpEvent = new RoutedEventHandler(MouseLeftButtonUpEventHandler);
            _routedLeftDownEvent = new RoutedEventHandler(MouseLeftButtonDownEventHandler);
            _routedLeftLevelEvent = new RoutedEventHandler(MouseLeaveEventHandler);
            _routedMoveEvent = new RoutedEventHandler(RoutedMoveEventHandler);
            SizeChanged += RippleMask_SizeChanged;
            MouseLeave += RippleMask_MouseLeave;
            IsVisibleChanged += RippleMask_IsVisibleChanged;
        }

        private void RippleMask_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsVisible)
            {
                _sbOpen?.Stop();
                _sbClose?.Stop();
                _canvas?.Children.Clear();
            }
        }




        #region CornerRadius


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(RippleMask), new PropertyMetadata(new CornerRadius(0), new PropertyChangedCallback(CornerRadiusChanged)));

        private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RippleMask mask)
            {
                var value = (CornerRadius)e.NewValue;
                mask.CornerRadius = value;
            }
        }



        #endregion

        #region RippleBrush

        /// <summary>
        /// 涟漪颜色
        /// </summary>
        public Brush RippleBrush
        {
            get { return (Brush)GetValue(RippleBrushProperty); }
            set { SetValue(RippleBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RippleColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RippleBrushProperty =
            DependencyProperty.Register("RippleBrush", typeof(Brush), typeof(RippleMask), new PropertyMetadata(null));



        #endregion

        #region Duration

        /// <summary>
        /// 动画延迟时间（此处为保持按下的时间，松开后时间将加速为8倍）
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(RippleMask), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(2000))));




        #endregion

        #region ParentElement

        /// <summary>
        /// 父级组件
        /// </summary>
        public FrameworkElement ParentElement
        {
            get { return (FrameworkElement)GetValue(ParentElementProperty); }
            set { SetValue(ParentElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParentElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParentElementProperty =
            DependencyProperty.Register("ParentElement", typeof(FrameworkElement), typeof(RippleMask), new PropertyMetadata(null, new PropertyChangedCallback(ParentElementChanged)));

        private static void ParentElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RippleMask mask)
            {
                if (e.OldValue is FrameworkElement element)
                {
                    try
                    {
                        element.RemoveHandler(MouseLeftButtonUpEvent, mask._routedLeftUpEvent);
                        element.RemoveHandler(MouseLeftButtonDownEvent, mask._routedLeftDownEvent);
                        element.RemoveHandler(MouseLeaveEvent, mask._routedLeftLevelEvent);
                        element.RemoveHandler(MouseMoveEvent, mask._routedMoveEvent);
                    }
                    catch
                    {
                    }
                }
                if (e.NewValue is FrameworkElement newelement)
                {
                    try
                    {
                        newelement.AddHandler(MouseLeftButtonUpEvent, mask._routedLeftUpEvent, true);
                        newelement.AddHandler(MouseLeftButtonDownEvent, mask._routedLeftDownEvent, true);
                        newelement.AddHandler(MouseLeaveEvent, mask._routedLeftLevelEvent, true);
                        newelement.AddHandler(MouseMoveEvent, mask._routedMoveEvent, true);
                    }
                    catch
                    {
                    }
                }
            }
        }


        #endregion

        #region IsOverflow

        /// <summary>
        /// 是否涟漪溢出
        /// 默认False
        /// </summary>
        public bool IsOverflow
        {
            get { return (bool)GetValue(IsOverflowProperty); }
            set { SetValue(IsOverflowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOverflow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOverflowProperty =
            DependencyProperty.Register("IsOverflow", typeof(bool), typeof(RippleMask));



        #endregion

        #region IsCustomSite


        /// <summary>
        /// 是否自定义位置
        /// 默认False
        /// </summary>
        public bool IsCustomSite
        {
            get { return (bool)GetValue(IsCustomSiteProperty); }
            set { SetValue(IsCustomSiteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCustomSite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCustomSiteProperty =
            DependencyProperty.Register("IsCustomSite", typeof(bool), typeof(RippleMask), new PropertyMetadata(false));





        #endregion

        #region CustomSite

        /// <summary>
        /// 自定义位置
        /// 默认 0,0
        /// </summary>
        public Point CustomSite
        {
            get { return (Point)GetValue(CustomSiteProperty); }
            set { SetValue(CustomSiteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomSite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomSiteProperty =
            DependencyProperty.Register("CustomSite", typeof(Point), typeof(RippleMask), new PropertyMetadata(new Point(0, 0)));




        #endregion


        #region 私有逻辑

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _canvas = GetTemplateChild(ElementCanvas) as Canvas;
        }

        private void RoutedMoveEventHandler(object sender, RoutedEventArgs e)
        {
            ///设置点位
            ///如果为自定义点位
            if (IsCustomSite)
            {
                _mouseSite = CustomSite;
            }
            else
            {
                ///如果为溢出模式则固定中心
                if (IsOverflow)
                {
                    _mouseSite = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
                }
                else
                {
                    _mouseSite = Mouse.GetPosition(this);
                }
            }
        }

        private void MouseLeaveEventHandler(object sender, RoutedEventArgs e)
        {
            if (_clickStates == 1)
            {
                if (_ripplePath?.RenderTransform is ScaleTransform scale)
                {
                    if (scale.ScaleX > 1)
                    {
                        _clickStates = 4;
                        _sbClose = new Storyboard();
                        DoubleAnimation xCDA = new DoubleAnimation()
                        {

                            To = 1,
                            Duration = new Duration(TimeSpan.FromMilliseconds(150)),
                        };
                        DoubleAnimation yCDA = new DoubleAnimation()
                        {

                            To = 1,
                            Duration = new Duration(TimeSpan.FromMilliseconds(150)),
                        };

                        _sbClose.Children.Add(xCDA);
                        _sbClose.Children.Add(yCDA);

                        Storyboard.SetTarget(xCDA, _ripplePath);
                        Storyboard.SetTargetProperty(xCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

                        Storyboard.SetTarget(yCDA, _ripplePath);
                        Storyboard.SetTargetProperty(yCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleYProperty }));
                        _sbClose.Completed += (ds, de) =>
                        {
                            _canvas?.Children.Clear();

                        };
                        _sbClose.Begin();
                    }
                }
            }
            else if (_clickStates == 2 || _clickStates == 3 || _clickStates == 4)
            {

            }
            else
            {
                _canvas?.Children.Clear();
            }
        }

        private void MouseLeftButtonDownEventHandler(object sender, RoutedEventArgs e)
        {
            _clickStates = 1;
            if (_sbOpen != null)
                _sbOpen.Stop();

            if (_canvas == null)
                return;

            _canvas.Children.Clear();
            _ripplePath = new Path
            {
                Name = "yuan",
                RenderTransform = new ScaleTransform() { ScaleX = 1, ScaleY = 1 },
                Fill = RippleBrush,
                Width = 8,
                Height = 8,
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = Helper.GetRoundRectangle(new Rect(0, 0, 8, 8), new Thickness(0), new CornerRadius(4))
            };

            _canvas.Children.Add(_ripplePath);

            Canvas.SetLeft(_ripplePath, _mouseSite.X - 4);
            Canvas.SetTop(_ripplePath, _mouseSite.Y - 4);
            _sbOpen = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };
            DoubleAnimation xDA = new DoubleAnimation()
            {

                To = _maxRadius / 4,
                Duration = Duration,
            };
            DoubleAnimation yDA = new DoubleAnimation()
            {

                To = _maxRadius / 4,
                Duration = Duration,
            };

            _sbOpen.Children.Add(xDA);
            _sbOpen.Children.Add(yDA);
            Storyboard.SetTarget(xDA, _ripplePath);
            Storyboard.SetTargetProperty(xDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

            Storyboard.SetTarget(yDA, _ripplePath);
            Storyboard.SetTargetProperty(yDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleYProperty }));
            _sbOpen.Completed += OpenSB_Completed;
            _sbOpen.Begin();

        }

        private void MouseLeftButtonUpEventHandler(object sender, RoutedEventArgs e)
        {
            if (_clickStates == 1)
            {
                _clickStates = 3;
                if (_sbOpen != null)
                {
                    _sbOpen.Pause();
                    _sbOpen.SpeedRatio = 10;
                    _sbOpen.Begin();
                }

            }
        }

        private void OpenSB_Completed(object? sender, EventArgs e)
        {
            if (_clickStates == 3)
            {
                _clickStates = 2;
                if (_ripplePath?.IsVisible == true)
                {
                    DoubleAnimation exit = new DoubleAnimation()
                    {
                        To = 0,
                        Duration = new Duration(TimeSpan.FromMilliseconds(150)),
                    };
                    _ripplePath.BeginAnimation(Path.OpacityProperty, exit);
                }
            }
        }

        private void RippleMask_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_clickStates == 1)
            {
                if (_ripplePath?.RenderTransform is ScaleTransform scale)
                {
                    if (scale.ScaleX > 1)
                    {
                        _sbClose = new Storyboard();
                        DoubleAnimation xCDA = new DoubleAnimation()
                        {

                            To = 1,
                            Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                        };
                        DoubleAnimation yCDA = new DoubleAnimation()
                        {

                            To = 1,
                            Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                        };

                        _sbClose.Children.Add(xCDA);
                        _sbClose.Children.Add(yCDA);

                        Storyboard.SetTarget(xCDA, _ripplePath);
                        Storyboard.SetTargetProperty(xCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

                        Storyboard.SetTarget(yCDA, _ripplePath);
                        Storyboard.SetTargetProperty(yCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleYProperty }));
                        _sbClose.Completed += (ds, de) =>
                        {
                            _canvas?.Children.Clear();

                        };
                        _sbClose.Begin();
                    }
                }
            }
            else if (_clickStates == 2 || _clickStates == 3)
            {

            }
            else
            {
                _canvas?.Children.Clear();
            }
        }

        private void RippleMask_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ///如果为溢出模式 则取最小值的1.25呗
            if (IsOverflow)
            {
                _maxRadius = Math.Min(this.ActualWidth, this.ActualHeight) * 1.05;
            }
            else
            {
                _maxRadius = this.ActualWidth + this.ActualHeight;
            }
        }
        #endregion


    }
}
