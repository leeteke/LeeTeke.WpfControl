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
    public class TabViewItem : ContentControl
    {
        static TabViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabViewItem), new FrameworkPropertyMetadata(typeof(TabViewItem)));
        }


        #region ItemCanClosing
        public static bool? GetItemCanClosing(DependencyObject obj)
        {
            return (bool?)obj.GetValue(ItemCanClosingProperty);
        }

        public static void SetItemCanClosing(DependencyObject obj, bool? value)
        {
            obj.SetValue(ItemCanClosingProperty, value);
        }

        // Using a DependencyProperty as the backing store for ItemCanClosing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCanClosingProperty =
            DependencyProperty.RegisterAttached("ItemCanClosing", typeof(bool?), typeof(TabViewItem), new PropertyMetadata(ItemCanClosingChanged));

        private static void ItemCanClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element && e.NewValue != null&&e.NewValue is bool @result)
            {
                var pd = StaticMethods.FindVisualParent<TabViewItem>(element);
                if (pd != null)
                {
                    pd.CanClosing = @result;
                }
            }
        }
        #endregion



        public TabViewItem()
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            try
            {
                if (this.Template.FindName("PART_ContextMenu", this) is ContextMenu contextMenu)
                {
                    this.ContextMenu = contextMenu;
                }

                if (this.Template.FindName("PART_MenuItem_CloseAll", this) is MenuItem allItem)
                {
                    allItem.Click += (es, ex) => RaiseClosed(TabViewItemClosedMode.All);
                }
                if (this.Template.FindName("PART_MenuItem_CloseOther", this) is MenuItem otherItem)
                {
                    otherItem.Click += (es, ex) => RaiseClosed(TabViewItemClosedMode.Other);
                }
                if (this.Template.FindName("PART_MenuItem_CloseSelf", this) is MenuItem selftItem)
                {
                    selftItem.Click += (es, ex) => RaiseClosed(TabViewItemClosedMode.Self);
                }

                if (this.Template.FindName("PART_MenuItem_Pin", this) is MenuItem btnPin)
                {
                    btnPin.Click += (es, ex) => IsFixed = !IsFixed;
                }

                if (this.Template.FindName("PART_CloseButton", this) is Button button)
                {
                    button.Click += (es, ex) => RaiseClosed(TabViewItemClosedMode.Self);
                }

                if (this.Template.FindName("PART_ContentPresenter", this) is ContentPresenter contentPresenter)
                {
                    contentPresenter.Loaded += (es, ex) =>
                    {
                        //var chlid = VisualTreeHelper.GetChild(contentPresenter, 0);
                        //BindingOperations.SetBinding(this, TabViewItem.CanClosingProperty, new Binding()
                        //{
                        //    Source = chlid,
                        //    Path = new PropertyPath("(0)", new DependencyProperty[] { LeeTeke.WpfControl.Dependencies.TabViewManager.ItemCanCloseProperty }),
                        //    Mode = BindingMode.OneWay
                        //});
                    };
                }


            }
            catch
            {
            }

        }



        #region 属性
        /// <summary>
        /// 是否执行了关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        public new ContextMenu ContextMenu { get; private set; }
        #endregion

        #region 依赖属性

        #region CanClosing
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool CanClosing
        {
            get { return (bool)GetValue(CanClosingProperty); }
            set { SetValue(CanClosingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanClosed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanClosingProperty =
            DependencyProperty.Register("CanClosing", typeof(bool), typeof(TabViewItem));

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
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(TabViewItem), new PropertyMetadata(false, IsSelectedChanged));

        private static void IsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabViewItem item && e.NewValue != e.OldValue)
            {
                if ((bool)e.NewValue == true)
                {
                    item.RaiseSelected();
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TabViewItem));

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
            DependencyProperty.Register("PinVisibly", typeof(bool), typeof(TabViewItem));
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
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TabViewItem));

        #endregion

        #region IsFixed
        /// <summary>
        /// 是否固定
        /// </summary>
        public bool IsFixed
        {
            get { return (bool)GetValue(IsFixedProperty); }
            set { SetValue(IsFixedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFixed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFixedProperty =
            DependencyProperty.Register("IsFixed", typeof(bool), typeof(TabViewItem));

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
            DependencyProperty.Register("CloseVisibly", typeof(ShowMode), typeof(TabViewItem));
        #endregion


        #endregion

        #region RouteEvent

        #region Closed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TabViewItemClosedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        "Closed", RoutingStrategy.Bubble, typeof(TabViewItemClosedEventHandler), typeof(TabViewItem));


        private void RaiseClosed(TabViewItemClosedMode newValue)
        {
            var arg = new TabViewItemClosedEventArgs(newValue, ClosedEvent);
            RaiseEvent(arg);
        }

        #endregion

        #region Selected
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TabViewItemSelectedEventHandler Selected
        {
            add { AddHandler(SelectedEvent, value); }
            remove { RemoveHandler(SelectedEvent, value); }
        }

        public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent(
        "Selected", RoutingStrategy.Bubble, typeof(TabViewItemSelectedEventHandler), typeof(TabViewItem));


        private void RaiseSelected()
        {
            var arg = new TabViewItemSelectedEventArgs(SelectedEvent);
            RaiseEvent(arg);
        }

        #endregion

        #endregion

        #region 公共方法
        /// <summary>
        /// 关闭Item
        /// </summary>
        public async Task CloseAsync()
        {
            if (CanClosing)
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
                    IsClosed = true;
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
                while (!IsClosed)
                {
                    await Task.Delay(100);
                }
            }
        }
        #endregion

    }
}
