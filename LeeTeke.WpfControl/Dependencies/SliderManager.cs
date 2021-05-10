﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class SliderManager 
    {


        #region Header
        /// <summary>
        /// Header
        /// </summary>
        public static object GetHeader(DependencyObject obj)
        {
            return (object)obj.GetValue(HeaderProperty);
        }

        public static void SetHeader(DependencyObject obj, object value)
        {
            obj.SetValue(HeaderProperty, value);
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.RegisterAttached("Header", typeof(object), typeof(SliderManager));
        #endregion


        #region HeaderDock
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static Dock GetHeaderDock(DependencyObject obj)
        {
            return (Dock)obj.GetValue(HeaderDockProperty);
        }

        public static void SetHeaderDock(DependencyObject obj, Dock value)
        {
            obj.SetValue(HeaderDockProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderDock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderDockProperty =
            DependencyProperty.RegisterAttached("HeaderDock", typeof(Dock), typeof(SliderManager));
        #endregion



        #region TrackThumb
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static object  GetTrackThumb(DependencyObject obj)
        {
            return (object)obj.GetValue(TrackThumbProperty);
        }

        public static void SetTrackThumb(DependencyObject obj, object value)
        {
            obj.SetValue(TrackThumbProperty, value);
        }

        // Using a DependencyProperty as the backing store for TrackThumb.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackThumbProperty =
            DependencyProperty.RegisterAttached("TrackThumb", typeof(object), typeof(SliderManager));
        #endregion


        #region TrackSize
        /// <summary>
        /// Track的大小
        /// </summary>
        public static double GetTrackSize(DependencyObject obj)
        {
            return (double)obj.GetValue(TrackSizeProperty);
        }

        public static void SetTrackSize(DependencyObject obj, double value)
        {
            obj.SetValue(TrackSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for TrackSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackSizeProperty =
            DependencyProperty.RegisterAttached("TrackSize", typeof(double), typeof(SliderManager));
        #endregion


        #region CornerRadius
        /// <summary>
        /// CornerRadius
        /// </summary>
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(SliderManager));
        #endregion


        #region TrackBrush
        /// <summary>
        /// TrackBrush
        /// </summary>
        public static Brush GetTrackBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TrackBrushProperty);
        }

        public static void SetTrackBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(TrackBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for TrackBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackBrushProperty =
            DependencyProperty.RegisterAttached("TrackBrush", typeof(Brush), typeof(SliderManager));
        #endregion

    }
}
