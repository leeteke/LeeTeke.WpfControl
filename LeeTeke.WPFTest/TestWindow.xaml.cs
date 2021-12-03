using System;
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
        private ObservableCollection<bool>  items= new ObservableCollection<bool>() { false,true,true,true, true,true };
        public TestWindow()
        {
            InitializeComponent();
            this.Loaded += TestWindow_Loaded;
        }

        private void TestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            test.ItemsSource = items;

            for (int i = 0; i < 100; i++)
            {
                tb.AppendText($"{i}\r\n");
            }
      
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void test_SelectionChanged(object sender, WpfControl.ToggleSelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LeeTeke.WpfControl.MessageBoxEx.Show("测试");
        }

        private void Navigation_ItemSelected(object sender, WpfControl.NavigationItemSelectedEventArgs e)
        {
            tb.AppendText($"S__{DateTime.Now:HH:mm:ss:fff}__{e.Value}\r\n");
        }

        private void test_ItemClosed(object sender, WpfControl.NavigationItemColsedEventArgs e)
        {
            tb.AppendText($"C__{DateTime.Now:HH:mm:ss:fff}__{e.Value}\r\n");
        }

        private void Navigation_DragDropOver(object sender, RoutedEventArgs e)
        {

        }

        private void test_SelectionChanged_1(object sender, WpfControl.ToggleSelectionChangedEventArgs e)
        {

        }
    }
}
