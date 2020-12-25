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

        private WrapPanel _wrapPanel;
        private object _selectedValue;
        private object _selectedItem;
        private object _selectedIndex;

        public ToggleGroup()
        {
            EventManager.RegisterClassHandler(typeof(ToggleButton), ToggleButton.CheckedEvent, new RoutedEventHandler(ToggleButton_Checked));
            EventManager.RegisterClassHandler(typeof(ToggleButton), ToggleButton.UncheckedEvent, new RoutedEventHandler(ToggleButton_UnChecked));
            EventManager.RegisterClassHandler(typeof(WrapPanel), WrapPanel.LoadedEvent, new RoutedEventHandler(PART_WrapPanel_Loaded));

        }

        #region override

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ToggleButton;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ToggleButton();
        }




        #endregion

        #region 依赖属性

        #region Orientation

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ToggleGroup));


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
            if (d is ToggleGroup group && e.NewValue != e.OldValue)
            {
                if (e.NewValue != group._selectedItem)
                {
                    switch (group.SelectionMode)
                    {
                        case ToggleGroupMode.Single:
                            if (e.NewValue is ToggleButton)
                            {
                                group.ItemChangedAsync(e.NewValue);
                            }
                            break;
                        case ToggleGroupMode.Multiple:
                            if (e.NewValue is not IList)
                            {
                                if (e.NewValue is ToggleButton)
                                {
                                    group.ItemChangedAsync(new List<ToggleButton>() { (ToggleButton)e.NewValue });
                                }
                            }
                            else
                            {
                                group.ItemChangedAsync(e.NewValue);
                            }
                            break;
                        default:
                            break;
                    }

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
                    switch (group.SelectionMode)
                    {
                        case ToggleGroupMode.Single:
                            group.ValueChangedAsync(e.NewValue);
                            break;
                        case ToggleGroupMode.Multiple:
                            if (e.NewValue is not IList)
                            {
                                group.ValueChangedAsync(new List<object>() { e.NewValue });
                            }
                            else
                            {
                                group.ValueChangedAsync(e.NewValue);
                            }
                            break;
                        default:
                            break;
                    }

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
            if (d is ToggleGroup group && e.NewValue != e.OldValue)
            {
                if (e.NewValue != group._selectedIndex)
                {

                    switch (group.SelectionMode)
                    {
                        case ToggleGroupMode.Single:
                            if (int.TryParse(e.NewValue.ToString(), out int value))
                            {
                                group.IndexChangedAsync(value);
                            }
                            break;
                        case ToggleGroupMode.Multiple:
                            if (e.NewValue is not IList)
                            {
                                if (int.TryParse(e.NewValue.ToString(), out int value2))
                                {
                                    group.IndexChangedAsync(new List<int>() { value2 });
                                }
                            }
                            else
                            {
                                group.IndexChangedAsync(e.NewValue);
                            }
                            break;
                        default:
                            break;
                    }

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
                group.ValueChangedAsync(null);
            }
        }

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
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(ToggleGroup), new PropertyMetadata(new Thickness(15, 5, 15, 5)));

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
            DependencyProperty.Register("ItemMargin", typeof(Thickness), typeof(ToggleGroup), new PropertyMetadata(new Thickness(5)));

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
            DependencyProperty.Register("ItemCorenerRadius", typeof(CornerRadius), typeof(ToggleGroup), new PropertyMetadata(new CornerRadius(5)));



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
            DependencyProperty.Register("ItemBackground", typeof(Brush), typeof(ToggleGroup), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

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
            DependencyProperty.Register("ItemBorderThickness", typeof(Thickness), typeof(ToggleGroup), new PropertyMetadata(new Thickness(2)));

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
            DependencyProperty.Register("ItemCheckedForeground", typeof(Brush), typeof(ToggleGroup), new PropertyMetadata(new SolidColorBrush(Colors.White)));

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



        #endregion


        #region 路由

        /// <summary>
        /// 选择改变
        /// </summary>
        public event EventHandler<object> SelectionChanged;
        #endregion

        #region 内部逻辑


        private void PART_WrapPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is WrapPanel wrap && StaticMethods.IsInControl(this, wrap))
            {
                if (wrap.Name == "PART_WrapPanel")
                {
                    _wrapPanel = wrap;

                }
            }
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {

            if (sender is ToggleButton button && StaticMethods.IsInControl(this, button))
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
                            else if (((List<ToggleButton>)SelectedItem).IndexOf(button) < 0)
                            {
                                var newValue = (List<ToggleButton>)SelectedValue;
                                newValue.Add(button);
                                ItemChangedAsync(newValue);

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
                                var newValue = (List<object>)SelectedValue;
                                newValue.Add(button.DataContext);
                                ValueChangedAsync(newValue);
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
            if (sender is ToggleButton button && StaticMethods.IsInControl(this, button))
            {

                if (ItemsSource == null)
                {

                    switch (SelectionMode)
                    {
                        case ToggleGroupMode.Single:
                            SelectedItem = null;
                            break;
                        case ToggleGroupMode.Multiple:

                            if (SelectedItem != null && SelectedItem is IList list)
                            {
                                list.Remove(button);
                                if (list.Count < 1)
                                {
                                    SelectedItem = null;
                                    ItemChangedAsync(null);
                                }
                                else
                                {
                                    ItemChangedAsync(list);
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
                            SelectedValue = null;
                            break;
                        case ToggleGroupMode.Multiple:
                            if (SelectedValue != null && SelectedValue is IList list)
                            {
                                list.Remove(button.DataContext);
                                if (list.Count < 1)
                                {
                                    SelectedValue = null;
                                    ValueChangedAsync(null);
                                }
                                else
                                {
                                    ValueChangedAsync(list);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }


            }
        }


        private async void ValueChangedAsync(object value)
        {

            if (ItemsSource == null)
            {
                ItemChangedAsync(value);
                return;
            }

            if (!await SelectedInitAsync(value))
                return;


            //单选项
            if (SelectionMode == ToggleGroupMode.Single)
            {
                foreach (ToggleButton chlid in _wrapPanel.Children)
                {
                    if (value == chlid.DataContext)
                    {
                        _selectedIndex = _wrapPanel.Children.IndexOf(chlid);
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

                EventGO();
                return;
            }

            //多选项
            if (SelectionMode == ToggleGroupMode.Multiple && value is IList valueList)
            {
                foreach (var singleList in valueList)
                {
                    foreach (ToggleButton child in _wrapPanel.Children)
                    {
                        if (child.DataContext == singleList)
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
                            ((List<int>)_selectedIndex).Add(_wrapPanel.Children.IndexOf(child));
                            SelectedIndex = _selectedIndex;
                            SelectedValue = _selectedValue;
                            SelectedItem = _selectedItem;
                            child.IsChecked = true;
                        }

                    }
                }


                EventGO();
                return;
            }

        }

        private async void ItemChangedAsync(object item)
        {


            if (!await SelectedInitAsync(item))
                return;


            //单选项
            if (SelectionMode == ToggleGroupMode.Single && item is ToggleButton toggle)
            {

                foreach (ToggleButton chlid in _wrapPanel.Children)
                {
                    if (toggle == chlid)
                    {
                        _selectedIndex = _wrapPanel.Children.IndexOf(chlid);
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
                EventGO();
                return;
            }

            //多选项
            if (SelectionMode == ToggleGroupMode.Multiple && item is List<ToggleButton> toogleList)
            {

                foreach (var singleList in toogleList)
                {
                    foreach (ToggleButton child in _wrapPanel.Children)
                    {
                        if (child == singleList)
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
                            ((List<int>)_selectedIndex).Add(_wrapPanel.Children.IndexOf(child));

                            SelectedIndex = _selectedIndex;
                            SelectedValue = _selectedValue;
                            SelectedItem = _selectedItem;
                            child.IsChecked = true;
                        }
                    }
                }


                EventGO();
                return;
            }


        }

        private async void IndexChangedAsync(object value)
        {

            if (!await SelectedInitAsync(value))
                return;

            //单选项
            if (SelectionMode == ToggleGroupMode.Single && value is int index)
            {



                foreach (ToggleButton chlid in _wrapPanel.Children)
                {
                    if (_wrapPanel.Children.IndexOf(chlid) == index)
                    {
                        _selectedIndex = _wrapPanel.Children.IndexOf(chlid);
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
                EventGO();
                return;
            }

            //多选项
            if (SelectionMode == ToggleGroupMode.Multiple && value is List<int> valueList)
            {

                foreach (var singleList in valueList)
                {
                    foreach (ToggleButton child in _wrapPanel.Children)
                    {
                        if (_wrapPanel.Children.IndexOf(child) == singleList)
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
                            ((List<int>)_selectedIndex).Add(_wrapPanel.Children.IndexOf(child));

                            SelectedIndex = _selectedIndex;
                            SelectedValue = _selectedValue;
                            SelectedItem = _selectedItem;
                            child.IsChecked = true;
                        }
                    }
                }


                EventGO();
                return;
            }


        }

        /// <summary>
        /// 清理选择
        /// </summary>
        private async Task<bool> SelectedInitAsync(object args)
        {
            while (!IsLoaded)
            {
                await Task.Delay(1);
            }
            _selectedIndex = null;
            _selectedItem = null;
            _selectedValue = null;
            if (args == null)
            {
                SelectedIndex = _selectedIndex;
                SelectedValue = _selectedValue;
                SelectedItem = _selectedItem;
                foreach (ToggleButton chlid in _wrapPanel.Children)
                {
                    chlid.IsChecked = false;
                }
                EventGO();
                return false;
            }

            return true;
        }

        private void EventGO()
        {
            SelectionChanged?.Invoke(this, SelectedValue);
            try
            {
                SelectionChangedCommand?.Execute(SelectedValue);
            }
            catch
            {
            }
        }

        #endregion

    }
}
