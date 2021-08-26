using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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



        #region TrackThumbStyle
        public static Style GetTrackThumbStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(TrackThumbStyleProperty);
        }

        public static void SetTrackThumbStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(TrackThumbStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for TrackThumbStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackThumbStyleProperty =
            DependencyProperty.RegisterAttached("TrackThumbStyle", typeof(Style), typeof(SliderManager));
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



        #region RegisterMouseWheel
        public static bool GetRegisterMouseWheel(DependencyObject obj)
        {
            return (bool)obj.GetValue(RegisterMouseWheelProperty);
        }

        public static void SetRegisterMouseWheel(DependencyObject obj, bool value)
        {
            obj.SetValue(RegisterMouseWheelProperty, value);
        }

        // Using a DependencyProperty as the backing store for RegisterMouseWheel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegisterMouseWheelProperty =
            DependencyProperty.RegisterAttached("RegisterMouseWheel", typeof(bool), typeof(SliderManager), new PropertyMetadata(RegisterMouseWheelChanged));

        private static void RegisterMouseWheelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Slider slider)
            {
                if (e.NewValue is bool _isRegister)
                {
                    slider.MouseWheel += (ex, es) =>
                    {
                        if (!_isRegister)
                            return;



                        if (es.Delta > 0)
                        {
                            var result = slider.Value + GetMouseWheelValue(d);
                            slider.Value = result > slider.Maximum ? slider.Maximum : result;

                        }
                        else
                        {
                            var result = slider.Value - GetMouseWheelValue(d);
                            slider.Value = result < slider.Minimum ? slider.Minimum : result;
                        }

                    };
                }

            }

        }
        #endregion


        #region MouseWheelValue
        public static double GetMouseWheelValue(DependencyObject obj)
        {
            return (double)obj.GetValue(MouseWheelValueProperty);
        }

        public static void SetMouseWheelValue(DependencyObject obj, double value)
        {
            obj.SetValue(MouseWheelValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseWheelValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseWheelValueProperty =
            DependencyProperty.RegisterAttached("MouseWheelValue", typeof(double), typeof(SliderManager), new PropertyMetadata(1.0));
        #endregion



    }
}
