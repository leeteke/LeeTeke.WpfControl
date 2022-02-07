﻿using LeeTeke.WPFTest.Datas;
using LeeTeke.WPFTest.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeeTeke.WPFTest
{
    /// <summary>
    /// TestPage.xaml 的交互逻辑
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();
            this.DataContext = new TestPageViewModel();
        }

        private void Navigation_DragDropOver(object sender, RoutedEventArgs e)
        {

        }

        private void GridView_ItemClicked(object sender, WpfControl.GridViewItemClickedEventArgs e)
        {

        }
    }
}
