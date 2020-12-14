
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
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
    /// ToggleGroup.xaml 的交互逻辑
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(ToggleButton))]
    public partial class ToggleGroup : ItemsControl
    {
        private WrapPanel _wrapPanel;
        public ToggleGroup()
        {
            InitializeComponent();

            ItemMouseOverBackground = StaticMethods.ChangeBrushDepth(Background, -0.3f);
            ItemMouseOverBorderBrush = StaticMethods.ChangeBrushDepth(BorderBrush, -0.3f);
            this.SetResourceReference(ToggleGroup.FocusVisualStyleProperty, "LeeFocusVisual");
            this.SetResourceReference(ToggleGroup.SnapsToDevicePixelsProperty, "LeeSnapsToDevicePixels");
            this.SetResourceReference(ToggleGroup.ItemBorderBrushProperty, "LeeBrush_Gray204");
            this.SetResourceReference(ToggleGroup.ItemRippleBrushProperty, "LeeBrush_RippleDefault");
            this.SetResourceReference(ToggleGroup.ItemCheckedBackgroundProperty, "LeeBrush_Main");
            this.SetResourceReference(ToggleGroup.ItemCheckedBorderBrushProperty, "LeeBrush_Main");
            this.SetResourceReference(ToggleGroup.ItemCheckedForegroundProperty, "LeeBrush_TextColor");
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

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
        }

        public new IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
                base.ItemsSource = value;
            }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(ToggleGroup), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourceChanged)));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleGroup control && e.OldValue != e.NewValue)
            {
                control.ItemsSource = (IList)e.NewValue;
            }
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
            if (d is ToggleGroup group && e.NewValue != e.OldValue)
            {
                if (e.NewValue != null)
                {
                    if (e.NewValue is IList listsValue)
                    {
                        foreach (var value in listsValue)
                        {
                            foreach (ToggleButton item in group._wrapPanel.Children)
                            {
                                if (item.DataContext == value)
                                {
                                    item.IsChecked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (ToggleButton item in group._wrapPanel.Children)
                        {
                            if (item.DataContext == e.NewValue)
                            {
                                item.IsChecked = true;
                            }
                        }
                    }
                }
            }
        }



        #endregion

        #region SelectMode



        public ToggleGroupSelectMode SelectMode
        {
            get { return (ToggleGroupSelectMode)GetValue(SelectModeProperty); }
            set { SetValue(SelectModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectModeProperty =
            DependencyProperty.Register("SelectMode", typeof(ToggleGroupSelectMode), typeof(ToggleGroup), new PropertyMetadata(ToggleGroupSelectMode.Single, new PropertyChangedCallback(SelectModeChanged)));

        private static void SelectModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleGroup group && e.NewValue != e.OldValue)
            {
                group.SelectedValue = null;
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




        #endregion

        #region 内部逻辑
        private void me_Loaded(object sender, RoutedEventArgs e)
        {
      
        }

        private void PART_WrapPanel_Loaded(object sender, RoutedEventArgs e)
        {
            _wrapPanel = (WrapPanel)sender;
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button)
            {
                if (SelectMode == ToggleGroupSelectMode.Single)
                {
                    SelectedValue = button.DataContext;
                    if (_wrapPanel != null)
                    {
                        foreach (ToggleButton item in _wrapPanel.Children)
                        {
                            if (item != button)
                            {
                                item.IsChecked = false;
                            }
                        }
                    }
                }
                else
                {
                    if (SelectedValue == null || !(SelectedValue is IList))
                        SelectedValue = new List<object>();
                    (SelectedValue as List<object>).Add(button.DataContext);
                }
            }
        }
        private void ToggleButton_UnChecked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button)
            {
                if (SelectMode == ToggleGroupSelectMode.Single)
                {
                    SelectedValue = null;
                }
                else
                {
                    if (SelectedValue != null && SelectedValue is IList list)
                    {
                        list.Remove(button.DataContext);
                    }
                }
            }
        }
        #endregion




    }

    public enum ToggleGroupSelectMode
    {/// <summary>
     /// 单个
     /// </summary>
        Single,
        /// <summary>
        /// 多个
        /// </summary>
        Multiple,
    }
}
