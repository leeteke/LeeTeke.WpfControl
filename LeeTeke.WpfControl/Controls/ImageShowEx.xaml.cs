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
    /// ImageShowEx.xaml 的交互逻辑
    /// </summary>
    public partial class ImageShowEx : UserControl
    {
        public ImageShowEx()
        {
            InitializeComponent();
            grid.DataContext = this;
            this.SetResourceReference(ToggleGroup.FocusVisualStyleProperty, "LeeFocusVisual");
            this.SetResourceReference(ToggleGroup.SnapsToDevicePixelsProperty, "LeeSnapsToDevicePixels");
        }

        #region 依赖属性

 


        #region DefaultContent
        /// <summary>
        /// 请填写描述
        /// </summary>
        public object DefaultContent
        {
            get { return (object)GetValue(DefaultContentProperty); }
            set { SetValue(DefaultContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultContentProperty =
            DependencyProperty.Register("DefaultContent", typeof(object), typeof(ImageShowEx));

        #endregion


        #region Image
        /// <summary>
        /// 图片
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageShowEx), new PropertyMetadata(null, new PropertyChangedCallback(ImageChanged)));

        private static void ImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageShowEx show && e.OldValue != e.NewValue)
            {
                show.ChangeShowImage();
            }
        }



        #endregion

        #region Duration
        /// <summary>
        /// 缓动时间
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(ImageShowEx), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(1000))));

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
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ImageShowEx), new PropertyMetadata(Stretch.UniformToFill));

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
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ImageShowEx), new PropertyMetadata(null,new PropertyChangedCallback(ContentChanged)));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageShowEx show && e.OldValue != e.NewValue)
            {
                if (e.NewValue is UIElement element)
                {
                    show.content.Child=element;
                }
                else
                {
                    show.content.Child = new TextBlock() { Text = e.NewValue.ToString() };
                }
            }
        }

        #endregion


        #region ContentVerticalAlignment
        /// <summary>
        /// 内容位置
        /// </summary>
        public VerticalAlignment ContentVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(ContentVerticalAlignmentProperty); }
            set { SetValue(ContentVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentVerticalAlignmentProperty =
            DependencyProperty.Register("ContentVerticalAlignment", typeof(VerticalAlignment), typeof(ImageShowEx),new PropertyMetadata(VerticalAlignment.Bottom));

        #endregion



        #region ContentHorizontalAlignment
        /// <summary>
        /// 内容位置
        /// </summary>
        public HorizontalAlignment ContentHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(ContentHorizontalAlignmentProperty); }
            set { SetValue(ContentHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentHorizontalAlignmentProperty =
            DependencyProperty.Register("ContentHorizontalAlignment", typeof(HorizontalAlignment), typeof(ImageShowEx),new PropertyMetadata(HorizontalAlignment.Left));

        #endregion



        #endregion




        private void ChangeShowImage()
        {
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(1000)));
            daV.Completed += BackgroundChangeDaV_Completed;
            imgBackgroundShow.BeginAnimation(OpacityProperty, daV);
        }

        private void BackgroundChangeDaV_Completed(object sender, EventArgs e)
        {
            Image image = new Image
            {
                Source = Image.Clone(),
                Stretch= this.Stretch
            };
            DefaultContent = image;
            imgBackgroundShow.Opacity = 0;
        }
    }



}
