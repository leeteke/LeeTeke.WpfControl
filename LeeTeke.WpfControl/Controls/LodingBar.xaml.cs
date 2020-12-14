using System;
using System.Collections.Generic;
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
    /// LodingBar.xaml 的交互逻辑
    /// </summary>
    public partial class LodingBar : UserControl
    {
        private Storyboard valueSB;
        private Storyboard vWatingSB;
        private Storyboard hWatingSB;
        private bool lodingGo = false;
        private bool goTwoSB = true;
        public LodingBar()
        {
            InitializeComponent();
            border.DataContext = this;
            this.SetResourceReference(LodingBar.FillBrushProperty, "LeeBrush_Main");
            this.SetResourceReference(ToggleGroup.FocusVisualStyleProperty, "LeeFocusVisual");
            this.SetResourceReference(ToggleGroup.SnapsToDevicePixelsProperty, "LeeSnapsToDevicePixels");
            this.Loaded += LodingBar_Loaded;
        }



        #region 依赖属性

        #region Background
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(LodingBar), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        #endregion

        #region BorderBrush
        /// <summary>
        /// BorderBrush
        /// </summary>
        public new Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(LodingBar), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        #endregion

        #region Padding
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Padding.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register("Padding", typeof(Thickness), typeof(LodingBar));

        #endregion

        #region BorderThickness
        /// <summary>
        /// BorderThickness
        /// </summary>
        public new Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderThickness.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(LodingBar), new PropertyMetadata(new Thickness(1)));

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LodingBar), new PropertyMetadata(new CornerRadius(2), new PropertyChangedCallback(CornerRadiusChanged)));

        private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LodingBar loding && e.NewValue != e.OldValue)
            {
                loding.canves_SizeChanged(null, null);
            }
        }



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
                    loding.ValueChanged();
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
                    loding.ValueChanged();
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
                loding.FormChanged();
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
                loding.FormChanged();
            }
        }

        #endregion

        #endregion

        #region 私有逻辑

        private void LodingBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (lodingGo)
                ValueChanged();

            if (Mode == LodingBarMode.Wating)
            {
                WatingStoryboardLoding();
            }
        }

        private void WatingStoryboardLoding()
        {
            try
            {

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
                Value = canves.ActualHeight,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000)),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut, },
            });
            vWatingSB.Children.Add(vDA);
            Storyboard.SetTarget(vDA, watingVerticalRecet);
            Storyboard.SetTargetProperty(vDA, new PropertyPath(Canvas.TopProperty));
            vWatingSB.Completed += async (e, s) =>
            {
                if (Mode == LodingBarMode.Wating && Orientation == Orientation.Vertical)
                {
                    if (!goTwoSB)
                        await Task.Delay(500);
                    Canvas.SetTop(watingVerticalRecet, watingVerticalRecet.Height * -1);
                    if (vWatingSB.Children[0] is DoubleAnimationUsingKeyFrames vdu)
                    {
                        vdu.KeyFrames[0].Value = canves.ActualHeight;
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
                Value = canves.ActualWidth,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000)),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut, },
            });
            hWatingSB.Children.Add(hDA);
            Storyboard.SetTarget(hDA, watingHorizontalRecet);
            Storyboard.SetTargetProperty(hDA, new PropertyPath(Canvas.LeftProperty));

            hWatingSB.Completed += async (e, s) =>
            {
                if (Mode == LodingBarMode.Wating && Orientation == Orientation.Horizontal)
                {
                    if (!goTwoSB)
                        await Task.Delay(500);
                    Canvas.SetLeft(watingHorizontalRecet, watingHorizontalRecet.Width * -1);
                    if (hWatingSB.Children[0] is DoubleAnimationUsingKeyFrames hdu)
                    {
                        hdu.KeyFrames[0].Value = canves.ActualWidth;
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

        private void ValueChanged()
        {
            try
            {

         
            if (Mode == LodingBarMode.Loding)
            {
                if (Maximum == 0)
                {
                    switch (Orientation)
                    {
                        case Orientation.Horizontal:
                            horizontalRect.Width = 0;
                            break;
                        case Orientation.Vertical:
                            verticalRect.Width = 0;
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
                            Orientation.Vertical => canves.ActualHeight,
                            Orientation.Horizontal => canves.ActualWidth,
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
                                Storyboard.SetTarget(eDA, horizontalRect);
                                Storyboard.SetTargetProperty(eDA, new PropertyPath("Width"));
                                break;
                            case Orientation.Vertical:
                                Storyboard.SetTarget(eDA, verticalRect);
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

        private void FormChanged()
        {
            try
            {

       
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    switch (Mode)
                    {
                        case LodingBarMode.Loding:
                            watingHorizontalRecet.Visibility = Visibility.Collapsed;
                            watingVerticalRecet.Visibility = Visibility.Collapsed;
                            verticalRect.Visibility = Visibility.Collapsed;
                            horizontalRect.Visibility = Visibility.Visible;
                            ValueChanged();
                            break;
                        case LodingBarMode.Wating:
                            watingHorizontalRecet.Visibility = Visibility.Visible;
                            watingVerticalRecet.Visibility = Visibility.Collapsed;
                            verticalRect.Visibility = Visibility.Collapsed;
                            horizontalRect.Visibility = Visibility.Collapsed;
                            WatingStoryboardLoding(); break;
                        default:
                            break;
                    }
                    break;
                case Orientation.Vertical:
                    switch (Mode)
                    {
                        case LodingBarMode.Loding:
                            watingHorizontalRecet.Visibility = Visibility.Collapsed;
                            watingVerticalRecet.Visibility = Visibility.Collapsed;
                            verticalRect.Visibility = Visibility.Visible;
                            horizontalRect.Visibility = Visibility.Collapsed;
                            ValueChanged();
                            break;
                        case LodingBarMode.Wating:
                            watingHorizontalRecet.Visibility = Visibility.Collapsed;
                            watingVerticalRecet.Visibility = Visibility.Visible;
                            verticalRect.Visibility = Visibility.Collapsed;
                            horizontalRect.Visibility = Visibility.Collapsed;
                            WatingStoryboardLoding();
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




        private async void HStoryboard_Completed(object sender, EventArgs e)
        {
            if (Resources["watingHStoryboard"] is Storyboard storyboard)
            {
                await Task.Delay(500);
                storyboard.Begin();
            }
        }



        private async void VStoryboard_Completed(object sender, EventArgs e)
        {
            if (Resources["watingVStoryboard"] is Storyboard storyboard)
            {

                await Task.Delay(500);
                storyboard.Begin();
            }
        }



        private void canves_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LeeTeke.WpfControl.Dependencies.ClipManager.SetCornerRadius(canves, CornerRadius);
        }
        #endregion
    }

    public enum LodingBarMode
    {
        Loding,
        Wating,
    }
}
