using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LeeTeke.WpfControl.Dependencies
{
   public class FrameManager 
    {


        #region DisableNavigation
        public static bool GetDisableNavigation(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisableNavigationProperty);
        }

        public static void SetDisableNavigation(DependencyObject obj, bool value)
        {
            obj.SetValue(DisableNavigationProperty, value);
        }

        // Using a DependencyProperty as the backing store for DisableNavigation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisableNavigationProperty =
            DependencyProperty.RegisterAttached("DisableNavigation", typeof(bool), typeof(FrameManager), new PropertyMetadata(DisableNavigationChanged));

        private static void DisableNavigationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Frame frame)
            {
                if (e.NewValue is bool disable && disable)
                {
                    frame.Navigated += Frame_Navigated;
                }
                else
                {
                    frame.Navigated -= Frame_Navigated;
                }
            }
        }

        private static void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ((Frame)sender).NavigationService.RemoveBackEntry();
        }
        #endregion



    }
}
