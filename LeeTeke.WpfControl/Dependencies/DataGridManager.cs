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
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{

    public class DataGridManager
    {


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

        #region HeaderEffect
        public static Effect GetHeaderEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(HeaderEffectProperty);
        }

        public static void SetHeaderEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(HeaderEffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderEffectProperty =
            DependencyProperty.RegisterAttached("HeaderEffect", typeof(Effect), typeof(DataGridManager));
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
            DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(DataGridManager),new PropertyMetadata(DependencyConst.FontSize));
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

        #region MainBackground
        public static Brush GetMainBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MainBackgroundProperty);
        }

        public static void SetMainBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MainBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainBackgroundProperty =
            DependencyProperty.RegisterAttached("MainBackground", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region MainBorderThickness
        public static Thickness GetMainBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MainBorderThicknessProperty);
        }

        public static void SetMainBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MainBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainBorderThicknessProperty =
            DependencyProperty.RegisterAttached("MainBorderThickness", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region MainBorderBursh
        public static Brush GetMainBorderBursh(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MainBorderBurshProperty);
        }

        public static void SetMainBorderBursh(DependencyObject obj, Brush value)
        {
            obj.SetValue(MainBorderBurshProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainBorderBursh.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainBorderBurshProperty =
            DependencyProperty.RegisterAttached("MainBorderBursh", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region MainMargin
        public static Thickness GetMainMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MainMarginProperty);
        }

        public static void SetMainMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MainMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainMarginProperty =
            DependencyProperty.RegisterAttached("MainMargin", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region MainPadding
        public static Thickness GetMainPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MainPaddingProperty);
        }

        public static void SetMainPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MainPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainPaddingProperty =
            DependencyProperty.RegisterAttached("MainPadding", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region MainCornerRadius
        public static CornerRadius GetMainCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(MainCornerRadiusProperty);
        }

        public static void SetMainCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(MainCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainCornerRadiusProperty =
            DependencyProperty.RegisterAttached("MainCornerRadius", typeof(CornerRadius), typeof(DataGridManager));
        #endregion

        #region MainIsClip
        public static bool GetMainIsClip(DependencyObject obj)
        {
            return (bool)obj.GetValue(MainIsClipProperty);
        }

        public static void SetMainIsClip(DependencyObject obj, bool value)
        {
            obj.SetValue(MainIsClipProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainIsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainIsClipProperty =
            DependencyProperty.RegisterAttached("MainIsClip", typeof(bool), typeof(DataGridManager));
        #endregion

        #region MainEffect
        public static Effect GetMainEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(MainEffectProperty);
        }

        public static void SetMainEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(MainEffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainEffectProperty =
            DependencyProperty.RegisterAttached("MainEffect", typeof(Effect), typeof(DataGridManager));
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

        #region RowNumberStartIndex
        public static int GetRowNumberStartIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(RowNumberStartIndexProperty);
        }

        public static void SetRowNumberStartIndex(DependencyObject obj, int value)
        {
            obj.SetValue(RowNumberStartIndexProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowNumberStartIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowNumberStartIndexProperty =
            DependencyProperty.RegisterAttached("RowNumberStartIndex", typeof(int), typeof(DataGridManager), new PropertyMetadata(1));
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

        private static void DataGrid_LoadingRow(object? sender, DataGridRowEventArgs e)
        {
            if (sender is DependencyObject depObj)
            {
                e.Row.Header = e.Row.GetIndex() + GetRowNumberStartIndex(depObj);
            }
            else
            {
                e.Row.Header = e.Row.GetIndex();
            }

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

        #region RowBackground
        public static Brush GetRowBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RowBackgroundProperty);
        }

        public static void SetRowBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(RowBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowBackgroundProperty =
            DependencyProperty.RegisterAttached("RowBackground", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region RowBorderBrush
        public static Brush GetRowBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RowBorderBrushProperty);
        }

        public static void SetRowBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(RowBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowBorderBrushProperty =
            DependencyProperty.RegisterAttached("RowBorderBrush", typeof(Brush), typeof(DataGridManager));
        #endregion

        #region RowBorderThickness
        public static Thickness GetRowBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(RowBorderThicknessProperty);
        }

        public static void SetRowBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(RowBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowBorderThicknessProperty =
            DependencyProperty.RegisterAttached("RowBorderThickness", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region RowCornerRadius
        public static CornerRadius GetRowCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(RowCornerRadiusProperty);
        }

        public static void SetRowCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(RowCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowCornerRadiusProperty =
            DependencyProperty.RegisterAttached("RowCornerRadius", typeof(CornerRadius), typeof(DataGridManager));
        #endregion

        #region RowIsClip
        public static bool GetRowIsClip(DependencyObject obj)
        {
            return (bool)obj.GetValue(RowIsClipProperty);
        }

        public static void SetRowIsClip(DependencyObject obj, bool value)
        {
            obj.SetValue(RowIsClipProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowIsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowIsClipProperty =
            DependencyProperty.RegisterAttached("RowIsClip", typeof(bool), typeof(DataGridManager));
        #endregion

        #region RowMargin
        public static Thickness GetRowMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(RowMarginProperty);
        }

        public static void SetRowMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(RowMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowMarginProperty =
            DependencyProperty.RegisterAttached("RowMargin", typeof(Thickness), typeof(DataGridManager));
        #endregion

        #region RowPadding
        public static Thickness GetRowPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(RowPaddingProperty);
        }

        public static void SetRowPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(RowPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowPaddingProperty =
            DependencyProperty.RegisterAttached("RowPadding", typeof(Thickness), typeof(DataGridManager));
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


        #region CheckBoxHook
        public static object GetCheckBoxHook(DependencyObject obj)
        {
            return (object)obj.GetValue(CheckBoxHookProperty);
        }

        public static void SetCheckBoxHook(DependencyObject obj, object value)
        {
            obj.SetValue(CheckBoxHookProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckBoxHook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckBoxHookProperty =
            DependencyProperty.RegisterAttached("CheckBoxHook", typeof(object), typeof(DataGridManager), new PropertyMetadata(CheckBoxHookChanged));

        public static void CheckBoxHookChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CheckBox check)
            {
                if (e.NewValue is DataGrid data)
                {
                    check.Click += Check_Click;

                    data.SelectedCellsChanged += (_, _) =>
                    {
                        check.IsChecked = data.Items.Count != 0 && data.SelectedItems.Count == data.Items.Count;
                    };
                }
                else
                {
                    check.Click -= Check_Click;
                }
            }
        }

        private static void Check_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox check && GetCheckBoxHook(check) is DataGrid datagrid)
            {
                if (check.IsChecked == true)
                {
                    datagrid.SelectAll();
                }

                if (check.IsChecked == false)
                {
                    datagrid.SelectedIndex = -1;
                }
            }
        }





        #endregion


        #region TextColumnStyle
        public static Style GetTextColumnStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(TextColumnStyleProperty);
        }

        public static void SetTextColumnStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(TextColumnStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for TextColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextColumnStyleProperty =
            DependencyProperty.RegisterAttached("TextColumnStyle", typeof(Style), typeof(DataGridManager), new PropertyMetadata(OnTextColumnStyleChanged));

        private static void OnTextColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid grid && e.OldValue == null && e.NewValue != null)
            {
                UpdateTextColumnStyles(grid);
            }
        }
        #endregion

        #region EditingTextColumnStyle
        public static Style GetEditingTextColumnStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(EditingTextColumnStyleProperty);
        }

        public static void SetEditingTextColumnStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(EditingTextColumnStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for EditingTextColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditingTextColumnStyleProperty =
            DependencyProperty.RegisterAttached("EditingTextColumnStyle", typeof(Style), typeof(DataGridManager), new PropertyMetadata(OnTextColumnStyleChanged));
        #endregion


        #region ComboBoxColumnStyle
        public static Style GetComboBoxColumnStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(ComboBoxColumnStyleProperty);
        }

        public static void SetComboBoxColumnStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(ComboBoxColumnStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for ComboBoxColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxColumnStyleProperty =
            DependencyProperty.RegisterAttached("ComboBoxColumnStyle", typeof(Style), typeof(DataGridManager), new PropertyMetadata(OnComboBoxColumnStyleChanged));

        private static void OnComboBoxColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid grid && e.OldValue == null && e.NewValue != null)
            {
                UpdateComboBoxColumnStyles(grid);
            }
        }
        #endregion


        #region EditingComboBoxColumnStyle
        public static Style GetEditingComboBoxColumnStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(EditingComboBoxColumnStyleProperty);
        }

        public static void SetEditingComboBoxColumnStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(EditingComboBoxColumnStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for EditingComboBoxColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditingComboBoxColumnStyleProperty =
            DependencyProperty.RegisterAttached("EditingComboBoxColumnStyle", typeof(Style), typeof(DataGridManager), new PropertyMetadata(OnComboBoxColumnStyleChanged));
        #endregion


        #region CheckBoxColumnStyle
        public static Style GetCheckBoxColumnStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(CheckBoxColumnStyleProperty);
        }

        public static void SetCheckBoxColumnStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(CheckBoxColumnStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckBoxColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckBoxColumnStyleProperty =
            DependencyProperty.RegisterAttached("CheckBoxColumnStyle", typeof(Style), typeof(DataGridManager), new PropertyMetadata(OnCheckBoxColumnStyleChanged));

        private static void OnCheckBoxColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid grid && e.OldValue == null && e.NewValue != null)
            {
                UpdateCheckBoxColumnStyles(grid);
            }
        }
        #endregion


        #region EditingCheckBoxColumnStyle
        public static Style GetEditingCheckBoxColumnStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(EditingCheckBoxColumnStyleProperty);
        }

        public static void SetEditingCheckBoxColumnStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(EditingCheckBoxColumnStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for EditingCheckBoxColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditingCheckBoxColumnStyleProperty =
            DependencyProperty.RegisterAttached("EditingCheckBoxColumnStyle", typeof(Style), typeof(DataGridManager));
        #endregion

        #region ApplyAutoGeneratedStyle
        public static bool GetApplyAutoGeneratedStyle(DependencyObject obj)
        {
            return (bool)obj.GetValue(ApplyAutoGeneratedStyleProperty);
        }

        public static void SetApplyAutoGeneratedStyle(DependencyObject obj, bool value)
        {
            obj.SetValue(ApplyAutoGeneratedStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for ApplyAutoGeneratedStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplyAutoGeneratedStyleProperty =
            DependencyProperty.RegisterAttached("ApplyAutoGeneratedStyle", typeof(bool), typeof(DataGridManager), new PropertyMetadata(OnApplyAutoGeneratedStyleChanged));

        private static void OnApplyAutoGeneratedStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid grid)
            {
                if ((bool)e.NewValue)
                {
                    if (grid.AutoGenerateColumns)
                    {
                        grid.AutoGeneratedColumns += Grid_AutoGeneratedColumns;
                        grid.AutoGeneratingColumn += Grid_AutoGeneratingColumn;
                        return;
                    }

                    UpdateTextColumnStyles(grid);
                    UpdateComboBoxColumnStyles(grid);
                    UpdateCheckBoxColumnStyles(grid);
                }
                else
                {
                    grid.AutoGeneratedColumns -= Grid_AutoGeneratedColumns;
                    grid.AutoGeneratingColumn -= Grid_AutoGeneratingColumn;
                }
            }
        }

        private static void Grid_AutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(ImageSource) && sender is DataGrid dataGrid)
            {
                var newColumn = new DataGridTemplateColumn()
                {
                    Width = e.Column.Width,
                    Header = e.Column.Header,
                    IsReadOnly = e.Column.IsReadOnly,
                    Visibility = e.Column.Visibility,
                };


                var dt = new DataTemplate();
                var fef = new FrameworkElementFactory(typeof(Image));
                fef.SetBinding(Image.SourceProperty, new Binding(e.PropertyName) { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                fef.SetValue(Image.StyleProperty, GetAutoGenerateImageStyle(dataGrid));
                dt.VisualTree = fef;
                newColumn.CellTemplate = dt;
                newColumn.CellEditingTemplate = dt;
                e.Column = newColumn;
            }
        }
        #endregion


        private static void Grid_AutoGeneratedColumns(object? sender, System.EventArgs e)
        {
            if (sender is DataGrid dg)
            {
                UpdateTextColumnStyles(dg);
                UpdateComboBoxColumnStyles(dg);
                UpdateCheckBoxColumnStyles(dg);
            }

        }
        private static void UpdateTextColumnStyles(DataGrid grid)
        {
            var textColumnStyle = GetTextColumnStyle(grid);
            var editingTextColumnStyle = GetEditingTextColumnStyle(grid);

            if (textColumnStyle != null)
            {
                foreach (var column in grid.Columns.OfType<DataGridTextColumn>())
                {
                    column.ElementStyle = textColumnStyle;
                }
            }

            if (editingTextColumnStyle != null)
            {
                foreach (var column in grid.Columns.OfType<DataGridTextColumn>())
                {

                    column.EditingElementStyle = editingTextColumnStyle;
                }
            }
        }
        private static void UpdateComboBoxColumnStyles(DataGrid grid)
        {
            var comboBoxColumnStyle = GetComboBoxColumnStyle(grid);
            var editingComboBoxColumnStyle = GetEditingComboBoxColumnStyle(grid);

            if (comboBoxColumnStyle != null)
            {
                foreach (var column in grid.Columns.OfType<DataGridComboBoxColumn>())
                {


                    column.ElementStyle = comboBoxColumnStyle;
                }
            }

            if (editingComboBoxColumnStyle != null)
            {
                foreach (var column in grid.Columns.OfType<DataGridComboBoxColumn>())
                {

                    column.EditingElementStyle = editingComboBoxColumnStyle;
                }
            }
        }

        private static void UpdateCheckBoxColumnStyles(DataGrid grid)
        {
            var checkBoxColumnStyle = GetCheckBoxColumnStyle(grid);
            var editingCheckBoxColumnStyle = GetEditingCheckBoxColumnStyle(grid);

            if (checkBoxColumnStyle != null)
            {
                foreach (var column in grid.Columns.OfType<DataGridCheckBoxColumn>())
                {

                    column.ElementStyle = checkBoxColumnStyle;
                }
            }

            if (editingCheckBoxColumnStyle != null)
            {
                foreach (var column in grid.Columns.OfType<DataGridCheckBoxColumn>())
                {
                    column.EditingElementStyle = editingCheckBoxColumnStyle;
                }
            }
        }
    }
}
