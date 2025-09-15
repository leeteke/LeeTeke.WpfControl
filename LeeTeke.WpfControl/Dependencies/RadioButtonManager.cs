using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class RadioButtonManager 
    {
        #region RippleBrush
        /// <summary>
        /// 涟漪颜色
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Brush GetRippleBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RippleBrushProperty);
        }

        public static void SetRippleBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(RippleBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for RippleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RippleBrushProperty =
            DependencyProperty.RegisterAttached("RippleBrush", typeof(Brush), typeof(RadioButtonManager));
        #endregion

        #region MarkBrush
        /// <summary>
        /// 选中标记的颜色
        /// </summary>
        public static Brush GetMarkBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MarkBrushProperty);
        }

        public static void SetMarkBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MarkBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkBrushProperty =
            DependencyProperty.RegisterAttached("MarkBrush", typeof(Brush), typeof(RadioButtonManager));
        #endregion

        #region MarkSize
        /// <summary>
        /// 标记大小
        /// </summary>
        public static double GetMarkSize(DependencyObject obj)
        {
            return (double)obj.GetValue(MarkSizeProperty);
        }

        public static void SetMarkSize(DependencyObject obj, double value)
        {
            obj.SetValue(MarkSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkSizeProperty =
            DependencyProperty.RegisterAttached("MarkSize", typeof(double), typeof(RadioButtonManager), new PropertyMetadata(DependencyConst.FontSize));
        #endregion

        #region MarkMargin
        /// <summary>
        /// Mark边距
        /// </summary>
        public static Thickness GetMarkMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarkMarginProperty);
        }

        public static void SetMarkMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarkMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for MarkMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkMarginProperty =
            DependencyProperty.RegisterAttached("MarkMargin", typeof(Thickness), typeof(RadioButtonManager));
        #endregion


    }



}
