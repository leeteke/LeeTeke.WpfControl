using LeeTeke.WpfControl.Dependencies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeeTeke.WpfControl.Controls
{
    /// <summary>
    /// TagControlEx.xaml 的交互逻辑
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(TagItem))]
    public partial class TagControlEx : ItemsControl
    {
        private ScrollViewer _scrollViewer;
        private StackPanel _stackPanel;


        public TagControlEx()
        {
            InitializeComponent();
            this.SetResourceReference(ToggleGroup.FocusVisualStyleProperty, "LeeFocusVisual");
            this.SetResourceReference(ToggleGroup.SnapsToDevicePixelsProperty, "LeeSnapsToDevicePixels");
            EventManager.RegisterClassHandler(typeof(TagItem), Button.ClickEvent, new RoutedEventHandler(OnCloseButtonClicked));
            EventManager.RegisterClassHandler(typeof(MenuItem), MenuItem.ClickEvent, new RoutedEventHandler(OnMenuItemClicked));
        }


        #region 属性

        /// <summary>
        /// 正在改变选择
        /// </summary>
        public bool Changing { get; set; } = false;
        #endregion

        #region override

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TagItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TagItem();
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
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(TagControlEx), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourceChanged)));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControlEx control && e.OldValue != e.NewValue)
            {
                control.ItemsSource = (IList)e.NewValue;
            }
        }


        #endregion

        #region 依赖属性

        #region Orientation

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TagControlEx), new PropertyMetadata(Orientation.Horizontal));

        #endregion

        #region ItemCornerRadius

        public CornerRadius ItemCornerRadius
        {
            get { return (CornerRadius)GetValue(ItemCornerRadiusProperty); }
            set { SetValue(ItemCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCornerRadiusProperty =
            DependencyProperty.Register("ItemCornerRadius", typeof(CornerRadius), typeof(TabControl));


        #endregion

        #region SelectedItem
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(TagControlEx), new PropertyMetadata(null, new PropertyChangedCallback(SelectedItemChanged)));

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControlEx control && e.NewValue != e.OldValue && e.NewValue is TagItem item)
            {
                if (control._stackPanel != null)
                    control.SelectedThisItem(item);
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
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(TagControlEx), new PropertyMetadata(null, new PropertyChangedCallback(SelectedValueChanged)));

        private static void SelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControlEx control && e.NewValue != e.OldValue)
            {
                if (control._stackPanel != null)
                    control.SelectedThisItem(e.NewValue);
            }
        }


        #endregion

        #region SelectedIndex

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(TagControlEx), new PropertyMetadata(-1, new PropertyChangedCallback(SelectedIndexChanged)));

        private static void SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagControlEx control && e.NewValue != e.OldValue && e.NewValue is int index)
            {
                if (control._stackPanel != null)
                    control.SelectedThisItem(index);
            }
        }


        #endregion

        #region SelectedBrush

        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(TagControlEx), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        #endregion

        #region ItemClosedCommand


        public ICommand ItemClosedCommand
        {
            get { return (ICommand)GetValue(ItemClosedCommandProperty); }
            set { SetValue(ItemClosedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemClosedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemClosedCommandProperty =
            DependencyProperty.Register("ItemClosedCommand", typeof(ICommand), typeof(TagControlEx));



        #endregion

        #region ItemSelectedCommand


        public ICommand ItemSelectedCommand
        {
            get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
            set { SetValue(ItemSelectedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSelectedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSelectedCommandProperty =
            DependencyProperty.Register("ItemSelectedCommand", typeof(ICommand), typeof(TagControlEx));




        #endregion

        #endregion

        #region Event
        /// <summary>
        /// item关闭事件
        /// </summary>
        public event EventHandler<TagItemClosedEventArgs> ItemClosedEvent;
        public event EventHandler<TagItemSelectedEventArgs> ItemSelectedEvent;
        #endregion

        #region 内部逻辑
        private void me_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Template.FindName("PART_ScrollViewer", this) is ScrollViewer scrollview)
            {
                _scrollViewer = scrollview;
            }

        }
        private void PART_StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            _stackPanel = (StackPanel)sender;
            if (SelectedIndex > -1)
            {
                SelectedThisItem(SelectedIndex);
                return;
            }

            if (SelectedItem != null)
            {
                SelectedThisItem((TagItem)SelectedItem);
                return;
            }
            if (SelectedValue != null)
            {
                SelectedThisItem(SelectedValue);
                return;
            }
        }

        private void me_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
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

        private async void OnCloseButtonClicked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button button && button.Name == "PART_CloseButton")
            {
                if (sender is TagItem tag)
                {
                    if (tag.CanClose)
                    {
                        CloseItemFromItem(tag, true);

                    }
                }
            }
        }

        private void OnMenuItemClicked(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                if (ItemsSource != null)
                {
                    switch (menuItem.Name)
                    {
                        case "PART_MenuItem_CloseAll":
                            for (int i = 0; i < ItemsSource.Count; i++)
                            {
                                CloseItemFromDataContent(ItemsSource[i]);
                            }
                            if (ItemsSource.Count > 0)
                            {
                                SelectedThisItem(ItemsSource[0]);
                            }
                            break;
                        case "PART_MenuItem_CloseOther":
                            for (int i = 0; i < ItemsSource.Count; i++)
                            {
                                if (ItemsSource[i] != menuItem.DataContext)
                                {
                                    CloseItemFromDataContent(ItemsSource[i]);
                                }
                            }
                            break;
                        case "PART_MenuItem_CloseSelf":
                            CloseItemFromDataContent(menuItem.DataContext, true);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (menuItem.Name)
                    {
                        case "PART_MenuItem_CloseAll":
                            for (int i = 0; i < Items.Count; i++)
                            {
                                CloseItemFromItem((TagItem)Items[i]);
                            }
                            if (Items.Count > 0)
                            {
                                SelectedThisItem((TagItem)Items[0]);
                            }
                            break;
                        case "PART_MenuItem_CloseOther":
                            for (int i = 0; i < Items.Count; i++)
                            {
                                if (Items[i] != SelectedItem)
                                {
                                    CloseItemFromItem((TagItem)Items[i]);
                                }
                            }
                            break;
                        case "PART_MenuItem_CloseSelf":
                            CloseItemFromItem((TagItem)SelectedItem, true);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private async Task CloseItemFromDataContent(object dataContext, bool tolast = false)
        {
            int? delIndex = null;
            for (int i = 0; i < _stackPanel.Children.Count; i++)
            {
                if (_stackPanel.Children[i] is TagItem tag)
                {
                    if (tag.DataContext == dataContext)
                    {
                        if (tag.CanClose)
                        {
                            delIndex = i;
                            await tag.CloseAsync();
                            ItemsSource.Remove(tag.DataContext);
                            try
                            {
                                ItemClosedCommand?.Execute(dataContext);
                            }
                            catch (Exception)
                            {
                            }
                            ItemClosedEvent?.Invoke(this, new TagItemClosedEventArgs(tag));

                        }
                    }
                }
            }
            if (delIndex == SelectedIndex)
            {
                if (tolast && delIndex != null)
                {
                    SelectedThisItem((int)(delIndex == 0 ? 0 : delIndex -= 1));
                }
            }
            else
            {
                if (delIndex < SelectedIndex)
                    SelectedIndex--;
            }

        }
        private async Task CloseItemFromItem(TagItem tagItem, bool tolast = false)
        {
            if (ItemsSource == null)
            {
                int? delIndex = null;
                for (int i = 0; i < _stackPanel.Children.Count; i++)
                {
                    if (_stackPanel.Children[i] is TagItem tag)
                    {
                        if (tagItem == tag)
                        {
                            if (tag.CanClose)
                            {
                                delIndex = i;
                                await tag.CloseAsync();
                                Items.Remove(tag);
                                try
                                {
                                    ItemClosedCommand?.Execute(tagItem.DataContext);
                                }
                                catch (Exception)
                                {
                                }
                                ItemClosedEvent?.Invoke(this, new TagItemClosedEventArgs(tagItem));
                            }
                        }
                    }
                }
                if (delIndex == SelectedIndex)
                {
                    if (tolast && delIndex != null)
                    {
                        SelectedThisItem((int)(delIndex == 0 ? 0 : delIndex -= 1));
                    }
                }
                else
                {
                    if (delIndex < SelectedIndex)
                        SelectedIndex--;
                }
            }
            else
            {
                CloseItemFromDataContent(tagItem.DataContext, tolast);
            }
        }

        private void TagItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TagItem content)
            {
                ///设置当前位置
                content.lastPoint = e.GetPosition(content);
                if (ItemsSource != null)
                {
                    SelectedThisItem(content.DataContext);
                }
                else
                {
                    SelectedThisItem(content);
                }
            }
        }

        #region 选择数据
        private void SelectedThisItem(int index)
        {
            if (Changing)
                return;
            Changing = true;
            if (index < 0)
            {
                foreach (TagItem item in _stackPanel.Children)
                {
                    item.IsSelected = false;
                    SelectedIndex = -1;
                    SelectedItem = null;
                    SelectedValue = null;
                }

                try
                {
                    ItemSelectedCommand?.Execute(SelectedValue);
                }
                catch (Exception)
                {
                }
                ItemSelectedEvent?.Invoke(this, new TagItemSelectedEventArgs(null));
                Changing = false;
                return;

            }
            for (int i = 0; i < _stackPanel.Children.Count; i++)
            {
                if (i == index)
                {
                    (_stackPanel.Children[i] as TagItem).IsSelected = true;
                    SelectedIndex = index;
                    SelectedItem = (_stackPanel.Children[i] as TagItem);
                    SelectedValue = (_stackPanel.Children[i] as TagItem).DataContext;
                    try
                    {
                        ItemSelectedCommand?.Execute(SelectedValue);
                    }
                    catch (Exception)
                    {
                    }
                    ItemSelectedEvent?.Invoke(this, new TagItemSelectedEventArgs(SelectedItem as TagItem));
                }
                else
                {
                    (_stackPanel.Children[i] as TagItem).IsSelected = false;
                }
            }
            Changing = false;
        }
        private void SelectedThisItem(TagItem item)
        {
            if (Changing)
                return;
            Changing = true;

            if (item == null)
            {
                for (int i = 0; i < _stackPanel.Children.Count; i++)
                {
                    (_stackPanel.Children[i] as TagItem).IsSelected = false;
                }
                try
                {
                    ItemSelectedCommand?.Execute(SelectedValue);
                }
                catch (Exception)
                {
                }
                ItemSelectedEvent?.Invoke(this, new TagItemSelectedEventArgs(null));
                Changing = false;
                return;
            }

            for (int i = 0; i < _stackPanel.Children.Count; i++)
            {
                if ((_stackPanel.Children[i] as TagItem) == item)
                {
                    item.IsSelected = true;
                    SelectedIndex = i;
                    SelectedItem = item;
                    SelectedValue = item.DataContext;
                    try
                    {
                        ItemSelectedCommand?.Execute(SelectedValue);
                    }
                    catch (Exception)
                    {
                    }
                    ItemSelectedEvent?.Invoke(this, new TagItemSelectedEventArgs(SelectedItem as TagItem));
                }
                else
                {
                    (_stackPanel.Children[i] as TagItem).IsSelected = false;
                }
            }
            Changing = false;
        }
        private void SelectedThisItem(object dataContext)
        {
            if (Changing)
                return;
            Changing = true;
            if (dataContext == null)
            {
                for (int i = 0; i < _stackPanel.Children.Count; i++)
                {
                    (_stackPanel.Children[i] as TagItem).IsSelected = false;
                }
                try
                {
                    ItemSelectedCommand?.Execute(SelectedValue);
                }
                catch (Exception)
                {
                }
                ItemSelectedEvent?.Invoke(this, new TagItemSelectedEventArgs(null));
                Changing = false;
                return;
            }

            if (ItemsSource != null)
            {
                for (int i = 0; i < _stackPanel.Children.Count; i++)
                {
                    if (dataContext == null)
                    {
                        (_stackPanel.Children[i] as TagItem).IsSelected = false;
                        continue;
                    }

                    if ((_stackPanel.Children[i] as TagItem).DataContext == dataContext)
                    {
                        (_stackPanel.Children[i] as TagItem).IsSelected = true;
                        SelectedIndex = i;
                        SelectedItem = (_stackPanel.Children[i] as TagItem);
                        SelectedValue = dataContext;
                        try
                        {
                            ItemSelectedCommand?.Execute(SelectedValue);
                        }
                        catch (Exception)
                        {
                        }
                        ItemSelectedEvent?.Invoke(this, new TagItemSelectedEventArgs(SelectedItem as TagItem));
                    }
                    else
                    {
                        (_stackPanel.Children[i] as TagItem).IsSelected = false;
                    }
                }
            }
            Changing = false;
        }
        #endregion
        private void TagItem_MouseMove(object sender, MouseEventArgs e)
        {
            #region 暂时不做


            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    if ((sender is TagItem content) && content.IsSelected)
            //    {
            //        var px = e.GetPosition(content);

            //        switch (Orientation)
            //        {
            //            case Orientation.Horizontal:
            //            //    tr.X= content.lastPoint.X - px.X ;
            //                content.lastPoint = px;
            //                break;
            //            case Orientation.Vertical:
            //                break;
            //            default:
            //                break;
            //        }

            //    }
            //}
            #endregion
        }

        private void TagItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TagItem item)
            {
                // var  item.FindName("PART_ContentPresenter");
                if (item.ContentTemplate?.HasContent == true)
                {
                    var contentPresenter = StaticMethods.FindVisualChild<ContentPresenter>(item);
                    if (contentPresenter != null)
                    {
                        if (VisualTreeHelper.GetChildrenCount(contentPresenter) > 0)
                        {
                            item.CanClose = TagControlManager.GetItemCanClose(VisualTreeHelper.GetChild(contentPresenter, 0));
                        }
                    }
                }
            }
        }

        #endregion


    }
    public class TagItem : ContentControl
    {
        public TagItem()
        {
            this.RenderTransformOrigin = new Point(0, 0.5);
        }

        public Point lastPoint;

        #region 属性
        /// <summary>
        /// 是否执行了关闭
        /// </summary>
        public bool Closed { get; private set; }

        #endregion

        #region 依赖属性

        #region Orientation

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TagItem), new PropertyMetadata(Orientation.Horizontal, new PropertyChangedCallback(OrientationChanged)));

        private static void OrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagItem control && e.OldValue != e.NewValue)
            {
                control.Orientation = (Orientation)e.NewValue;
                switch (control.Orientation)
                {
                    case Orientation.Horizontal:
                        control.RenderTransformOrigin = new Point(0, 0.5);
                        break;
                    case Orientation.Vertical:
                        control.RenderTransformOrigin = new Point(0.5, 0);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region CanClose
        public bool CanClose
        {
            get { return (bool)GetValue(CanCloseProperty); }
            set { SetValue(CanCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanCloseProperty =
            DependencyProperty.Register("CanClose", typeof(bool), typeof(TagItem), new PropertyMetadata(true));

        #endregion

        #region IsSelected


        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(TagItem));


        #endregion

        #region CornerRadius
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TagItem));
        #endregion

        #endregion


        #region 内部逻辑

        private void OnMouseDownEvent(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
        }
        #endregion

        /// <summary>
        /// 关闭Item
        /// </summary>
        public async Task CloseAsync()
        {
            if (CanClose)
            {
                var hover = new DoubleAnimationUsingKeyFrames();
                hover.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    Value = 0,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100)),
                    EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn }
                });
                hover.Completed += CLose_DoubleAnimation;
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        this.Width = ActualWidth;
                        this.MinWidth = 0;
                        this.BeginAnimation(WidthProperty, hover);
                        break;
                    case Orientation.Vertical:
                        this.Height = ActualHeight;
                        this.MinHeight = 0;
                        this.BeginAnimation(HeightProperty, hover);
                        break;
                    default:
                        break;
                }
                while (!Closed)
                {
                    await Task.Delay(100);
                }
            }
        }

        private void CLose_DoubleAnimation(object sender, EventArgs e)
        {
            Closed = true;
        }
    }

    public class TagItemClosedEventArgs : EventArgs
    {
        public TagItem Item { get; private set; }
        public TagItemClosedEventArgs(TagItem item)
        {
            Item = item;
        }
    }

    public class TagItemSelectedEventArgs : EventArgs
    {
        public TagItem Item { get; private set; }
        public TagItemSelectedEventArgs(TagItem item)
        {
            Item = item;
        }
    }
}
