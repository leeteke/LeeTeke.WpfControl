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
    public class TagControlItem : ContentControl
    {
        static TagControlItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TagControlItem), new FrameworkPropertyMetadata(typeof(TagControlItem)));
        }

        public TagControlItem()
        {
            MouseDown += TagControlItem_MouseDown;
            Loaded += TagControlItem_Loaded;
        }

        private void TagControlItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.ContentTemplate?.HasContent == true)
            {

                var contentPresenter = StaticMethods.FindVisualChild<ContentPresenter>(this);
                if (contentPresenter != null)
                {
                    var chlid = VisualTreeHelper.GetChild(contentPresenter, 0);
                    BindingOperations.SetBinding(this, TagControlItem.CanClosedProperty, new Binding()
                    {
                        Source = chlid,
                        Path = new PropertyPath("(0)", new DependencyProperty[] { LeeTeke.WpfControl.Dependencies.TagControlManager.ItemCanCloseProperty }),
                        Mode = BindingMode.OneWay
                    });
                }
            }
        }

        private void TagControlItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            try
            {
         

                if (this.Template.FindName("PART_MenuItem_CloseAll", this) is MenuItem allItem)
                {
                    allItem.Click += (es, ex) => RaiseClosed(TagControlItemClosedMode.All);
                }
                if (this.Template.FindName("PART_MenuItem_CloseOther", this) is MenuItem otherItem)
                {
                    otherItem.Click += (es, ex) => RaiseClosed(TagControlItemClosedMode.Other);
                }
                if (this.Template.FindName("PART_MenuItem_CloseSelf", this) is MenuItem selftItem)
                {
                    selftItem.Click += (es, ex) => RaiseClosed(TagControlItemClosedMode.Self);
                }

                if (this.Template.FindName("PART_CloseButton", this) is Button button)
                {
                    button.Click += (es, ex) => RaiseClosed(TagControlItemClosedMode.Self);
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

        #endregion


        #region 依赖属性

        #region CanClosed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool CanClosed
        {
            get { return (bool)GetValue(CanClosedProperty); }
            set { SetValue(CanClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanClosed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanClosedProperty =
            DependencyProperty.Register("CanClosed", typeof(bool), typeof(TagControlItem));

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
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(TagControlItem), new PropertyMetadata(false, IsSelectedChanged));

        private static void IsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControlItem item && e.NewValue != e.OldValue)
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TagControlItem));

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
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TagControlItem));

        #endregion

        #endregion

        #region Closed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TagControlItemClosedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        "Closed", RoutingStrategy.Bubble, typeof(EventHandler<TagControlItemClosedEventHandler>), typeof(TagControlItem));


        private void RaiseClosed(TagControlItemClosedMode newValue)
        {
            var arg = new TagControlItemClosedEventArgs(newValue, ClosedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #region Selected
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TagControlItemSelectedEventHandler Selected
        {
            add { AddHandler(SelectedEvent, value); }
            remove { RemoveHandler(SelectedEvent, value); }
        }

        public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent(
        "Selected", RoutingStrategy.Bubble, typeof(EventHandler<TagControlItemSelectedEventHandler>), typeof(TagControlItem));


        private void RaiseSelected()
        {
            var arg = new TagControlItemSelectedEventArgs(SelectedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #region 公共方法
        /// <summary>
        /// 关闭Item
        /// </summary>
        public async Task CloseAsync()
        {
            if (CanClosed)
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
