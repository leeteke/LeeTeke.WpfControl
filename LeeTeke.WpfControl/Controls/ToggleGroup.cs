﻿using System;
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
    ///     <MyNamespace:ToggleGroup/>
    ///
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(ToggleButton))]
    public class ToggleGroup : ItemsControl
    {
        static ToggleGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleGroup), new FrameworkPropertyMetadata(typeof(ToggleGroup)));
        }

        private UIElementCollection _items;
        private object _selectedValue;
        private object _selectedItem;
        private object _selectedIndex;

        public ToggleGroup()
        {
            var hfac = new FrameworkElementFactory(typeof(StackPanel));
            hfac.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);
            hfac.AddHandler(StackPanel.LoadedEvent, new RoutedEventHandler((es, ex) =>
            {
                var panel = es as Panel;
                if (panel != null)
                {
                    _items = panel.Children;

                    if (SelectedIndex != null)
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

            }));
            ItemsPanel = new ItemsPanelTemplate(hfac);

        }

   



        #region override

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is ToggleButton toggle)
            {
                toggle.Checked += ToggleButton_Checked;
                toggle.Unchecked += ToggleButton_UnChecked;
                return true;
            }
            return false;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var toggle = new ToggleButton();
            toggle.Checked += ToggleButton_Checked;
            toggle.Unchecked += ToggleButton_UnChecked;
            return toggle;
        }




        #endregion

        #region 依赖属性

        #region Orientation

        public ToggleGroupOrientation Orientation
        {
            get { return (ToggleGroupOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(ToggleGroupOrientation), typeof(ToggleGroup), new PropertyMetadata(OrientationChanged));

        private static void OrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleGroup toggleGroup && e.NewValue is ToggleGroupOrientation orientation)
            {
                switch (orientation)
                {
                    case ToggleGroupOrientation.Horizontal:
                        var hfac = new FrameworkElementFactory(typeof(StackPanel));
                        hfac.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);
                        hfac.AddHandler(StackPanel.LoadedEvent, new RoutedEventHandler((es, ex) =>
                        {
                            var panel = es as Panel;
                            if (panel != null)
                            {
                                toggleGroup._items = panel.Children;
                            }
                        }));
                        toggleGroup.ItemsPanel = new ItemsPanelTemplate(hfac);
                        break;
                    case ToggleGroupOrientation.Vertical:

                        var vfac = new FrameworkElementFactory(typeof(StackPanel));
                        vfac.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Vertical);
                        vfac.AddHandler(StackPanel.LoadedEvent, new RoutedEventHandler((es, ex) =>
                        {
                            var panel = es as Panel;
                            if (panel != null)
                            {
                                toggleGroup._items = panel.Children;
                            }
                        }));
                        toggleGroup.ItemsPanel = new ItemsPanelTemplate(vfac);
                        break;
                    case ToggleGroupOrientation.WrapHorizontal:

                        var whfac = new FrameworkElementFactory(typeof(WrapPanel));
                        whfac.SetValue(WrapPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);
                        whfac.AddHandler(WrapPanel.LoadedEvent, new RoutedEventHandler((es, ex) =>
                        {
                            var panel = es as Panel;
                            if (panel != null)
                            {
                                toggleGroup._items = panel.Children;
                            }
                        }));
                        toggleGroup.ItemsPanel = new ItemsPanelTemplate(whfac);
                        break;
                    case ToggleGroupOrientation.WrapVertical:
                        var wvfac = new FrameworkElementFactory(typeof(WrapPanel));
                        wvfac.SetValue(WrapPanel.OrientationProperty, System.Windows.Controls.Orientation.Vertical);
                        wvfac.AddHandler(WrapPanel.LoadedEvent, new RoutedEventHandler((es, ex) =>
                        {
                            var panel = es as Panel;
                            if (panel != null)
                            {
                                toggleGroup._items = panel.Children;
                            }
                        }));
                        toggleGroup.ItemsPanel = new ItemsPanelTemplate(wvfac);
                        break;
                    default:
                        break;
                }
            }

        }


        #endregion


        #region IsClip
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsClip
        {
            get { return (bool)GetValue(IsClipProperty); }
            set { SetValue(IsClipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClipProperty =
            DependencyProperty.Register("IsClip", typeof(bool), typeof(ToggleGroup));
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ToggleGroup));
        #endregion

        #region SelectedItem
        /// <summary>
        /// 选择的Item
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ToggleGroup), new PropertyMetadata(null, new PropertyChangedCallback(SelectedItemChanged)));

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleGroup group)
            {
                if (e.NewValue != group._selectedItem)
                {

                    group.SelectedItemChanged();
                }
            }
        }

        #endregion

        #region SelectedValue


        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(ToggleGroup), new PropertyMetadata(null, new PropertyChangedCallback(SelectedValueChanged)));

        private static void SelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleGroup group)
            {
                if (e.NewValue != group._selectedValue)
                {
                    group.SelectedValueChanged();

                }
            }
        }



        #endregion

        #region SelectedIndex
        /// <summary>
        /// 请填写描述
        /// </summary>
        public object SelectedIndex
        {
            get { return (object)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(object), typeof(ToggleGroup), new PropertyMetadata(null, new PropertyChangedCallback(SelectedIndexChanged)));

        private static void SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleGroup group)
            {
                if (e.NewValue != group._selectedIndex)
                {
                    group.SelectedIndexChanged();

                }
            }
        }

        #endregion

        #region SelectionMode
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ToggleGroupMode SelectionMode
        {
            get { return (ToggleGroupMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(ToggleGroupMode), typeof(ToggleGroup), new PropertyMetadata(ToggleGroupMode.Single, new PropertyChangedCallback(SelectionModeChanged)));

        private static void SelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleGroup group)
            {
                group.ValueChanged(null);
            }
        }

        #endregion

        #region SelectedMinimum
        /// <summary>
        /// 最小选择数
        /// </summary>
        public int SelectedMinimum
        {
            get { return (int)GetValue(SelectedMinimumProperty); }
            set { SetValue(SelectedMinimumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMinimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMinimumProperty =
            DependencyProperty.Register("SelectedMinimum", typeof(int), typeof(ToggleGroup));
        #endregion

        #region SelectedMaximum
        /// <summary>
        /// 最多选择
        /// </summary>
        public int SelectedMaximum
        {
            get { return (int)GetValue(SelectedMaximumProperty); }
            set { SetValue(SelectedMaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMaximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedMaximumProperty =
            DependencyProperty.Register("SelectedMaximum", typeof(int), typeof(ToggleGroup));
        #endregion


        #region VerticalScrollBarVisibility
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalScrollBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty =
            DependencyProperty.Register("VerticalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(ToggleGroup));
        #endregion


        #region HorizontalScrollBarVisibility
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalScrollBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
            DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(ToggleGroup));
        #endregion

        #region Item功能


        #region ItemPadding
        /// <summary>
        /// ItemPadding
        /// </summary>
        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(ToggleGroup));

        #endregion


        #region ItemMargin
        /// <summary>
        /// ItemMargin
        /// </summary>
        public Thickness ItemMargin
        {
            get { return (Thickness)GetValue(ItemMarginProperty); }
            set { SetValue(ItemMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMarginProperty =
            DependencyProperty.Register("ItemMargin", typeof(Thickness), typeof(ToggleGroup));

        #endregion


        #region ItemCornerRadius
        /// <summary>
        /// item的圆角
        /// </summary>
        public CornerRadius ItemCorenerRadius
        {
            get { return (CornerRadius)GetValue(ItemCorenerRadiusProperty); }
            set { SetValue(ItemCorenerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCorenerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCorenerRadiusProperty =
            DependencyProperty.Register("ItemCorenerRadius", typeof(CornerRadius), typeof(ToggleGroup));



        #endregion



        #region ItemIsClip
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool ItemIsClip
        {
            get { return (bool)GetValue(ItemIsClipProperty); }
            set { SetValue(ItemIsClipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemIsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemIsClipProperty =
            DependencyProperty.Register("ItemIsClip", typeof(bool), typeof(ToggleGroup));
        #endregion


        #region ItemRippleBrush
        /// <summary>
        /// item的涟漪颜色
        /// </summary>
        public Brush ItemRippleBrush
        {
            get { return (Brush)GetValue(ItemRippleBrushProperty); }
            set { SetValue(ItemRippleBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemRippleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemRippleBrushProperty =
            DependencyProperty.Register("ItemRippleBrush", typeof(Brush), typeof(ToggleGroup));
        #endregion


        #region ItemBackground
        /// <summary>
        /// Item的默认背景色
        /// </summary>
        public Brush ItemBackground
        {
            get { return (Brush)GetValue(ItemBackgroundProperty); }
            set { SetValue(ItemBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemBackgroundProperty =
            DependencyProperty.Register("ItemBackground", typeof(Brush), typeof(ToggleGroup));

        #endregion


        #region ItemBorderBrush
        /// <summary>
        /// Item边框刷
        /// </summary>
        public Brush ItemBorderBrush
        {
            get { return (Brush)GetValue(ItemBorderBrushProperty); }
            set { SetValue(ItemBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemBorderBrushProperty =
            DependencyProperty.Register("ItemBorderBrush", typeof(Brush), typeof(ToggleGroup));

        #endregion


        #region ItemBorderThickness
        /// <summary>
        /// Item的边框粗细
        /// </summary>
        public Thickness ItemBorderThickness
        {
            get { return (Thickness)GetValue(ItemBorderThicknessProperty); }
            set { SetValue(ItemBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemBorderThicknessProperty =
            DependencyProperty.Register("ItemBorderThickness", typeof(Thickness), typeof(ToggleGroup));

        #endregion


        #region ItemMouseOverBackground
        /// <summary>
        /// Item鼠标移入的颜色
        /// </summary>
        public Brush ItemMouseOverBackground
        {
            get { return (Brush)GetValue(ItemMouseOverBackgroundProperty); }
            set { SetValue(ItemMouseOverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMouseOverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMouseOverBackgroundProperty =
            DependencyProperty.Register("ItemMouseOverBackground", typeof(Brush), typeof(ToggleGroup));

        #endregion


        #region ItemMouseOverBorderBrush
        /// <summary>
        /// Item鼠标移入的变宽yanse
        /// </summary>
        public Brush ItemMouseOverBorderBrush
        {
            get { return (Brush)GetValue(ItemMouseOverBorderBrushProperty); }
            set { SetValue(ItemMouseOverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMouseOverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMouseOverBorderBrushProperty =
            DependencyProperty.Register("ItemMouseOverBorderBrush", typeof(Brush), typeof(ToggleGroup));

        #endregion



        #region ItemMouseOverBorderThickness
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness ItemMouseOverBorderThickness
        {
            get { return (Thickness)GetValue(ItemMouseOverBorderThicknessProperty); }
            set { SetValue(ItemMouseOverBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMouseOverBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMouseOverBorderThicknessProperty =
            DependencyProperty.Register("ItemMouseOverBorderThickness", typeof(Thickness), typeof(ToggleGroup));
        #endregion



        #region ItemMouseOverForeground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush ItemMouseOverForeground
        {
            get { return (Brush)GetValue(ItemMouseOverForegroundProperty); }
            set { SetValue(ItemMouseOverForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMouseOverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMouseOverForegroundProperty =
            DependencyProperty.Register("ItemMouseOverForeground", typeof(Brush), typeof(ToggleGroup));
        #endregion


        #region ItemCheckedBackground
        /// <summary>
        /// Item被选中的背景色
        /// </summary>
        public Brush ItemCheckedBackground
        {
            get { return (Brush)GetValue(ItemCheckedBackgroundProperty); }
            set { SetValue(ItemCheckedBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCheckedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCheckedBackgroundProperty =
            DependencyProperty.Register("ItemCheckedBackground", typeof(Brush), typeof(ToggleGroup));

        #endregion


        #region ItemCheckedBorderBrush
        /// <summary>
        /// Item被选中的边框颜色
        /// </summary>
        public Brush ItemCheckedBorderBrush
        {
            get { return (Brush)GetValue(ItemCheckedBorderBrushProperty); }
            set { SetValue(ItemCheckedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCheckedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCheckedBorderBrushProperty =
            DependencyProperty.Register("ItemCheckedBorderBrush", typeof(Brush), typeof(ToggleGroup));

        #endregion


        #region ItemCheckedBorderThickness
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Thickness ItemCheckedBorderThickness
        {
            get { return (Thickness)GetValue(ItemCheckedBorderThicknessProperty); }
            set { SetValue(ItemCheckedBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCheckedBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCheckedBorderThicknessProperty =
            DependencyProperty.Register("ItemCheckedBorderThickness", typeof(Thickness), typeof(ToggleGroup));
        #endregion



        #region ItemCheckedForeground
        /// <summary>
        /// Item被选中的字体颜色
        /// </summary>
        public Brush ItemCheckedForeground
        {
            get { return (Brush)GetValue(ItemCheckedForegroundProperty); }
            set { SetValue(ItemCheckedForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCheckedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCheckedForegroundProperty =
            DependencyProperty.Register("ItemCheckedForeground", typeof(Brush), typeof(ToggleGroup));

        #endregion


        #region ItemMinWidth
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double ItemMinWidth
        {
            get { return (double)GetValue(ItemMinWidthProperty); }
            set { SetValue(ItemMinWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMinWidthProperty =
            DependencyProperty.Register("ItemMinWidth", typeof(double), typeof(ToggleGroup));
        #endregion


        #region ItemMinHeight
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double ItemMinHeight
        {
            get { return (double)GetValue(ItemMinHeightProperty); }
            set { SetValue(ItemMinHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMinHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMinHeightProperty =
            DependencyProperty.Register("ItemMinHeight", typeof(double), typeof(ToggleGroup));
        #endregion


        #endregion

        #region SelectionChangedCommand
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ICommand SelectionChangedCommand
        {
            get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
            set { SetValue(SelectionChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.Register("SelectionChangedCommand", typeof(ICommand), typeof(ToggleGroup));

        #endregion


        #region SelectionAlertedCommand
        /// <summary>
        /// 请添加描述
        /// </summary>
        public ICommand SelectionAlertedCommand
        {
            get { return (ICommand)GetValue(SelectionAlertedCommandProperty); }
            set { SetValue(SelectionAlertedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionAlertedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionAlertedCommandProperty =
            DependencyProperty.Register("SelectionAlertedCommand", typeof(ICommand), typeof(ToggleGroup));
        #endregion


        #endregion


        #region RouteEvent
        #region SelectionChanged
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event ToggleSelectionChangedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
        "SelectionChanged", RoutingStrategy.Bubble, typeof(ToggleSelectionChangedEventHandler), typeof(ToggleGroup));


        private void RaiseSelectionChanged(object newValue)
        {
            try
            {
                if (SelectionChangedCommand!=null&& SelectionChangedCommand.CanExecute(newValue))
                {
                    SelectionChangedCommand.Execute(SelectedValue);
                }
            }
            catch
            {

            }

            var arg = new ToggleSelectionChangedEventArgs(newValue, SelectionChangedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #region SelectionAlerted
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event RoutedEventHandler SelectionAlerted
        {
            add { AddHandler(SelectionAlertedEvent, value); }
            remove { RemoveHandler(SelectionAlertedEvent, value); }
        }

        public static readonly RoutedEvent SelectionAlertedEvent = EventManager.RegisterRoutedEvent(
        "SelectionAlerted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleGroup));


        private void RaiseSelectionAlerted()
        {
            try
            {
                if (SelectionAlertedCommand != null && SelectionAlertedCommand.CanExecute(null))
                {
                    SelectionAlertedCommand.Execute(null);
                }
            }
            catch
            {

            }
            var arg = new RoutedEventArgs(SelectionAlertedEvent, this);
            RaiseEvent(arg);
        }

        #endregion


        #endregion


        #region 内部逻辑

        #region Selected私有

        private void SelectedIndexChanged()
        {
            switch (SelectionMode)
            {
                case ToggleGroupMode.Single:
                    if (int.TryParse(SelectedIndex.ToString(), out int value))
                    {
                        IndexChanged(value);
                    }
                    break;
                case ToggleGroupMode.Multiple:

                    if (!(SelectedIndex is IList))
                    {
                        if (int.TryParse(SelectedIndex.ToString(), out int value2))
                        {
                            IndexChanged(new List<int>() { value2 });
                        }
                    }
                    else
                    {
                        IndexChanged(SelectedIndex);
                    }
                    break;
                default:
                    break;
            }
        }

        private void SelectedValueChanged()
        {
            switch (SelectionMode)
            {
                case ToggleGroupMode.Single:
                    ValueChanged(SelectedValue);
                    break;
                case ToggleGroupMode.Multiple:
                    if (!(SelectedValue is IList))
                    {
                        ValueChanged(new List<object>() { SelectedValue });
                    }
                    else
                    {
                        ValueChanged(SelectedValue);
                    }
                    break;
                default:
                    break;
            }
        }


        private void SelectedItemChanged()
        {
            switch (SelectionMode)
            {
                case ToggleGroupMode.Single:
                    if (SelectedItem is ToggleButton)
                    {
                        ItemChanged(SelectedItem);
                    }
                    break;
                case ToggleGroupMode.Multiple:
                    if (!(SelectedItem is IList))
                    {
                        if (SelectedItem is ToggleButton)
                        {
                            ItemChanged(new List<ToggleButton>() { (ToggleButton)SelectedItem });
                        }
                    }
                    else
                    {
                        ItemChanged(SelectedItem);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion



        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button)
            {
                if (ItemsSource == null)
                {
                    switch (SelectionMode)
                    {
                        case ToggleGroupMode.Single:
                            SelectedItem = button;
                            break;
                        case ToggleGroupMode.Multiple:
                            if (SelectedItem == null)
                            {
                                SelectedItem = new List<ToggleButton>() { button };
                            }

                            if (((List<ToggleButton>)SelectedItem).IndexOf(button) < 0)
                            {

                                if (SelectedMaximum > SelectedMinimum && !(((List<ToggleButton>)SelectedItem).Count < SelectedMaximum))
                                {
                                    button.IsChecked = false;
                                    RaiseSelectionAlerted();
                                    return;
                                }


                                var newValue = (List<ToggleButton>)SelectedValue;
                                newValue.Add(button);
                                ItemChanged(newValue);

                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (SelectionMode)
                    {
                        case ToggleGroupMode.Single:
                            SelectedValue = button.DataContext;
                            break;
                        case ToggleGroupMode.Multiple:
                            if (SelectedValue == null)
                            {
                                SelectedValue = new List<object>() { button.DataContext };
                            }
                            else if (((List<object>)SelectedValue).IndexOf(button.DataContext) < 0)
                            {

                                if (SelectedMaximum > SelectedMinimum && !(((List<object>)SelectedValue).Count < SelectedMaximum))
                                {
                                    button.IsChecked = false;
                                    RaiseSelectionAlerted();
                                    return;
                                }

                                var newValue = (List<object>)SelectedValue;
                                newValue.Add(button.DataContext);
                                ValueChanged(newValue);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void ToggleButton_UnChecked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button)
            {
                switch (SelectionMode)
                {
                    case ToggleGroupMode.Single:
                        if (SelectedMinimum > 0 && sender.Equals(SelectedItem))
                        {
                            button.IsChecked = true;
                            RaiseSelectionAlerted();
                            return;
                        }
                        break;
                    case ToggleGroupMode.Multiple:
                        if (SelectedMinimum > 0 && SelectedItem is IList list && list.Count <= SelectedMinimum)
                        {

                            button.IsChecked = true;
                            RaiseSelectionAlerted();
                            return;
                        }
                        break;
                    default:
                        break;
                }

                if (ItemsSource == null)
                {
                    switch (SelectionMode)
                    {
                        case ToggleGroupMode.Single:
                            if (sender.Equals(SelectedItem))
                            {
                                SelectedItem = null;
                            }
                            break;
                        case ToggleGroupMode.Multiple:

                            if (SelectedItem != null && SelectedItem is IList list)
                            {
                                list.Remove(button);
                                if (list.Count < 1)
                                {
                                    SelectedItem = null;
                                    ItemChanged(null);
                                }
                                else
                                {
                                    ItemChanged(list);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (SelectionMode)
                    {
                        case ToggleGroupMode.Single:
                            if (button.DataContext.Equals(SelectedValue))
                            {
                                SelectedValue = null;
                            }
                            break;
                        case ToggleGroupMode.Multiple:
                            if (SelectedValue != null && SelectedValue is IList list)
                            {
                                list.Remove(button.DataContext);
                                if (list.Count < 1)
                                {
                                    SelectedValue = null;
                                    ValueChanged(null);
                                }
                                else
                                {
                                    ValueChanged(list);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        private void ValueChanged(object value)
        {

            if (ItemsSource == null)
            {
                ItemChanged(value);
                return;
            }

            if (!SelectedInit(value))
                return;


            //单选项
            if (SelectionMode == ToggleGroupMode.Single)
            {



                foreach (ToggleButton chlid in _items)
                {

                    if (chlid.DataContext.Equals(value))
                    {
                        _selectedIndex = _items.IndexOf(chlid);
                        _selectedItem = chlid;
                        _selectedValue = chlid.DataContext;
                        SelectedIndex = _selectedIndex;
                        SelectedValue = _selectedValue;
                        SelectedItem = _selectedItem;
                        chlid.IsChecked = true;
                    }
                    else
                    {
                        SelectedIndex = _selectedIndex;
                        SelectedValue = _selectedValue;
                        SelectedItem = _selectedItem;
                        chlid.IsChecked = false;
                    }
                }

                RaiseSelectionChanged(SelectedValue);
                return;
            }

            //多选项
            if (SelectionMode == ToggleGroupMode.Multiple && value is IList valueList)
            {
                foreach (var singleList in valueList)
                {
                    foreach (ToggleButton child in _items)
                    {
                        if (child.DataContext.Equals(singleList))
                        {
                            #region 如果没有初始化Value值
                            if (_selectedValue == null)
                            {
                                _selectedValue = new List<object>();
                                _selectedItem = new List<ToggleButton>();
                                _selectedIndex = new List<int>();
                            }
                            #endregion
                                ((List<object>)_selectedValue).Add(child.DataContext);
                            ((List<ToggleButton>)_selectedItem).Add(child);
                            ((List<int>)_selectedIndex).Add(_items.IndexOf(child));
                            SelectedIndex = _selectedIndex;
                            SelectedValue = _selectedValue;
                            SelectedItem = _selectedItem;
                            child.IsChecked = true;
                        }

                    }
                }


                RaiseSelectionChanged(SelectedValue);
                return;
            }

        }

        private void ItemChanged(object item)
        {


            if (!SelectedInit(item))
                return;


            //单选项
            if (SelectionMode == ToggleGroupMode.Single && item is ToggleButton toggle)
            {

                foreach (ToggleButton chlid in _items)
                {
                    if (toggle.Equals(chlid))
                    {
                        _selectedIndex = _items.IndexOf(chlid);
                        _selectedItem = chlid;
                        _selectedValue = chlid;
                        SelectedIndex = _selectedIndex;
                        SelectedValue = _selectedValue;
                        SelectedItem = _selectedItem;
                        chlid.IsChecked = true;
                    }
                    else
                    {
                        SelectedIndex = _selectedIndex;
                        SelectedValue = _selectedValue;
                        SelectedItem = _selectedItem;
                        chlid.IsChecked = false;
                    }
                }
                RaiseSelectionChanged(SelectedValue);
                return;
            }

            //多选项
            if (SelectionMode == ToggleGroupMode.Multiple && item is List<ToggleButton> toogleList)
            {

                foreach (var singleList in toogleList)
                {
                    foreach (ToggleButton child in _items)
                    {
                        if (child.Equals(singleList))
                        {
                            #region 如果没有初始化Value值
                            if (_selectedValue == null)
                            {
                                _selectedValue = new List<ToggleButton>();
                                _selectedItem = new List<ToggleButton>();
                                _selectedIndex = new List<int>();
                            }
                            #endregion
                                ((List<ToggleButton>)_selectedValue).Add(child);
                            ((List<ToggleButton>)_selectedItem).Add(child);
                            ((List<int>)_selectedIndex).Add(_items.IndexOf(child));

                            SelectedIndex = _selectedIndex;
                            SelectedValue = _selectedValue;
                            SelectedItem = _selectedItem;
                            child.IsChecked = true;
                        }
                    }
                }


                RaiseSelectionChanged(SelectedValue);
                return;
            }


        }

        private void IndexChanged(object value)
        {

            if (!SelectedInit(value))
                return;

            //单选项
            if (SelectionMode == ToggleGroupMode.Single && value is int index)
            {



                foreach (ToggleButton chlid in _items)
                {
                    if (_items.IndexOf(chlid) == index)
                    {
                        _selectedIndex = _items.IndexOf(chlid);
                        _selectedItem = chlid;
                        _selectedValue = chlid;
                        SelectedIndex = _selectedIndex;
                        SelectedValue = _selectedValue;
                        SelectedItem = _selectedItem;
                        chlid.IsChecked = true;
                    }
                    else
                    {
                        SelectedIndex = _selectedIndex;
                        SelectedValue = _selectedValue;
                        SelectedItem = _selectedItem;
                        chlid.IsChecked = false;
                    }
                }
                RaiseSelectionChanged(SelectedValue);
                return;
            }

            //多选项
            if (SelectionMode == ToggleGroupMode.Multiple && value is List<int> valueList)
            {

                foreach (var singleList in valueList)
                {
                    foreach (ToggleButton child in _items)
                    {
                        if (_items.IndexOf(child) == singleList)
                        {

                            #region 如果没有初始化Value值
                            if (_selectedValue == null)
                            {
                                _selectedValue = new List<ToggleButton>();
                                _selectedItem = new List<ToggleButton>();
                                _selectedIndex = new List<int>();
                            }
                            #endregion

                             ((List<ToggleButton>)_selectedValue).Add(child);
                            ((List<ToggleButton>)_selectedItem).Add(child);
                            ((List<int>)_selectedIndex).Add(_items.IndexOf(child));

                            SelectedIndex = _selectedIndex;
                            SelectedValue = _selectedValue;
                            SelectedItem = _selectedItem;
                            child.IsChecked = true;
                        }
                    }
                }


                RaiseSelectionChanged(SelectedValue);
                return;
            }


        }

        /// <summary>
        /// 清理选择
        /// </summary>
        private bool SelectedInit(object args)
        {
            if (_items == null)
            {
                return false;
            }

            _selectedIndex = null;
            _selectedItem = null;
            _selectedValue = null;
            if (args == null)
            {
                SelectedIndex = _selectedIndex;
                SelectedValue = _selectedValue;
                SelectedItem = _selectedItem;
                foreach (ToggleButton chlid in _items)
                {
                    chlid.IsChecked = false;
                }
                RaiseSelectionChanged(SelectedValue);
                return false;
            }

            return true;
        }


        #endregion

    }
}
