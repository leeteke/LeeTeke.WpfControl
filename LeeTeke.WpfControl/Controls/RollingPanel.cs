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
    ///     <MyNamespace:RollingPanel/>
    ///
    /// </summary>
    public class RollingPanel : ContentControl
    {
        static RollingPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RollingPanel), new FrameworkPropertyMetadata(typeof(RollingPanel)));
        }
        private ContentPresenter _contentPresenter;
        private ScrollViewer _scrollViewer;
        private Storyboard _sbMove;
        private double _scrollExtentHeight;
        private double _scrolExtentWidth;
        public RollingPanel()
        {
            this.IsVisibleChanged += RollingPanel_IsVisibleChanged;
        }

        #region override


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.ContextMenu.Items[0] is MenuItem item)
            {
                item.Click += (es, ex) =>
                {

                    this.Visibility = Visibility.Collapsed;
                };
            }
            if (this.Template.FindName("PART_ScrollViewer", this) is ScrollViewer scroll)
            {
                _scrollViewer = scroll;
                _scrollViewer.ScrollChanged += _scrollViewer_ScrollChanged;
                _scrollViewer.SizeChanged += _scrollViewer_SizeChanged;
            }
            if (this.Template.FindName("PART_Content", this) is ContentPresenter content)
            {
                _contentPresenter = content;
            }



        }

        private void _scrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_scrollViewer == null || _contentPresenter == null)
                return;
            if (Rolling == null)
            {
                if (this.Orientation == Orientation.Horizontal)
                {
                    if (_contentPresenter.ActualWidth > (this.ActualWidth - this.BorderThickness.Left - this.BorderThickness.Right))
                    {
                        IsRolling = true;
                    }
                    else
                    {
                        IsRolling = false;
                        _sbMove?.Stop();
                        _scrollViewer.ScrollToHorizontalOffset(0);
                    }
                }
                else
                {
                    if (_contentPresenter.ActualHeight > (this.ActualHeight - this.BorderThickness.Top - this.BorderThickness.Bottom))
                    {
                        IsRolling = true;
                    }
                    else
                    {
                        IsRolling = false;
                        _sbMove?.Stop();
                        _scrollViewer.ScrollToVerticalOffset(0);
                    }
                }

            }

            if (Rolling == false)
            {
                _sbMove?.Stop();
                _scrollViewer.ScrollToHorizontalOffset(0);
            }
        }

        private void _scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsRolling)
            {
                if (_scrollExtentHeight != e.ExtentHeight || _scrolExtentWidth != e.ExtentWidth)
                {
                    _scrollExtentHeight = e.ExtentHeight;
                    _scrolExtentWidth = e.ExtentWidth;
                    RollingGO();
                }
            }
        }

        private void RollingPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch (Visibility)
            {
                case Visibility.Hidden:
                case Visibility.Collapsed:
                    IsRolling = false;
                    _sbMove?.Stop();
                    break;
                default:
                    break;
            }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            if (newContent != null)
            {
                this.Visibility = Visibility.Visible;
                _scrollViewer_SizeChanged(null, null);
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }

        }

        #endregion

        #region 依赖属性

        #region Orientation
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(RollingPanel));
        #endregion

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(RollingPanel));
        #endregion

        #region Rolling
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool? Rolling
        {
            get { return (bool?)GetValue(RollingProperty); }
            set { SetValue(RollingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rolling.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RollingProperty =
            DependencyProperty.Register("Rolling", typeof(bool?), typeof(RollingPanel), new PropertyMetadata(RollingChanged));

        private static void RollingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RollingPanel rolling)
            {
                if (e.NewValue is bool isRooling)
                {
                    rolling.IsRolling = isRooling;
                }
                else
                {
                    rolling.IsRolling = false;
                }
            }
        }
        #endregion

        #region IsRolling
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsRolling
        {
            get { return (bool)GetValue(IsRollingProperty); }
            set { SetValue(IsRollingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRolling.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRollingProperty =
            DependencyProperty.Register("IsRolling", typeof(bool), typeof(RollingPanel));
        #endregion

        #region Duration
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(double), typeof(RollingPanel));
        #endregion

        #region Speed
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Speed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(RollingPanel), new PropertyMetadata(SpeedChanged));

        private static void SpeedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RollingPanel control && e.OldValue != e.NewValue)
            {
                if (control._sbMove != null)
                {
                    control._sbMove.SetSpeedRatio((double)e.NewValue);
                }
            }
        }
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
            DependencyProperty.Register("TransitionEasing", typeof(IEasingFunction), typeof(RollingPanel), new PropertyMetadata(null, new PropertyChangedCallback(TransitionEasingChanged)));

        private static void TransitionEasingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RollingPanel control && e.OldValue != e.NewValue)
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
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(RollingPanel), new PropertyMetadata(0.0, new PropertyChangedCallback(HorizontalOffsetChanged)));

        private static void HorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RollingPanel control)
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
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(RollingPanel), new PropertyMetadata(0.0, new PropertyChangedCallback(VerticalOffsetChanged)));

        private static void VerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RollingPanel control && e.OldValue != e.NewValue)
            {
                control._scrollViewer.ScrollToVerticalOffset(control.VerticalOffset);
            }
        }
        #endregion

        #endregion

        #region public



        #endregion

        #region private

        private void RollingGO()
        {
            if (_scrollViewer == null)
                return;
            ///停止当前动画
            _sbMove?.Stop();
            switch (Orientation)
            {
                case Orientation.Horizontal:

                    var vPath = _scrolExtentWidth - _scrollViewer.ActualWidth + 10;
                    _sbMove = new Storyboard() { RepeatBehavior = RepeatBehavior.Forever };
                    DoubleAnimationUsingKeyFrames xDA = new DoubleAnimationUsingKeyFrames();
                    xDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = 0,
                        KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                    });
                    xDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {

                        Value = vPath,
                        KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(10 * vPath / 600)),
                        EasingFunction = TransitionEasing,
                    });
                    _sbMove.Children.Add(xDA);
                    Storyboard.SetTarget(xDA, this);
                    Storyboard.SetTargetProperty(xDA, new PropertyPath(RollingPanel.HorizontalOffsetProperty));
                    _sbMove.SpeedRatio = Speed;
                    _sbMove.Begin();
                    break;
                case Orientation.Vertical:
                    var hPath = _scrollExtentHeight - _scrollViewer.ActualHeight + 10;
                    _sbMove = new Storyboard() { RepeatBehavior = RepeatBehavior.Forever };
                    DoubleAnimationUsingKeyFrames yDA = new DoubleAnimationUsingKeyFrames();
                    yDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        Value = 0,
                        KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                    });
                    yDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {

                        Value = hPath,
                        KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(10 * (hPath - VerticalOffset) / hPath)),
                        EasingFunction = TransitionEasing,
                    });
                    _sbMove.Children.Add(yDA);
                    Storyboard.SetTarget(yDA, this);
                    Storyboard.SetTargetProperty(yDA, new PropertyPath(RollingPanel.VerticalOffsetProperty));

                    _sbMove.SpeedRatio = Speed;
                    _sbMove.Begin();

                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
