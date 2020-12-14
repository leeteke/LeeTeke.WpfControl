﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace LeeTeke.WpfControl.Controls
{
    /// <summary>
    /// MessageBoxEx.xaml 的交互逻辑
    /// </summary>
    public partial class Message : Window
    {
        public object Value { get; set; }
        private bool _canClose;

        public bool CanClose
        {
            get { return _canClose; }
            set
            {
                _canClose = value;
                btnClose.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Message()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            CanClose = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                this.Close();
        }

        public void AddButton(string name, object value)
        {
            var theButton = new Button()
            {
                Content = name,
                DataContext = value,
                MinWidth = 80,
                Margin = new Thickness(5, 5, 5, 0),
                Height = 30,
            };
            theButton.Click += TheButton_Click;
            btnPanle.Children.Add(theButton);
        }

        private void TheButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Value = button.DataContext;
                DialogResult = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CanClose;
        }
    }
}
