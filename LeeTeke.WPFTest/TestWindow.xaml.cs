﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeeTeke.WPFTest
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
            this.Loaded += TestWindow_Loaded;
        }

        private void TestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            listbox.ItemsSource=new ObservableCollection<string>() { "测试1", "测试2", "测试3", "测试4", };
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton== MouseButtonState.Pressed)
            {
         
                DragDrop.DoDragDrop(sender as UIElement, new DataObject("UIElement", sender as UIElement, true), DragDropEffects.Move);
            }
        }
    }
}
