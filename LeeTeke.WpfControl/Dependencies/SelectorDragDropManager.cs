using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LeeTeke.WpfControl.Dependencies
{
    public class SelectorDragDropManager
    {
        #region IsEnabled
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for Open.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(SelectorDragDropManager), new PropertyMetadata(OnIsEnabledChanged));

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Selector items)
            {
                if (GetSelectorDragDropService(d) is SelectorDragDropService dds)
                {
                    dds.Dispose();
                }
                if (e.NewValue is bool isopen && isopen)
                {
                    var newDDS = new SelectorDragDropService(items);

                    newDDS.DragAdornerOpacity = GetDragAdornerOpacity(d);
                    newDDS.LockDragOrientation = GetLockDragOrientation(d);
                    newDDS.FreezeIndex = GetFreezeIndex(d);
                    SetSelectorDragDropService(d, newDDS);
                    newDDS.Init();
                }

            }
        }
        #endregion


        #region DragAdornerOpacity
        public static double GetDragAdornerOpacity(DependencyObject obj)
        {
            var drop = GetSelectorDragDropService(obj);
            if (drop != null)
                return drop.DragAdornerOpacity;
            return (double)obj.GetValue(DragAdornerOpacityProperty);
        }

        public static void SetDragAdornerOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(DragAdornerOpacityProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragAdornerOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragAdornerOpacityProperty =
            DependencyProperty.RegisterAttached("DragAdornerOpacity", typeof(double), typeof(SelectorDragDropManager), new PropertyMetadata(0.7, OnDragAdornerOpacityChanged));

        private static void OnDragAdornerOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetSelectorDragDropService(d) is SelectorDragDropService drop)
                drop.DragAdornerOpacity = (double)e.NewValue;
        }
        #endregion

        #region IsDragInProgress
        public static bool GetIsDragInProgress(DependencyObject obj)
        {
            var have = GetSelectorDragDropService(obj);
            if (have != null)
                return have.IsDragInProgress;
            return (bool)obj.GetValue(IsDragInProgressProperty);
        }

        // Using a DependencyProperty as the backing store for IsDragInProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDragInProgressProperty =
            DependencyProperty.RegisterAttached("IsDragInProgress", typeof(bool), typeof(SelectorDragDropManager));
        #endregion

        #region LockDragOrientation
        public static Orientation? GetLockDragOrientation(DependencyObject obj)
        {
            var drop = GetSelectorDragDropService(obj);
            if (drop != null)
                return drop.LockDragOrientation;
            return (Orientation?)obj.GetValue(LockDragOrientationProperty);
        }

        public static void SetLockDragOrientation(DependencyObject obj, Orientation? value)
        {
            obj.SetValue(LockDragOrientationProperty, value);
        }

        // Using a DependencyProperty as the backing store for LockDragOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LockDragOrientationProperty =
            DependencyProperty.RegisterAttached("LockDragOrientation", typeof(Orientation?), typeof(SelectorDragDropManager), new PropertyMetadata(OnLockDragOrientationChanged));

        private static void OnLockDragOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetSelectorDragDropService(d) is SelectorDragDropService drop)
                drop.LockDragOrientation = (Orientation?)e.NewValue;
        }
        #endregion

        #region FreezeIndex
        public static int GetFreezeIndex(DependencyObject obj)
        {
            var drop = GetSelectorDragDropService(obj);
            if (drop != null)
                return drop.FreezeIndex;
            return (int)obj.GetValue(FreezeIndexProperty);
        }

        public static void SetFreezeIndex(DependencyObject obj, int value)
        {
            obj.SetValue(FreezeIndexProperty, value);
        }

        // Using a DependencyProperty as the backing store for FreezeIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FreezeIndexProperty =
            DependencyProperty.RegisterAttached("FreezeIndex", typeof(int), typeof(SelectorDragDropManager), new PropertyMetadata(-1, OnFreezeIndexChanged));

        private static void OnFreezeIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetSelectorDragDropService(d) is SelectorDragDropService drop)
                drop.FreezeIndex = (int)e.NewValue;
        }
        #endregion

        #region SelectorDragDropService
        static SelectorDragDropService GetSelectorDragDropService(DependencyObject obj)
        {
            return (SelectorDragDropService)obj.GetValue(SelectorDragDropServiceProperty);
        }

        static void SetSelectorDragDropService(DependencyObject obj, SelectorDragDropService value)
        {
            obj.SetValue(SelectorDragDropServiceProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectorDragDropService.  This enables animation, styling, binding, etc...
        static readonly DependencyProperty SelectorDragDropServiceProperty =
           DependencyProperty.RegisterAttached("SelectorDragDropService", typeof(SelectorDragDropService), typeof(SelectorDragDropManager));
        #endregion





        #region DragDropOver

        public static void AddDragDropOverHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie)
                uie.AddHandler(DragDropOverEvent, handler);
        }
        public static void RemoveDragDropOverHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie)
                uie.RemoveHandler(DragDropOverEvent, handler);
        }

        public static readonly RoutedEvent DragDropOverEvent =
            EventManager.RegisterRoutedEvent("DragDropOver", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SelectorDragDropManager));

        #endregion



        #region DragDropOverCommand
        public static ICommand GetDragDropOverCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DragDropOverCommandProperty);
        }

        public static void SetDragDropOverCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DragDropOverCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragDropOverCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragDropOverCommandProperty =
            DependencyProperty.RegisterAttached("DragDropOverCommand", typeof(ICommand), typeof(SelectorDragDropManager));
        #endregion




    }

    class SelectorDragDropService : IDisposable
    {

        #region DragAdornerOpacity

        /// <summary>
        /// Gets/sets the opacity of the drag adorner.  This property has no
        /// effect if ShowDragAdorner is false. The default value is 0.7
        /// </summary>
        public double DragAdornerOpacity
        {
            get { return this._dragAdornerOpacity; }
            set
            {
                if (this.IsDragInProgress)
                    throw new InvalidOperationException("Cannot set the DragAdornerOpacity property during a drag operation.");

                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException("DragAdornerOpacity", value, "Must be between 0 and 1.");

                this._dragAdornerOpacity = value;
            }
        }

        #endregion // DragAdornerOpacity

        #region IsDragInProgress

        /// <summary>
        /// Returns true if there is currently a drag operation being managed.
        /// </summary>
        public bool IsDragInProgress
        {
            get { return _isDragInProgress; }
        }

        #endregion // IsDragInProgress

        #region LockDragOrientation
        public Orientation? LockDragOrientation { get; set; }

        #endregion

        #region 锁定头部
        public int FreezeIndex { get; set; } = -1;
        #endregion



        private Selector _control;
        private bool _canInitiateDrag;
        private DragAdorner? _dragAdorner;
        private double _dragAdornerOpacity;
        private int _indexToSelect;
        private bool _isDragInProgress;
        private FrameworkElement? _itemUnderDragCursor;
        private Point _ptMouseDown;



        public SelectorDragDropService(Selector control)
        {
            _control = control;
            _canInitiateDrag = false;
            _indexToSelect = -1;
        }

        public void Init()
        {
            try
            {
                _control.AllowDrop = true;
                _control.PreviewMouseLeftButtonDown += _control_PreviewMouseLeftButtonDown;
                _control.PreviewMouseMove += _control_PreviewMouseMove;
                _control.DragOver += _control_DragOver;
                _control.DragLeave += _control_DragLeave;
                _control.DragEnter += _control_DragEnter;
                _control.Drop += _control_Drop;
            }
            catch
            {
            }
        }

        #region Event


        private void _control_Drop(object sender, DragEventArgs e)
        {
            if (this.ItemUnderDragCursor != null)
                this.ItemUnderDragCursor = null;
            e.Effects = DragDropEffects.None;
            if (_dragAdorner == null)
            {
                return;
            }

            if (_control.ItemsSource != null)
            {
                var sourceType = _control.ItemsSource.GetType();
                ///泛型
                if (sourceType.IsGenericType && sourceType.GenericTypeArguments.Length == 1 && _control.ItemsSource is IList list)
                {
                    var oldIndex = list.IndexOf(_dragAdorner.AdornedItem.DataContext);
                    if (oldIndex > -1)
                    {
                        var toalIndex = GetIndexUnderDragCursor();
                        if (toalIndex > -1 && toalIndex > FreezeIndex && toalIndex != oldIndex)
                        {
                            ///看看支持Move方法不，也就是是否是 ob<>
                            var method = sourceType.GetMethod("Move");
                            if (method != null)
                            {
                                _ = method.Invoke(_control.ItemsSource, new object[] { oldIndex, toalIndex });
                            }
                            else
                            {
                                list.Remove(_dragAdorner.AdornedItem.DataContext);
                                list.Insert(toalIndex, _dragAdorner.AdornedItem.DataContext);
                            }

                        }
                        e.Effects = DragDropEffects.Move;
                    }



                }
            }
            else if (e.Source is FrameworkElement contentItem && Helper.IsInControl(_control, contentItem))
            {

                var oldIndex = _control.Items.IndexOf(contentItem);
                if (oldIndex > FreezeIndex)
                {
                    _control.Items.Remove(_dragAdorner.AdornedItem);
                    _control.Items.Insert(oldIndex, _dragAdorner.AdornedItem);
                }
                e.Effects = DragDropEffects.Move;
            }

            #region 触发命令与事件

            #region Event
            var routedArgs = new RoutedEventArgs(SelectorDragDropManager.DragDropOverEvent, _control);
            _control.RaiseEvent(routedArgs);
            #endregion
            #region Command
            if (SelectorDragDropManager.GetDragDropOverCommand(_control) is ICommand command)
            {
                if (command.CanExecute(_control))
                {
                    command.Execute(_control);
                }
            }
            #endregion

            #endregion


        }

        private void _control_DragEnter(object sender, DragEventArgs e)
        {
            if (this._dragAdorner != null && this._dragAdorner.Visibility != Visibility.Visible)
            {
                // Update the location of the adorner and then show it.				
                this.UpdateDragAdornerLocation();
                this._dragAdorner.Visibility = Visibility.Visible;
            }
        }

        private void _control_DragLeave(object sender, DragEventArgs e)
        {
            if (!this.IsMouseOver(_control))
            {

                if (this._dragAdorner != null)
                    this._dragAdorner.Visibility = Visibility.Collapsed;
            }
        }

        private void _control_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;

            if (_dragAdornerOpacity > 0.0)
                UpdateDragAdornerLocation();


        }

        private void _control_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!CanStartDragOperation)
                return;



            //Select the item the user clicked on.
            if (_control.SelectedIndex != this._indexToSelect)
                _control.SelectedIndex = this._indexToSelect;

            //     If the item at the selected index is null, there's nothing
            //      we can do, so just return;
            if (_control.SelectedItem == null)
                return;

            ///锁定
            if (_indexToSelect <= FreezeIndex)
            {
                return;
            }

            var itemToDrag = this.GetItem(_control.SelectedIndex);
            if (itemToDrag == null)
                return;

            AdornerLayer? adornerLayer = _dragAdornerOpacity > 0.0 ? this.InitializeAdornerLayer(itemToDrag) : null;


            this.InitializeDragOperation(itemToDrag);
            this.PerformDragOperation();
            this.FinishDragOperation(itemToDrag, adornerLayer);
        }

        private void _control_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.IsMouseOverScrollbar)
            {
                // 4/13/2007 - Set the flag to false when cursor is over scrollbar.
                _canInitiateDrag = false;
                return;
            }

            int index = GetIndexUnderDragCursor();
            this._canInitiateDrag = index > -1;

            if (this._canInitiateDrag)
            {
                // Remember the location and index of the ListViewItem the user clicked on for later.
                this._ptMouseDown = Helper.GetMousePosition(_control);
                this._indexToSelect = index;
            }
            else
            {
                this._ptMouseDown = new Point(-10000, -10000);
                this._indexToSelect = -1;
            }
        }
        #endregion

        #region Private

        #region CanStartDragOperation

        private bool CanStartDragOperation
        {
            get
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed)
                    return false;

                if (!this._canInitiateDrag)
                    return false;

                if (this._indexToSelect == -1)
                    return false;

                if (!this.HasCursorLeftDragThreshold)
                    return false;



                return true;
            }
        }

        #endregion

        #region HasCursorLeftDragThreshold

        private bool HasCursorLeftDragThreshold
        {
            get
            {
                if (this._indexToSelect < 0)
                    return false;

                var item = this.GetItem(this._indexToSelect);
                Rect bounds = VisualTreeHelper.GetDescendantBounds(item);
                Point ptInItem = _control.TranslatePoint(this._ptMouseDown, item);

                // In case the cursor is at the very top or bottom of the ListViewItem
                // we want to make the vertical threshold very small so that dragging
                // over an adjacent item does not select it.
                double topOffset = Math.Abs(ptInItem.Y);
                double btmOffset = Math.Abs(bounds.Height - ptInItem.Y);
                double vertOffset = Math.Min(topOffset, btmOffset);

                double width = SystemParameters.MinimumHorizontalDragDistance * 2;
                double height = Math.Min(SystemParameters.MinimumVerticalDragDistance, vertOffset) * 2;
                Size szThreshold = new Size(width, height);

                Rect rect = new Rect(this._ptMouseDown, szThreshold);
                rect.Offset(szThreshold.Width / -2, szThreshold.Height / -2);
                Point ptInListView = Helper.GetMousePosition(_control);
                return !rect.Contains(ptInListView);
            }
        }

        #endregion // HasCursorLeftDragThreshold

        #region IsMouseOverScrollbar

        /// <summary>
        /// Returns true if the mouse cursor is over a scrollbar in the ListView.
        /// </summary>
        private bool IsMouseOverScrollbar
        {
            get
            {
                Point ptMouse = Helper.GetMousePosition(_control);
                HitTestResult res = VisualTreeHelper.HitTest(_control, ptMouse);
                if (res == null)
                    return false;

                DependencyObject depObj = res.VisualHit;
                while (depObj != null)
                {
                    if (depObj is ScrollBar)
                        return true;

                    // VisualTreeHelper works with objects of type Visual or Visual3D.
                    // If the current object is not derived from Visual or Visual3D,
                    // then use the LogicalTreeHelper to find the parent element.
                    if (depObj is Visual || depObj is System.Windows.Media.Media3D.Visual3D)
                        depObj = VisualTreeHelper.GetParent(depObj);
                    else
                        depObj = LogicalTreeHelper.GetParent(depObj);
                }

                return false;
            }
        }



        #endregion

        #region IndexUnderDragCursor

        /// <summary>
        /// Returns the index of the ListViewItem underneath the
        /// drag cursor, or -1 if the cursor is not over an item.
        /// </summary>
        private int GetIndexUnderDragCursor()
        {

            int index = -1;
            for (int i = 0; i < _control.Items.Count; ++i)
            {
                var item = this.GetItem(i);
                if (IsMouseOver(item))
                {
                    index = i;
                    break;
                }
            }
            return index;

        }

        #endregion // IndexUnderDragCursor

        #region GetItem

        private FrameworkElement? GetItem(int index)
        {
            if (_control.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return _control.ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
        }

        private FrameworkElement? GetItem(FrameworkElement dataItem)
        {
            if (_control.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return _control.ItemContainerGenerator.ContainerFromItem(dataItem) as FrameworkElement;
        }

        #endregion // GetListViewItem

        #region IsMouseOver

        bool IsMouseOver(Visual? target)
        {
            if (target == null)
                return false;
            // We need to use MouseUtilities to figure out the cursor
            // coordinates because, during a drag-drop operation, the WPF
            // mechanisms for getting the coordinates behave strangely.
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePos = Helper.GetMousePosition(target);
            return bounds.Contains(mousePos);
        }

        #endregion // IsMouseOver

        #region InitializeAdornerLayer

        AdornerLayer InitializeAdornerLayer(FrameworkElement itemToDrag)
        {
            // Create a brush which will paint the ListViewItem onto
            // a visual in the adorner layer.
            VisualBrush brush = new VisualBrush(itemToDrag);

            // Create an element which displays the source item while it is dragged.
            _dragAdorner = new DragAdorner(_control, itemToDrag, brush);

            // Set the drag adorner's opacity.		
            _dragAdorner.Opacity = this.DragAdornerOpacity;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(_control);
            layer.Add(_dragAdorner);

            // Save the location of the cursor when the left mouse button was pressed.
            _ptMouseDown = Helper.GetMousePosition(_control);

            return layer;
        }

        #endregion // InitializeAdornerLayer

        #region InitializeDragOperation

        void InitializeDragOperation(FrameworkElement itemToDrag)
        {
            // Set some flags used during the drag operation.
            _isDragInProgress = true;
            _canInitiateDrag = false;

            // Let the ListViewItem know that it is being dragged.
            SelectorItemDragState.SetIsBeingDragged(itemToDrag, true);
        }

        #endregion // InitializeDragOperation



        #region PerformDragOperation

        void PerformDragOperation()
        {
            try
            {
                var selectedItem = GetItem(_control.SelectedIndex);
                DragDropEffects allowedEffects = DragDropEffects.Move | DragDropEffects.Move | DragDropEffects.Link;
                if (DragDrop.DoDragDrop(_control, selectedItem, allowedEffects) != DragDropEffects.None)
                {
                    // The item was dropped into a new location,
                    // so make it the new selected item.
                    _control.SelectedItem = selectedItem;
                }
            }
            catch (Exception)
            {


            }

        }

        #endregion // PerformDragOperation

        #region FinishDragOperation

        void FinishDragOperation(FrameworkElement draggedItem, AdornerLayer? adornerLayer)
        {
            // Let the ListViewItem know that it is not being dragged anymore.
            SelectorItemDragState.SetIsBeingDragged(draggedItem, false);

            _isDragInProgress = false;

            if (ItemUnderDragCursor != null)
                this.ItemUnderDragCursor = null;

            // Remove the drag adorner from the adorner layer.
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this._dragAdorner);
                this._dragAdorner = null;
            }
        }

        #endregion // FinishDragOperation

        #region ItemUnderDragCursor

        private FrameworkElement? ItemUnderDragCursor
        {
            get { return _itemUnderDragCursor; }
            set
            {
                if (_itemUnderDragCursor == value)
                    return;

                // The first pass handles the previous item under the cursor.
                // The second pass handles the new one.
                for (int i = 0; i < 2; ++i)
                {
                    if (i == 1)
                        _itemUnderDragCursor = value;

                    if (_itemUnderDragCursor != null)
                    {
                        var listViewItem = GetItem(_itemUnderDragCursor);
                        if (listViewItem != null)
                            SelectorItemDragState.SetIsUnderDragCursor(listViewItem, i == 1);
                    }
                }
            }
        }

        #endregion // ItemUnderDragCursor

        #region UpdateDragAdornerLocation

        void UpdateDragAdornerLocation()
        {
            if (this._dragAdorner != null)
            {
                Point ptCursor = Helper.GetMousePosition(_control);
                // 4/13/2007 - Made the top offset relative to the item being dragged.
                var itemBeingDragged = this.GetItem(_indexToSelect);
                if (itemBeingDragged != null)
                {

                    Point itemLoc = itemBeingDragged.TranslatePoint(new Point(0, 0), _control);
                    double top = 0;
                    double left;
                    //添加锁定
                    switch (LockDragOrientation)
                    {
                        case Orientation.Horizontal:
                            left = ptCursor.X + itemLoc.X - this._ptMouseDown.X;
                            break;
                        case Orientation.Vertical:
                            left = _control.BorderThickness.Left + _control.Padding.Left;
                            top = itemLoc.Y + ptCursor.Y - this._ptMouseDown.Y;
                            break;
                        default:
                            left = ptCursor.X + itemLoc.X - this._ptMouseDown.X;
                            top = itemLoc.Y + ptCursor.Y - this._ptMouseDown.Y;
                            break;
                    }
                    _dragAdorner.SetOffsets(left, top);
                }
            }
        }

        #endregion // UpdateDragAdornerLocation
        #endregion

        #region IDisposable
        /// <summary>
        /// 析构
        /// </summary>
        ~SelectorDragDropService()
        {
            Dispose(false);
        }
        bool _disposed; //是否回收完毕
        /// <summary>
        /// IDisposable接口
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing">是否需要释放那些实现IDisposable接口的托管对象</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return; //如果已经被回收，就中断执行
            if (disposing)
            {
                _control.AllowDrop = false;
                _control.PreviewMouseLeftButtonDown -= _control_PreviewMouseLeftButtonDown;
                _control.PreviewMouseMove -= _control_PreviewMouseMove;
                _control.DragOver -= _control_DragOver;
                _control.DragLeave -= _control_DragLeave;
                _control.DragEnter -= _control_DragEnter;
                _control.Drop -= _control_Drop;
                //TODO:释放那些实现IDisposable接口的托管对象
            }
            //TODO:释放非托管资源，设置对象为null
            _disposed = true;
        }
        #endregion


    }


    /// <summary>
    /// Renders a visual which can follow the mouse cursor, 
    /// such as during a drag-and-drop operation.
    /// </summary>
    class DragAdorner : Adorner
    {
        #region Data

        private Rectangle? _child = null;
        private double _offsetLeft = 0;
        private double _offsetTop = 0;

        #endregion // Data

        #region Constructor

        /// <summary>
        /// Initializes a new instance of DragVisualAdorner.
        /// </summary>
        /// <param name="adornedElement">The element being adorned.</param>
        /// <param name="size">The size of the adorner.</param>
        /// <param name="brush">A brush to with which to paint the adorner.</param>
        public DragAdorner(FrameworkElement adornedElement, FrameworkElement item, Brush brush)
            : base(adornedElement)
        {
            AdornedItem = item;
            Rectangle rect = new Rectangle();
            rect.Fill = brush;
            rect.Width = item.RenderSize.Width;
            rect.Height = item.RenderSize.Height;
            rect.IsHitTestVisible = false;
            this._child = rect;
        }

        #endregion // Constructor

        #region Public Interface

        public FrameworkElement AdornedItem { get; }


        #region GetDesiredTransform

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            GeneralTransformGroup result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(this._offsetLeft, this._offsetTop));
            return result;
        }

        #endregion // GetDesiredTransform

        #region OffsetLeft

        /// <summary>
        /// Gets/sets the horizontal offset of the adorner.
        /// </summary>
        public double OffsetLeft
        {
            get { return this._offsetLeft; }
            set
            {
                this._offsetLeft = value;
                UpdateLocation();
            }
        }

        #endregion // OffsetLeft

        #region SetOffsets

        /// <summary>
        /// Updates the location of the adorner in one atomic operation.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public void SetOffsets(double left, double top)
        {
            this._offsetLeft = left;
            this._offsetTop = top;
            this.UpdateLocation();
        }

        #endregion // SetOffsets

        #region OffsetTop

        /// <summary>
        /// Gets/sets the vertical offset of the adorner.
        /// </summary>
        public double OffsetTop
        {
            get { return this._offsetTop; }
            set
            {
                this._offsetTop = value;
                UpdateLocation();
            }
        }

        #endregion // OffsetTop

        #endregion // Public Interface

        #region Protected Overrides

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            this._child?.Measure(constraint);
            return this._child == null ? Size.Empty : this._child.DesiredSize;
        }

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            this._child?.Arrange(new Rect(finalSize));
            return finalSize;
        }

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual? GetVisualChild(int index)
        {
            return this._child;
        }

        /// <summary>
        /// Override.  Always returns 1.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        #endregion // Protected Overrides

        #region Private Helpers

        private void UpdateLocation()
        {
            if (this.Parent is AdornerLayer adornerLayer)
                adornerLayer.Update(this.AdornedElement);
        }

        #endregion // Private Helpers
    }

    #region ListViewItemDragState

    /// <summary>
    /// Exposes attached properties used in conjunction with the ListViewDragDropManager class.
    /// Those properties can be used to allow triggers to modify the appearance of ListViewItems
    /// in a ListView during a drag-drop operation.
    /// </summary>
    static class SelectorItemDragState
    {
        #region IsBeingDragged

        /// <summary>
        /// Identifies the ListViewItemDragState's IsBeingDragged attached property.  
        /// This field is read-only.
        /// </summary>
        public static readonly DependencyProperty IsBeingDraggedProperty =
            DependencyProperty.RegisterAttached(
                "IsBeingDragged",
                typeof(bool),
                typeof(SelectorItemDragState),
                new UIPropertyMetadata(false));

        /// <summary>
        /// Returns true if the specified ListViewItem is being dragged, else false.
        /// </summary>
        /// <param name="item">The ListViewItem to check.</param>
        public static bool GetIsBeingDragged(FrameworkElement item)
        {
            return (bool)item.GetValue(IsBeingDraggedProperty);
        }

        /// <summary>
        /// Sets the IsBeingDragged attached property for the specified ListViewItem.
        /// </summary>
        /// <param name="item">The ListViewItem to set the property on.</param>
        /// <param name="value">Pass true if the element is being dragged, else false.</param>
        internal static void SetIsBeingDragged(FrameworkElement item, bool value)
        {
            item.SetValue(IsBeingDraggedProperty, value);
        }

        #endregion // IsBeingDragged

        #region IsUnderDragCursor

        /// <summary>
        /// Identifies the ListViewItemDragState's IsUnderDragCursor attached property.  
        /// This field is read-only.
        /// </summary>
        public static readonly DependencyProperty IsUnderDragCursorProperty =
            DependencyProperty.RegisterAttached(
                "IsUnderDragCursor",
                typeof(bool),
                typeof(SelectorItemDragState),
                new UIPropertyMetadata(false));

        /// <summary>
        /// Returns true if the specified ListViewItem is currently underneath the cursor 
        /// during a drag-drop operation, else false.
        /// </summary>
        /// <param name="item">The ListViewItem to check.</param>
        public static bool GetIsUnderDragCursor(FrameworkElement item)
        {
            return (bool)item.GetValue(IsUnderDragCursorProperty);
        }

        /// <summary>
        /// Sets the IsUnderDragCursor attached property for the specified ListViewItem.
        /// </summary>
        /// <param name="item">The ListViewItem to set the property on.</param>
        /// <param name="value">Pass true if the element is underneath the drag cursor, else false.</param>
        internal static void SetIsUnderDragCursor(FrameworkElement item, bool value)
        {
            item.SetValue(IsUnderDragCursorProperty, value);
        }

        #endregion // IsUnderDragCursor
    }

    #endregion // ListViewItemDragState


}
