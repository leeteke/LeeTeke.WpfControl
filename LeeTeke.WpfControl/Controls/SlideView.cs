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
using System.Windows.Threading;

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
    ///     <MyNamespace:SlideView/>
    ///
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(SlideViewItem))]
    public class SlideView : ItemsControl
    {
        static SlideView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SlideView), new FrameworkPropertyMetadata(typeof(SlideView)));
        }

        private Storyboard sbMovew;
        private ScrollViewer _scrollViewer;
        private DispatcherTimer autoPalyTime;
        public SlideView()
        {
            EventManager.RegisterClassHandler(typeof(SlideViewItem), SlideViewItem.MouseLeftButtonUpEvent, new RoutedEventHandler(ItemMouseLeftButtonUpEvent));
            PreviewMouseWheel += SlideView_PreviewMouseWheel;
        }

     

   




        #region 属性
        /// <summary>
        /// 总数量
        /// </summary>
        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }
        #endregion


        #region override
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is SlideViewItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SlideViewItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _scrollViewer = this.Template.FindName("PART_ScrollViewer", this) as ScrollViewer;
            ToIndex(0);
        }

        #endregion

        #region 依赖属性

        #region Orientation

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(SlideView), new PropertyMetadata(Orientation.Vertical, new PropertyChangedCallback(OrientationChanged)));

        private static void OrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SlideView control && e.OldValue != e.NewValue)
            {
                if (control.sbMovew != null)
                    control.sbMovew.Stop();
            }
        }
        #endregion

        #region CurrentValue
        /// <summary>
        /// 当前值
        /// </summary>
        public object CurrentValue
        {
            get { return (object)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(object), typeof(SlideView), new PropertyMetadata(null));

        #endregion

        #region CurrentIndex

        /// <summary>
        /// 当前值下表
        /// </summary>
        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(SlideView), new PropertyMetadata(0));

        #endregion

        #region Interval

        /// <summary>
        /// 自动轮播间隔时间
        /// </summary>
        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(SlideView), new PropertyMetadata(3000));



        #endregion

        #region AutoPaly


        public bool AutoPaly
        {
            get { return (bool)GetValue(AutoPalyProperty); }
            set { SetValue(AutoPalyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoPaly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoPalyProperty =
            DependencyProperty.Register("AutoPaly", typeof(bool), typeof(SlideView), new PropertyMetadata(false, new PropertyChangedCallback(AutoPalyChanged)));

        private static void AutoPalyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SlideView control && e.OldValue != e.NewValue)
            {
                control.AutoPaly = (bool)e.NewValue;
                if (control.AutoPaly)
                {
                    if (control.autoPalyTime == null)
                        control.autoPalyTime = new DispatcherTimer();
                    control.autoPalyTime.Interval = TimeSpan.FromMilliseconds(control.Interval);
                    control.autoPalyTime.Tick += control.AutoPalyTime_Tick;
                    control.autoPalyTime.Start();
                }
                else
                {
                    if (control.autoPalyTime != null)
                        control.autoPalyTime.Stop();
                }
            }
        }






        #endregion

        #region TransitionDuration


        /// <summary>
        /// 过渡动画延迟
        /// </summary>
        public int TransitionDuration
        {
            get { return (int)GetValue(TransitionDurationProperty); }
            set { SetValue(TransitionDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TransitionDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TransitionDurationProperty =
            DependencyProperty.Register("TransitionDuration", typeof(int), typeof(SlideView), new PropertyMetadata(500));


        #endregion

        #region TransitionEasing


        /// <summary>
        /// 过度动画
        /// </summary>
        public IEasingFunction TransitionEasing
        {
            get { return (IEasingFunction)GetValue(TransitionEasingProperty); }
            set { SetValue(TransitionEasingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TransitionEasing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TransitionEasingProperty =
            DependencyProperty.Register("TransitionEasing", typeof(IEasingFunction), typeof(SlideView), new PropertyMetadata(null, new PropertyChangedCallback(TransitionEasingChanged)));

        private static void TransitionEasingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SlideView control && e.OldValue != e.NewValue)
            {
                control.TransitionEasing = (IEasingFunction)e.NewValue;
            }
        }


        #endregion

        #region HorizontalOffset


        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(SlideView), new PropertyMetadata(0.0, new PropertyChangedCallback(HorizontalOffsetChanged)));

        private static void HorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SlideView control)
            {
                control._scrollViewer.ScrollToHorizontalOffset(control.HorizontalOffset);
            }
        }
        #endregion

        #region VerticalOffset
        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(SlideView), new PropertyMetadata(0.0, new PropertyChangedCallback(VerticalOffsetChanged)));

        private static void VerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SlideView control && e.OldValue != e.NewValue)
            {
                control._scrollViewer.ScrollToVerticalOffset(control.VerticalOffset);
            }
        }
        #endregion

        #region IsCycle
        public bool IsCycle
        {
            get { return (bool)GetValue(IsCycleProperty); }
            set { SetValue(IsCycleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCycle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCycleProperty =
            DependencyProperty.Register("IsCycle", typeof(bool), typeof(SlideView), new PropertyMetadata(true));

        #endregion

        #region ItemClickCommand


        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.Register("ItemClickCommand", typeof(ICommand), typeof(SlideView), new PropertyMetadata(default));

        #endregion

        #endregion


        #region 事件

        public event EventHandler<SlideViewItemClickedEventArgs> ItemClickEvent;


        #region ItemClicked
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event SlideViewItemClickedEventHandler ItemClicked
        {
            add { AddHandler(ItemClickedEvent, value); }
            remove { RemoveHandler(ItemClickedEvent, value); }
        }

        public static readonly RoutedEvent ItemClickedEvent = EventManager.RegisterRoutedEvent(
        "ItemClicked", RoutingStrategy.Bubble, typeof(EventHandler<SlideViewItemClickedEventHandler>), typeof(SlideView));


        private void RaiseItemClicked(object newValue)
        {
            var arg = new SlideViewItemClickedEventArgs(newValue, ItemClickedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #endregion

        #region 私有逻辑


        private void AutoPalyTime_Tick(object sender, EventArgs e)
        {
            ToIndex(CurrentIndex + 1);
        }

        /// <summary>
        /// 去选项
        /// </summary>
        /// <param name="index"></param>
        public void ToIndex(int index)
        {
            try
            {
                if (_scrollViewer==null )
                    return;

                if (Items.Count > 1)
                {
                    if (index < Count && index > -1)
                    {
                        CurrentValue = Items[index];
                        CurrentIndex = index;
                        switch (Orientation)
                        {
                            case Orientation.Horizontal:

                                if (sbMovew != null)
                                    sbMovew.Stop();

                                sbMovew = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };
                                DoubleAnimationUsingKeyFrames xDA = new DoubleAnimationUsingKeyFrames();
                                xDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                                {

                                    Value = this.ActualWidth * CurrentIndex,
                                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(TransitionDuration)),
                                    EasingFunction = TransitionEasing,
                                });
                                sbMovew.Children.Add(xDA);
                                Storyboard.SetTarget(xDA, this);
                                Storyboard.SetTargetProperty(xDA, new PropertyPath(SlideView.HorizontalOffsetProperty));
                                sbMovew.Begin();
                                break;
                            case Orientation.Vertical:
                                sbMovew = new Storyboard() { FillBehavior = FillBehavior.HoldEnd };
                                DoubleAnimationUsingKeyFrames yDA = new DoubleAnimationUsingKeyFrames();
                                yDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                                {

                                    Value = this.ActualHeight * CurrentIndex,
                                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(TransitionDuration)),
                                    EasingFunction = TransitionEasing,
                                });
                                sbMovew.Children.Add(yDA);
                                Storyboard.SetTarget(yDA, this);
                                Storyboard.SetTargetProperty(yDA, new PropertyPath(SlideView.VerticalOffsetProperty));
                                sbMovew.Begin();

                                break;
                            default:
                                break;
                        }
                    }
                    if (IsCycle)
                    {
                        if (index == Count)
                        {

                            ToIndex(0);
                        }
                        if (index == -1)
                        {

                            ToIndex(Items.Count - 1);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void SlideView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (e.Delta > 0)
            {
                ToIndex(CurrentIndex - 1);
            }
            if (e.Delta < 0)
            {
                ToIndex(CurrentIndex + 1);
            }
        }

        private void ItemMouseLeftButtonUpEvent(object sender, RoutedEventArgs e)
        {
            if (sender is SlideViewItem content&&StaticMethods.IsInControl(this,content))
            {
                try
                {
                    ItemClickCommand?.Execute(content.DataContext);
                }
                catch (Exception)
                {

                }

                RaiseItemClicked(content);
            }
        }
    }

    #endregion

}

