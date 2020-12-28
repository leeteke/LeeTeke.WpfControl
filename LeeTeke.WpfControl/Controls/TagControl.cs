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
    ///     <MyNamespace:TagControl/>
    ///
    /// </summary>

    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(TagControlItem))]
    public class TagControl : ItemsControl
    {
        static TagControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TagControl), new FrameworkPropertyMetadata(typeof(TagControl)));
        }

        private ScrollViewer _scrollViewer;
        private StackPanel _stackPanel;

        private IList _ilist;

        private int _selectedIndex;
        private object _selectedItem;
        private object _seletedValue;

        private IList ItemList
        {
            get { return _ilist; }
            set
            {
                _ilist = value;
                base.ItemsSource = value;
            }
        }

        public TagControl()
        {
            EventManager.RegisterClassHandler(typeof(TagControlItem), TagControlItem.ClosedEvent, new RoutedEventHandler(TagControlItemClosedEventAsync));
            EventManager.RegisterClassHandler(typeof(TagControlItem), TagControlItem.SelectedEvent, new RoutedEventHandler(TagControlItemSelectedEvent));
            EventManager.RegisterClassHandler(typeof(StackPanel), StackPanel.LoadedEvent, new RoutedEventHandler(StackPanelLoadedEvent));

            Loaded += TagControl_Loaded;
            PreviewMouseWheel += TagControl_PreviewMouseWheel;


        }






        #region override

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TagControlItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TagControlItem();
        }




        #endregion


        #region RouteEvent



        #region ItemClosed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TagControlColsedEventHandler ItemClosed
        {
            add { AddHandler(ItemClosedEvent, value); }
            remove { RemoveHandler(ItemClosedEvent, value); }
        }

        public static readonly RoutedEvent ItemClosedEvent = EventManager.RegisterRoutedEvent(
        "ItemClosed", RoutingStrategy.Bubble, typeof(EventHandler<TagControlColsedEventHandler>), typeof(TagControl));


        private void RaiseItemClosed(object newValue)
        {
            var arg = new TagControlColsedEventArgs(newValue, ItemClosedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #region ItemSelected
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TagControlSelectedEventHandler ItemSelected
        {
            add { AddHandler(ItemSelectedEvent, value); }
            remove { RemoveHandler(ItemSelectedEvent, value); }
        }

        public static readonly RoutedEvent ItemSelectedEvent = EventManager.RegisterRoutedEvent(
        "ItemSelected", RoutingStrategy.Bubble, typeof(EventHandler<TagControlSelectedEventHandler>), typeof(TagControl));


        private void RaiseItemSelected(object newValue)
        {
            var arg = new TagControlSelectedEventArgs(newValue, ItemSelectedEvent);
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
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(TagControl), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourceChanged)));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControl tag)
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
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(TagControl), new PropertyMetadata(-1, SelectedIndexChanged));

        private static void SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControl tag && e.NewValue != e.OldValue && tag.Items != null &&e.NewValue is int newValue && tag._selectedIndex != newValue)
            {

                if (newValue > -1 && newValue < tag._stackPanel.Children.Count)
                {
                    tag.SelectedItesmAsync(tag.Items[newValue] as TagControlItem);
                }

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
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(TagControl), new PropertyMetadata(null, SelectedItemChanged));

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControl tag && e.NewValue != e.OldValue)
            {
                if (e.NewValue is TagControlItem item && tag._stackPanel.Children != null && tag._selectedItem != item)
                {
                    tag.SelectedItesmAsync(item);
                }
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
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(TagControl), new PropertyMetadata(null, SelectedValueChanged));

        private static void SelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControl tag && e.NewValue != e.OldValue)
            {
                if (tag._stackPanel.Children != null && tag._seletedValue != e.NewValue)
                {
                    if (tag.ItemsSource == null)
                    {
                        tag.SelectedItesmAsync(e.NewValue as TagControlItem);
                    }
                    else
                    {
                        foreach (TagControlItem item in tag.Items)
                        {
                            if (item.DataContext == e.NewValue)
                            {
                                tag.SelectedItesmAsync(item);
                                return;
                            }
                        }
                    }
                }
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
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TagControl));

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
            DependencyProperty.Register("ItemCornerRadius", typeof(CornerRadius), typeof(TagControl));

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
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(TagControl));

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
            DependencyProperty.Register("ItemClosedCommand", typeof(ICommand), typeof(TagControl));

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
            DependencyProperty.Register("ItemSelectedCommand", typeof(ICommand), typeof(TagControl));

        #endregion


        #endregion

        #region 内部逻辑



        private void TagControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _scrollViewer = this.Template.FindName("PART_ScrollViewer", this) as ScrollViewer;
            }
            catch (Exception)
            {
            }
        }

        private void StackPanelLoadedEvent(object sender, RoutedEventArgs e)
        {
            if (sender is StackPanel stackPanel && stackPanel.Name == "PART_StackPanel" && StaticMethods.IsInControl(this, stackPanel))
            {
                _stackPanel = stackPanel;
            }
        }


        private async void TagControlItemClosedEventAsync(object sender, RoutedEventArgs e)
        {

            switch ((e as TagControlItemClosedEventArgs).ClosedMode)
            {
                case TagControlItemClosedMode.Self:
                    var index = Items.IndexOf(sender);
                    await CloseItemAsync(sender as TagControlItem);
                    if ((sender as TagControlItem).IsSelected)
                    {
                        if (index > 0)
                        {
                            SelectedItesmAsync(Items[index - 1] as TagControlItem);
                        }
                        else if (Items != null && Items.Count > 0)
                        {
                            SelectedItesmAsync(Items[0] as TagControlItem);
                        }
                    }
                    else
                    {
                        SelectedItesmAsync(null);
                    }
                    break;
                case TagControlItemClosedMode.Other:

                    List<object> needCloseItem = new List<object>();
                    for (int i = 0; i < _stackPanel.Children.Count; i++)
                    {
                        ///关闭全部
                        if (sender != _stackPanel.Children[i])
                        {
                            needCloseItem.Add(_stackPanel.Children[i]);
                        }
                    }
                    foreach (TagControlItem item in needCloseItem)
                    {
                        CloseItemAsync(item);
                        ItemRemoveChanged(item);
                    }

                    break;
                case TagControlItemClosedMode.All:
                    List<object> allItem = new List<object>();
                    for (int i = 0; i < _stackPanel.Children.Count; i++)
                    {
                        allItem.Add(_stackPanel.Children[i]);
                    }
                    foreach (TagControlItem item in allItem)
                    {
                        CloseItemAsync(item);
                        ItemRemoveChanged(item);
                    }

                    if (_stackPanel.Children.Count > 0)
                    {
                        ///选择首个
                        SelectedItesmAsync(_stackPanel.Children[0] as TagControlItem);
                    }
                    else
                    {
                        SelectedItesmAsync(null);
                    }
                    break;
                default:
                    break;
            }

        }

        private void TagControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (_scrollViewer != null)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        _scrollViewer.ScrollToHorizontalOffset(e.Delta);
                        break;
                    case Orientation.Vertical:
                        _scrollViewer.ScrollToVerticalOffset(e.Delta);
                        break;
                    default:
                        break;
                }
            }
        }
        private void TagControlItemSelectedEvent(object sender, RoutedEventArgs e)
        {
            if (sender is TagControlItem item && StaticMethods.IsInControl(this, item))
            {
                if (SelectedItem != item)
                {
                    SelectedItesmAsync(item);
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="item"></param>
        private async Task CloseItemAsync(TagControlItem item)
        {
            ///如果不能关闭
            if (!item.CanClosed)
                return;

            await item.CloseAsync();

            var index = Items.IndexOf(item);
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


        private async void SelectedItesmAsync(TagControlItem item)
        {
            if (item == null)
            {
                ItemSelectedChanged(null);
                return;
            }
            for (int i = 0; i < Items.Count; i++)
            {

                if (_stackPanel.Children[i] == item)
                {
                    item.IsSelected = true;
                    ItemSelectedChanged(item);
                }
                else
                {
                    (_stackPanel.Children[i] as TagControlItem).IsSelected = false;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void ItemValueChanged(TagControlItem item)
        {
            _seletedValue = ItemsSource == null ? item : item?.DataContext;
            _selectedIndex = _stackPanel.Children.IndexOf(item);
            _selectedItem = item;

            SelectedValue = _seletedValue;
            SelectedIndex = _selectedIndex;
            SelectedItem = _selectedItem;

        }


        /// <summary>
        /// Item移动事件发生
        /// </summary>
        /// <param name="item"></param>
        private void ItemRemoveChanged(TagControlItem item)
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
        private void ItemSelectedChanged(TagControlItem item)
        {
            ItemValueChanged(item);
            RaiseItemSelected(item);
            try
            {
                ItemSelectedCommand?.Execute(ItemsSource == null ? item : item.DataContext);
            }
            catch
            {
            }
        }
        #endregion
    }
}
