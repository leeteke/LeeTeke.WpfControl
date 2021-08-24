using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
    ///     <MyNamespace:Pagination/>
    ///
    /// </summary>
    public class Pagination : Control
    {
        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
        }



        private ListBox _list;
        private Button _headButton;
        private Button _endButton;
        private Button _previousButton;
        private Button _nextButton;
        public Pagination()
        {
            Loaded += Pagination_Loaded;
        }



        #region override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();



            if (this.GetTemplateChild("PART_PageList") is ListBox list)
            {
                _list = list;

                _list.SelectionChanged += _list_SelectionChanged; ;
            }

            if (this.GetTemplateChild("PART_PreviousButton") is Button pbtn)
            {
                _previousButton = pbtn;
                _previousButton.Click += (ox, oe) =>
                {
                    if (_list != null)
                    {
                        if (_list.SelectedIndex > -1)
                        {
                            _list.SelectedIndex--;
                        }
                        else if (_list.SelectedIndex == -1 && _list.Items != null)
                        {
                            _list.SelectedIndex = 0;
                        }
                    }
                };
            }

            if (this.GetTemplateChild("PART_HeadButton") is Button hbtn)
            {
                _headButton = hbtn;
                _headButton.Click += (ox, oe) =>
                {
                    if (_list != null)
                    {
                        _list.SelectedIndex = 0;
                    }
                };
            }

            if (this.GetTemplateChild("PART_NextButton") is Button nbtn)
            {
                _nextButton = nbtn;
                _nextButton.Click += (ox, oe) =>
                {
                    if (_list != null)
                    {
                        _list.SelectedIndex++;
                    }
                };
            }

            if (this.GetTemplateChild("PART_EndButton") is Button ebtn)
            {
                _endButton = ebtn;
                _endButton.Click += (ox, oe) =>
                {
                    if (_list != null && _list.Items != null)
                    {
                        _list.SelectedIndex = _list.Items.Count - 1;
                    }
                };
            }
        }









        #endregion

        #region 依赖

        #region MaxPageCount
        /// <summary>
        /// 请添加描述
        /// </summary>
        public int MaxPageCount
        {
            get { return (int)GetValue(MaxPageCountProperty); }
            set { SetValue(MaxPageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPageCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxPageCountProperty =
            DependencyProperty.Register("MaxPageCount", typeof(int), typeof(Pagination), new PropertyMetadata(MaxPageCountChanged));

        private static void MaxPageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination pagination&&e.NewValue is int _value)
            {
                pagination.ViewLoding();
            }
        }
        #endregion

        #region PageIndex
        /// <summary>
        /// 请添加描述
        /// </summary>
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(Pagination), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        #endregion



        #region ButtonWidth
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register("ButtonWidth", typeof(double), typeof(Pagination));
        #endregion


        #region ButtonMargin
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness ButtonMargin
        {
            get { return (Thickness)GetValue(ButtonMarginProperty); }
            set { SetValue(ButtonMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonMarginProperty =
            DependencyProperty.Register("ButtonMargin", typeof(Thickness), typeof(Pagination));
        #endregion



        #region ButtonHeight
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonHeightProperty =
            DependencyProperty.Register("ButtonHeight", typeof(double), typeof(Pagination));
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Pagination));
        #endregion


        #region ShowIndex
        /// <summary>
        /// 请添加描述
        /// </summary>
        public int ShowIndex
        {
            get { return (int)GetValue(ShowIndexProperty); }
            set { SetValue(ShowIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowIndexProperty =
            DependencyProperty.Register("ShowIndex", typeof(int), typeof(Pagination));
        #endregion

        #region JumpVisibility
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Visibility JumpVisibility
        {
            get { return (Visibility)GetValue(JumpVisibilityProperty); }
            set { SetValue(JumpVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for JumpVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JumpVisibilityProperty =
            DependencyProperty.Register("JumpVisibility", typeof(Visibility), typeof(Pagination));
        #endregion

        #endregion


        #region 私有


        /// <summary>
        /// 试图加载
        /// </summary>
        private void ViewLoding()
        {
            if (!IsLoaded)
                return;
            PageIndex = -1;
            _list.ItemsSource = null;
            if (MaxPageCount < 1)
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }

            ///如果小于1 则进行判断 自动

            List<int> pages = new();
            for (int i = 0; i < MaxPageCount; i++)
            {
                pages.Add(i + 1);
            }

            _list.ItemsSource = pages;

        }



        /// <summary>
        /// 控件加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pagination_Loaded(object sender, RoutedEventArgs e)
        {
            ViewLoding();
        }


        private void _list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox list)
            {
                if (list.SelectedIndex == 0)
                {
                    _headButton.IsEnabled = false;
                    _previousButton.IsEnabled = false;
                }
                else
                {
                    _headButton.IsEnabled = true;
                    _previousButton.IsEnabled = true;
                }

                if (list.SelectedIndex == list.Items.Count - 1)
                {
                    _endButton.IsEnabled = false;
                    _nextButton.IsEnabled = false;
                }
                else
                {
                    _endButton.IsEnabled = true;
                    _nextButton.IsEnabled = true;
                }

                if (list.SelectedIndex>-1)
                {
                    list.ScrollIntoView(list.SelectedItem);
                }
            }
        }
        #endregion

    }
}
