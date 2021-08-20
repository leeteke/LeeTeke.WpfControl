using LeeTeke.WpfControl.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{

    public class DataGridManager
    {

        #region HeaderFreeze
        public static bool GetHeaderFreeze(DependencyObject obj)
        {
            return (bool)obj.GetValue(HeaderFreezeProperty);
        }

        public static void SetHeaderFreeze(DependencyObject obj, bool value)
        {
            obj.SetValue(HeaderFreezeProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderFreeze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFreezeProperty =
            DependencyProperty.RegisterAttached("HeaderFreeze", typeof(bool), typeof(DataGridManager));
        #endregion

        #region HeaderBackground
        public static Brush GetHeaderBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderBackgroundProperty);
        }

        public static void SetHeaderBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.RegisterAttached("HeaderBackground", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region HeaderBorderThickness
        public static Thickness GetHeaderBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HeaderBorderThicknessProperty);
        }

        public static void SetHeaderBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HeaderBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBorderThicknessProperty =
            DependencyProperty.RegisterAttached("HeaderBorderThickness", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region HeaderBorderBursh
        public static Brush GetHeaderBorderBursh(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderBorderBurshProperty);
        }

        public static void SetHeaderBorderBursh(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderBorderBurshProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderBorderBursh.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBorderBurshProperty =
            DependencyProperty.RegisterAttached("HeaderBorderBursh", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region HeaderMargin
        public static Thickness GetHeaderMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HeaderMarginProperty);
        }

        public static void SetHeaderMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HeaderMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.RegisterAttached("HeaderMargin", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region HeaderPadding
        public static Thickness GetHeaderPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HeaderPaddingProperty);
        }

        public static void SetHeaderPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HeaderPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderPaddingProperty =
            DependencyProperty.RegisterAttached("HeaderPadding", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region HeaderCornerRadius
        public static CornerRadius GetHeaderCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(HeaderCornerRadiusProperty);
        }

        public static void SetHeaderCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(HeaderCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderCornerRadiusProperty =
            DependencyProperty.RegisterAttached("HeaderCornerRadius", typeof(CornerRadius), typeof(DataGridManager));
        #endregion

        #region HeaderIsClip
        public static bool GetHeaderIsClip(DependencyObject obj)
        {
            return (bool)obj.GetValue(HeaderIsClipProperty);
        }

        public static void SetHeaderIsClip(DependencyObject obj, bool value)
        {
            obj.SetValue(HeaderIsClipProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderIsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderIsClipProperty =
            DependencyProperty.RegisterAttached("HeaderIsClip", typeof(bool), typeof(DataGridManager));
        #endregion


        #region NoItemsContent
        public static object GetNoItemsContent(DependencyObject obj)
        {
            return (object)obj.GetValue(NoItemsContentProperty);
        }

        public static void SetNoItemsContent(DependencyObject obj, object value)
        {
            obj.SetValue(NoItemsContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for NoItemsContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoItemsContentProperty =
            DependencyProperty.RegisterAttached("NoItemsContent", typeof(object), typeof(DataGridManager));

        #endregion

        #region ListEndContent
        public static object GetListEndContent(DependencyObject obj)
        {
            return (object)obj.GetValue(ListEndContentProperty);
        }

        public static void SetListEndContent(DependencyObject obj, object value)
        {
            obj.SetValue(ListEndContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for ListEndContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListEndContentProperty =
            DependencyProperty.RegisterAttached("ListEndContent", typeof(object), typeof(DataGridManager));
        #endregion


        #region ShowRowNumber
        public static bool GetShowRowNumber(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowRowNumberProperty);
        }

        public static void SetShowRowNumber(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowRowNumberProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowRowNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowRowNumberProperty =
            DependencyProperty.RegisterAttached("ShowRowNumber", typeof(bool), typeof(DataGridManager), new PropertyMetadata(ShowRowNumberChanged));

        private static void ShowRowNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                if (e.NewValue is bool show && show)
                {
                    dataGrid.LoadingRow += DataGrid_LoadingRow;

                }
                else
                {
                    dataGrid.LoadingRow -= DataGrid_LoadingRow;

                }
            }
        }

        private static void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        #endregion


        #region RowHeaderVerticalContentAlignment
        public static VerticalAlignment GetRowHeaderVerticalContentAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(RowHeaderVerticalContentAlignmentProperty);
        }

        public static void SetRowHeaderVerticalContentAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(RowHeaderVerticalContentAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowHeaderVerticalContentAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowHeaderVerticalContentAlignmentProperty =
            DependencyProperty.RegisterAttached("RowHeaderVerticalContentAlignment", typeof(VerticalAlignment), typeof(DataGridManager));
        #endregion

        #region RowHeaderHorizontalContentAlignment
        public static HorizontalAlignment GetRowHeaderHorizontalContentAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(RowHeaderHorizontalContentAlignmentProperty);
        }

        public static void SetRowHeaderHorizontalContentAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(RowHeaderHorizontalContentAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowHeaderHorizontalContentAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowHeaderHorizontalContentAlignmentProperty =
            DependencyProperty.RegisterAttached("RowHeaderHorizontalContentAlignment", typeof(HorizontalAlignment), typeof(DataGridManager));
        #endregion


        #region CellPadding
        public static Thickness GetCellPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(CellPaddingProperty);
        }

        public static void SetCellPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(CellPaddingProperty, value);
        }

        public static readonly DependencyProperty CellPaddingProperty =
            DependencyProperty.RegisterAttached("CellPadding", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region HeaderMinHeight
        public static double GetHeaderMinHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(HeaderMinHeightProperty);
        }

        public static void SetHeaderMinHeight(DependencyObject obj, double value)
        {
            obj.SetValue(HeaderMinHeightProperty, value);
        }

        public static readonly DependencyProperty HeaderMinHeightProperty =
            DependencyProperty.RegisterAttached("HeaderMinHeight", typeof(double), typeof(DataGridManager));
        #endregion

        #region RowMinHeight
        public static double GetRowMinHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(RowMinHeightProperty);
        }

        public static void SetRowMinHeight(DependencyObject obj, double value)
        {
            obj.SetValue(RowMinHeightProperty, value);
        }

        public static readonly DependencyProperty RowMinHeightProperty =
            DependencyProperty.RegisterAttached("RowMinHeight", typeof(double), typeof(DataGridManager));


        #endregion

        #region HeaderForeground
        public static Brush GetHeaderForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderForegroundProperty);
        }

        public static void SetHeaderForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderForegroundProperty, value);
        }

        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.RegisterAttached("HeaderForeground", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region HeaderFontSize
        public static double GetHeaderFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(HeaderFontSizeProperty);
        }

        public static void SetHeaderFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(HeaderFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(DataGridManager));
        #endregion

        #region HeaderFontWeight
        public static FontWeight GetHeaderFontWeight(DependencyObject obj)
        {
            return (FontWeight)obj.GetValue(HeaderFontWeightProperty);
        }

        public static void SetHeaderFontWeight(DependencyObject obj, FontWeight value)
        {
            obj.SetValue(HeaderFontWeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFontWeightProperty =
            DependencyProperty.RegisterAttached("HeaderFontWeight", typeof(FontWeight), typeof(DataGridManager));
        #endregion



        #region ColumnVerticalContentAlignment
        public static VerticalAlignment GetColumnVerticalContentAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(ColumnVerticalContentAlignmentProperty);
        }

        public static void SetColumnVerticalContentAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(ColumnVerticalContentAlignmentProperty, value);
        }

        public static readonly DependencyProperty ColumnVerticalContentAlignmentProperty =
            DependencyProperty.RegisterAttached("ColumnVerticalContentAlignment", typeof(VerticalAlignment), typeof(DataGridManager));
        #endregion

        #region ColumnHorizontalContentAlignment
        public static HorizontalAlignment GetColumnHorizontalContentAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(ColumnHorizontalContentAlignmentProperty);
        }

        public static void SetColumnHorizontalContentAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(ColumnHorizontalContentAlignmentProperty, value);
        }

        public static readonly DependencyProperty ColumnHorizontalContentAlignmentProperty =
            DependencyProperty.RegisterAttached("ColumnHorizontalContentAlignment", typeof(HorizontalAlignment), typeof(DataGridManager));
        #endregion



        #region MouseOverBackground
        public static Brush GetMouseOverBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBackgroundProperty);
        }

        public static void SetMouseOverBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region MouseOrverBorderBrush
        public static Brush GetMouseOrverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOrverBorderBrushProperty);
        }

        public static void SetMouseOrverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOrverBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOrverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOrverBorderBrushProperty =
            DependencyProperty.RegisterAttached("MouseOrverBorderBrush", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region MouseOrverForeground
        public static Brush GetMouseOrverForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOrverForegroundProperty);
        }

        public static void SetMouseOrverForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOrverForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOrverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOrverForegroundProperty =
            DependencyProperty.RegisterAttached("MouseOrverForeground", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region SelectedBackground
        public static Brush GetSelectedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedBackgroundProperty);
        }

        public static void SetSelectedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedBackgroundProperty, value);
        }

        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.RegisterAttached("SelectedBackground", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region SelectedBorderBrush
        public static Brush GetSelectedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedBorderBrushProperty);
        }

        public static void SetSelectedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBorderBrushProperty =
            DependencyProperty.RegisterAttached("SelectedBorderBrush", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region SelectedForeground
        public static Brush GetSelectedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedForegroundProperty);
        }

        public static void SetSelectedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedForegroundProperty, value);
        }

        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.RegisterAttached("SelectedForeground", typeof(Brush), typeof(DataGridManager));


        #endregion

        #region AutoGenerateCheckBoxStyle
        public static Style GetAutoGenerateCheckBoxStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(AutoGenerateCheckBoxStyleProperty);
        }

        public static void SetAutoGenerateCheckBoxStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(AutoGenerateCheckBoxStyleProperty, value);
        }

        public static readonly DependencyProperty AutoGenerateCheckBoxStyleProperty =
            DependencyProperty.RegisterAttached("AutoGenerateCheckBoxStyle", typeof(Style), typeof(DataGridManager));


        #endregion

        #region  AutoGenerateComboxStyle
        public static Style GetAutoGenerateComboxStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue( AutoGenerateComboxStyleProperty);
        }

        public static void SetAutoGenerateComboxStyle(DependencyObject obj, Style value)
        {
            obj.SetValue( AutoGenerateComboxStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for  AutoGenerateComboxStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoGenerateComboxStyleProperty =
            DependencyProperty.RegisterAttached("AutoGenerateComboxStyle", typeof(Style), typeof(DataGridManager));
        #endregion

        #region AutoGenerateTextBoxStyle
        public static Style GetAutoGenerateTextBoxStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(AutoGenerateTextBoxStyleProperty);
        }

        public static void SetAutoGenerateTextBoxStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(AutoGenerateTextBoxStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoGenerateTextBoxStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoGenerateTextBoxStyleProperty =
            DependencyProperty.RegisterAttached("AutoGenerateTextBoxStyle", typeof(Style), typeof(DataGridManager));
        #endregion

        #region AutoGenerateImageStyle
        public static Style GetAutoGenerateImageStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(AutoGenerateImageStyleProperty);
        }

        public static void SetAutoGenerateImageStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(AutoGenerateImageStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoGenerateImageStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoGenerateImageStyleProperty =
            DependencyProperty.RegisterAttached("AutoGenerateImageStyle", typeof(Style), typeof(DataGridManager));
        #endregion


        #region SelectedItems
        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(DataGridManager));
        #endregion

        #region (internal)CheckBoxHook
        internal static object GetCheckBoxHook(DependencyObject obj)
        {
            return (object)obj.GetValue(CheckBoxHookProperty);
        }

        internal static void SetCheckBoxHook(DependencyObject obj, object value)
        {
            obj.SetValue(CheckBoxHookProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckBoxHook.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty CheckBoxHookProperty =
            DependencyProperty.RegisterAttached("CheckBoxHook", typeof(object), typeof(DataGridManager), new PropertyMetadata(CheckBoxHookChanged));

        private static void CheckBoxHookChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CheckBox check)
            {
                if (e.NewValue is DataGrid data)
                {
                    check.Checked += Check_Checked;
                    check.Unchecked += Check_Unchecked;
                    data.SelectedCellsChanged += (es, ew) =>
                    {
                        check.IsChecked = data.SelectedItems.Count == data.Items.Count;
                    };
                }
                else
                {
                    check.Checked -= Check_Checked;
                }
            }
        }

        private static void Check_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox check && GetCheckBoxHook(check) is DataGrid datagrid)
            {
                datagrid.SelectedIndex = -1;
            }
        }

        private static void Check_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox check && GetCheckBoxHook(check) is DataGrid datagrid)
            {
                datagrid.SelectAll();
            }
        }



        #endregion


        #region (Internal) DataGridHook
        internal static bool GetDataGridHook(DependencyObject obj)
        {
            return (bool)obj.GetValue(DataGridHookProperty);
        }

        internal static void SetDataGridHook(DependencyObject obj, bool value)
        {
            obj.SetValue(DataGridHookProperty, value);
        }

        internal static readonly DependencyProperty DataGridHookProperty =
            DependencyProperty.RegisterAttached("DataGridHook", typeof(bool), typeof(DataGridManager), new PropertyMetadata(OnDataGridHookChanged));

        private static void OnDataGridHookChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            dataGrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;
            SetSelectedItems(dataGrid, dataGrid.SelectedItems);
        }

        private static void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var dataGrid = sender as DataGrid;

            #region Get User Custom DataGridColumnAttribute

            var visibility = e.Column.Visibility;
            var width = e.Column.Width;
            var header = e.Column.Header;
            var readOnly = e.Column.IsReadOnly;
            var bindingMode = BindingMode.TwoWay;
            var updateSourceTrigger = UpdateSourceTrigger.PropertyChanged;


            #endregion

            if (e.PropertyType.IsEnum)
            {
                var newColumn = new DataGridComboBoxColumn()
                {
                    Width = width,
                    Header = header,
                    IsReadOnly = readOnly,
                    Visibility = visibility,
                };

                newColumn.ItemsSource = Enum.GetValues(e.PropertyType).Cast<Enum>();
                newColumn.SelectedItemBinding = new Binding(e.PropertyName) { Mode = bindingMode, UpdateSourceTrigger = updateSourceTrigger };
                newColumn.EditingElementStyle = GetAutoGenerateComboxStyle(dataGrid);
             
                e.Column = newColumn;
            }
            else if (e.PropertyType == typeof(bool))
            {
                var newColumn = new DataGridCheckBoxColumn()
                {
                    Width = width,
                    Header = header,
                    IsReadOnly = readOnly,
                    Visibility = visibility,
                };
                newColumn.Binding = new Binding(e.PropertyName) { Mode = bindingMode, UpdateSourceTrigger = updateSourceTrigger };
                newColumn.ElementStyle = GetAutoGenerateCheckBoxStyle(dataGrid);
                newColumn.EditingElementStyle = GetAutoGenerateCheckBoxStyle(dataGrid);
                if (dataGrid.IsReadOnly)
                {
                    newColumn.ElementStyle.Setters.Add(new Setter(CheckBox.IsEnabledProperty, false));
                }

                e.Column = newColumn;
            }
            else if (e.PropertyType == typeof(ImageSource))
            {
                var newColumn = new DataGridTemplateColumn()
                {
                    Width = width,
                    Header = header,
                    IsReadOnly = readOnly,
                    Visibility = visibility,
                };


                var dt= new DataTemplate();
                var fef = new FrameworkElementFactory(typeof(Image));
                fef.SetBinding(Image.SourceProperty, new Binding(e.PropertyName) { Mode = bindingMode, UpdateSourceTrigger = updateSourceTrigger });
                fef.SetValue(Image.StyleProperty, GetAutoGenerateImageStyle(dataGrid));
                dt.VisualTree = fef;
                newColumn.CellTemplate = dt;
                newColumn.CellEditingTemplate = dt;
                e.Column = newColumn;
            }
            else
            {
                var newColumn = new DataGridTextColumn()
                {
                    Width = width,
                    Header = header,
                    IsReadOnly = readOnly,
                    Visibility = visibility,
                };

                newColumn.Binding = new Binding(e.PropertyName) { Mode = bindingMode, UpdateSourceTrigger = updateSourceTrigger };

                newColumn.ElementStyle = GetAutoGenerateTextBoxStyle(dataGrid);

                newColumn.EditingElementStyle = GetAutoGenerateTextBoxStyle(dataGrid);
                if (dataGrid.IsReadOnly)
                {
                    newColumn.ElementStyle.Setters.Add(new Setter(TextBox.IsReadOnlyProperty, true));
                    newColumn.ElementStyle.Setters.Add(new Setter(TextBox.BorderThicknessProperty, 0));
                }

                e.Column = newColumn;
            }
        }

        #endregion

    }
}
