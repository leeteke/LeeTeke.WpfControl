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
    /// RippleMask.xaml 的交互逻辑
    /// </summary>
    public partial class RippleMask : UserControl
    {
        /// <summary>
        /// 鼠标当前再控件中的位置
        /// </summary>
        private Point mouseSite;
        /// <summary>
        /// 最大半径
        /// </summary>
        public double maxRadius;

        private Storyboard sbOpen;
        private Storyboard sbClose;
        private Path ripplePath;

        private RoutedEventHandler routedLeftUpEvent;
        private RoutedEventHandler routedLeftDownEvent;
        private RoutedEventHandler routedLeftLevelEvent;
        private RoutedEventHandler routedMoveEvent;
        /// <summary>
        /// 点击状态 0-未点击 1按下 2-按下并松开
        /// </summary>
        private int clickStates = 0;
        public RippleMask()
        {
            InitializeComponent();
  
            routedLeftUpEvent = new RoutedEventHandler(MouseLeftButtonUpEventHandler);
            routedLeftDownEvent = new RoutedEventHandler(MouseLeftButtonDownEventHandler);
            routedLeftLevelEvent = new RoutedEventHandler(MouseLeaveEventHandler);
            routedMoveEvent=new RoutedEventHandler(RoutedMoveEventHandler);

            this.SetResourceReference(RippleMask.RippleBrushProperty, "LeeBrush_RippleDefault");

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
                var old = (FrameworkElement)e.OldValue;
                old?.RemoveHandler(MouseLeftButtonUpEvent, mask.routedLeftUpEvent);
                old?.RemoveHandler(MouseLeftButtonDownEvent, mask.routedLeftDownEvent);
                old?.RemoveHandler(MouseLeaveEvent, mask.routedLeftLevelEvent);
                old?.RemoveHandler(MouseMoveEvent, mask.routedMoveEvent);
           
                mask.ParentElement.AddHandler(MouseLeftButtonUpEvent, mask.routedLeftUpEvent, true);
                mask.ParentElement.AddHandler(MouseLeftButtonDownEvent, mask.routedLeftDownEvent, true);
                mask.ParentElement.AddHandler(MouseLeaveEvent, mask.routedLeftLevelEvent, true);
                mask.ParentElement.AddHandler(MouseMoveEvent, mask.routedMoveEvent, true);
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
            DependencyProperty.Register("IsOverflow", typeof(bool), typeof(RippleMask), new PropertyMetadata(false, new PropertyChangedCallback(IsOverflowChanged)));

        private static void IsOverflowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RippleMask mask)
            {
                var value = (bool)e.NewValue;
                if (value)
                {
                    mask.canvas.Visibility = Visibility.Collapsed;
                    mask.canvasOverflow.Visibility = Visibility.Visible;
                }
                else
                {
                    mask.canvas.Visibility = Visibility.Visible;
                    mask.canvasOverflow.Visibility = Visibility.Collapsed;
                }
            }
        }


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

        private void OpenSB_Completed(object sender, EventArgs e)
        {
            if (clickStates == 3)
            {
                clickStates = 2;
                if (ripplePath.Visibility == Visibility.Visible)
                {
                    DoubleAnimation exit = new DoubleAnimation()
                    {
                        To = 0,
                        Duration = new Duration(TimeSpan.FromMilliseconds(150)),
                    };
                    ripplePath.BeginAnimation(Path.OpacityProperty, exit);
                }
            }
        }

        private void Control_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickStates == 1)
            {
                if (ripplePath?.RenderTransform is ScaleTransform scale)
                {
                    if (scale.ScaleX > 1)
                    {
                        sbClose = new Storyboard();
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

                        sbClose.Children.Add(xCDA);
                        sbClose.Children.Add(yCDA);

                        Storyboard.SetTarget(xCDA, ripplePath);
                        Storyboard.SetTargetProperty(xCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

                        Storyboard.SetTarget(yCDA, ripplePath);
                        Storyboard.SetTargetProperty(yCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleYProperty }));
                        sbClose.Completed += (ds, de) =>
                        {
                            canvas.Children.Clear();

                        };
                        sbClose.Begin();
                    }
                }
            }
            else if (clickStates == 2 || clickStates == 3)
            {

            }
            else
            {
                canvas.Children.Clear();
            }
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ///如果为溢出模式 则取最小值的1.25呗
            if (IsOverflow)
            {
                maxRadius = Math.Min(this.ActualWidth, this.ActualHeight) * 1.05;
            }
            else
            {
                maxRadius = this.ActualWidth + this.ActualHeight;
            }
        }

        private void RoutedMoveEventHandler(object sender, RoutedEventArgs e)
        {
            ///设置点位
            ///如果为自定义点位
            if (IsCustomSite)
            {
                mouseSite = CustomSite;
            }
            else
            {
                ///如果为溢出模式则固定中心
                if (IsOverflow)
                {
                    mouseSite = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
                }
                else
                {
                    mouseSite = Mouse.GetPosition(this);
                }
            }
        }

    

        private void MouseLeaveEventHandler(object sender, RoutedEventArgs e)
        {
            if (clickStates == 1)
            {
                if (ripplePath?.RenderTransform is ScaleTransform scale)
                {
                    if (scale.ScaleX > 1)
                    {
                        clickStates = 4;
                        sbClose = new Storyboard();
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

                        sbClose.Children.Add(xCDA);
                        sbClose.Children.Add(yCDA);

                        Storyboard.SetTarget(xCDA, ripplePath);
                        Storyboard.SetTargetProperty(xCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

                        Storyboard.SetTarget(yCDA, ripplePath);
                        Storyboard.SetTargetProperty(yCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleYProperty }));
                        sbClose.Completed += (ds, de) =>
                        {
                            canvas.Children.Clear();

                        };
                        sbClose.Begin();
                    }
                }
            }
            else if (clickStates == 2 || clickStates == 3 || clickStates == 4)
            {

            }
            else
            {
                canvas.Children.Clear();
            }
        }

        private void MouseLeftButtonDownEventHandler(object sender, RoutedEventArgs e)
        {
            clickStates = 1;
            if (sbOpen != null)
                sbOpen.Stop();
            canvas.Children.Clear();
            canvasOverflow.Children.Clear();
            ripplePath = new Path
            {
                Name = "yuan",
                RenderTransform = new ScaleTransform() { ScaleX = 1, ScaleY = 1 },
                Fill = RippleBrush,
                Width = 8,
                Height = 8,
                RenderTransformOrigin = new Point(0.5, 0.5),
                Data = StaticMethods.GetRoundRectangle(new Rect(0, 0, 8, 8), new Thickness(0), new CornerRadius(4))
            };
            if (IsOverflow)
            {
                canvasOverflow.Children.Add(ripplePath);
            }
            else
            {
                canvas.Children.Add(ripplePath);
            }

            Canvas.SetLeft(ripplePath, mouseSite.X - 4);
            Canvas.SetTop(ripplePath, mouseSite.Y - 4);
            sbOpen = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };
            DoubleAnimation xDA = new DoubleAnimation()
            {

                To = maxRadius / 4,
                Duration = Duration,
            };
            DoubleAnimation yDA = new DoubleAnimation()
            {

                To = maxRadius / 4,
                Duration = Duration,
            };

            sbOpen.Children.Add(xDA);
            sbOpen.Children.Add(yDA);
            Storyboard.SetTarget(xDA, ripplePath);
            Storyboard.SetTargetProperty(xDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

            Storyboard.SetTarget(yDA, ripplePath);
            Storyboard.SetTargetProperty(yDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.RenderTransformProperty, ScaleTransform.ScaleYProperty }));
            sbOpen.Completed += OpenSB_Completed;
            sbOpen.Begin();

        }

        private void MouseLeftButtonUpEventHandler(object sender, RoutedEventArgs e)
        {
            if (clickStates == 1)
            {
                clickStates = 3;
                sbOpen.Pause();
                sbOpen.SpeedRatio = 10;
                sbOpen.Begin();
            }
        }

        private void control_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
