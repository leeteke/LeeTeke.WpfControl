using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Dependencies
{
    public class TextBoxManager
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
            DependencyProperty.RegisterAttached("EscToEmpty", typeof(bool), typeof(TextBoxManager), new PropertyMetadata(EscToEmptyChanged));

        private static void EscToEmptyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox box && e.NewValue is bool @bool)
            {
                if (@bool)
                {
                    box.KeyDown += Box_EscKeyDown;
                }
                else
                {
                    box.KeyDown -= Box_EscKeyDown;
                }
            }
        }

        private static void Box_EscKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape && sender is TextBox box)
            {
                box.Text = string.Empty;
            }
        }
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
            DependencyProperty.RegisterAttached("EnterCommand", typeof(ICommand), typeof(TextBoxManager), new PropertyMetadata(EnterCommandChanged));

        private static void EnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox box)
            {
                box.KeyDown -= Box_EnterKeyDown;
                if (e.NewValue != null)
                    box.KeyDown += Box_EnterKeyDown;
            }
        }

        private static void Box_EnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && sender is TextBox box)
            {
                Keyboard.ClearFocus();
                var command = GetEnterCommand(box);
                command?.Execute(box.Text);
            }
        }
        #endregion

        #region ActiveBackground
        public static Brush GetActiveBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ActiveBackgroundProperty);
        }

        public static void SetActiveBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ActiveBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for ActiveBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveBackgroundProperty =
            DependencyProperty.RegisterAttached("ActiveBackground", typeof(Brush), typeof(TextBoxManager));
        #endregion

    }
}
