using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LeeTeke.WpfControl.Dependencies
{
  public  class PasswordManager 
    {
        

        #region Password
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordManager), new PropertyMetadata(PasswordChanged));

        private static void PasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox pwdBox)
            {
                pwdBox.PasswordChanged -= PwdBox_PasswordChanged;
                if (!(bool)GetIsUpdating(pwdBox))
                {
                    pwdBox.Password = (string)e.NewValue;
                }
                pwdBox.PasswordChanged += PwdBox_PasswordChanged; ;
            }

        }

        private static void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pwdBox)
            {
                SetIsUpdating(pwdBox, true);
                SetPassword(pwdBox, pwdBox.Password);
                SetIsUpdating(pwdBox, false);
            }
        }
        #endregion


        #region Attach
        public static bool GetAttach(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachProperty, value);
        }

        // Using a DependencyProperty as the backing store for Attach.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordManager), new PropertyMetadata(false, AttachChanged));

        private static void AttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox pwdBox)
            {

                if (pwdBox == null)
                    return;
                if ((bool)e.OldValue)
                {
                    pwdBox.PasswordChanged -= PwdBox_PasswordChanged;
                }
                if ((bool)e.NewValue)
                {
                    pwdBox.PasswordChanged += PwdBox_PasswordChanged;
                }
            }
        }
        #endregion


        #region IsUpdating
        public static bool GetIsUpdating(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsUpdatingProperty);
        }

        public static void SetIsUpdating(DependencyObject obj, bool value)
        {
            obj.SetValue(IsUpdatingProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsUpdating.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordManager));
        #endregion


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
            DependencyProperty.RegisterAttached("EscToEmpty", typeof(bool), typeof(PasswordManager), new PropertyMetadata(EscToEmptyChanged));

        private static void EscToEmptyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox box && e.NewValue is bool @bool)
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
            if (e.Key == System.Windows.Input.Key.Escape && sender is PasswordBox box)
            {
                box.Password = string.Empty;
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
            DependencyProperty.RegisterAttached("EnterCommand", typeof(ICommand), typeof(PasswordManager), new PropertyMetadata(EnterCommandChanged));

        private static void EnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox box)
            {
                box.KeyDown -= Box_EnterKeyDown;
                if (e.NewValue != null)
                    box.KeyDown += Box_EnterKeyDown;
            }
        }

        private static void Box_EnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && sender is PasswordBox box)
            {
                var command = GetEnterCommand(box);
                if (command != null)
                    command.Execute(box.Password);
            }
        }
        #endregion


    }
}
