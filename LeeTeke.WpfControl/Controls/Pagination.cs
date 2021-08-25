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



        private ToggleGroup _group;
        private ComboBox _comboBox;
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


            if (this.GetTemplateChild("PART_PageGroup") is ToggleGroup group)
            {
                _group = group;

                _group.SelectionChanged += _group_SelectionChanged;
            }

            if (this.GetTemplateChild("PART_ComboBox") is ComboBox box)
            {
                _comboBox = box; ;
                _comboBox.SelectionChanged += _comboBox_SelectionChanged;
            }

            if (this.GetTemplateChild("PART_PreviousButton") is Button pbtn)
            {
                _previousButton = pbtn;
                _previousButton.Click += (ox, oe) =>
                {
                    this.PageIndex--;
                };
            }

            if (this.GetTemplateChild("PART_HeadButton") is Button hbtn)
            {
                _headButton = hbtn;
                _headButton.Click += (ox, oe) =>
                {
                    this.PageIndex = 1;
                };
            }

            if (this.GetTemplateChild("PART_NextButton") is Button nbtn)
            {
                _nextButton = nbtn;
                _nextButton.Click += (ox, oe) =>
                {
                    this.PageIndex++;
                };
            }

            if (this.GetTemplateChild("PART_EndButton") is Button ebtn)
            {
                _endButton = ebtn;
                _endButton.Click += (ox, oe) =>
                {
                    this.PageIndex = this.MaxPageCount;
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
            if (d is Pagination pagination && e.NewValue is int _value)
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
            DependencyProperty.Register("PageIndex", typeof(int), typeof(Pagination), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PageIndexChanged) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

        private static void PageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination pagination && e.NewValue != e.OldValue)
            {
                pagination.GropShowNumber((int)e.NewValue);

            }
        }
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


        #region DisplayPages
        /// <summary>
        /// 请添加描述
        /// </summary>
        public int DisplayPages
        {
            get { return (int)GetValue(DisplayPagesProperty); }
            set { SetValue(DisplayPagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayPages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayPagesProperty =
            DependencyProperty.Register("DisplayPages", typeof(int), typeof(Pagination), new PropertyMetadata(DisplayPagesChanged));

        private static void DisplayPagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination pagination && e.NewValue is int _value)
            {
                pagination.ViewLoding();
            }
        }
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

            _group.ItemsSource = null;
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
            _comboBox.ItemsSource = pages;

            GropShowNumber(PageIndex);
        }


        private void GropShowNumber(int number)
        {
            if (!IsLoaded)
                return;
            if (DisplayPages < MaxPageCount)
            {
                List<int> pages = new();
                //前面的数
                int hd = (int)Math.Round((((double)DisplayPages - 1) / 2), MidpointRounding.AwayFromZero);
                //后面的数
                int ed = DisplayPages - 1 - hd;

                if (number - hd <= 1)
                {
                    ed += hd + (hd - number) - 1;
                    hd = DisplayPages - ed - 1;

                }
                //////这里算法有问题
                for (int i = 0; i <= hd ; i++)
                    pages.Add(0);
                for (int i = 0 ; i <=ed; i++)
                    pages.Add(i+number);


                _group.ItemsSource = pages;
                _group.SelectedValue = number;
            }
            else
            {
                List<int> pages = new();
                for (int i = 0; i < MaxPageCount; i++)
                    pages.Add(i + 1);
                _group.ItemsSource = pages;
                _group.SelectedValue = number;
            }

            if (_comboBox.SelectedValue == null || (_comboBox.SelectedValue is int svalue && svalue != number))
            {
                _comboBox.SelectedValue = number;
            }
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

        private void _group_SelectionChanged(object sender, ToggleSelectionChangedEventArgs e)
        {

            if (sender is ToggleGroup group && group.SelectedIndex is int selectedIndex)
            {

                if (selectedIndex == 0)
                {
                    _headButton.IsEnabled = false;
                    _previousButton.IsEnabled = false;
                }
                else
                {
                    _headButton.IsEnabled = true;
                    _previousButton.IsEnabled = true;
                }

                if (selectedIndex == _group.Items.Count - 1)
                {
                    _endButton.IsEnabled = false;
                    _nextButton.IsEnabled = false;
                }
                else
                {
                    _endButton.IsEnabled = true;
                    _nextButton.IsEnabled = true;
                }

                if (selectedIndex > -1)
                {
                    PageIndex = (int)group.SelectedValue;
                }
            }
        }

        private void _comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                PageIndex = (int)comboBox.SelectedValue;
            }

        }
        #endregion



    }


    internal class PaginationPageMode
    {
        public int Number { get; set; }

        public bool IsClicked { get; set; }
    }
}
