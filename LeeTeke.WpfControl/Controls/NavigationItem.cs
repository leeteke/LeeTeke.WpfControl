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
                var pd = StaticMethods.FindVisualParent<NavigationItem>(element);
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

        //public new ContextMenu ContextMenu { get; private set; }
        #endregion

        /// <summary>
        /// 是否自我关闭
        /// </summary>
        internal bool SelfClose { set; get; }


        private Navigation ParentNavigation
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this) as Navigation;
            }
        }
        private Button _closeBtn;
        private bool _isClosed = false;
        
        public NavigationItem()
        {
            MouseDown += TabViewItem_MouseDown;
            KeyDown += TabViewItem_KeyDown;
        }

        private void TabViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    IsSelected = true;
                    break;
                default:
                    break;
            }


        }

        private void TabViewItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = true;
        }

        #region Override

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Focus() && e.Key == Key.Enter)
            {
                IsSelected = true;
            }

            base.OnKeyDown(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (Focus())
            {
                IsSelected = true;
            }

            base.OnMouseDown(e);
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
