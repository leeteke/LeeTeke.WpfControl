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
    ///     <MyNamespace:SwitchButton/>
    ///
    /// </summary>
    public class SwitchButton : ContentControl
    {
        static SwitchButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchButton), new FrameworkPropertyMetadata(typeof(SwitchButton)));
        }


        private Storyboard sbOpen;
        private Storyboard sbClose;

        private Canvas _canvas;
        private Border _openBorder;
        private Border _closeBorder;
        private Ellipse _ellipse;

        public SwitchButton()
        {
            Loaded += SwitchButton_Loaded;
            MouseDown += SwitchButton_MouseDown;
        }



        private void SwitchButton_Loaded(object sender, RoutedEventArgs e)
        {
            _canvas = this.Template.FindName("PART_Canvas", this) as Canvas;
            _closeBorder = this.Template.FindName("PART_CloseBorder", this) as Border;
            _openBorder = this.Template.FindName("PART_OpenBorder", this) as Border;
            _ellipse = this.Template.FindName("PART_Ellipse", this) as Ellipse;
            if (_canvas != null)
            {
                _canvas.SizeChanged += me_SizeChanged;
            }
        }

        #region RouteEvent

        #region SwitchChanged
        /// <summary>
        /// 开关发生
        /// </summary>
        public event SwitchChangedEventHandler SwitchChanged
        {
            add { AddHandler(SwitchChangedEvent, value); }
            remove { RemoveHandler(SwitchChangedEvent, value); }
        }

        public static readonly RoutedEvent SwitchChangedEvent = EventManager.RegisterRoutedEvent(
        "SwitchChanged", RoutingStrategy.Bubble, typeof(EventHandler<SwitchChangedEventHandler>), typeof(SwitchButton));


        private void RaiseSwitchChanged(bool newValue)
        {
            var arg = new SwitchChangedEventArgs(newValue, SwitchChangedEvent);
            RaiseEvent(arg);
        }

        #endregion

        #endregion




        #region 依赖属性

        #region HasContent
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool HasContent
        {
            get { return (bool)GetValue(HasContentProperty); }
            private set { SetValue(HasContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasContentProperty =
            DependencyProperty.Register("HasContent", typeof(bool), typeof(SwitchButton));

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
                @switch.ControlStoryBoardAsync(@switch.Switch);
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
        /// 请填写描述
        /// </summary>
        public new object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(SwitchButton), new PropertyMetadata(null, new PropertyChangedCallback(ContentChanged)));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SwitchButton @switch && e.NewValue != e.OldValue)
            {
                if (e.NewValue== null && @switch.CloseContent == null)
                {
                    @switch.HasContent = false;
                    return;
                }

                if (e.NewValue == null)
                {
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
                if (e.NewValue== null && @switch.CloseContent == null)
                {
                    @switch.HasContent = false;
                    return;
                }

                if (e.NewValue == null)
                {
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
            DependencyProperty.Register("ContentDock", typeof(Dock), typeof(SwitchButton), new PropertyMetadata(Dock.Right));





        #endregion

        #endregion

        #region 内部逻辑
        private void SwitchButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Switch = !Switch;
            RaiseSwitchChanged(Switch);
        }

        private async void ControlStoryBoardAsync(bool _switch)
        {

            while (!IsLoaded)
            {
                await Task.Delay(1);
            }

            if (!(_closeBorder.RenderTransform is ScaleTransform scale))
                _closeBorder.RenderTransform = new ScaleTransform();

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

                    Value = _canvas.ActualWidth - _ellipse.ActualWidth - 1,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)),
                    EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut, Amplitude = 0.2 },
                });

                sbOpen.Children.Add(xDA);
                sbOpen.Children.Add(yDA);
                sbOpen.Children.Add(eDA);

                Storyboard.SetTarget(xDA, _closeBorder);
                Storyboard.SetTargetProperty(xDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Border.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

                Storyboard.SetTarget(yDA, _closeBorder);
                Storyboard.SetTargetProperty(yDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Border.RenderTransformProperty, ScaleTransform.ScaleYProperty }));


                Storyboard.SetTarget(eDA, _ellipse);
                Storyboard.SetTargetProperty(eDA, new PropertyPath(Canvas.LeftProperty));
                _openBorder.BorderThickness = new Thickness(0);
                sbOpen.Completed += (e, s) =>
                {
                    Canvas.SetLeft(_ellipse, _canvas.ActualWidth - _ellipse.ActualWidth - 1);
                    (_closeBorder.RenderTransform as ScaleTransform).ScaleX = 0;
                    (_closeBorder.RenderTransform as ScaleTransform).ScaleY = 0;
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

                Storyboard.SetTarget(xDA, _closeBorder);
                Storyboard.SetTargetProperty(xDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Border.RenderTransformProperty, ScaleTransform.ScaleXProperty }));

                Storyboard.SetTarget(yDA, _closeBorder);
                Storyboard.SetTargetProperty(yDA, new PropertyPath("(0).(1)", new DependencyProperty[] { Border.RenderTransformProperty, ScaleTransform.ScaleYProperty }));


                Storyboard.SetTarget(eDA, _ellipse);
                Storyboard.SetTargetProperty(eDA, new PropertyPath(Canvas.LeftProperty));


                _openBorder.BorderThickness = new Thickness(2);

                sbClose.Begin();
            }
        }

        private void me_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Canvas.GetLeft(_ellipse) > 3)
            {
                Canvas.SetLeft(_ellipse, _canvas.ActualWidth - _ellipse.ActualWidth - 1);
            }
        }


        #endregion

    }
}
