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
    [TemplatePart(Name = ElementPageGroup, Type = typeof(ListBox))]
    [TemplatePart(Name = ElementComboBox, Type = typeof(ComboBox))]
    [TemplatePart(Name = ElementPreviousButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementHeadButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementNextButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementEndButton, Type = typeof(Button))]
    public class Pagination : System.Windows.Controls.Control
    {
        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
        }
        #region consts
        private const string ElementPageGroup = "PART_PageGroup";
        private const string ElementComboBox = "PART_ComboBox";
        private const string ElementPreviousButton = "PART_PreviousButton";
        private const string ElementHeadButton = "PART_HeadButton";
        private const string ElementNextButton = "PART_NextButton";
        private const string ElementEndButton = "PART_EndButton";
        #endregion

        private ListBox? _group;
        private Button? _headButton;
        private Button? _endButton;
        private Button? _previousButton;
        private Button? _nextButton;
        public Pagination()
        {
            Loaded += Pagination_Loaded;
        }



        #region override

        public override void OnApplyTemplate()
        {
            if (_group != null)
                _group.SelectionChanged -= _group_SelectionChanged;

            if (_previousButton != null)
                _previousButton.Click -= _previousButton_Click;
            if (_nextButton != null)
                _nextButton.Click -= _nextButton_Click;
            if (_headButton != null)
                _headButton.Click -= _headButton_Click;
            if (_endButton != null)
                _endButton.Click -= _endButton_Click;
            base.OnApplyTemplate();


            _group = GetTemplateChild(ElementPageGroup) as ListBox;

            _previousButton = GetTemplateChild(ElementPreviousButton) as Button;
            _headButton = GetTemplateChild(ElementHeadButton) as Button;
            _nextButton = GetTemplateChild(ElementNextButton) as Button;
            _endButton = GetTemplateChild(ElementEndButton) as Button;

            if (_group != null)
                _group.SelectionChanged += _group_SelectionChanged; ;

            if (_previousButton != null)
                _previousButton.Click += _previousButton_Click;
            if (_nextButton != null)
                _nextButton.Click += _nextButton_Click;
            if (_headButton != null)
                _headButton.Click += _headButton_Click;
            if (_endButton != null)
                _endButton.Click += _endButton_Click;

        }

        private void _group_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox group && group.SelectedIndex is int selectedIndex)
            {
                if (_headButton != null)
                    _headButton.IsEnabled = selectedIndex != 0;

                if (_previousButton != null)
                    _previousButton.IsEnabled = selectedIndex != 0;

                if (_endButton != null)
                    _endButton.IsEnabled = selectedIndex != _group?.Items.Count - 1;

                if (_nextButton != null)
                    _nextButton.IsEnabled = selectedIndex != _group?.Items.Count - 1;
                if (selectedIndex > -1)
                {
                    if (group.SelectedValue is int pk)
                        PageIndex = pk;
                }
            }
        }

        private void _endButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = this.PageCount;
        }

        private void _headButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = 1;
        }

        private void _nextButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex++;
        }

        private void _previousButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex--;
        }








        #endregion

        #region 依赖

        #region PageCount
        /// <summary>
        /// 请添加描述
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPageCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(Pagination), new PropertyMetadata(PageCountChanged));

        private static void PageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination pagination && e.NewValue is int _value)
            {
                pagination.ViewLoading();
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
            DependencyProperty.Register("PageIndex", typeof(int), typeof(Pagination), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPageIndexChanged) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

        private static void OnPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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
            if (d is Pagination pagination && e.NewValue is int)
            {
                pagination.ViewLoading();
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

        #region PageIndexChangedCommand
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ICommand PageIndexChangedCommand
        {
            get { return (ICommand)GetValue(PageIndexChangedCommandProperty); }
            set { SetValue(PageIndexChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndexChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexChangedCommandProperty =
            DependencyProperty.Register("PageIndexChangedCommand", typeof(ICommand), typeof(Pagination));
        #endregion

        #region PageIndexList
        /// <summary>
        /// 请添加描述
        /// </summary>
        internal int[] PageIndexList
        {
            get { return (int[])GetValue(PageIndexListProperty); }
            set { SetValue(PageIndexListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndexList.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty PageIndexListProperty =
            DependencyProperty.Register(nameof(PageIndexList), typeof(int[]), typeof(Pagination));
        #endregion

        #region SelectedItem(只是为了解决一个很名莫名奇妙的错误问题而添加的属性)
        /// <summary>
        /// 该属性实际意义，主要是Combox不为何老是寻找本控件的SelectedItem，单是又无任何操作，一直在报错绑定失败。
        /// 故此加之
        /// </summary>
        internal object? SelectedItem
        {
            get;
            set;
        }

        //// Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(Pagination));


        #endregion

        #endregion

        #region RouteEvent


        #region PageIndexChanged
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event PageIndexChangedEventHandler PageIndexChanged
        {
            add { AddHandler(PageIndexChangedEvent, value); }
            remove { RemoveHandler(PageIndexChangedEvent, value); }
        }

        public static readonly RoutedEvent PageIndexChangedEvent = EventManager.RegisterRoutedEvent(
        "PageIndexChanged", RoutingStrategy.Bubble, typeof(PageIndexChangedEventHandler), typeof(Pagination));


        private void RaisePageIndexChanged(int newValue)
        {
            var arg = new PageIndexChangedEventArgs(newValue, PageIndexChangedEvent);
            RaiseEvent(arg);
            PageIndexChangedCommand?.Execute(newValue);
        }

        #endregion

        #endregion

        #region 私有


        /// <summary>
        /// 视图
        /// </summary>
        private void ViewLoading()
        {
            if (_group == null)
                return;

            _group.ItemsSource = null;
            if (PageCount < 1)
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }
            else
            {
                this.Visibility = Visibility.Visible;
            }

            ///如果小于1 则进行判断 自动
            var pages = new int[PageCount];
            for (int i = 0; i < PageCount; i++)
            {
                pages[i] = (i + 1);
            }
            PageIndexList = pages;
            GropShowNumber(PageIndex);
        }


        private void GropShowNumber(int number)
        {

            if (_group == null)
                return;

            var pageList = ShowPagesList(PageCount, DisplayPages, number);
            if (pageList.Count > 0 && _group.ItemsSource is List<int> vlist && vlist != null && vlist.Count == pageList.Count && vlist.First() == pageList.First())
            {
                _group.SelectedValue = number;
            }
            else
            {
                _group.ItemsSource = pageList;
                _group.SelectedValue = number;
            }


            RaisePageIndexChanged(number);
        }

        /// <summary>
        /// 算法完成。
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static List<int> ShowPagesList(int maxCount, int dispalyNumber, int number)
        {

            if (dispalyNumber < 1)
            {
                return new List<int>();
            }

            ///最小显示
            if (dispalyNumber < 2)
            {
                return new List<int>() { number };
            }



            var result = new List<int>();
            if (dispalyNumber > maxCount)
            {
                for (int i = 0; i < maxCount; i++)
                {
                    result.Add(i + 1);
                }
                return result;
            }

            int center = (int)Math.Round((double)(dispalyNumber / 2), MidpointRounding.AwayFromZero);

            if (number <= center)
            {
                for (int i = 1; i <= dispalyNumber; i++)
                {
                    result.Add(i);
                }

                return result;
            }

            int start = number + center > maxCount ? maxCount - dispalyNumber + 1 : number - center;
            for (int i = 0; i < dispalyNumber; i++)
            {
                result.Add(i + start);
            }

            return result;


        }

        /// <summary>
        /// 控件加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pagination_Loaded(object sender, RoutedEventArgs e)
        {
            ViewLoading();
        }



        #endregion

    }


    internal class PaginationPageMode
    {
        public int Number { get; set; }

        public bool IsClicked { get; set; }
    }
}
