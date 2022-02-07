using LeeTeke.WpfControl.Dependencies;
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
    [TemplatePart(Name =ElementScrollViewer,Type =typeof(ScrollViewer))]
    public class SlideView : ItemsControl
    {
        static SlideView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SlideView), new FrameworkPropertyMetadata(typeof(SlideView)));
        }

        private const string ElementScrollViewer = "PART_ScrollViewer";
        private ScrollViewer _scrollViewer;
        private DispatcherTimer _autoPalyTime;
        public SlideView()
        {
            EventManager.RegisterClassHandler(typeof(SlideViewItem), SlideViewItem.MouseLeftButtonUpEvent, new RoutedEventHandler(ItemMouseLeftButtonUpEvent));
            PreviewMouseWheel += SlideView_PreviewMouseWheel;
            PreviewKeyDown += SlideView_PreviewKeyDown;
            IsVisibleChanged += SlideView_IsVisibleChanged;
            Loaded += SlideView_Loaded;
            SizeChanged += SlideView_SizeChanged;
        }

        private void SlideView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ToIndex(CurrentIndex);
        }

        private void SlideView_Loaded(object sender, RoutedEventArgs e)
        {
            SetAutoPaly(AutoPaly);
        }

        private void SlideView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                SetAutoPaly(AutoPaly);
            }
            else
            {
                SetAutoPaly(false);
            }
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
            _scrollViewer =GetTemplateChild(ElementScrollViewer) as ScrollViewer;
            
        }

        #endregion

        #region 依赖属性


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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SlideView));
        #endregion


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
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(SlideView));

     
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
            DependencyProperty.Register("Interval", typeof(int), typeof(SlideView),new PropertyMetadata(OnIntervalChanged));

        private static void OnIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SlideView control && e.OldValue != e.NewValue)
            {
                control.SetAutoPaly(control.AutoPaly);
            }
        }



        #endregion

        #region AutoPaly


        public bool AutoPaly
        {
            get { return (bool)GetValue(AutoPalyProperty); }
            set { SetValue(AutoPalyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoPaly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoPalyProperty =
            DependencyProperty.Register("AutoPaly", typeof(bool), typeof(SlideView), new PropertyMetadata(new PropertyChangedCallback(AutoPalyChanged)));

        private static void AutoPalyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SlideView control && e.OldValue != e.NewValue)
            {
                control.SetAutoPaly((bool)e.NewValue);
            }
        }






        #endregion

        #region Duration


        /// <summary>
        /// 过渡动画延迟
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TransitionDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(SlideView));


        #endregion

        #region EasingFunction


        /// <summary>
        /// 过度动画
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TransitionEasing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(SlideView));

  

        #endregion

        #region IsCycle
        public bool IsCycle
        {
            get { return (bool)GetValue(IsCycleProperty); }
            set { SetValue(IsCycleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCycle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCycleProperty =
            DependencyProperty.Register("IsCycle", typeof(bool), typeof(SlideView));

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

        #region Event

   

        /// <summary>
        /// 请填写描述
        /// </summary>
        public event SlideViewItemClickedEventHandler ItemClicked
        {
            add { AddHandler(ItemClickedEvent, value); }
            remove { RemoveHandler(ItemClickedEvent, value); }
        }

        public static readonly RoutedEvent ItemClickedEvent = EventManager.RegisterRoutedEvent(
        "ItemClicked", RoutingStrategy.Bubble, typeof(SlideViewItemClickedEventHandler), typeof(SlideView));


        private void RaiseItemClicked(object newValue)
        {
            var arg = new SlideViewItemClickedEventArgs(newValue, ItemClickedEvent);
            RaiseEvent(arg);
        }

        #endregion

        #region 私有逻辑

        private void SetAutoPaly(bool isAuto)
        {
            if (!IsLoaded)
                return;

            if (_autoPalyTime != null)
                _autoPalyTime.Stop();
            if (isAuto)
            {
                if (_autoPalyTime == null)
                    _autoPalyTime = new DispatcherTimer();
                _autoPalyTime.Interval = TimeSpan.FromMilliseconds(Interval);
                _autoPalyTime.Tick += AutoPalyTime_Tick;
                _autoPalyTime.Start();
            }
        }

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
                if (_scrollViewer == null)
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

                                ScrollViewerManager.ScrollToHorizontalOffset(_scrollViewer, this.ActualWidth * CurrentIndex);

                          
                                break;
                            case Orientation.Vertical:

                                ScrollViewerManager.ScrollToVerticalOffset(_scrollViewer, this.ActualHeight * CurrentIndex);

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

        private void SlideView_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Left:
                case Key.Up:
                    e.Handled = true;
                    ToIndex(CurrentIndex - 1);
                    break;

                case Key.Right:
                case Key.Down:
                    e.Handled = true;
                    ToIndex(CurrentIndex +1);
                    break;
                default:
                    break;
            }
      
        }

        private void ItemMouseLeftButtonUpEvent(object sender, RoutedEventArgs e)
        {
            if (sender is SlideViewItem content && StaticMethods.IsInControl(this, content))
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

