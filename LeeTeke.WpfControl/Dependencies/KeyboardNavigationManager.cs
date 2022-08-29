using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LeeTeke.WpfControl.Dependencies
{
    public class KeyboardNavigationManager
    {

        #region Ban
        public static bool GetBan(DependencyObject obj)
        {
            return (bool)obj.GetValue(BanProperty);
        }

        public static void SetBan(DependencyObject obj, bool value)
        {
            obj.SetValue(BanProperty, value);
        }

        // Using a DependencyProperty as the backing store for Ban.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BanProperty =
            DependencyProperty.RegisterAttached("Ban", typeof(bool), typeof(KeyboardNavigationManager), new PropertyMetadata(BanChanged));
        #endregion

        private static void BanChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                if (e.NewValue is bool _value && _value)
                {
                    element.PreviewKeyDown += Box_PreviewKeyDown;
                    element.PreviewMouseWheel += Box_PreviewMouseWheel;
                }
                else
                {
                    element.PreviewKeyDown -= Box_PreviewKeyDown;
                    element.PreviewMouseWheel -= Box_PreviewMouseWheel;
                }
            }
        }

        private static void Box_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private static void Box_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Right:
                case Key.Up:
                case Key.Down:
                    e.Handled = true;
                    return;
                default:
                    break;
            }
        }
    }
}
