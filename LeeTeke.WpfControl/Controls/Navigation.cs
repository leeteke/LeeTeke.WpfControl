
using LeeTeke.WpfControl.Dependencies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
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

    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(NavigationItem))]
    [TemplatePart(Name = ElementLeft, Type = typeof(Button))]
    [TemplatePart(Name = ElementRight, Type = typeof(Button))]
    [TemplatePart(Name = ElementScrollViewer, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = ElementContextMenu, Type = typeof(ContextMenu))]
    [TemplatePart(Name = ElementMenuItemCloseAll, Type = typeof(MenuItem))]
    [TemplatePart(Name = ElementMenuItemCloseOther, Type = typeof(MenuItem))]
    [TemplatePart(Name = ElementMenuItemCloseSelf, Type = typeof(MenuItem))]
    [TemplatePart(Name = ElementMenuItemPin, Type = typeof(MenuItem))]
    [TemplatePart(Name = ElementMenuItemSelected, Type = typeof(MenuItem))]
    public class Navigation : ItemsControl
    {
        static Navigation()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Navigation), new FrameworkPropertyMetadata(typeof(Navigation)));
        }

        #region Consts
        private const string ElementLeft = "PART_Left";
        private const string ElementRight = "PART_Right";
        private const string ElementScrollViewer = "PART_ScrollViewer";
        private const string ElementContextMenu = "PART_ContextMenu";
        private const string ElementMenuItemCloseAll = "PART_MenuItem_CloseAll";
        private const string ElementMenuItemCloseOther = "PART_MenuItem_CloseOther";
        private const string ElementMenuItemCloseSelf = "PART_MenuItem_CloseSelf";
        private const string ElementMenuItemPin = "PART_MenuItem_Pin";
        private const string ElementMenuItemSelected = "PART_MenuItem_Selected";
        #endregion




        private ScrollViewer _scrollViewer;
        private Button _leftBtn;
        private Button _rightBtn;
        private ContextMenu _contextMenu;
        private MenuItem _selectedMenuItem;
        private MenuItem _pinMenuItem;
        private MenuItem _allMenuItem;
        private MenuItem _otherMenuItem;
        private MenuItem _selfMenuItem;

        private UIElementCollection _items;


        private int _currentIndex = -1;
        private NavigationItem _currentItem;
        private object _currentValue;
        private Point? _currentPoint;


        private bool _isLoaded = false;
        public Navigation()
        {


            #region ItemsPanelTemplate设置
            var hfac = new FrameworkElementFactory(typeof(StackPanel));
            hfac.SetBinding(StackPanel.OrientationProperty, new Binding() { Source = this, Path = new PropertyPath(Navigation.OrientationProperty), Mode = BindingMode.OneWay });
            hfac.AddHandler(StackPanel.LoadedEvent, new RoutedEventHandler((es, ex) =>
            {
                var panel = es as Panel;
                if (panel != null)
                {
                    _items = panel.Children;

                    _isLoaded = true;

                    if (SelectedIndex != -1)
                    {
                        ChangeSelectedIndex(SelectedIndex);
                        return;
                    }

                    if (SelectedItem != null)
                    {
                        ChangeSelectedItem(SelectedItem);
                        return;
                    }

                    if (SelectedValue != null)
                    {
                        ChangeSelectedValue(SelectedValue);
                    }

                }
            }));
            ItemsPanel = new ItemsPanelTemplate(hfac);
            #endregion

        }





        #region override


        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is NavigationItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {

            return new NavigationItem();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            UpdateSelectedIndex();
            base.OnItemsChanged(e);
        }




        protected override void OnContextMenuOpening(ContextMenuEventArgs e)
        {
            e.Handled = true;

            NavigationItem item = e.OriginalSource as NavigationItem;
            if (item == null)
            {
                if (SelectedItem != null && SelectedItem.IsMouseOver)
                {
                    item = SelectedItem;
                }
            }

            if (item != null)
            {
                if (item.IsMouseOver)
                {
                    _selectedMenuItem.Visibility = Visibility.Collapsed;
                    _contextMenu.Placement = PlacementMode.MousePoint;
                }
                else
                {
                    _selectedMenuItem.Visibility = Visibility.Visible;
                    _contextMenu.PlacementTarget = item;
                    _contextMenu.Placement = PlacementMode.Bottom;

                }

                _contextMenu.DataContext = item;
                if (item.CanClose)
                {
                    _pinMenuItem.Visibility = Visibility.Visible;
                    _selfMenuItem.Visibility = Visibility.Visible;
                    if (_pinMenuItem.Icon is TextBlock tb)
                    {
                        if (item.IsPinned)
                        {
                            tb.Text = "\xe77a";
                            _pinMenuItem.Header = "解除固定(_P)";
                        }
                        else
                        {
                            tb.Text = "\xe718";
                            _pinMenuItem.Header = "固定选项卡(_P)";
                        }
                    }
                }
                else
                {
                    _pinMenuItem.Visibility = Visibility.Collapsed;
                    _selfMenuItem.Visibility = Visibility.Collapsed;
                }
                _contextMenu.IsOpen = true;
            }

        }

        protected override void OnContextMenuClosing(ContextMenuEventArgs e)
        {
            _contextMenu.DataContext = null;

            base.OnContextMenuClosing(e);

        }
        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (_scrollViewer != null)
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        ScrollViewerManager.ScrollHorizontalOffsetAdd(_scrollViewer, e.Delta);
                        break;
                    case Orientation.Vertical:
                        ScrollViewerManager.ScrollVerticalOffsetAdd(_scrollViewer, e.Delta);
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _currentPoint = e.GetPosition(this);
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _currentPoint = null;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            _currentPoint = null;
            base.OnMouseLeave(e);
        }

        public override void OnApplyTemplate()
        {
            if (_leftBtn != null)
            {
                _leftBtn.Click -= _leftBtn_Click;
            }
            if (_rightBtn != null)
            {
                _rightBtn.Click -= _rightBtn_Click;
            }
            if (_contextMenu != null)
            {
                _contextMenu.Closed -= _contextMenu_Closed;
            }
            if (_allMenuItem != null)
            {
                _allMenuItem.Click -= _allMenuItem_Click;
            }
            if (_otherMenuItem != null)
            {
                _otherMenuItem.Click -= _otherMenuItem_Click;
            }
            if (_selfMenuItem != null)
            {
                _selfMenuItem.Click -= _selfMenuItem_Click;
            }
            if (_pinMenuItem != null)
            {
                _pinMenuItem.Click -= _pinMenuItem_Click;
            }
            if (_selectedMenuItem != null)
            {
                _selectedMenuItem.Click -= _selectedMenuItem_Click; ;
            }
            if (_scrollViewer != null)
            {
                _scrollViewer.ScrollChanged -= _scrollViewer_ScrollChanged;
                _scrollViewer.SizeChanged -= _scrollViewer_SizeChanged;
            }

            base.OnApplyTemplate();

            _leftBtn = GetTemplateChild(ElementLeft) as Button;
            _rightBtn = GetTemplateChild(ElementRight) as Button;
            _contextMenu = GetTemplateChild(ElementContextMenu) as ContextMenu;
            _allMenuItem = GetTemplateChild(ElementMenuItemCloseAll) as MenuItem;
            _otherMenuItem = GetTemplateChild(ElementMenuItemCloseOther) as MenuItem;
            _selfMenuItem = GetTemplateChild(ElementMenuItemCloseSelf) as MenuItem;
            _pinMenuItem = GetTemplateChild(ElementMenuItemPin) as MenuItem;
            _selectedMenuItem = GetTemplateChild(ElementMenuItemSelected) as MenuItem;
            _scrollViewer = GetTemplateChild(ElementScrollViewer) as ScrollViewer;


            if (_leftBtn != null)
            {
                _leftBtn.Click += _leftBtn_Click;
            }
            if (_rightBtn != null)
            {
                _rightBtn.Click += _rightBtn_Click;
            }
            if (_contextMenu != null)
            {
                _contextMenu.SetBinding(Dependencies.ContextMenuManager.ContentDockProperty, new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(Navigation.ItemContxtMenuContentDockProperty),
                    Mode = BindingMode.OneWay,
                });
                _contextMenu.SetBinding(Dependencies.ContextMenuManager.ContentProperty, new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(Navigation.ItemContxtMenuContentProperty),
                    Mode = BindingMode.OneWay,
                });
                _contextMenu.Closed += _contextMenu_Closed;
            }
            if (_allMenuItem != null)
            {
                _allMenuItem.Click += _allMenuItem_Click;
            }
            if (_otherMenuItem != null)
            {
                _otherMenuItem.Click += _otherMenuItem_Click;
            }
            if (_selfMenuItem != null)
            {
                _selfMenuItem.Click += _selfMenuItem_Click;
            }
            if (_pinMenuItem != null)
            {
                _pinMenuItem.Click += _pinMenuItem_Click;
            }
            if (_selectedMenuItem != null)
            {
                _selectedMenuItem.Click += _selectedMenuItem_Click; ;
            }
            if (_scrollViewer != null)
            {
                _scrollViewer.ScrollChanged += _scrollViewer_ScrollChanged;
                _scrollViewer.SizeChanged += _scrollViewer_SizeChanged;
            }



        }




        #endregion

        #region RouteEvent



        #region ItemClosed
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event NavigationItemColsedEventHandler ItemClosed
        {
            add { AddHandler(ItemClosedEvent, value); }
            remove { RemoveHandler(ItemClosedEvent, value); }
        }

        public static readonly RoutedEvent ItemClosedEvent = EventManager.RegisterRoutedEvent(
        "ItemClosed", RoutingStrategy.Bubble, typeof(NavigationItemColsedEventHandler), typeof(Navigation));


        private void RaiseItemClosed(object newValue)
        {
            var arg = new NavigationItemColsedEventArgs(newValue, ItemClosedEvent);
            RaiseEvent(arg);
        }

        #endregion

        #region ItemSelected
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event NavigationItemSelectedEventHandler ItemSelected
        {
            add { AddHandler(ItemSelectedEvent, value); }
            remove { RemoveHandler(ItemSelectedEvent, value); }
        }

        public static readonly RoutedEvent ItemSelectedEvent = EventManager.RegisterRoutedEvent(
        "ItemSelected", RoutingStrategy.Bubble, typeof(NavigationItemSelectedEventHandler), typeof(Navigation));


        private void RaiseItemSelected(object newValue)
        {
            var arg = new NavigationItemSelectedEventArgs(newValue, ItemSelectedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #endregion

        #region 依赖属性


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
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(Navigation), new PropertyMetadata(-1, SelectedIndexChanged));

        private static void SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Navigation tag && e.NewValue != e.OldValue)
            {
                tag.ChangeSelectedIndex((int)e.NewValue);
            }
        }

        #endregion

        #region SelectedItem
        /// <summary>
        /// 请填写描述
        /// </summary>
        public NavigationItem SelectedItem
        {
            get { return (NavigationItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(Navigation), new PropertyMetadata(null, SelectedItemChanged));

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Navigation tag && e.NewValue != e.OldValue)
            {
                tag.ChangeSelectedItem(e.NewValue as NavigationItem);
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
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(Navigation), new PropertyMetadata(null, SelectedValueChanged));

        private static void SelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (d is Navigation tag && e.NewValue != e.OldValue)
            {
                tag.ChangeSelectedValue(e.NewValue);
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
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Navigation));

        #endregion


        #region LeftButtonStyle
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Style LeftButtonStyle
        {
            get { return (Style)GetValue(LeftButtonStyleProperty); }
            set { SetValue(LeftButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftButtonStyleProperty =
            DependencyProperty.Register("LeftButtonStyle", typeof(Style), typeof(Navigation));
        #endregion


        #region RightButtonStyle
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Style RightButtonStyle
        {
            get { return (Style)GetValue(RightButtonStyleProperty); }
            set { SetValue(RightButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightButtonStyleProperty =
            DependencyProperty.Register("RightButtonStyle", typeof(Style), typeof(Navigation));
        #endregion


        #region CanItemDisplacement
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool CanItemDisplacement
        {
            get { return (bool)GetValue(CanItemDisplacementProperty); }
            set { SetValue(CanItemDisplacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanItemDisplacement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanItemDisplacementProperty =
            DependencyProperty.Register("CanItemDisplacement", typeof(bool), typeof(Navigation));
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
            DependencyProperty.Register("ItemCornerRadius", typeof(CornerRadius), typeof(Navigation));

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
            DependencyProperty.Register("ItemPinVisibly", typeof(bool), typeof(Navigation));
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
            DependencyProperty.Register("ItemMargin", typeof(Thickness), typeof(Navigation));
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
            DependencyProperty.Register("CloseVisibly", typeof(ShowMode), typeof(Navigation));
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
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(Navigation));
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
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(Navigation));
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
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(Navigation));
        #endregion


        #region IsScrollToSelected
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsScrollToSelected
        {
            get { return (bool)GetValue(IsScrollToSelectedProperty); }
            set { SetValue(IsScrollToSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsScrollToSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsScrollToSelectedProperty =
            DependencyProperty.Register("IsScrollToSelected", typeof(bool), typeof(Navigation));
        #endregion


        #region SeparatorVisibly
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool SeparatorVisibly
        {
            get { return (bool)GetValue(SeparatorVisiblyProperty); }
            set { SetValue(SeparatorVisiblyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeparatorVisibly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorVisiblyProperty =
            DependencyProperty.Register("SeparatorVisibly", typeof(bool), typeof(Navigation));
        #endregion

        #region SeparatorFill
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush SeparatorFill
        {
            get { return (Brush)GetValue(SeparatorFillProperty); }
            set { SetValue(SeparatorFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeparatorFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorFillProperty =
            DependencyProperty.Register("SeparatorFill", typeof(Brush), typeof(Navigation));
        #endregion

        #region SeparatorSize
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double SeparatorSize
        {
            get { return (double)GetValue(SeparatorSizeProperty); }
            set { SetValue(SeparatorSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeparatorSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorSizeProperty =
            DependencyProperty.Register("SeparatorSize", typeof(double), typeof(Navigation));
        #endregion


        #region MouseOverBackground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(Navigation));
        #endregion

        #region MouseOverForeground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(Navigation));
        #endregion

        #region MouseOverBorderBrush
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.Register("MouseOverBorderBrush", typeof(Brush), typeof(Navigation));
        #endregion

        #region MouseOverBorderThickness
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness MouseOverBorderThickness
        {
            get { return (Thickness)GetValue(MouseOverBorderThicknessProperty); }
            set { SetValue(MouseOverBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBorderThicknessProperty =
            DependencyProperty.Register("MouseOverBorderThickness", typeof(Thickness), typeof(Navigation));
        #endregion

        #region MouseOverEffect
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Effect MouseOverEffect
        {
            get { return (Effect)GetValue(MouseOverEffectProperty); }
            set { SetValue(MouseOverEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverEffectProperty =
            DependencyProperty.Register("MouseOverEffect", typeof(Effect), typeof(Navigation));
        #endregion

        #region SelectedBackground
        /// <summary>
        /// 请填写描述
        /// </summary>
        public Brush SelectedBackground
        {
            get { return (Brush)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register("SelectedBackground", typeof(Brush), typeof(Navigation));

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
            DependencyProperty.Register("SelectedForeground", typeof(Brush), typeof(Navigation));
        #endregion

        #region SelectedBorderBrush
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush SelectedBorderBrush
        {
            get { return (Brush)GetValue(SelectedBorderBrushProperty); }
            set { SetValue(SelectedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBorderBrushProperty =
            DependencyProperty.Register("SelectedBorderBrush", typeof(Brush), typeof(Navigation));
        #endregion

        #region SelectedBorderThickness
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness SelectedBorderThickness
        {
            get { return (Thickness)GetValue(SelectedBorderThicknessProperty); }
            set { SetValue(SelectedBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBorderThicknessProperty =
            DependencyProperty.Register("SelectedBorderThickness", typeof(Thickness), typeof(Navigation));
        #endregion

        #region SelectedEffect
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Effect SelectedEffect
        {
            get { return (Effect)GetValue(SelectedEffectProperty); }
            set { SetValue(SelectedEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEffectProperty =
            DependencyProperty.Register("SelectedEffect", typeof(Effect), typeof(Navigation));
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
            DependencyProperty.Register("ItemClosedCommand", typeof(ICommand), typeof(Navigation));

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
            DependencyProperty.Register("ItemSelectedCommand", typeof(ICommand), typeof(Navigation));

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
            DependencyProperty.Register("ShowScrollToButton", typeof(bool), typeof(Navigation));
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
            DependencyProperty.Register("ItemContxtMenuContent", typeof(object), typeof(Navigation));
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
            DependencyProperty.Register("ItemContxtMenuContentDock", typeof(Dock), typeof(Navigation));
        #endregion


        #endregion

        #region TemplateEvent
        private void _pinMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_contextMenu.DataContext is NavigationItem item)
            {
                item.IsPinned = !item.IsPinned;
                if (item.IsPinned)
                {
                    PinnedMove(item);
                }
                else
                {
                    UnPinnedMove(item);
                }
            }
        }
        private void _selectedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_contextMenu.DataContext is NavigationItem item)
            {
                ChangeSelectedItem(item);
            }
        }
        private void _selfMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_contextMenu.DataContext is NavigationItem item)
            {
                ///关闭我自己
                ItemCloseSelf(item);
            }
        }

        private void _otherMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_contextMenu.DataContext is NavigationItem item)
            {
                ItemCloseOther(item);
            }
        }

        private void _contextMenu_Closed(object sender, RoutedEventArgs e)
        {
            _contextMenu.DataContext = this.DataContext;
        }

        private void _allMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //关闭全部Item
            if (_contextMenu.DataContext is NavigationItem item)
            {
                ///关闭我自己
                ItemCloseAll(item);
            }
        }

        private void _rightBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Orientation == Orientation.Horizontal && _scrollViewer != null)
            {
                ScrollViewerManager.ScrollHorizontalOffsetAdd(_scrollViewer, -120);
            }
        }

        private void _leftBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Orientation == Orientation.Horizontal && _scrollViewer != null)
            {
                ScrollViewerManager.ScrollHorizontalOffsetAdd(_scrollViewer, 120);
            }
        }

        private void _scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (Orientation == Orientation.Horizontal && ShowScrollToButton && sender is ScrollViewer scrollViewer)
            {
                if (scrollViewer.ScrollableWidth > 0)
                {
                    _leftBtn.Visibility = Visibility.Visible;
                    _rightBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    _leftBtn.Visibility = Visibility.Collapsed;
                    _rightBtn.Visibility = Visibility.Collapsed;
                }

                _leftBtn.IsEnabled = e.HorizontalOffset != 0;

                _rightBtn.IsEnabled = e.HorizontalOffset! < scrollViewer.ScrollableWidth;
            }
        }

        private void _scrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SelectedItem is NavigationItem item)
            {
                if (IsScrollToSelected)
                {
                    ScrollToItem(item);
                }
            }
        }

        #endregion

        #region PublicMethod

        public void ScrollToItem(NavigationItem item)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {

                    if (Orientation == Orientation.Horizontal)
                    {
                        // 获取要定位之前 ScrollViewer 目前的滚动位置
                        var currentScrollPosition = _scrollViewer.HorizontalOffset;
                        var point = new Point(currentScrollPosition, 0);
                        // 计算出目标位置并滚动
                        var targetPosition = item.TransformToVisual(_scrollViewer).Transform(point);
                        var seto = targetPosition.X - ((_scrollViewer.ActualWidth - item.ActualWidth) / 2);
                        ScrollViewerManager.ScrollToHorizontalOffset(_scrollViewer, seto);
                    }
                    else
                    {
                        // 获取要定位之前 ScrollViewer 目前的滚动位置
                        var currentScrollPosition = _scrollViewer.VerticalOffset;
                        var point = new Point(0, currentScrollPosition);
                        // 计算出目标位置并滚动
                        var targetPosition = item.TransformToVisual(_scrollViewer).Transform(point);

                        var seto = targetPosition.Y - ((_scrollViewer.ActualHeight - item.ActualHeight) / 2);

                        ScrollViewerManager.ScrollToVerticalOffset(_scrollViewer, seto);
                    }

                });
            }
            catch
            {

            }
        }
        #endregion

        #region internalMethod

        internal bool CanItemMove
        {
            get
            {
                if (!CanItemDisplacement)
                    return false;

                if (_currentPoint == null)
                    return false;

                return Orientation switch
                {
                    Orientation.Horizontal => Math.Abs(Mouse.GetPosition(this).X - _currentPoint.Value.X) > 5,
                    Orientation.Vertical => Math.Abs(Mouse.GetPosition(this).Y - _currentPoint.Value.Y) > 0,
                    _ => false,
                };
            }
        }


        /// <summary>
        /// 获取之前的Item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        internal NavigationItem GetBeforeItem(NavigationItem item, bool forMove = true)
        {
            var func = new Func<IList, NavigationItem>(items =>
            {
                var itemIndex = items.IndexOf(item);
                if (itemIndex > 0)
                {
                    if (forMove)
                    {
                        var getItem = items[itemIndex - 1] as NavigationItem;
                        if (getItem != null)
                        {
                            if (item.IsPinned || !item.CanClose)
                            {
                                if (getItem.IsPinned || !getItem.CanClose)
                                {
                                    return getItem;
                                }
                            }
                            else
                            {
                                if (!getItem.IsPinned && getItem.CanClose)
                                {
                                    return getItem;
                                }
                            }
                        }
                    }
                    else
                    {
                        return items[itemIndex - 1] as NavigationItem;
                    }

                }

                return null;
            });

            return ItemsSource == null ? func(Items) : func(_items);
        }

        /// <summary>
        /// 获取之后的Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal NavigationItem GetAfterItem(NavigationItem item, bool forMove = true)
        {
            var func = new Func<IList, NavigationItem>(items =>
            {
                var itemIndex = items.IndexOf(item);
                if (itemIndex > -1 && itemIndex < items.Count - 1)
                {
                    if (forMove)
                    {
                        var getItem = items[itemIndex + 1] as NavigationItem;
                        if (getItem != null)
                        {
                            if (item.IsPinned || !item.CanClose)
                            {
                                if (getItem.IsPinned || !getItem.CanClose)
                                {
                                    return getItem;
                                }
                            }
                            else
                            {
                                if (!getItem.IsPinned && getItem.CanClose)
                                {
                                    return getItem;
                                }
                            }
                        }
                    }
                    else
                    {
                        return items[itemIndex + 1] as NavigationItem;
                    }

                }
                return null;
            });

            return ItemsSource == null ? func(Items) : func(_items);
        }

        /// <summary>
        /// 通知Item关闭
        /// </summary>
        /// <param name="item"></param>
        internal void NotifyItemClose(NavigationItem item)
        {
            var closeItem = GetItemFormItem(item);
            if (closeItem != null)
            {
                ChangedCloseItem(item);
            }
        }

        /// <summary>
        /// 通知Item选择
        /// </summary>
        /// <param name="item"></param>
        internal void NotifyItemSelecte(NavigationItem item)
        {
            var closeItem = GetItemFormItem(item);
            if (closeItem != null)
            {
                ChangeSelectedItem(item);
            }

        }

        /// <summary>
        /// 通知Item选择
        /// </summary>
        /// <param name="item"></param>
        internal void NotifyItemMove(NavigationItem item, bool isforward)
        {
            var closeItem = GetItemFormItem(item);
            if (closeItem != null)
            {

                if (ItemsSource == null)
                {
                    ItemMove(item, isforward ? Items.IndexOf(item) - 1 : Items.IndexOf(item) + 1);

                }
                else
                {
                    ItemMove(item, isforward ? _items.IndexOf(item) - 1 : _items.IndexOf(item) + 1);
                }

            }

        }
        #endregion

        #region PrivateMethod


        /// <summary>
        /// 关闭所有的Item
        /// </summary>
        private void ItemCloseAll(NavigationItem self)
        {
            bool needSelected = (!self.CanClose || SelectedIndex == -1);

            List<NavigationItem> closeItem = new List<NavigationItem>();

            ///关闭方法
            var action = new Action<NavigationItem>(x =>
           {
               if ((x.IsPinned || !x.CanClose) && !needSelected)
               {
                   ///指定给他
                   needSelected = true;
                   ChangeSelectedItem(x);
               }
               else if (!x.IsPinned && x.CanClose)
               {
                   closeItem.Add(x);
               }
           });

            if (ItemsSource == null)
            {
                try
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        if (Items[i] is NavigationItem ni)
                        {
                            action(ni);
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    action(_items[i] as NavigationItem);
                }
            }

            if (!needSelected)
            {
                ChangeSelectedItem(null);
            }


            foreach (var item in closeItem)
            {
                item.Close();
            }

        }

        private void ItemCloseOther(NavigationItem self)
        {


            List<NavigationItem> closeItem = new List<NavigationItem>();

            ///关闭方法
            var action = new Action<NavigationItem>(x =>
            {
                if (!x.IsPinned && x.CanClose && x != self)
                {
                    closeItem.Add(x);
                }
            });

            if (ItemsSource == null)
            {
                try
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        if (Items[i] is NavigationItem ni)
                        {
                            action(ni);
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    action(_items[i] as NavigationItem);
                }
            }


            foreach (var item in closeItem)
            {
                item.Close();
            }

        }

        private void ItemCloseSelf(NavigationItem self)
        {

            self.SelfClose = true;
            self.Close();

        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="item"></param>
        private void ChangedCloseItem(NavigationItem item)
        {
            if (!item.IsClosed)
                return;


            bool needSelected = SelectedIndex == -1;

            if (ItemsSource == null)
            {
                if (item.SelfClose)
                {
                    try
                    {
                        for (int i = 0; i < Items.Count; i++)
                        {
                            if (!needSelected && Items[i] is NavigationItem ni && ni == item)
                            {
                                if (i > 0)
                                {
                                    ///默认选择前一个
                                    ChangeSelectedItem(Items[i - 1] as NavigationItem);
                                }
                                else if (i < Items.Count - 1)
                                {
                                    ///默认选择前一个
                                    ChangeSelectedItem(Items[i + 1] as NavigationItem);
                                }
                                break;
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                Items.Remove(item);
            }
            else
            {
                if (item.SelfClose)
                {
                    for (int i = 0; i < _items.Count; i++)
                    {
                        if (!needSelected && _items[i] == item)
                        {
                            if (i > 0)
                            {
                                ///默认选择前一个
                                ChangeSelectedItem(_items[i - 1] as NavigationItem);
                            }
                            else if (i < Items.Count - 1)
                            {
                                ///默认选择前一个
                                ChangeSelectedItem(_items[i + 1] as NavigationItem);
                            }
                            break;
                        }
                    }
                }

                var list = ItemsSource as IList;
                if (list != null)
                    list.Remove(item.DataContext);

            }
            UpdateSelectedIndex();
            ItemRemoveChanged(item);

        }

        /// <summary>
        /// 选择Index
        /// </summary>
        /// <param name="index"></param>
        private void ChangeSelectedIndex(int index)
        {

            if (index == _currentIndex)
            {
                return;
            }

            ChangeSelectedItem(GetItemFormIndex(index));

        }

        private void ChangeSelectedValue(object vaule)
        {


            if (vaule == _currentValue)
            {
                return;
            }

            ChangeSelectedItem(GetItemFormDataContext(vaule));
        }
        /// <summary>
        /// 选择Item
        /// </summary>
        /// <param name="item"></param>
        private void ChangeSelectedItem(NavigationItem item)
        {
            if (!_isLoaded)
                return;

            if (item == null)
            {
                SelectedIndex = _currentIndex = -1;
                SelectedItem = _currentItem = null;
                SelectedValue = _currentValue = null;
                return;
            }

            if (item == _currentItem)
                return;

            if (!item.IsSelected)
            {
                item.IsSelected = true;
                return;
            }

            if (IsScrollToSelected)
            {
                ScrollToItem(item);
            }


            if (ItemsSource == null)
            {
                try
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        if (Items[i] is NavigationItem ni)
                        {
                            if (ni == item)
                            {
                                SelectedIndex = _currentIndex = i;
                                SelectedItem = _currentItem = item;
                                SelectedValue = _currentValue = item.DataContext ?? item;
                            }
                            else
                            {
                                ni.IsSelected = false;
                            }
                        }
                    }

                }
                catch
                {
                }

            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i] == item)
                    {
                        SelectedIndex = _currentIndex = i;
                        SelectedItem = _currentItem = item;
                        SelectedValue = _currentValue = item.DataContext ?? item;
                    }
                    else
                    {
                        ((NavigationItem)_items[i]).IsSelected = false;
                    }
                }
            }
            ItemSelectedChanged(item);

        }

        /// <summary>
        /// 更新选项
        /// </summary>
        private void UpdateSelectedIndex()
        {
            if (SelectedItem != null && SelectedValue != null)
            {
                if (ItemsSource == null)
                {
                    SelectedIndex = _currentIndex = ItemContainerGenerator.IndexFromContainer(SelectedItem);
                }
                else
                {
                    var list = ItemsSource as IList;
                    if (list != null)
                    {
                        SelectedIndex = _currentIndex = list.IndexOf(SelectedValue);
                    }
                }

            }
        }

        /// <summary>
        /// Item移动事件发生
        /// </summary>
        /// <param name="item"></param>
        private void ItemRemoveChanged(NavigationItem item)
        {
            RaiseItemClosed(item);
            try
            {
                if (ItemClosedCommand != null)
                {
                    var commandArgs = ItemsSource == null ? item : item?.DataContext;
                    if (ItemClosedCommand.CanExecute(commandArgs))
                    {
                        ItemClosedCommand.Execute(commandArgs);
                    }
                }

            }
            catch
            {
            }
        }
        /// <summary>
        /// Item选择事件发生
        /// </summary>
        /// <param name="item"></param>
        private void ItemSelectedChanged(NavigationItem item)
        {
            RaiseItemSelected(item);
            try
            {
                if (ItemSelectedCommand != null)
                {
                    var commandArgs = ItemsSource == null ? item : item?.DataContext;
                    if (ItemSelectedCommand.CanExecute(commandArgs))
                    {
                        ItemSelectedCommand.Execute(commandArgs);
                    }
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 获取Item来自序列
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private NavigationItem GetItemFormItem(NavigationItem item)
        {
            if (ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            if (ItemsSource == null)
            {
                return ItemContainerGenerator.ContainerFromItem(item) as NavigationItem;
            }

            if (_items == null)
                return null;

            if (_items.Contains(item))
            {
                return item;
            }

            return null;
        }

        /// <summary>
        /// 获取Item来自序列
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private NavigationItem GetItemFormIndex(int index)
        {
            if (ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            if (ItemsSource == null)
            {
                return ItemContainerGenerator.ContainerFromIndex(index) as NavigationItem;
            }

            if (_items == null)
                return null;

            if (index > -1 && index < _items.Count)
            {
                return _items[index] as NavigationItem;
            }

            return null;

        }

        /// <summary>
        /// 获取Item来自DataContext
        /// </summary>
        /// <param name="datacontext"></param>
        /// <returns></returns>
        private NavigationItem GetItemFormDataContext(object dataContext)
        {
            try
            {

                if (dataContext == null)
                    return null;

                if (ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                    return null;

                if (ItemsSource == null)
                {
                    foreach (NavigationItem item in Items)
                    {
                        if (item == dataContext || item.DataContext == dataContext)
                        {
                            return item;
                        }
                    }
                }
                if (_items == null)
                    return null;

                foreach (var item in _items)
                {
                    if (item is NavigationItem element && element.DataContext == dataContext)
                    {
                        return element;
                    }
                }

                return null;
            }
            catch
            {
                return null;

            }
        }

        /// <summary>
        /// 固定后移动
        /// </summary>
        /// <param name="item"></param>
        private void PinnedMove(NavigationItem item)
        {


            var toalIndex = -1;
            if (ItemsSource == null)
            {

                for (int i = 0; i < Items.Count; i++)
                {
                    ///不用动
                    if (Items[i] == item && toalIndex < 0)
                    {
                        break;
                    }

                    if (Items[i] is NavigationItem checkItem && checkItem.CanClose && !checkItem.IsPinned)
                    {
                        toalIndex = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    ///不用动
                    if (_items[i] == item && toalIndex < 0)
                    {
                        break;
                    }

                    if (_items[i] is NavigationItem checkItem && checkItem.CanClose && !checkItem.IsPinned)
                    {
                        toalIndex = i;
                        break;
                    }
                }


            }
            ItemMove(item, toalIndex);
            if (IsScrollToSelected && SelectedItem == item)
            {
                ///增加延时移动
                Task.Run(async () =>
                {
                    await Task.Delay(60);
                    ScrollToItem(item);
                });

            }
        }

        /// <summary>
        /// 取消固定后移动
        /// </summary>
        /// <param name="item"></param>
        private void UnPinnedMove(NavigationItem item)
        {

            if (item.IsPinned)
                return;

            var toalIndex = -1;

            if (ItemsSource == null)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    ///不用动
                    if (Items[i] != item && Items[i] is NavigationItem checkItem && checkItem.CanClose && !checkItem.IsPinned)
                    {

                        toalIndex = i - 1;
                        break;
                    }
                    ///如果没有找到则释放到最后
                    if (toalIndex == -1)
                    {
                        toalIndex = _items.Count - 1;
                    }
                }

            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {

                    if (_items[i] != item && _items[i] is NavigationItem checkItem && checkItem.CanClose && !checkItem.IsPinned)
                    {
                        toalIndex = i - 1;
                        break;
                    }
                }
                ///如果没有找到则释放到最后
                if (toalIndex == -1)
                {
                    toalIndex = _items.Count - 1;
                }


            }
            ItemMove(item, toalIndex);
            if (IsScrollToSelected && SelectedItem == item)
            {
                ///增加延时移动
                Task.Run(async () =>
                {
                    await Task.Delay(60);
                    ScrollToItem(item);
                });
            }
        }

        /// <summary>
        /// ItemMove
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toalIndex"></param>
        private void ItemMove(NavigationItem item, int toalIndex)
        {
            try
            {
                if (toalIndex < 0 || toalIndex >= Items.Count)
                    return;

                if (ItemsSource == null)
                {
                    var oldIndex = Items.IndexOf(item);
                    if (oldIndex > -1 && oldIndex != toalIndex)
                    {
                        Items.Remove(item);
                        Items.Insert(toalIndex, item);
                        UpdateSelectedIndex();
                        return;
                    }
                }
                else
                {
                    var oldIndex = _items.IndexOf(item);
                    if (oldIndex > -1 && oldIndex != toalIndex)
                    {
                        #region ItemSource的移动方式
                        var sourceType = ItemsSource.GetType();
                        ///泛型
                        if (sourceType.IsGenericType && sourceType.GenericTypeArguments.Length == 1 && ItemsSource is IList list)
                        {

                            ///看看支持Move方法不，也就是是否是 ob<>
                            var method = sourceType.GetMethod("Move");
                            if (method != null)
                            {
                                _ = method.Invoke(ItemsSource, new object?[] { oldIndex, toalIndex });
                            }
                            else
                            {
                                list.Remove(item.DataContext);
                                list.Insert(toalIndex, item.DataContext);
                            }

                            UpdateSelectedIndex();
                            return;
                        }
                        else
                        {
                            return;
                        }
                        #endregion
                    }
                }


            }
            catch
            {

            }
        }

        #endregion


    }
}
