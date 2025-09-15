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

    [TemplatePart(Name = ElementCloseButton, Type = typeof(Button))]
    public class NavigationItem : ContentControl
    {
        #region ItemCanClosing
        public static bool? GetItemCanClose(DependencyObject obj)
        {
            return (bool?)obj.GetValue(ItemCanCloseProperty);
        }

        public static void SetItemCanClose(DependencyObject obj, bool? value)
        {
            obj.SetValue(ItemCanCloseProperty, value);
        }



        // Using a DependencyProperty as the backing store for ItemCanClosing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCanCloseProperty =
            DependencyProperty.RegisterAttached("ItemCanClose", typeof(bool?), typeof(NavigationItem), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ItemCanCloseChanged) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

        private static void ItemCanCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element && e.NewValue != null)
            {
                var pd = Helper.FindVisualParent<NavigationItem>(element);
                if (pd != null)
                {
                    pd.SetBinding(CanCloseProperty, new Binding()
                    {
                        Source = d,
                        Path = new PropertyPath(NavigationItem.ItemCanCloseProperty),
                        Mode = BindingMode.TwoWay
                    });
                }
            }
        }
        #endregion

        static NavigationItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationItem), new FrameworkPropertyMetadata(typeof(NavigationItem)));
        }

        private const string ElementCloseButton = "PART_CloseButton";

        #region 属性
        /// <summary>
        /// 是否执行了关闭
        /// </summary>
        public bool IsClosed => _isClosed;


        #endregion

        /// <summary>
        /// 是否自我关闭
        /// </summary>
        internal bool SelfClose { set; get; }

        internal bool IsMoving { get; private set; }


        private Point? _point;
        private NavigationItem? _beExchange;
        private bool _canDrag;


        private Navigation? ParentNavigation
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this) as Navigation;
            }
        }
        private Button? _closeBtn;
        private bool _isClosed = false;
        private Storyboard? _moveSB;



        public NavigationItem()
        {
            RenderTransform = new TranslateTransform();
           
        }




        #region Override


        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Enter:
                    Focus();
                    e.Handled = true;
                    IsSelected = true;
                    break;
                default:
                    base.OnPreviewKeyDown(e);
                    break;
            }

        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            Focus();
            IsSelected = true;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _canDrag = true;
            _point = e.GetPosition(this);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {

            _canDrag = false;
            _point = null;

            ReSetRenderTransform();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            _canDrag = false;
            _point = null;

            ReSetRenderTransform();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!IsCanDrag)
                return;
            IsMoving = true;
            if (RenderTransform is TranslateTransform tt && ParentNavigation != null && _point != null)
            {


                if (ParentNavigation.Orientation == Orientation.Horizontal)
                {
                    var newpt = Mouse.GetPosition(this);
                    var offset = newpt.X - _point.Value.X;
                    if (offset < 0)
                    {
                        _beExchange = ParentNavigation.GetBeforeItem(this);
                        if (_beExchange == null)
                        {
                            if (tt.X + offset < 0)
                            {
                                tt.X = 0;
                            }
                            else
                            {
                                tt.X += offset;
                            }
                            return;
                        }
                    }
                    else
                    {
                        _beExchange = ParentNavigation.GetAfterItem(this);
                        if (_beExchange == null)
                        {

                            if (tt.X + offset > 0)
                            {
                                tt.X = 0;
                            }
                            else
                            {
                                tt.X += offset;
                            }

                            return;
                        }
                    }
                    tt.X += offset;
                    var max = (_beExchange.ActualWidth + _beExchange.Margin.Left + _beExchange.Margin.Right + ActualWidth + Margin.Left + Margin.Right) / 4 + 1;
                    if (Math.Abs(tt.X) > max)
                    {
                        //通知调换位置
                        _beExchange.Opacity = 0;
                        ParentNavigation.NotifyItemMove(_beExchange, tt.X > 0);
                        tt.X = -tt.X;
                        _beExchange.SetMoveRenderTransform(tt.X > 0 ? -(ActualWidth + Margin.Left + Margin.Right) : (ActualWidth + Margin.Left + Margin.Right), 0);

                    }

                }
                else
                {
                    var newpt = Mouse.GetPosition(this);
                    var offset = newpt.Y - _point.Value.Y;
                    if (offset < 0)
                    {
                        _beExchange = ParentNavigation.GetBeforeItem(this);

                        if (_beExchange == null)
                        {
                            if (tt.Y + offset < 0)
                            {
                                tt.Y = 0;
                            }
                            else
                            {
                                tt.Y += offset;
                            }
                            return;
                        }
                    }
                    else
                    {
                        _beExchange = ParentNavigation.GetAfterItem(this);
                        if (_beExchange == null)
                        {
                            if (tt.Y + offset > 0)
                            {
                                tt.Y = 0;
                            }
                            else
                            {
                                tt.Y += offset;
                            }
                            return;
                        }
                    }

                    tt.Y += offset;
                    var max = (_beExchange.ActualHeight + _beExchange.Margin.Top + _beExchange.Margin.Bottom + ActualHeight + Margin.Top + Margin.Bottom) / 4 + 1;
                    if (Math.Abs(tt.Y) > max)
                    {
                        _beExchange.Opacity = 0;
                        //通知调换位置
                        ParentNavigation.NotifyItemMove(_beExchange, tt.Y > 0);
                        tt.Y = -tt.Y;

                        _beExchange.SetMoveRenderTransform(0, tt.Y > 0 ? -(ActualHeight + Margin.Top + Margin.Bottom) : ActualHeight + Margin.Top + Margin.Bottom);
                    }
                }
            }


        }


        public override void OnApplyTemplate()
        {
            if (_closeBtn != null)
            {
                _closeBtn.Click -= _closeBtn_Click;
            }

            base.OnApplyTemplate();

            _closeBtn = GetTemplateChild(ElementCloseButton) as Button;
            if (_closeBtn != null)
            {
                _closeBtn.Click += _closeBtn_Click;
            }
        }
        #endregion
        private void _closeBtn_Click(object sender, RoutedEventArgs e)
        {
            SelfClose = true;
            Close();
        }


        #region 依赖属性

        #region CanClose
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool CanClose
        {
            get { return (bool)GetValue(CanCloseProperty); }
            set { SetValue(CanCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanClosed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanCloseProperty =
            DependencyProperty.Register("CanClose", typeof(bool), typeof(NavigationItem));

        #endregion

        #region IsSelected
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(NavigationItem), new PropertyMetadata(false, IsSelectedChanged));

        private static void IsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NavigationItem item && e.NewValue != e.OldValue)
            {
                if ((bool)e.NewValue == true)
                {
                    item.NotifyParentSelecte();
                }
            }
        }

        #endregion

        #region CornerRadius
        /// <summary>
        /// 请填写描述
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NavigationItem));

        #endregion

        #region PinVisibly
        /// <summary>
        /// 是否显示固定按钮
        /// </summary>
        public bool PinVisibly
        {
            get { return (bool)GetValue(PinVisiblyProperty); }
            set { SetValue(PinVisiblyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PinVisibly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PinVisiblyProperty =
            DependencyProperty.Register("PinVisibly", typeof(bool), typeof(NavigationItem));
        #endregion

        #region Orientation
        /// <summary>
        /// Orientation
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(NavigationItem));

        #endregion

        #region IsPinned
        /// <summary>
        /// 是否固定
        /// </summary>
        public bool IsPinned
        {
            get { return (bool)GetValue(IsPinnedProperty); }
            set { SetValue(IsPinnedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFixed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPinnedProperty =
            DependencyProperty.Register("IsPinned", typeof(bool), typeof(NavigationItem));

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
            DependencyProperty.Register("CloseVisibly", typeof(ShowMode), typeof(NavigationItem));
        #endregion

        #endregion



        #region InternalMethod
        /// <summary>
        /// 设置移动动画
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal void SetMoveRenderTransform(double x, double y)
        {
            Opacity = 1;
            _moveSB = new Storyboard() { FillBehavior = FillBehavior.Stop };
            DoubleAnimation xCDA = new DoubleAnimation()
            {
                From = x,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            };
            DoubleAnimation yCDA = new DoubleAnimation()
            {
                From = y,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            };

            _moveSB.Children.Add(xCDA);
            _moveSB.Children.Add(yCDA);

            Storyboard.SetTarget(xCDA, this);
            Storyboard.SetTargetProperty(xCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { RenderTransformProperty, TranslateTransform.XProperty }));

            Storyboard.SetTarget(yCDA, this);
            Storyboard.SetTargetProperty(yCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { RenderTransformProperty, TranslateTransform.YProperty }));
            _moveSB.Completed += (ds, de) =>
            {
                if (RenderTransform is TranslateTransform tt)
                {
                    tt.X = 0;
                    tt.Y = 0;
                }
            };
            _moveSB.Begin();
        }

        /// <summary>
        /// 关闭Item
        /// </summary>
        internal void Close()
        {
            if (CanClose && !IsClosed)
            {
                var hover = new DoubleAnimationUsingKeyFrames();
                hover.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = 0,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100)),
                    EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn }
                });
                hover.Completed += (ss, se) =>
                {
                    _isClosed = true;
                    NotifyParentClose();
                };

                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        this.Width = ActualWidth;
                        this.MinWidth = 0;
                        this.BeginAnimation(WidthProperty, hover);
                        break;
                    case Orientation.Vertical:
                        this.Height = ActualHeight;
                        this.MinHeight = 0;
                        this.BeginAnimation(HeightProperty, hover);
                        break;
                    default:
                        break;
                }

            }
        }
        #endregion


        #region PrivateMethod


        private bool IsCanDrag
        {
            get
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed)
                    return false;
                if (!_canDrag)
                    return false;

                if (ParentNavigation == null)
                    return false;

                if (!ParentNavigation.CanItemMove)
                    return false;


                if (!IsMouseOver)
                    return false;

                if (!IsSelected)
                    return false;

                if (_point == null)
                    return false;








                return true;
            }
        }


        private void ReSetRenderTransform()
        {
            if (!IsMoving)
                return;
            _moveSB = new Storyboard() { FillBehavior = FillBehavior.Stop };
            DoubleAnimation xCDA = new DoubleAnimation()
            {
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            };
            DoubleAnimation yCDA = new DoubleAnimation()
            {

                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            };

            _moveSB.Children.Add(xCDA);
            _moveSB.Children.Add(yCDA);

            Storyboard.SetTarget(xCDA, this);
            Storyboard.SetTargetProperty(xCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { RenderTransformProperty, TranslateTransform.XProperty }));

            Storyboard.SetTarget(yCDA, this);
            Storyboard.SetTargetProperty(yCDA, new PropertyPath("(0).(1)", new DependencyProperty[] { RenderTransformProperty, TranslateTransform.YProperty }));
            _moveSB.Completed += (ds, de) =>
            {
                IsMoving = false;
                if (RenderTransform is TranslateTransform tt)
                {
                    tt.X = 0;
                    tt.Y = 0;
                }
                if (ParentNavigation?.IsScrollToSelected == true)
                {
                    ParentNavigation.ScrollToItem(this);
                }

            };
            _moveSB.Begin();
        }



        /// <summary>
        /// 通知夫组件我选择了
        /// </summary>
        private void NotifyParentSelecte()
        {
            ParentNavigation?.NotifyItemSelecte(this);
        }

        /// <summary>
        /// 通知夫组件我关闭了要
        /// </summary>
        private void NotifyParentClose()
        {
            ParentNavigation?.NotifyItemClose(this);
        }
        #endregion
    }
}
