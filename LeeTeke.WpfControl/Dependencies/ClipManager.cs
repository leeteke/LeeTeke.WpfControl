using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ClipManager
    {


        #region CornerRadius
        public static CornerRadius? GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius?)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius? value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ClipManager), new PropertyMetadata(OnCornerRadiusChanged));

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                if (e.NewValue is CornerRadius cr)
                {
                    element.SizeChanged += Element_SizeChanged;
                }
                else
                {
                    element.Clip = null;
                    element.SizeChanged -= Element_SizeChanged;
                }
            }
        }

        private static void Element_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (sender is FrameworkElement element)
            {
                var cr = GetCornerRadius(element);
                if (cr != null)
                {
                    element.Clip = Helper.GetRoundRectangle(new Rect(0, 0, element.ActualWidth, element.ActualHeight), cr.Value);
                }
                else
                {
                    element.Clip = null;
                }

            }

        }
        #endregion

    }
}
