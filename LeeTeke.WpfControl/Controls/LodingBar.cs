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
    ///     <MyNamespace:LodingBar/>
    ///
    /// </summary>
    public class LodingBar : Control
    {
        static LodingBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LodingBar), new FrameworkPropertyMetadata(typeof(LodingBar)));
        }

        private Storyboard valueSB;
        private Storyboard vWatingSB;
        private Storyboard hWatingSB;

        private Canvas _canvas;
        private Rectangle _horizontalRect;
        private Rectangle _verticalRect;
        private Rectangle _watingVerticalRect;
        private Rectangle _watingHorizontalRect;

        private bool lodingGo = false;
        private bool goTwoSB = true;
        public LodingBar()
        {
            Loaded += LodingBar_Loaded;
        }




        #region 依赖

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LodingBar), new PropertyMetadata(new CornerRadius(2)));



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
            DependencyProperty.Register("Maximum", typeof(double), typeof(LodingBar), new PropertyMetadata(100.0, new PropertyChangedCallback(MaximumChanged)));

        private static void MaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LodingBar loding && e.NewValue != e.OldValue)
            {
                if (e.NewValue is double value && value >= 0)
                {
                    loding.ValueChangedAsync();
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
            DependencyProperty.Register("Value", typeof(double), typeof(LodingBar), new PropertyMetadata(0.0, new PropertyChangedCallback(ValueChanged)));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LodingBar loding && e.NewValue != e.OldValue)
            {
                loding.ValueChangedAsync();
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
            DependencyProperty.Register("FillBrush", typeof(Brush), typeof(LodingBar));

        #endregion

        #region Mode
        /// <summary>
        /// Mode
        /// </summary>
        public LodingBarMode Mode
        {
            get { return (LodingBarMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(LodingBarMode), typeof(LodingBar), new PropertyMetadata(LodingBarMode.Loding, new PropertyChangedCallback(ModeChanged)));

        private static void ModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LodingBar loding && e.NewValue != e.OldValue)
            {
                loding.FormChangedAsync();
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
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(LodingBar), new PropertyMetadata(Orientation.Horizontal, new PropertyChangedCallback(OrientationChanged)));

        private static void OrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LodingBar loding && e.NewValue != e.OldValue)
            {
                loding.FormChangedAsync();
            }
        }

        #endregion

        #endregion


        #region 私有逻辑

        private void LodingBar_Loaded(object sender, RoutedEventArgs e)
        {

            _canvas = this.Template.FindName("PART_Canvas", this) as Canvas;
            _horizontalRect = this.Template.FindName("PART_HRect", this) as Rectangle;
            _verticalRect = this.Template.FindName("PART_VRect", this) as Rectangle;
            _watingVerticalRect = this.Template.FindName("PART_WVRect", this) as Rectangle;
            _watingHorizontalRect = this.Template.FindName("PART_WHRect", this) as Rectangle;

            if (lodingGo)
                ValueChangedAsync();

            if (Mode == LodingBarMode.Wating)
            {
                WatingStoryboardLodingAsync();
            }
        }

        private async void WatingStoryboardLodingAsync()
        {
            try
            {
                while (!IsLoaded)
                {
                    await Task.Delay(1);
                }
                if (vWatingSB != null)
                    vWatingSB.Pause();

                if (hWatingSB != null)
                    hWatingSB.Pause();

                vWatingSB = new Storyboard()
                {
                    FillBehavior = FillBehavior.Stop
                };
                DoubleAnimationUsingKeyFrames vDA = new DoubleAnimationUsingKeyFrames();
                vDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = _canvas.ActualHeight,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000)),
                    EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut, },
                });
                vWatingSB.Children.Add(vDA);
                Storyboard.SetTarget(vDA, _watingVerticalRect);
                Storyboard.SetTargetProperty(vDA, new PropertyPath(Canvas.TopProperty));
                vWatingSB.Completed += async (e, s) =>
                {
                    if (Mode == LodingBarMode.Wating && Orientation == Orientation.Vertical)
                    {
                        if (!goTwoSB)
                            await Task.Delay(500);
                        Canvas.SetTop(_watingVerticalRect, _watingVerticalRect.Height * -1);
                        if (vWatingSB.Children[0] is DoubleAnimationUsingKeyFrames vdu)
                        {
                            vdu.KeyFrames[0].Value = _canvas.ActualHeight;
                        }
                        goTwoSB = !goTwoSB;
                        vWatingSB.Begin();
                    }
                };


                hWatingSB = new Storyboard()
                {
                    FillBehavior = FillBehavior.Stop
                };
                DoubleAnimationUsingKeyFrames hDA = new DoubleAnimationUsingKeyFrames();
                hDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = _canvas.ActualWidth,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000)),
                    EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut, },
                });
                hWatingSB.Children.Add(hDA);
                Storyboard.SetTarget(hDA, _watingHorizontalRect);
                Storyboard.SetTargetProperty(hDA, new PropertyPath(Canvas.LeftProperty));

                hWatingSB.Completed += async (e, s) =>
                {
                    if (Mode == LodingBarMode.Wating && Orientation == Orientation.Horizontal)
                    {
                        if (!goTwoSB)
                            await Task.Delay(500);
                        Canvas.SetLeft(_watingHorizontalRect, _watingHorizontalRect.Width * -1);
                        if (hWatingSB.Children[0] is DoubleAnimationUsingKeyFrames hdu)
                        {
                            hdu.KeyFrames[0].Value = _canvas.ActualWidth;
                        }
                        goTwoSB = !goTwoSB;
                        hWatingSB.Begin();
                    }
                };

                if (Mode == LodingBarMode.Wating)
                {
                    switch (Orientation)
                    {
                        case Orientation.Horizontal:
                            hWatingSB.Begin();
                            break;
                        case Orientation.Vertical:
                            vWatingSB.Begin();
                            break;
                        default:
                            break;
                    }
                }


            }
            catch (Exception)
            {

            }

        }

        private async void ValueChangedAsync()
        {
            try
            {
                while (!IsLoaded)
                {
                    await Task.Delay(1);
                }
                if (Mode == LodingBarMode.Loding)
                {
                    if (Maximum == 0)
                    {
                        switch (Orientation)
                        {
                            case Orientation.Horizontal:
                                _horizontalRect.Width = 0;
                                break;
                            case Orientation.Vertical:
                                _verticalRect.Width = 0;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (Maximum > 0)
                    {
                        if (Value >= 0)
                        {
                            if (valueSB != null)
                                valueSB.Pause();
                            valueSB = new Storyboard();

                            double max = Orientation switch
                            {
                                Orientation.Vertical => _canvas.ActualHeight,
                                Orientation.Horizontal => _canvas.ActualWidth,
                                _ => -1
                            };
                            if (max < 0)
                                return;
                            if (max == 0 && Value > 0)
                            {
                                lodingGo = true;
                                return;
                            }

                            double to = (this.Value / this.Maximum) * max;
                            DoubleAnimationUsingKeyFrames eDA = new DoubleAnimationUsingKeyFrames();
                            eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                            {
                                Value = to,
                                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)),
                                EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, },
                            });

                            switch (Orientation)
                            {
                                case Orientation.Horizontal:
                                    Storyboard.SetTarget(eDA, _horizontalRect);
                                    Storyboard.SetTargetProperty(eDA, new PropertyPath("Width"));
                                    break;
                                case Orientation.Vertical:
                                    Storyboard.SetTarget(eDA, _verticalRect);
                                    Storyboard.SetTargetProperty(eDA, new PropertyPath("Height"));
                                    break;
                                default:
                                    break;
                            }

                            valueSB.Children.Add(eDA);
                            valueSB.Begin();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private async void FormChangedAsync()
        {
            try
            {
                while (!IsLoaded)
                {
                   await Task.Delay(1);
                }

                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        switch (Mode)
                        {
                            case LodingBarMode.Loding:
                                _watingHorizontalRect.Visibility = Visibility.Collapsed;
                                _watingVerticalRect.Visibility = Visibility.Collapsed;
                                _verticalRect.Visibility = Visibility.Collapsed;
                                _horizontalRect.Visibility = Visibility.Visible;
                                ValueChangedAsync();
                                break;
                            case LodingBarMode.Wating:
                                _watingHorizontalRect.Visibility = Visibility.Visible;
                                _watingVerticalRect.Visibility = Visibility.Collapsed;
                                _verticalRect.Visibility = Visibility.Collapsed;
                                _horizontalRect.Visibility = Visibility.Collapsed;
                                WatingStoryboardLodingAsync(); break;
                            default:
                                break;
                        }
                        break;
                    case Orientation.Vertical:
                        switch (Mode)
                        {
                            case LodingBarMode.Loding:
                                _watingHorizontalRect.Visibility = Visibility.Collapsed;
                                _watingVerticalRect.Visibility = Visibility.Collapsed;
                                _verticalRect.Visibility = Visibility.Visible;
                                _horizontalRect.Visibility = Visibility.Collapsed;
                                ValueChangedAsync();
                                break;
                            case LodingBarMode.Wating:
                                _watingHorizontalRect.Visibility = Visibility.Collapsed;
                                _watingVerticalRect.Visibility = Visibility.Visible;
                                _verticalRect.Visibility = Visibility.Collapsed;
                                _horizontalRect.Visibility = Visibility.Collapsed;
                                WatingStoryboardLodingAsync();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }

        }

        #endregion
    }
}
