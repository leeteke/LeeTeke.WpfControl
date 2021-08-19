using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl.Dependencies
{
   public  class CornerRadiusManager
    {
        #region CornerRadius
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
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(CornerRadiusManager));
        #endregion

        #region IsClip
        public static bool GetIsClip(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsClipProperty);
        }

        public static void SetIsClip(DependencyObject obj, bool value)
        {
            obj.SetValue(IsClipProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClipProperty =
            DependencyProperty.RegisterAttached("IsClip", typeof(bool), typeof(CornerRadiusManager));
        #endregion

    }
}
