using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
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
    /// SwitchButton.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchButton : UserControl
    {
        private Storyboard sbOpen;
        private Storyboard sbClose;

        public SwitchButton()
        {
            InitializeComponent();
            this.SetResourceReference(ToggleGroup.FocusVisualStyleProperty, "LeeFocusVisual");
            this.SetResourceReference(ToggleGroup.SnapsToDevicePixelsProperty, "LeeSnapsToDevicePixels");

        }

        public event EventHandler<SwitchChangedEventArgs> SwitchChangedEvent;

        private bool _hasContent;

        public new bool HasContent
        {
            get { return _hasContent; }
            private set
            {
                _hasContent = value;
                if (value)
                {
                    switch (ContentDock)
                    {
                        case Dock.Left:
                            canvas.HorizontalAlignment = HorizontalAlignment.Right;
                            canvas.VerticalAlignment = VerticalAlignment.Center;
                            break;
                        case Dock.Top:
                            canvas.HorizontalAlignment = HorizontalAlignment.Center;
                            canvas.VerticalAlignment = VerticalAlignment.Top;
                            break;
                        case Dock.Right:
                            canvas.HorizontalAlignment = HorizontalAlignment.Left;
                            canvas.VerticalAlignment = VerticalAlignment.Center;
                            break;
                        case Dock.Bottom:
                            canvas.HorizontalAlignment = HorizontalAlignment.Center;
                            canvas.VerticalAlignment = VerticalAlignment.Bottom;
                            break;
                        default:
                            break;
                    }

                    BindingOperations.SetBinding(canvas, Canvas.HeightProperty, new Binding() { Source = this, Path = new PropertyPath("ButtonHeight") });
                    BindingOperations.SetBinding(canvas, Canvas.WidthProperty, new Binding() { Source = this, Path = new PropertyPath("ButtonWidth") });
                }
                else
                {
                    BindingOperations.ClearBinding(canvas, Canvas.HeightProperty);
                    BindingOperations.ClearBinding(canvas, Canvas.WidthProperty);
                    canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
                    canvas.VerticalAlignment = VerticalAlignment.Stretch;
                }
                SetGridSite();
            }
        }
        #region 依赖属性


        #region VerticalContentAlignment
        /// <summary>
        /// VerticalContentAlignment
        /// </summary>
        public new VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalContentAlignment.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register("VerticalContentAlignment", typeof(VerticalAlignment), typeof(SwitchButton), new PropertyMetadata(VerticalAlignment.Center));

        #endregion


        #region HorizontalContentAlignment
        /// <summary>
        /// HorizontalContentAlignment
        /// </summary>
        public new HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalContentAlignment.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(SwitchButton), new PropertyMetadata(HorizontalAlignment.Center));

        #endregion


        #region Switch

        public bool Switch
        {
            get { return (bool)GetValue(SwitchProperty); }
            set { SetValue(SwitchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Switch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SwitchProperty =
            DependencyProperty.Register("Switch", typeof(bool), typeof(SwitchButton), new PropertyMetadata(false, new PropertyChangedCallback(SwitchButtonChanged)));

        private static void SwitchButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SwitchButton @switch)
            {
                @switch.ControlStoryBoard(@switch.Switch);
                if (@switch.Switch)
                {
                    BindingOperations.ClearBinding(@switch.contentControl, ContentControl.ContentProperty);
                    BindingOperations.SetBinding(@switch.contentControl, ContentControl.ContentProperty, new Binding() { Source = @switch, Path = new PropertyPath("Content") });
                }
                else
                {
                    BindingOperations.ClearBinding(@switch.contentControl, ContentControl.ContentProperty);
                    BindingOperations.SetBinding(@switch.contentControl, ContentControl.ContentProperty, new Binding() { Source = @switch, Path = new PropertyPath("CloseContent") });
                }
            }
        }






        #endregion


        #region ButtonWidth
        /// <summary>
        /// 开关的宽度啊
        /// </summary>
        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register("ButtonWidth", typeof(double), typeof(SwitchButton), new PropertyMetadata(60.0));

        #endregion


        #region ButtonHeight
        /// <summary>
        /// 开关的高度
        /// </summary>
        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonHeightProperty =
            DependencyProperty.Register("ButtonHeight", typeof(double), typeof(SwitchButton), new PropertyMetadata(30.0));

        #endregion


        #region Content
        /// <summary>
        /// 内容
        /// </summary>
        public new object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(SwitchButton), new PropertyMetadata(null, new PropertyChangedCallback(ContentChanged)));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SwitchButton @switch && e.NewValue != e.OldValue)
            {
                if (@switch.Content == null && @switch.CloseContent == null)
                {
                    @switch.HasContent = false;
                    return;
                }
                if (e.NewValue is not FrameworkElement)
                {
                    @switch.Content = new TextBlock() { Text = e.NewValue.ToString() };
                }
                @switch.HasContent = true;
            }
        }


        #endregion



        #region CloseContent
        /// <summary>
        /// 请填写描述
        /// </summary>
        public object CloseContent
        {
            get { return (object)GetValue(CloseContentProperty); }
            set { SetValue(CloseContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseContentProperty =
            DependencyProperty.Register("CloseContent", typeof(object), typeof(SwitchButton), new PropertyMetadata(null, new PropertyChangedCallback(CloseContentChanged)));

        private static void CloseContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SwitchButton @switch && e.NewValue != e.OldValue)
            {
                if (@switch.Content == null && @switch.CloseContent == null)
                {
                    @switch.HasContent = false;
                    return;
                }
                if (e.NewValue is not FrameworkElement)
                {
                    @switch.CloseContent = new TextBlock() { Text = e.NewValue.ToString() };
                }
                @switch.HasContent = true;
            }
        }

        #endregion




        #region ContentDock
        /// <summary>
        /// 内容位置
        /// </summary>
        public Dock ContentDock
        {
            get { return (Dock)GetValue(ContentDockProperty); }
            set { SetValue(ContentDockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentDock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentDockProperty =
            DependencyProperty.Register("ContentDock", typeof(Dock), typeof(SwitchButton), new PropertyMetadata(Dock.Right, new PropertyChangedCallback(ContentDockChanged)));

        private static void ContentDockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SwitchButton @switch && e.NewValue != e.OldValue)
            {

                switch ((Dock)e.NewValue)
                {
                    case Dock.Top:
                        Grid.SetColumn(@switch.contentControl, 1);
                        Grid.SetRow(@switch.contentControl, 0);
                        break;
                    case Dock.Bottom:
                        Grid.SetColumn(@switch.contentControl, 1);
                        Grid.SetRow(@switch.contentControl, 2);
                        break;
                    case Dock.Left:
                        Grid.SetColumn(@switch.contentControl, 0);
                        Grid.SetRow(@switch.contentControl, 1);
                        break;
                    case Dock.Right:
                        Grid.SetColumn(@switch.contentControl, 2);
                        Grid.SetRow(@switch.contentControl, 1);
                        break;
                    default:
                        break;
                }
                @switch.SetGridSite();

            }
        }


        #endregion







        #endregion

        #region 内部逻辑
        private void me_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Switch = !Switch;
            SwitchChangedEvent?.Invoke(this, new SwitchChangedEventArgs() { Switch = this.Switch });
        }

        private void ControlStoryBoard(bool _switch)
        {

            if (!(boderFalse.RenderTransform is ScaleTransform scale))
                boderFalse.RenderTransform = new ScaleTransform();

            if (_switch)
            {
                if (sbClose != null)
                    sbClose.Stop();
                if (sbOpen != null)
                    sbOpen.Stop();
                sbOpen = new Storyboard();
                sbOpen.FillBehavior = FillBehavior.Stop;
                DoubleAnimation xDA = new DoubleAnimation()
                {
                    To = 0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                };
                DoubleAnimation yDA = new DoubleAnimation()
                {
                    To = 0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                };
                DoubleAnimationUsingKeyFrames eDA = new DoubleAnimationUsingKeyFrames();
                eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {

                    Value = canvas.ActualWidth - ellipse.ActualWidth - 1,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)),
                    EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut, Amplitude = 0.2 },
                });

                sbOpen.Children.Add(xDA);
                sbOpen.Children.Add(yDA);
                sbOpen.Children.Add(eDA);

                Storyboard.SetTarget(xDA, boderFalse);
                Storyboard.SetTargetProperty(xDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Border.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

                Storyboard.SetTarget(yDA, boderFalse);
                Storyboard.SetTargetProperty(yDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Border.RenderTransformProperty, ScaleTransform.ScaleYProperty }));


                Storyboard.SetTarget(eDA, ellipse);
                Storyboard.SetTargetProperty(eDA, new PropertyPath(Canvas.LeftProperty));
                boderTrue.BorderThickness = new Thickness(0);
                sbOpen.Completed += (e, s) =>
                {
                    Canvas.SetLeft(ellipse, canvas.ActualWidth - ellipse.ActualWidth - 1);
                    (boderFalse.RenderTransform as ScaleTransform).ScaleX = 0;
                    (boderFalse.RenderTransform as ScaleTransform).ScaleY = 0;
                };
                sbOpen.Begin();
            }
            else
            {
                if (sbOpen != null)
                    sbOpen.Stop();
                sbClose = new Storyboard();

                DoubleAnimation xDA = new DoubleAnimation()
                {
                    To = 1,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                };
                DoubleAnimation yDA = new DoubleAnimation()
                {
                    To = 1,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                };

                DoubleAnimationUsingKeyFrames eDA = new DoubleAnimationUsingKeyFrames();
                eDA.KeyFrames.Add(new EasingDoubleKeyFrame()
                {

                    Value = 1,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)),
                    EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut, Amplitude = 0.2 },
                });

                sbClose.Children.Add(xDA);
                sbClose.Children.Add(yDA);
                sbClose.Children.Add(eDA);

                Storyboard.SetTarget(xDA, boderFalse);
                Storyboard.SetTargetProperty(xDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Border.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

                Storyboard.SetTarget(yDA, boderFalse);
                Storyboard.SetTargetProperty(yDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Border.RenderTransformProperty, ScaleTransform.ScaleYProperty }));


                Storyboard.SetTarget(eDA, ellipse);
                Storyboard.SetTargetProperty(eDA, new PropertyPath(Canvas.LeftProperty));


                boderTrue.BorderThickness = new Thickness(2);

                sbClose.Begin();
            }
        }

        private void me_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Canvas.GetLeft(ellipse) > 3)
            {
                Canvas.SetLeft(ellipse, this.ActualWidth - ellipse.ActualWidth - 1);
            }
        }

        private void SetGridSite()
        {
            if (HasContent)
            {
                switch (ContentDock)
                {
                    case Dock.Top:
                        rowTop.Height = new GridLength(1, GridUnitType.Star);
                        rowContent.Height = GridLength.Auto;
                        rowBottom.Height = GridLength.Auto;

                        columnleft.Width = GridLength.Auto;
                        columnContent.Width = new GridLength(1, GridUnitType.Star);
                        columnright.Width = GridLength.Auto;
                        break;
                    case Dock.Bottom:

                        rowTop.Height = GridLength.Auto;
                        rowContent.Height = GridLength.Auto;
                        rowBottom.Height = new GridLength(1, GridUnitType.Star);

                        columnleft.Width = GridLength.Auto;
                        columnContent.Width = new GridLength(1, GridUnitType.Star);
                        columnright.Width = GridLength.Auto;

                        break;
                    case Dock.Left:
                        rowTop.Height = GridLength.Auto;
                        rowContent.Height = new GridLength(1, GridUnitType.Star);
                        rowBottom.Height = GridLength.Auto;

                        columnleft.Width = new GridLength(1, GridUnitType.Star);
                        columnContent.Width = GridLength.Auto;
                        columnright.Width = GridLength.Auto;
                        break;
                    case Dock.Right:
                        rowTop.Height = GridLength.Auto;
                        rowContent.Height = new GridLength(1, GridUnitType.Star);
                        rowBottom.Height = GridLength.Auto;

                        columnleft.Width = GridLength.Auto;
                        columnContent.Width = GridLength.Auto;
                        columnright.Width = new GridLength(1, GridUnitType.Star);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                rowTop.Height = GridLength.Auto;
                rowContent.Height = new GridLength(1, GridUnitType.Star);
                rowBottom.Height = GridLength.Auto;

                columnleft.Width = GridLength.Auto;
                columnContent.Width = new GridLength(1, GridUnitType.Star);
                columnright.Width = GridLength.Auto;
            }
        }
        #endregion
    }

    public class SwitchChangedEventArgs : EventArgs
    {
        public bool Switch { get; set; }
    }



}


