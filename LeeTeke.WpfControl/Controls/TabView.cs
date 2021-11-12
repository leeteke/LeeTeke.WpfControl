using System;
using System.Collections;
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
    ///     <MyNamespace:TabView/>
    ///
    /// </summary>

    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(TabViewItem))]
    public class TabView : Selector
    {
        static TabView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabView), new FrameworkPropertyMetadata(typeof(TabView)));
        }

        private ScrollViewer _scrollViewer;
        private StackPanel _stackPanel;
        private Button _leftBtn;
        private Button _rightBtn;
        private ContextMenu _contextMenu;
        private MenuItem _pinMenuItem;
        private MenuItem _allMenuItem;
        private MenuItem _otherMenuItem;
        private MenuItem _selfMenuItem;

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

            this.PreviewMouseRightButtonDown += TabView_PreviewMouseRightButtonDown;

            PreviewMouseWheel += TabView_PreviewMouseWheel;
        }

        private void TabView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject source = e.OriginalSource as DependencyObject;
            while (source != null && source.GetType() != typeof(TabViewItem))
                source = VisualTreeHelper.GetParent(source);
            if (source != null && source is TabViewItem item)
            {
                e.Handled = true;
                _contextMenu.DataContext = item;
                if (item.CanClosing)
                {
                    _pinMenuItem.Visibility = Visibility.Visible;
                    _selfMenuItem.Visibility = Visibility.Visible;
                    if (_pinMenuItem.Icon is TextBlock tb)
                    {
                        if (item.IsFixed)
                        {
                            tb.Text = "\xe77a";
                            _pinMenuItem.Header = "解除固定";
                        }
                        else
                        {
                            tb.Text = "\xe718";
                            _pinMenuItem.Header = "固定选项卡";
                        }
                    }
                }
                else
                {
                    _pinMenuItem.Visibility = Visibility.Collapsed;
                    _selfMenuItem.Visibility = Visibility.Collapsed;
                }


            }
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



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.GetTemplateChild("PART_Left") is Button leftBtn)
            {
                _leftBtn = leftBtn;
                _leftBtn.Click += _leftBtn_Click;
                _leftBtn.MouseDown += _leftBtn_MouseDown;
            }

            if (this.GetTemplateChild("PART_Right") is Button rightBtn)
            {
                _rightBtn = rightBtn;
                _rightBtn.Click += _rightBtn_Click;
            }


            if (this.GetTemplateChild("PART_ScrollViewer") is ScrollViewer scroll)
            {
                _scrollViewer = scroll;
                _scrollViewer.ScrollChanged += _scrollViewer_ScrollChanged;
                _scrollViewer.SizeChanged += _scrollViewer_SizeChanged;
            }
            if (this.GetTemplateChild("PART_ContextMenu") is ContextMenu contextMenu)
            {
                _contextMenu = contextMenu;

                _contextMenu.SetBinding(Dependencies.ContextMenuManager.ContentDockProperty, new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(TabView.ItemContxtMenuContentDockProperty),
                    Mode = BindingMode.OneWay,
                });
                _contextMenu.SetBinding(Dependencies.ContextMenuManager.ContentProperty, new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(TabView.ItemContxtMenuContentProperty),
                    Mode = BindingMode.OneWay,
                });

                _contextMenu.Closed += (e, s) => contextMenu.DataContext = this.DataContext;
            }

            if (this.GetTemplateChild("PART_MenuItem_CloseAll") is MenuItem allItem)
            {
                _allMenuItem = allItem;
                _allMenuItem.Click += (es, ex) =>
                {
                    if (_contextMenu.DataContext is TabViewItem item)
                    {
                        TabViewItemClosedEventAsync(item, new TabViewItemClosedEventArgs(TabViewItemClosedMode.All, null));
                    }
                };
            }
            if (this.GetTemplateChild("PART_MenuItem_CloseOther") is MenuItem otherItem)
            {
                _otherMenuItem = otherItem;
                _otherMenuItem.Click += (es, ex) =>
                {
                    if (_contextMenu.DataContext is TabViewItem item)
                    {
                        TabViewItemClosedEventAsync(item, new TabViewItemClosedEventArgs(TabViewItemClosedMode.Other, null));
                    }
                };
            }
            if (this.GetTemplateChild("PART_MenuItem_CloseSelf") is MenuItem selftItem)
            {
                _selfMenuItem = selftItem;
                _selfMenuItem.Click += (es, ex) =>
                {
                    if (_contextMenu.DataContext is TabViewItem item)
                    {
                        TabViewItemClosedEventAsync(item, new TabViewItemClosedEventArgs(TabViewItemClosedMode.Self, null));
                    }
                };
            }

            if (this.GetTemplateChild("PART_MenuItem_Pin") is MenuItem btnPin)
            {
                _pinMenuItem = btnPin;
                btnPin.Click += (es, ex) =>
                {
                    if (_contextMenu.DataContext is TabViewItem item)
                    {
                        item.IsFixed = !item.IsFixed;
                    }
                };
            }


        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

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
        "ItemClosed", RoutingStrategy.Bubble, typeof(TabViewColsedEventHandler), typeof(TabView));


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
        "ItemSelected", RoutingStrategy.Bubble, typeof(TabViewSelectedEventHandler), typeof(TabView));


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
        public new int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty SelectedIndexProperty =
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
        public new object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty SelectedItemProperty =
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
        public new object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValue.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty SelectedValueProperty =
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


        #region ShowScrollToButton
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool ShowScrollToButton
        {
            get { return (bool)GetValue(ShowScrollToButtonProperty); }
            set { SetValue(ShowScrollToButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowScrollToButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowScrollToButtonProperty =
            DependencyProperty.Register("ShowScrollToButton", typeof(bool), typeof(TabView));
        #endregion


        #region ItemContxtMenuContent
        /// <summary>
        /// 请添加描述
        /// </summary>
        public object ItemContxtMenuContent
        {
            get { return (object)GetValue(ItemContxtMenuContentProperty); }
            set { SetValue(ItemContxtMenuContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemContxtMenuContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContxtMenuContentProperty =
            DependencyProperty.Register("ItemContxtMenuContent", typeof(object), typeof(TabView));
        #endregion

        #region ItemContxtMenuContentDock
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Dock ItemContxtMenuContentDock
        {
            get { return (Dock)GetValue(ItemContxtMenuContentDockProperty); }
            set { SetValue(ItemContxtMenuContentDockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemContxtMenuContentDock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContxtMenuContentDockProperty =
            DependencyProperty.Register("ItemContxtMenuContentDock", typeof(Dock), typeof(TabView));
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



        private void _leftBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void _rightBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Orientation == Orientation.Horizontal && _scrollViewer != null)
            {

                _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset + 120);
            }
        }

        private void _leftBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Orientation == Orientation.Horizontal && _scrollViewer != null)
            {
                _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset - 120);
            }
        }

        private void _scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (Orientation == Orientation.Horizontal && ShowScrollToButton)
            {
                _leftBtn.Visibility = e.HorizontalOffset == 0 ? Visibility.Collapsed : Visibility.Visible;

                if (e.ExtentWidth > (e.ViewportWidth + e.HorizontalOffset))
                {
                    _rightBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    _rightBtn.Visibility = Visibility.Collapsed;
                }
            }
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
                            ///等待关闭
                            await CloseItemAsync(self);
                            ///如果当前是选择的
                            if (SelectedItem == self)
                            {
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
                        }
                        break;
                    case TabViewItemClosedMode.Other:

                        ///关闭其他
                        if (SelectedItem != null)
                            SelectedItem = self;

                        ///需要关闭的窗口
                        List<object> needCloseItem = new List<object>();
                        for (int i = 0; i < _stackPanel.Children.Count; i++)
                        {
                            ///关闭全部
                            if (sender != _stackPanel.Children[i])
                            {
                                needCloseItem.Add(_stackPanel.Children[i]);
                            }
                        }
                        foreach (TabViewItem needitem in needCloseItem)
                        {
                            if (needitem.CanClosing && !needitem.IsFixed)
                            {
                                CloseItemAsync(needitem);
                                ItemRemoveChanged(needitem);
                            }
                        }

                        break;
                    case TabViewItemClosedMode.All:
                        List<object> allItem = new List<object>();
                        for (int i = 0; i < _stackPanel.Children.Count; i++)
                        {
                            allItem.Add(_stackPanel.Children[i]);
                        }
                        var needSelected = allItem.Find(p => p is TabViewItem finditem && (finditem.CanClosing == false || finditem.IsFixed));
                        if (needSelected is TabViewItem selecteditem && SelectedItem != null)
                        {
                            SelectedItem= selecteditem;
                        }
                   
                        foreach (TabViewItem item in allItem)
                        {
                            if (item.CanClosing && !item.IsFixed)
                            {
                                CloseItemAsync(item);
                                ItemRemoveChanged(item);
                            }
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
                        _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset - e.Delta);
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
                    ScrollToItem(item);
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

        private void _scrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SelectedItem is TabViewItem item)
            {
                ScrollToItem(item);
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

        #region 公开方法


        public void ScrollToItem(TabViewItem item)
        {
            if (Orientation == Orientation.Horizontal)
            {
                // 获取要定位之前 ScrollViewer 目前的滚动位置
                var currentScrollPosition = _scrollViewer.HorizontalOffset;
                var point = new Point(currentScrollPosition, 0);
                // 计算出目标位置并滚动
                var targetPosition = item.TransformToVisual(_scrollViewer).Transform(point);

                var seto = targetPosition.X - ((_scrollViewer.ActualWidth - item.ActualWidth) / 2);

                _scrollViewer.ScrollToHorizontalOffset(seto);

            }
            else
            {
                // 获取要定位之前 ScrollViewer 目前的滚动位置
                var currentScrollPosition = _scrollViewer.VerticalOffset;
                var point = new Point(0, currentScrollPosition);
                // 计算出目标位置并滚动
                var targetPosition = item.TransformToVisual(_scrollViewer).Transform(point);

                var seto = targetPosition.Y - ((_scrollViewer.ActualHeight - item.ActualHeight) / 2);

                _scrollViewer.ScrollToVerticalOffset(seto);
            }


        }
        #endregion
    }
}
