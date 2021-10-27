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
using System.Windows.Media.Effects;
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
    ///     <MyNamespace:ProgressRing/>
    ///
    /// </summary>
    public class ProgressRing : ContentControl
    {
        static ProgressRing()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));
        }


        private Path _outer;
        private Path _orbitBackground;
        private Path _orbit;
        private Border _border;
        private Path _wating;

        private Storyboard _lodingSB;
        private Storyboard _watingSB;
        private long _lastLodingTime = 0;



        private double _backDiameter;
        public ProgressRing()
        {
            SizeChanged += ProgressRing_SizeChanged;
            Loaded += ProgressRing_Loaded;

        }



        private void ProgressRing_Loaded(object sender, RoutedEventArgs e)
        {
            LodingIntView();
        }

        private void ProgressRing_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LodingIntView();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _outer = this.GetTemplateChild("PART_OuterBorder") as Path;
            _orbitBackground = this.GetTemplateChild("PART_OrbitBackgound") as Path;
            _orbit = this.GetTemplateChild("PATY_Orbit") as Path;
            _border = this.GetTemplateChild("PART_ContentBorder") as Border;
            _wating = this.GetTemplateChild("PATY_Wating") as Path;
            _wating.IsVisibleChanged += _wating_IsVisibleChanged;

            if (this.GetTemplateChild("PART_Grid") is Grid grid)
            {
                _watingSB = grid.Resources["SB"] as Storyboard;

            }
        }




        #region 依赖


        #region BorderThickness
        /// <summary>
        /// 请添加描述
        /// </summary>
        public new double BorderThickness
        {
            get { return (double)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderThickness.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(double), typeof(ProgressRing), new PropertyMetadata(OnBorderThicknessChanged));

        private static void OnBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressRing progress)
            {
                progress.LodingIntView();
            }
        }
        #endregion


        #region Thickness
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Thickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(ProgressRing), new PropertyMetadata(OnThicknessPropertyChanged));

        private static void OnThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressRing progress)
            {
                progress.LodingIntView();
            }
        }
        #endregion


        #region ThicknessPadding
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double ThicknessPadding
        {
            get { return (double)GetValue(ThicknessPaddingProperty); }
            set { SetValue(ThicknessPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThicknessPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessPaddingProperty =
            DependencyProperty.Register("ThicknessPadding", typeof(double), typeof(ProgressRing), new PropertyMetadata(OnThicknessPaddingPropertyChanged));

        private static void OnThicknessPaddingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressRing progress)
            {
                progress.LodingIntView();
            }
        }
        #endregion


        #region Effect
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Effect RingEffect
        {
            get { return (Effect)GetValue(RingEffectProperty); }
            set { SetValue(RingEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Effect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RingEffectProperty =
            DependencyProperty.Register("RingEffect", typeof(Effect), typeof(ProgressRing));
        #endregion

        #region IsClip
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsClip
        {
            get { return (bool)GetValue(IsClipProperty); }
            set { SetValue(IsClipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClipProperty =
            DependencyProperty.Register("IsClip", typeof(bool), typeof(ProgressRing));
        #endregion


        #region Value
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ProgressRing), new PropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressRing ring && e.OldValue != e.NewValue)
            {
                ring.LodingRing();
            }
        }
        #endregion



        #region RingValue
        /// <summary>
        /// 请添加描述
        /// </summary>
        internal double RingValue
        {
            get { return (double)GetValue(RingValueProperty); }
            set { SetValue(RingValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RingValue.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty RingValueProperty =
            DependencyProperty.Register("RingValue", typeof(double), typeof(ProgressRing), new PropertyMetadata(OnRingValueChanged));

        private static void OnRingValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressRing ring && e.OldValue != e.NewValue)
            {
                ring.ChangeRing((double)e.NewValue);
            }
        }
        #endregion



        #region Maximum
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(ProgressRing), new PropertyMetadata(OnMaximumChanged));

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressRing ring)
            {
                ring.LodingRing();
            }
        }
        #endregion



        #region Stroke
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(ProgressRing));
        #endregion


        #region Mode
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ProgressControlMode Mode
        {
            get { return (ProgressControlMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(ProgressControlMode), typeof(ProgressRing), new PropertyMetadata(OnModePropertyChanged));

        private static void OnModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressRing progress)
            {
                progress.LodingWating();
            }
        }
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
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(ProgressRing));

        #endregion

        #endregion



        private void LodingIntView()
        {
            if (!IsLoaded || Visibility != Visibility.Visible)
                return;

            ///直径 =最小单位-边框厚度
            var diameter = (this.ActualWidth >= this.ActualHeight ? this.ActualHeight : this.ActualWidth) - BorderThickness;
            _outer.Data = GetRoundPath(360, diameter / 2, new Point(this.ActualWidth / 2, BorderThickness / 2));

            ///背景直径
            _orbitBackground.StrokeThickness = Thickness + (ThicknessPadding * 2);
            _backDiameter = diameter - _orbitBackground.StrokeThickness - BorderThickness;
            _orbitBackground.Data = GetRoundPath(360, _backDiameter / 2, new Point(this.ActualWidth / 2, _orbitBackground.StrokeThickness / 2 + BorderThickness));

            ///加载边框
            _border.BorderThickness = new Thickness(BorderThickness);
            _border.Height = diameter - _orbitBackground.StrokeThickness * 2 - BorderThickness;
            _border.Width = _border.Height;
            _border.CornerRadius = new CornerRadius(_border.Width / 2);

            switch (Mode)
            {
                case ProgressControlMode.Loding:
                    LodingRing();
                    break;
                case ProgressControlMode.Wating:
                    LodingWating();
                    break;
                default:
                    break;
            }

        }

        private void LodingRing()
        {
            if (!IsLoaded || Mode != ProgressControlMode.Loding || Visibility != Visibility.Visible)
                return;

            var angel = Value / Maximum * 360;

            if ((DateTime.Now.ToFileTime() - _lastLodingTime) < 2000000)
            {
                _lodingSB?.Stop();
                _lastLodingTime = DateTime.Now.ToFileTime();

                RingValue = angel;
                return;
            }
            _lastLodingTime = DateTime.Now.ToFileTime();
            _lodingSB = new Storyboard() { FillBehavior = FillBehavior.Stop };
            DoubleAnimationUsingKeyFrames eDA = new DoubleAnimationUsingKeyFrames();
            eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                Value = angel,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)),
                EasingFunction = EasingFunction,
            });
            _lodingSB.Children.Add(eDA);
            Storyboard.SetTarget(eDA, this);
            Storyboard.SetTargetProperty(eDA, new PropertyPath(ProgressRing.RingValueProperty));
            _lodingSB.Completed += (e, s) =>
            {
                RingValue = angel;
            };
            _lodingSB.Begin();

        }

        private void ChangeRing(double value)
        {

            _orbit.Data = GetRoundPath(value, _backDiameter / 2, new Point(this.ActualWidth / 2, _orbit.StrokeThickness / 2 + BorderThickness + ThicknessPadding));

        }

        private void LodingWating()
        {
            if (!IsLoaded || Mode != ProgressControlMode.Wating || Visibility != Visibility.Visible)
                return;

            var angel = 1;
            _wating.Data = GetRoundPath(angel, _backDiameter / 2, new Point(this.ActualWidth / 2, _orbit.StrokeThickness / 2 + BorderThickness + ThicknessPadding));

            _watingSB.Begin();

        }
        private void _wating_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_wating.Visibility == Visibility.Visible)
            {
                _watingSB.Begin();
            }
            else
            {
                _watingSB.Stop();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angel">0~360</param>
        /// <param name="radius">半径</param>
        /// <param name="startPt">开始坐标</param>
        /// <param name="sweep">顺逆</param>
        /// <returns></returns>
        private PathGeometry GetRoundPath(double angel, double radius, Point startPt, SweepDirection sweep = SweepDirection.Clockwise)
        {

            if (angel > 360)
                angel = 360;

            var endPt = new Point();

            bool isLagreCircel = false;  //是否优势弧？？？
            double ra;//弧度
#if NET5_0_OR_GREATER
            switch (angel)
            {
                case <= 90:
                    ra = (90 - angel) * Math.PI / 180;
                    endPt.X = startPt.X + Math.Cos(ra) * radius;
                    endPt.Y = startPt.Y + radius - Math.Sin(ra) * radius;
                    break;
                case <= 180:
                    ra = (angel - 90) * Math.PI / 180;
                    endPt.X = startPt.X + Math.Cos(ra) * radius;
                    endPt.Y = startPt.Y + radius + Math.Sin(ra) * radius;
                    break;

                case <= 270:
                    isLagreCircel = true;
                    ra = (angel - 180) * Math.PI / 180;
                    endPt.X = startPt.X - Math.Sin(ra) * radius;
                    endPt.Y = startPt.Y + radius + Math.Cos(ra) * radius;
                    break;

                case < 360:
                    isLagreCircel = true;
                    ra = (angel - 270) * Math.PI / 180;
                    endPt.X = startPt.X - Math.Cos(ra) * radius;
                    endPt.Y = startPt.Y + radius - Math.Sin(ra) * radius;
                    break;

                default:
                    isLagreCircel = true;
                    endPt.X = startPt.X - 0.001;
                    endPt.Y = startPt.Y;
                    break;
            }
#else
    if (angel<=90)
	{
                    ra = (90 - angel) * Math.PI / 180;
                    endPt.X = startPt.X + Math.Cos(ra) * radius;
                    endPt.Y = startPt.Y + radius - Math.Sin(ra) * radius;
	} else if (angel<= 180)
	{
     ra = (angel - 90) * Math.PI / 180;
                    endPt.X = startPt.X + Math.Cos(ra) * radius;
                    endPt.Y = startPt.Y + radius + Math.Sin(ra) * radius;
	} else if (angel<= 270)
    {
                        isLagreCircel = true;
                    ra = (angel - 180) * Math.PI / 180;
                    endPt.X = startPt.X - Math.Sin(ra) * radius;
                    endPt.Y = startPt.Y + radius + Math.Cos(ra) * radius;
    } else if(angel< 360)
    {
     isLagreCircel = true;
                    ra = (angel - 270) * Math.PI / 180;
                    endPt.X = startPt.X - Math.Cos(ra) * radius;
                    endPt.Y = startPt.Y + radius - Math.Sin(ra) * radius;
     }else
     {
                    isLagreCircel = true;
                    endPt.X = startPt.X - 0.001;
                    endPt.Y = startPt.Y;
     }
#endif
            PathGeometry pathGeometry = new PathGeometry()
            {
                Figures = new PathFigureCollection{
                    new PathFigure{
                        StartPoint = startPt,
                        Segments = new PathSegmentCollection{
                            new ArcSegment(endPt, new Size(radius, radius), 0, isLagreCircel, sweep, true)
                        },
                        IsClosed= angel>=360
                    }
                }
            };

            return pathGeometry;
            //   Data = pathGeometry;

        }
    }
}
