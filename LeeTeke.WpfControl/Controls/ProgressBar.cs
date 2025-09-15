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
    ///     <MyNamespace:LoadingBar/>
    ///
    /// </summary>
    public class ProgressBar : System.Windows.Controls.Control
    {
        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }


        private Storyboard? _loadingSB;
        public ProgressBar()
        {
            this.SizeChanged += LoadingBar_SizeChanged;
            this.IsVisibleChanged += LoadingBar_IsVisibleChanged;
        }

        private void LoadingBar_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                switch (Mode)
                {
                    case ProgressControlMode.Wating:
                        LoadingStoryboard(true);
                        break;
                    default:
                        break;
                }
            }
            else
            {

                _loadingSB?.Stop();
            }

        }

        private void LoadingBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LoadingStoryboard(true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


        }

        #region 属性

        #region ProgressLength
        /// <summary>
        /// 请填写描述
        /// </summary>
        public double ProgressLength
        {
            get { return (double)GetValue(ProgressLengthProperty); }
            private set { SetValue(ProgressLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressLengthProperty =
            DependencyProperty.Register("ProgressLength", typeof(double), typeof(ProgressBar));

        #endregion



        #region RectangleSite
        /// <summary>
        /// 请填写描述
        /// </summary>
        public double RectangleSite
        {
            get { return (double)GetValue(RectangleSiteProperty); }
            private set { SetValue(RectangleSiteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RectangleSite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RectangleSiteProperty =
            DependencyProperty.Register("RectangleSite", typeof(double), typeof(ProgressBar));

        #endregion


        #endregion

        #region 依赖


        #region WatingStop
        /// <summary>
        /// WatingStop
        /// </summary>
        public bool WatingStop
        {
            get { return (bool)GetValue(WatingStopProperty); }
            set { SetValue(WatingStopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WatingStop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatingStopProperty =
            DependencyProperty.Register("WatingStop", typeof(bool), typeof(ProgressBar), new PropertyMetadata(false, new PropertyChangedCallback(WatinStopChanged)));

        private static void WatinStopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressBar loading && e.NewValue is bool isStop)
            {
                if (loading.Mode == ProgressControlMode.Wating)
                {
                    if (isStop)
                    {
                        loading._loadingSB?.Stop();
                    }
                    else
                    {
                        loading.LoadingStoryboard();
                    }
                }
            }
        }
        #endregion


        #region CornerRadius
        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ProgressBar));



        #endregion

        #region Maximum
        /// <summary>
        /// Maximum
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(ProgressBar), new PropertyMetadata(100.0, new PropertyChangedCallback(MaximumChanged)));

        private static void MaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressBar loading && e.NewValue != e.OldValue)
            {
                if (e.NewValue is double value && value >= 0)
                {

                }
                else
                {
                    loading.Maximum = 1;
                }
            }
        }

        #endregion

        #region Value
        /// <summary>
        /// Value
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ProgressBar), new PropertyMetadata(0.0, new PropertyChangedCallback(ValuePropertyChanged)));

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressBar loading && e.NewValue != e.OldValue && e.NewValue is double value)
            {
                if (value < 0)
                {
                    loading.Value = 0;
                    return;
                }
                if (value > loading.Maximum)
                {
                    loading.Value = loading.Maximum;
                    return;
                }

                loading.RaiseValueChanged(value);
                if (loading.Mode == ProgressControlMode.Loading)
                {
                    loading.LoadingStoryboard();
                }

            }
        }


        #endregion

        #region FillBrush
        /// <summary>
        /// FillBrsh
        /// </summary>
        public Brush FillBrush
        {
            get { return (Brush)GetValue(FillBrushProperty); }
            set { SetValue(FillBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FillBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillBrushProperty =
            DependencyProperty.Register("FillBrush", typeof(Brush), typeof(ProgressBar));

        #endregion

        #region Mode
        /// <summary>
        /// Mode
        /// </summary>
        public ProgressControlMode Mode
        {
            get { return (ProgressControlMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(ProgressControlMode), typeof(ProgressBar), new PropertyMetadata(ProgressControlMode.Loading, ModeChanged));

        private static void ModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressBar loading)
            {
                loading.LoadingStoryboard();
            }
        }


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
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ProgressBar));


        #endregion

        #region EasingFunction
        /// <summary>
        /// 请填写描述
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(ProgressBar));


        #endregion

        #region Duration
        /// <summary>
        /// Duration
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(ProgressBar));
        #endregion


        #endregion

        #region RouteEvent


        #region ValueChanged
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event ProgressControlValueChangedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
        "ValueChanged", RoutingStrategy.Bubble, typeof(ProgressControlValueChangedEventHandler), typeof(ProgressBar));


        private void RaiseValueChanged(double newValue)
        {
            var arg = new ProgressControlValueChangedEventArgs(newValue, ValueChangedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #endregion

        #region 私有逻辑

        private double ProgressGenCalculation()
        {
            try
            {
                return Orientation switch
                {
                    Orientation.Horizontal => this.ActualWidth - BorderThickness.Left - BorderThickness.Right - Padding.Left - Padding.Right,
                    Orientation.Vertical => this.ActualHeight - BorderThickness.Top - BorderThickness.Bottom - Padding.Top - Padding.Bottom,
                    _ => 0,
                };
            }
            catch
            {
                return 0;
            }
        }


        private void LoadingStoryboard(bool size = false)
        {
            if (size)
            {
                ///勿删，用来初始化加载的
            }
            else if (!IsLoaded)
            {
                return;
            }
            if (Mode == ProgressControlMode.Loading)
            {
                var plc = Value * (ProgressGenCalculation() / Maximum);

                var animation = new DoubleAnimation(plc, Duration)
                {
                    EasingFunction = this.EasingFunction,
                    FillBehavior = FillBehavior.Stop
                };
                animation.Completed += (e, s) =>
                {
                    ProgressLength = plc;
                };
                BeginAnimation(ProgressLengthProperty, animation, HandoffBehavior.Compose);

            }
            else
            {
                if (_loadingSB != null)
                    _loadingSB.Pause();

                if (WatingStop)
                    return;
                _loadingSB = new Storyboard() { RepeatBehavior = RepeatBehavior.Forever };

                DoubleAnimationUsingKeyFrames eDA = new DoubleAnimationUsingKeyFrames();
                eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = -(ProgressGenCalculation() / 3),
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)),
                });
                eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = ProgressGenCalculation(),
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3)),
                });
                eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = -(ProgressGenCalculation() / 3),
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3)),
                });
                eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = ProgressGenCalculation(),
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)),
                });
                eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = ProgressGenCalculation(),
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.3)),
                });

                _loadingSB.Children.Add(eDA);
                Storyboard.SetTarget(eDA, this);
                Storyboard.SetTargetProperty(eDA, new PropertyPath(ProgressBar.RectangleSiteProperty));

                _loadingSB.Begin();
            }
        }
        #endregion



    }
}
