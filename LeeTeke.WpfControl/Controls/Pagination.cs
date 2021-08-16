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


        private ComboBox _jumpBox;
        private ToggleGroup _group;
        public Pagination()
        {
            Loaded += Pagination_Loaded;
        }



        #region override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.Template.FindName("PART_JumpComboBox", this) is ComboBox comboBox)
            {
                _jumpBox = comboBox;
                _jumpBox.SelectionChanged += _jumpBox_SelectionChanged;
            }

            if (this.Template.FindName("PART_PageGroup", this) is ToggleGroup toggle)
            {
                _group = toggle;

                _group.SelectionChanged += _group_SelectionChanged;
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
            DependencyProperty.Register("MaxPageCount", typeof(int), typeof(Pagination));
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


        #region PageButtonSize
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double PageButtonSize
        {
            get { return (double)GetValue(PageButtonSizeProperty); }
            set { SetValue(PageButtonSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageButtonSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageButtonSizeProperty =
            DependencyProperty.Register("PageButtonSize", typeof(double), typeof(Pagination));
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
            _group.Items.Clear();
            if (MaxPageCount < 1)
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }

            ///如果小于1 则进行判断 自动

            var lodingInt = GetLodingIndex();

            for (int i = 0; i < lodingInt; i++)
            {
                _group.Items.Add(RasiztoggleGroup(i + 1));
            }

        }

        private int GetLodingIndex()
        {
            var result = 1;
            ///如果show大于maxpage。则区maxpage
            if (ShowIndex > MaxPageCount)
            {
                return MaxPageCount;
            }

            ///如果有实际数,否则为自动判断 默认最小为1
            if (ShowIndex > 0)
            {
                return ShowIndex;
            }

            result = (int)((_group.ActualWidth - 1) / PageButtonSize);
            if (result < 1)
                return 1;
            return result;
        }

        private ToggleButton RasiztoggleGroup(int index)
        {
            ToggleButton button = new ToggleButton()
            {
                Content = index,
                DataContext = index,
            };
            return button;
        }

        /// <summary>
        /// 控件加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pagination_Loaded(object sender, RoutedEventArgs e)
        {
         //   ViewLoding();
        }
        /// <summary>
        /// 选择窗口选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _jumpBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void _group_SelectionChanged(object sender, ToggleSelectionChangedEventArgs e)
        {

        }
        #endregion

    }
}
