using LeeTeke.WpfControl.Controls;
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
using System.Windows.Shapes;

namespace LeeTeke.WpfControl.Demo.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

           
            LeeTeke.WpfControl.MessageBoxEx.Show("123123",MessageStatus.Question);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Config.ThemeMode= Models.ThemeMode.Dark;
            nav.Items.Add(new NavigationItem() { Content = "你好", IsSelected=true, CanClose = false, Tag = "123123" });

        }
    }
}
