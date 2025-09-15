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
    ///     <MyNamespace:GridView/>
    ///
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(GridViewItem))]
    public class GridView : ItemsControl
    {
        static GridView()
        {

            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridView), new FrameworkPropertyMetadata(typeof(GridView)));
        }

        #region override
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is GridViewItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new GridViewItem();
        }
        #endregion

        #region Orientation
        /// <summary>
        /// 位置顺序
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(GridView));

        #endregion

        #region HorizontalScrollBarVisibility
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalScrollBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
            DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(GridView), new PropertyMetadata(ScrollBarVisibility.Disabled));

        #endregion

        #region VerticalScrollBarVisibility
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalScrollBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty =
            DependencyProperty.Register("VerticalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(GridView), new PropertyMetadata(ScrollBarVisibility.Auto));

        #endregion


        #region NoItemsContent
        /// <summary>
        /// 请添加描述
        /// </summary>
        public object NoItemsContent
        {
            get { return (object)GetValue(NoItemsContentProperty); }
            set { SetValue(NoItemsContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoItemsContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoItemsContentProperty =
            DependencyProperty.Register("NoItemsContent", typeof(object), typeof(GridView));
        #endregion


        #region EndContent
        /// <summary>
        /// 请添加描述
        /// </summary>
        public object EndContent
        {
            get { return (object)GetValue(EndContentProperty); }
            set { SetValue(EndContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndContentProperty =
            DependencyProperty.Register("EndContent", typeof(object), typeof(GridView));
        #endregion


        #region ItemClickedCommand
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ICommand ItemClickedCommand
        {
            get { return (ICommand)GetValue(ItemClickedCommandProperty); }
            set { SetValue(ItemClickedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemClickedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemClickedCommandProperty =
            DependencyProperty.Register("ItemClickedCommand", typeof(ICommand), typeof(GridView));
        #endregion


        #region ItemClicked
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event GridViewItemClickedEventHandler ItemClicked
        {
            add { AddHandler(ItemClickedEvent, value); }
            remove { RemoveHandler(ItemClickedEvent, value); }
        }

        public static readonly RoutedEvent ItemClickedEvent = EventManager.RegisterRoutedEvent(
        "ItemClicked", RoutingStrategy.Bubble, typeof(GridViewItemClickedEventHandler), typeof(GridView));


        private void RaiseItemClicked(GridViewItem item)
        {
            var arg = new GridViewItemClickedEventArgs(item, ItemClickedEvent);
            RaiseEvent(arg);
            var contentParsm = item.DataContext ?? item;
            if (ItemClickedCommand!=null&& ItemClickedCommand.CanExecute(contentParsm))
            {
                ItemClickedCommand.Execute(contentParsm);
            }
        }

        #endregion

       

        #region internal

        internal void NotifyItemClicked(GridViewItem item)
        {

            RaiseItemClicked(item);
        }
        #endregion
    }
}
