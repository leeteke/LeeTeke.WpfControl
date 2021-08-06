using LeeTeke.WpfControls;
using System;
using System.Collections.Generic;
using System.IO;
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
    ///     <MyNamespace:NotifyBannerItem/>
    ///
    /// </summary>
    public class NotifyBannerItem : ContentControl
    {
        static NotifyBannerItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotifyBannerItem), new FrameworkPropertyMetadata(typeof(NotifyBannerItem)));

        }
        public NotifyBannerItem()
        {
        }



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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NotifyBannerItem));
        #endregion

        #region Status
        /// <summary>
        /// 请添加描述
        /// </summary>
        public NotifyStatus Status
        {
            get { return (NotifyStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(NotifyStatus), typeof(NotifyBannerItem));
        #endregion

        #region Sound
        /// <summary>
        /// 声音
        /// </summary>
        public Stream Sound
        {
            get { return (Stream)GetValue(SoundProperty); }
            set { SetValue(SoundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sound.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SoundProperty =
            DependencyProperty.Register("Sound", typeof(Stream), typeof(NotifyBannerItem));
        #endregion

        #region Duration
        /// <summary>
        /// 持续时间
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(NotifyBannerItem));
        #endregion

        #region EasingFunction
        /// <summary>
        /// 动画效果
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(NotifyBannerItem));
        #endregion

        #region CanClick
        /// <summary>
        /// 是否可以点击
        /// </summary>
        public bool CanClick
        {
            get { return (bool)GetValue(CanClickProperty); }
            set { SetValue(CanClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanClickProperty =
            DependencyProperty.Register("CanClick", typeof(bool), typeof(NotifyBannerItem));
        #endregion


        #region Path
        /// <summary>
        /// 请添加描述
        /// </summary>
        public NotifyPath Path
        {
            get { return (NotifyPath)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(NotifyPath), typeof(NotifyBannerItem));
        #endregion



        #endregion

        #region Closed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event NotifyClosedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        "Closed", RoutingStrategy.Bubble, typeof(NotifyClosedEventHandler), typeof(NotifyBannerItem));


        private void RaiseClosed(object newValue)
        {
            var arg = new NotifyClosedEventArgs(newValue, ClosedEvent);
            RaiseEvent(arg);
        }

        #endregion

        #region Clicked
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event NotifyClickedEventHandler Clicked
        {
            add { AddHandler(ClickedEvent, value); }
            remove { RemoveHandler(ClickedEvent, value); }
        }

        public static readonly RoutedEvent ClickedEvent = EventManager.RegisterRoutedEvent(
        "Clicked", RoutingStrategy.Bubble, typeof(NotifyClickedEventHandler), typeof(NotifyBannerItem));


        private void RaiseClicked(object newValue)
        {
            if (!CanClick)
                return;
            var arg = new NotifyClickedEventArgs(newValue, ClickedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #region Prviate


        /// <summary>
        /// 关闭
        /// </summary>
        private void Closing()
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames oDA = new DoubleAnimationUsingKeyFrames();
            oDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                Value = 0,
                KeyTime = KeyTime.FromTimeSpan(Duration.TimeSpan),
                EasingFunction = EasingFunction,
            });
            storyboard.Children.Add(oDA);
            Storyboard.SetTarget(oDA, this);
            Storyboard.SetTargetProperty(oDA, new PropertyPath(NotifyBannerItem.OpacityMaskProperty));
                                                 
             
        }


        #endregion


    }
}
