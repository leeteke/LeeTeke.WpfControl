using System;
using System.Collections;
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
    ///     <MyNamespace:TabView/>
    ///
    /// </summary>

    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(TabViewItem))]
    public class TabView : ItemsControl
    {
        static TabView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabView), new FrameworkPropertyMetadata(typeof(TabView)));
        }

        private ScrollViewer _scrollViewer;
        private StackPanel _stackPanel;

        private IList _ilist;

        private int _selectedIndex = -1;
        private object _selectedItem;
        private object _selectedValue;

        private IList ItemList
        {
            get { return _ilist; }
            set
            {
                _ilist = value;
                base.ItemsSource = value;
            }
        }

        public TabView()
        {
            
            EventManager.RegisterClassHandler(typeof(TabViewItem), TabViewItem.ClosedEvent, new RoutedEventHandler(TabViewItemClosedEventAsync));
            EventManager.RegisterClassHandler(typeof(TabViewItem), TabViewItem.SelectedEvent, new RoutedEventHandler(TabViewItemSelectedEvent));
            EventManager.RegisterClassHandler(typeof(StackPanel), StackPanel.SizeChangedEvent, new RoutedEventHandler(StackPanelLoadedEvent));
            PreviewMouseWheel += TabView_PreviewMouseWheel;
        }






        #region override

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TabViewItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TabViewItem();
        }

     

   


        #endregion


        #region RouteEvent



        #region ItemClosed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TabViewColsedEventHandler ItemClosed
        {
            add { AddHandler(ItemClosedEvent, value); }
            remove { RemoveHandler(ItemClosedEvent, value); }
        }

        public static readonly RoutedEvent ItemClosedEvent = EventManager.RegisterRoutedEvent(
        "ItemClosed", RoutingStrategy.Bubble, typeof( TabViewColsedEventHandler), typeof(TabView));


        private void RaiseItemClosed(object newValue)
        {
            var arg = new TabViewColsedEventArgs(newValue, ItemClosedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #region ItemSelected
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TabViewSelectedEventHandler ItemSelected
        {
            add { AddHandler(ItemSelectedEvent, value); }
            remove { RemoveHandler(ItemSelectedEvent, value); }
        }

        public static readonly RoutedEvent ItemSelectedEvent = EventManager.RegisterRoutedEvent(
        "ItemSelected", RoutingStrategy.Bubble, typeof( TabViewSelectedEventHandler), typeof(TabView));


        private void RaiseItemSelected(object newValue)
        {
            var arg = new TabViewSelectedEventArgs(newValue, ItemSelectedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #endregion

        #region 依赖属性

        #region ItemsSource
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(TabView), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourceChanged)));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabView tag)
            {
                tag.ItemList = e.NewValue as IList;
            }
        }

        #endregion

        #region SelectedIndex
        /// <summary>
        /// 请填写描述
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(TabView), new PropertyMetadata(-1, SelectedIndexChanged));

        private static void SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabView tag && e.NewValue != e.OldValue && e.NewValue is int newValue && tag._selectedIndex != newValue)
            {
                tag.SelectedIndexChanged();

            }
        }

        #endregion

        #region SelectedItem
        /// <summary>
        /// 请填写描述
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(TabView), new PropertyMetadata(null, SelectedItemChanged));

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabView tag && e.NewValue != e.OldValue && e.NewValue is TabViewItem item && tag._selectedItem != item)
            {
                tag.SelectedItemChanged();
            }
        }

        #endregion

        #region SelectedValue
        /// <summary>
        /// 请填写描述
        /// </summary>
        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(TabView), new PropertyMetadata(null, SelectedValueChanged));

        private static void SelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabView tag && e.NewValue != e.OldValue && tag._selectedValue != e.NewValue)
            {
                tag.SelectedValueChanged();
            }
        }

        #endregion

        #region Orientation
        /// <summary>
        /// 请填写描述
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TabView));

        #endregion

        #region ItemCornerRadius
        /// <summary>
        /// 请填写描述
        /// </summary>
        public CornerRadius ItemCornerRadius
        {
            get { return (CornerRadius)GetValue(ItemCornerRadiusProperty); }
            set { SetValue(ItemCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCornerRadiusProperty =
            DependencyProperty.Register("ItemCornerRadius", typeof(CornerRadius), typeof(TabView));

        #endregion


        #region ItemPinVisibly
        /// <summary>
        /// 固定显示
        /// </summary>
        public bool ItemPinVisibly
        {
            get { return (bool)GetValue(ItemPinVisiblyProperty); }
            set { SetValue(ItemPinVisiblyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemPinVisibly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPinVisiblyProperty =
            DependencyProperty.Register("ItemPinVisibly", typeof(bool), typeof(TabView));
        #endregion


        #region ItemMargin
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness ItemMargin
        {
            get { return (Thickness)GetValue(ItemMarginProperty); }
            set { SetValue(ItemMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMarginProperty =
            DependencyProperty.Register("ItemMargin", typeof(Thickness), typeof(TabView));
        #endregion


        #region CloseVisibly
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ShowMode CloseVisibly
        {
            get { return (ShowMode)GetValue(CloseVisiblyProperty); }
            set { SetValue(CloseVisiblyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseVisibly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseVisiblyProperty =
            DependencyProperty.Register("CloseVisibly", typeof(ShowMode), typeof(TabView));
        #endregion


        #region ItemPadding
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(TabView));
        #endregion




        #region ItemWidth
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(TabView));
        #endregion


        #region ItemHeight
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(TabView));
        #endregion



        #region SelectedBrush
        /// <summary>
        /// 请填写描述
        /// </summary>
        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(TabView));

        #endregion


        #region SelectedForeground
        /// <summary>
        /// 选择的前景色
        /// </summary>
        public Brush SelectedForeground
        {
            get { return (Brush)GetValue(SelectedForegroundProperty); }
            set { SetValue(SelectedForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.Register("SelectedForeground", typeof(Brush), typeof(TabView));
        #endregion


        #region ItemClosedCommand
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ICommand ItemClosedCommand
        {
            get { return (ICommand)GetValue(ItemClosedCommandProperty); }
            set { SetValue(ItemClosedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemClosedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemClosedCommandProperty =
            DependencyProperty.Register("ItemClosedCommand", typeof(ICommand), typeof(TabView));

        #endregion

        #region ItemSelectedCommand
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ICommand ItemSelectedCommand
        {
            get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
            set { SetValue(ItemSelectedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSelectedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSelectedCommandProperty =
            DependencyProperty.Register("ItemSelectedCommand", typeof(ICommand), typeof(TabView));

        #endregion


        #endregion

        #region 内部逻辑



        private void SelectedIndexChanged()
        {
            if (SelectedIndex < 0 || SelectedIndex > _stackPanel.Children.Count - 1)
            {
                SelectedInit(null);
                return;
            }

            if (!SelectedInit(SelectedIndex))
                return;

            SelectedItemsAsync(_stackPanel.Children[SelectedIndex] as TabViewItem);

        }

        private void SelectedValueChanged()
        {
            if (!SelectedInit(SelectedValue))
                return;

            if (ItemsSource == null)
            {
                SelectedItemsAsync(SelectedValue as TabViewItem);
            }
            else
            {
                foreach (TabViewItem item in _stackPanel.Children)
                {
                    if (item.DataContext == SelectedValue)
                    {
                        SelectedItemsAsync(item);
                        return;
                    }
                }
            }

        }


        private void SelectedItemChanged()
        {

            if (!SelectedInit(SelectedItem))
                return;

            SelectedItemsAsync(SelectedItem as TabViewItem);

        }



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _scrollViewer = this.Template.FindName("PART_ScrollViewer", this) as ScrollViewer;
        }





        private void StackPanelLoadedEvent(object sender, RoutedEventArgs e)
        {
            if (sender is StackPanel stackPanel && stackPanel.Name == "PART_StackPanel" && StaticMethods.IsInControl(this, stackPanel))
            {
                _stackPanel = stackPanel;
                if (SelectedIndex > -1)
                {
                    SelectedIndexChanged();
                    return;
                }

                if (SelectedValue != null)
                {
                    SelectedValueChanged();
                    return;
                }

                if (SelectedItem != null)
                {
                    SelectedItemChanged();
                    return;
                }
            }
        }


        private async void TabViewItemClosedEventAsync(object sender, RoutedEventArgs e)
        {
            if (sender is TabViewItem self)
                switch ((e as TabViewItemClosedEventArgs).ClosedMode)
                {
                    case TabViewItemClosedMode.Self:
                        if (self.CanClosing)
                        {

                            var index = _stackPanel.Children.IndexOf(self);
                            ItemRemoveChanged(self);
                            await CloseItemAsync(self);
                            if (self.IsSelected)
                            {
                                if (index > 0)
                                {
                                    SelectedItem = _stackPanel.Children[index - 1] as TabViewItem;
                                }
                                else if (_stackPanel.Children != null && _stackPanel.Children.Count > 0)
                                {
                                    SelectedItem = _stackPanel.Children[0] as TabViewItem;
                                }
                            }
                            else if (_stackPanel.Children.Count < 1)
                            {
                                SelectedItem = null;
                            }
                        }
                        break;
                    case TabViewItemClosedMode.Other:

                        List<object> needCloseItem = new List<object>();
                        for (int i = 0; i < _stackPanel.Children.Count; i++)
                        {
                            ///关闭全部
                            if (sender != _stackPanel.Children[i])
                            {
                                needCloseItem.Add(_stackPanel.Children[i]);
                            }
                        }
                        foreach (TabViewItem item in needCloseItem)
                        {
                            if (item.CanClosing&&!item.IsFixed)
                            {
                                CloseItemAsync(item);
                                ItemRemoveChanged(item);
                            }
                        }

                        break;
                    case TabViewItemClosedMode.All:
                        List<object> allItem = new List<object>();
                        for (int i = 0; i < _stackPanel.Children.Count; i++)
                        {
                            allItem.Add(_stackPanel.Children[i]);
                        }
                        foreach (TabViewItem item in allItem)
                        {
                            if (item.CanClosing&&!item.IsFixed)
                            {
                                CloseItemAsync(item);
                                ItemRemoveChanged(item);
                            }
                        }

                        if (_stackPanel.Children.Count > 0)
                        {
                            ///选择首个
                            SelectedItem = _stackPanel.Children[0] as TabViewItem;
                        }
                        else
                        {
                            SelectedItemsAsync(null);
                        }
                        break;
                    default:
                        break;
                }

        }

        private void TabView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (_scrollViewer != null)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        _scrollViewer.ScrollToHorizontalOffset( _scrollViewer.HorizontalOffset- e.Delta);
                        break;
                    case Orientation.Vertical:
                        _scrollViewer.ScrollToVerticalOffset(_scrollViewer.VerticalOffset - e.Delta);
                        break;
                    default:
                        break;
                }
            }
        }
        private void TabViewItemSelectedEvent(object sender, RoutedEventArgs e)
        {
            if (sender is TabViewItem item && StaticMethods.IsInControl(this, item))
            {
                if (SelectedItem != item)
                {
                    SelectedItem = item;
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="item"></param>
        private async Task CloseItemAsync(TabViewItem item)
        {

            await item.CloseAsync();


            if (ItemsSource == null)
            {
                Items.Remove(item);
            }
            else
            {
                ItemList.Remove(item.DataContext);
            }
            ItemRemoveChanged(item);



        }

        private async void SelectedItemsAsync(TabViewItem item)
        {
            if (item == null)
            {
                ItemSelectedChanged(null);
                return;
            }


            for (int i = 0; i < _stackPanel.Children.Count; i++)
            {

                if (_stackPanel.Children[i] == item)
                {
                    ItemSelectedChanged(item);
                    item.IsSelected = true;
                }
                else
                {
                    (_stackPanel.Children[i] as TabViewItem).IsSelected = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void ItemValueChanged(TabViewItem item)
        {
            _selectedValue = ItemsSource == null ? item : item?.DataContext;
            _selectedIndex = _stackPanel.Children.IndexOf(item);
            _selectedItem = item;

            SelectedValue = _selectedValue;
            SelectedIndex = _selectedIndex;
            SelectedItem = _selectedItem;

        }


        /// <summary>
        /// Item移动事件发生
        /// </summary>
        /// <param name="item"></param>
        private void ItemRemoveChanged(TabViewItem item)
        {
            RaiseItemClosed(item);
            try
            {
                ItemClosedCommand?.Execute(ItemsSource == null ? item : item.DataContext);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Item选择事件发生
        /// </summary>
        /// <param name="item"></param>
        private void ItemSelectedChanged(TabViewItem item)
        {
            ItemValueChanged(item);
            RaiseItemSelected(item);
            try
            {
                ItemSelectedCommand?.Execute(ItemsSource == null ? item : item?.DataContext);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 清理选择
        /// </summary>
        private bool SelectedInit(object args)
        {
            if (_stackPanel == null)
            {
                return false;
            }


            _selectedIndex = -1;
            _selectedItem = null;
            _selectedValue = null;

            if (args == null)
            {
                SelectedIndex = _selectedIndex;
                SelectedValue = _selectedValue;
                SelectedItem = _selectedItem;
                foreach (TabViewItem chlid in _stackPanel.Children)
                {
                    chlid.IsSelected = false;
                }
                RaiseItemSelected(SelectedValue);
                return false;
            }

            return true;
        }

        #endregion
    }
}
