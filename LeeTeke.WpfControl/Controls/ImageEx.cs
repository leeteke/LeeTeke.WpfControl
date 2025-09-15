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
    ///     <MyNamespace:ImageEx/>
    ///
    /// </summary>
    public class ImageEx : System.Windows.Controls.Control
    {
        #region 字段
        private const string ElementDefaultContent = "PART_DefaultContent";
        private const string ElementImage = "PART_Image";
        private const string ElementFailedContent = "PART_FailedContent";
        #endregion

        private ContentPresenter? _defaultContent;
        private ContentPresenter? _failedContent;
        private Image? _image;

        public Image Image => _image!;
        public ImageEx()
        {

        }


        static ImageEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageEx), new FrameworkPropertyMetadata(typeof(ImageEx)));
        }


        #region Override

        public override void OnApplyTemplate()
        {

            if (_image != null)
            {
                _image.ImageFailed -= _image_ImageFailed;
            }

            base.OnApplyTemplate();

            _defaultContent = GetTemplateChild(ElementDefaultContent) as ContentPresenter;
            _image = GetTemplateChild(ElementImage) as Image;
            _failedContent = GetTemplateChild(ElementFailedContent) as ContentPresenter;

            if (_image != null)
            {
                _image.ImageFailed += _image_ImageFailed;
            }

        }

        private void _image_ImageFailed(object? sender, ExceptionRoutedEventArgs e)
        {
            if (_failedContent != null)
            {
                _failedContent.Visibility = Visibility.Visible;

            }
        }


        #endregion

        #region 依赖属性

        #region DefaultContent
        /// <summary>
        /// 默认内容
        /// </summary>
        public object DefaultContent
        {
            get { return (object)GetValue(DefaultContentProperty); }
            set { SetValue(DefaultContentProperty, value); }
        }


        // Using a DependencyProperty as the backing store for DefaultContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultContentProperty =
            DependencyProperty.Register("DefaultContent", typeof(object), typeof(ImageEx));

        #endregion



        #region FailedContent
        /// <summary>
        /// 图片加载失败的内容   
        /// </summary>
        public object FailedContent
        {
            get { return (object)GetValue(FailedContentProperty); }
            set { SetValue(FailedContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FailedContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FailedContentProperty =
            DependencyProperty.Register(nameof(FailedContent), typeof(object), typeof(ImageEx));
        #endregion



        #region Source
        /// <summary>
        /// ImageSource
        /// </summary>
        public ImageSource? Source
        {
            get { return (ImageSource?)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImageEx), new PropertyMetadata(null, new PropertyChangedCallback(SourceChanged)));

        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageEx imageEX && e.OldValue != e.NewValue)
            {
                imageEX.ChangeShowImage(e.NewValue as ImageSource);
            }
        }



        #endregion

        #region Stretch
        /// <summary>
        /// Stretch
        /// </summary>
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ImageEx), new PropertyMetadata(Stretch.UniformToFill));

        #endregion

        #region Watermark
        /// <summary>
        /// 水印系列
        /// </summary>
        public object Watermark
        {
            get { return (object)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(object), typeof(ImageEx));

        #endregion

        #region ShowDuration
        /// <summary>
        /// 显示动作持续事件
        /// </summary>
        public Duration ShowDuration
        {
            get { return (Duration)GetValue(ShowDurationProperty); }
            set { SetValue(ShowDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDurationProperty =
            DependencyProperty.Register("ShowDuration", typeof(Duration), typeof(ImageEx), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(1000))));

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ImageEx));

        #endregion


        #endregion

        #region 私有逻辑


        /// </summary>
        /// 改变显示图片
        /// </summary>
        private void ChangeShowImage(ImageSource? source)
        {
            if (_image != null)
            {
                if (source != null)
                {
                    _image.Visibility = Visibility.Visible;
                    if (_defaultContent != null)
                    {
                        _defaultContent.Visibility = Visibility.Collapsed;
                    }

                    if (_failedContent != null)
                    {
                        _failedContent.Visibility = Visibility.Collapsed;
                    }
                    
                    _image.Opacity = 0;
                    _image.Source = source;
                    DoubleAnimation daV = new DoubleAnimation(0, 1, ShowDuration) { FillBehavior = FillBehavior.HoldEnd };
                    _image.BeginAnimation(OpacityProperty, daV, HandoffBehavior.Compose);

                }
                else
                {
                    _image.Source = null;
                    _image.Visibility = Visibility.Collapsed;

                    if (_defaultContent != null)
                    {
                        _defaultContent.Visibility = Visibility.Visible;
                    }

                    if (_failedContent != null)
                    {
                        _failedContent.Visibility = Visibility.Collapsed;
                    }
                }
            }


            #endregion

        }
    }
}
