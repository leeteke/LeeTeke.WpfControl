using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ComboBoxManager 
    {

        #region EscToEmpty
        public static bool GetEscToEmpty(DependencyObject obj)
        {
            return (bool)obj.GetValue(EscToEmptyProperty);
        }

        public static void SetEscToEmpty(DependencyObject obj, bool value)
        {
            obj.SetValue(EscToEmptyProperty, value);
        }

        // Using a DependencyProperty as the backing store for EscToEmpty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EscToEmptyProperty =
            DependencyProperty.RegisterAttached("EscToEmpty", typeof(bool), typeof(ComboBoxManager));


   
        #endregion

        #region EnterCommand
        public static ICommand GetEnterCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(EnterCommandProperty);
        }

        public static void SetEnterCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(EnterCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnterCommandProperty =
            DependencyProperty.RegisterAttached("EnterCommand", typeof(ICommand), typeof(ComboBoxManager));




        #endregion

        #region ShowPreviewText
        public static bool GetShowPreviewText(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowPreviewTextProperty);
        }

        public static void SetShowPreviewText(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowPreviewTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowPreviewText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowPreviewTextProperty =
            DependencyProperty.RegisterAttached("ShowPreviewText", typeof(bool), typeof(ComboBoxManager));
        #endregion

        #region PreviewText
        public static string GetPreviewText(DependencyObject obj)
        {
            return (string)obj.GetValue(PreviewTextProperty);
        }

        public static void SetPreviewText(DependencyObject obj, string value)
        {
            obj.SetValue(PreviewTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewTextProperty =
            DependencyProperty.RegisterAttached("PreviewText", typeof(string), typeof(ComboBoxManager));
        #endregion

    }
}
