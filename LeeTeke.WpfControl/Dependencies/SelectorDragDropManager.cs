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
        #region Open
        public static bool GetOpen(DependencyObject obj)
        {
            return (bool)obj.GetValue(OpenProperty);
        }

        public static void SetOpen(DependencyObject obj, bool value)
        {
            obj.SetValue(OpenProperty, value);
        }

        // Using a DependencyProperty as the backing store for Open.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenProperty =
            DependencyProperty.RegisterAttached("Open", typeof(bool), typeof(SelectorDragDropManager), new PropertyMetadata(OnOpenChanged));

        private static void OnOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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
                    SetSelectorDragDropService(d, newDDS);
                    newDDS.Init();
                }

            }
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
            get { return this.dragAdornerOpacity; }
            set
            {
                if (this.IsDragInProgress)
                    throw new InvalidOperationException("Cannot set the DragAdornerOpacity property during a drag operation.");

                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException("DragAdornerOpacity", value, "Must be between 0 and 1.");

                this.dragAdornerOpacity = value;
            }
        }

        #endregion // DragAdornerOpacity

        #region IsDragInProgress

        /// <summary>
        /// Returns true if there is currently a drag operation being managed.
        /// </summary>
        public bool IsDragInProgress
        {
            get { return this.isDragInProgress; }
            private set { this.isDragInProgress = value; }
        }

        #endregion // IsDragInProgress

        #region ShowDragAdorner

        /// <summary>
        /// Gets/sets whether a visual representation of the ListViewItem being dragged
        /// follows the mouse cursor during a drag operation.  The default value is true.
        /// </summary>
        public bool ShowDragAdorner
        {
            get { return this.showDragAdorner; }
            set
            {
                if (this.IsDragInProgress)
                    throw new InvalidOperationException("Cannot set the ShowDragAdorner property during a drag operation.");

                this.showDragAdorner = value;
            }
        }

        #endregion // ShowDragAdorner


        private Selector _control;
        private bool canInitiateDrag;
        private DragAdorner dragAdorner;
        private double dragAdornerOpacity;
        private int indexToSelect;
        private bool isDragInProgress;
        private FrameworkElement itemUnderDragCursor;
        private Point ptMouseDown;
        private bool showDragAdorner;


        public SelectorDragDropService(Selector control)
        {
            _control = control;
            this.canInitiateDrag = false;
            this.dragAdornerOpacity = 0.7;
            this.indexToSelect = -1;
            this.showDragAdorner = true;
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
            if (dragAdorner == null)
            {
                return;
            }

            if (e.Source is FrameworkElement contentItem && StaticMethods.IsInControl(_control, contentItem))
            {

                var oldIndex = _control.Items.IndexOf(contentItem);
                _control.Items.Remove(dragAdorner.AdornedItem);
                _control.Items.Insert(oldIndex, dragAdorner.AdornedItem);
                e.Effects = DragDropEffects.Move;
            }
            else//
            {

                var sourceType = _control.ItemsSource.GetType();
                ///泛型
                if (sourceType.IsGenericType && sourceType.GenericTypeArguments.Length == 1 && _control.ItemsSource is IList list)
                {
                    if (list.Contains(dragAdorner.AdornedItem.DataContext))
                    {
                        var pd = dragAdorner.AdornedItem.DataContext;
                        var toalIndex = IndexUnderDragCursor;
                        if (toalIndex > -1)
                        {
                            list.Remove(dragAdorner.AdornedItem.DataContext);
                            list.Insert(toalIndex, pd);
                        }
                        e.Effects = DragDropEffects.Move;
                    }

                }

            }
        }

        private void _control_DragEnter(object sender, DragEventArgs e)
        {
            if (this.dragAdorner != null && this.dragAdorner.Visibility != Visibility.Visible)
            {
                // Update the location of the adorner and then show it.				
                this.UpdateDragAdornerLocation();
                this.dragAdorner.Visibility = Visibility.Visible;
            }
        }

        private void _control_DragLeave(object sender, DragEventArgs e)
        {
            if (!this.IsMouseOver(_control))
            {

                if (this.dragAdorner != null)
                    this.dragAdorner.Visibility = Visibility.Collapsed;
            }
        }

        private void _control_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;

            if (this.ShowDragAdornerResolved)
                this.UpdateDragAdornerLocation();


        }

        private void _control_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!CanStartDragOperation)
                return;

            // Select the item the user clicked on.
            if (_control.SelectedIndex != this.indexToSelect)
                _control.SelectedIndex = this.indexToSelect;

            // If the item at the selected index is null, there's nothing
            // we can do, so just return;
            if (_control.SelectedItem == null)
                return;

            var itemToDrag = this.GetItem(_control.SelectedIndex);
            if (itemToDrag == null)
                return;

            AdornerLayer adornerLayer = this.ShowDragAdornerResolved ? this.InitializeAdornerLayer(itemToDrag) : null;

            this.InitializeDragOperation(itemToDrag);
            this.PerformDragOperation();
            this.FinishDragOperation(itemToDrag, adornerLayer);
        }

        private void _control_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.IsMouseOverScrollbar)
            {
                // 4/13/2007 - Set the flag to false when cursor is over scrollbar.
                canInitiateDrag = false;
                return;
            }

            int index = this.IndexUnderDragCursor;
            this.canInitiateDrag = index > -1;

            if (this.canInitiateDrag)
            {
                // Remember the location and index of the ListViewItem the user clicked on for later.
                this.ptMouseDown = StaticMethods.GetMousePosition(_control);
                this.indexToSelect = index;
            }
            else
            {
                this.ptMouseDown = new Point(-10000, -10000);
                this.indexToSelect = -1;
            }
        }
        #endregion

        #region Private

        #region CanStartDragOperation

        bool CanStartDragOperation
        {
            get
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed)
                    return false;

                if (!this.canInitiateDrag)
                    return false;

                if (this.indexToSelect == -1)
                    return false;

                if (!this.HasCursorLeftDragThreshold)
                    return false;

              

                return true;
            }
        }

        #endregion 

        #region HasCursorLeftDragThreshold

        bool HasCursorLeftDragThreshold
        {
            get
            {
                if (this.indexToSelect < 0)
                    return false;

                var item = this.GetItem(this.indexToSelect);
                Rect bounds = VisualTreeHelper.GetDescendantBounds(item);
                Point ptInItem = _control.TranslatePoint(this.ptMouseDown, item);

                // In case the cursor is at the very top or bottom of the ListViewItem
                // we want to make the vertical threshold very small so that dragging
                // over an adjacent item does not select it.
                double topOffset = Math.Abs(ptInItem.Y);
                double btmOffset = Math.Abs(bounds.Height - ptInItem.Y);
                double vertOffset = Math.Min(topOffset, btmOffset);

                double width = SystemParameters.MinimumHorizontalDragDistance * 2;
                double height = Math.Min(SystemParameters.MinimumVerticalDragDistance, vertOffset) * 2;
                Size szThreshold = new Size(width, height);

                Rect rect = new Rect(this.ptMouseDown, szThreshold);
                rect.Offset(szThreshold.Width / -2, szThreshold.Height / -2);
                Point ptInListView = StaticMethods.GetMousePosition(_control);
                return !rect.Contains(ptInListView);
            }
        }

        #endregion // HasCursorLeftDragThreshold

        #region IsMouseOverScrollbar

        /// <summary>
        /// Returns true if the mouse cursor is over a scrollbar in the ListView.
        /// </summary>
        bool IsMouseOverScrollbar
        {
            get
            {
                Point ptMouse = StaticMethods.GetMousePosition(_control);
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
        int IndexUnderDragCursor
        {
            get
            {
                int index = -1;
                for (int i = 0; i < _control.Items.Count; ++i)
                {
                    FrameworkElement item = this.GetItem(i);
                    if (this.IsMouseOver(item))
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }
        }

        #endregion // IndexUnderDragCursor

        #region GetItem

        private FrameworkElement GetItem(int index)
        {
            if (_control.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return _control.ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
        }

        private FrameworkElement GetItem(FrameworkElement dataItem)
        {
            if (_control.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return _control.ItemContainerGenerator.ContainerFromItem(dataItem) as FrameworkElement;
        }

        #endregion // GetListViewItem

        #region IsMouseOver

        bool IsMouseOver(Visual target)
        {
            // We need to use MouseUtilities to figure out the cursor
            // coordinates because, during a drag-drop operation, the WPF
            // mechanisms for getting the coordinates behave strangely.
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePos = StaticMethods.GetMousePosition(target);
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
            dragAdorner = new DragAdorner(_control, itemToDrag, brush);

            // Set the drag adorner's opacity.		
            dragAdorner.Opacity = this.DragAdornerOpacity;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(_control);
            layer.Add(dragAdorner);

            // Save the location of the cursor when the left mouse button was pressed.
            ptMouseDown = StaticMethods.GetMousePosition(_control);

            return layer;
        }

        #endregion // InitializeAdornerLayer

        #region InitializeDragOperation

        void InitializeDragOperation(FrameworkElement itemToDrag)
        {
            // Set some flags used during the drag operation.
            this.IsDragInProgress = true;
            this.canInitiateDrag = false;

            // Let the ListViewItem know that it is being dragged.
            SelectorItemDragState.SetIsBeingDragged(itemToDrag, true);
        }

        #endregion // InitializeDragOperation

        #region ShowDragAdornerResolved

        bool ShowDragAdornerResolved
        {
            get { return ShowDragAdorner && DragAdornerOpacity > 0.0; }
        }

        #endregion // ShowDragAdornerResolved

        #region PerformDragOperation

        void PerformDragOperation()
        {
            try
            {
                var selectedItem = this._control.SelectedItem;
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

        void FinishDragOperation(FrameworkElement draggedItem, AdornerLayer adornerLayer)
        {
            // Let the ListViewItem know that it is not being dragged anymore.
            SelectorItemDragState.SetIsBeingDragged(draggedItem, false);

            this.IsDragInProgress = false;

            if (this.ItemUnderDragCursor != null)
                this.ItemUnderDragCursor = null;

            // Remove the drag adorner from the adorner layer.
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this.dragAdorner);
                this.dragAdorner = null;
            }
        }

        #endregion // FinishDragOperation

        #region ItemUnderDragCursor

        FrameworkElement ItemUnderDragCursor
        {
            get { return itemUnderDragCursor; }
            set
            {
                if (this.itemUnderDragCursor == value)
                    return;

                // The first pass handles the previous item under the cursor.
                // The second pass handles the new one.
                for (int i = 0; i < 2; ++i)
                {
                    if (i == 1)
                        this.itemUnderDragCursor = value;

                    if (this.itemUnderDragCursor != null)
                    {
                        var listViewItem = this.GetItem(itemUnderDragCursor);
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
            if (this.dragAdorner != null)
            {
                Point ptCursor = StaticMethods.GetMousePosition(_control);



                // 4/13/2007 - Made the top offset relative to the item being dragged.
                var itemBeingDragged = this.GetItem(indexToSelect);
                Point itemLoc = itemBeingDragged.TranslatePoint(new Point(0, 0), _control);
                double top = itemLoc.Y + ptCursor.Y - this.ptMouseDown.Y;
                double left = ptCursor.X + itemLoc.X - this.ptMouseDown.X;
                this.dragAdorner.SetOffsets(left, top);
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

        private Rectangle child = null;
        private double offsetLeft = 0;
        private double offsetTop = 0;

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
            this.child = rect;
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
            result.Children.Add(new TranslateTransform(this.offsetLeft, this.offsetTop));
            return result;
        }

        #endregion // GetDesiredTransform

        #region OffsetLeft

        /// <summary>
        /// Gets/sets the horizontal offset of the adorner.
        /// </summary>
        public double OffsetLeft
        {
            get { return this.offsetLeft; }
            set
            {
                this.offsetLeft = value;
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
            this.offsetLeft = left;
            this.offsetTop = top;
            this.UpdateLocation();
        }

        #endregion // SetOffsets

        #region OffsetTop

        /// <summary>
        /// Gets/sets the vertical offset of the adorner.
        /// </summary>
        public double OffsetTop
        {
            get { return this.offsetTop; }
            set
            {
                this.offsetTop = value;
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
            this.child.Measure(constraint);
            return this.child.DesiredSize;
        }

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            this.child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return this.child;
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
            AdornerLayer adornerLayer = this.Parent as AdornerLayer;
            if (adornerLayer != null)
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
    public static class SelectorItemDragState
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
