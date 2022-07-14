using LeeTeke.WPFTest.UserControls;
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
        private ObservableCollection<bool> items = new ObservableCollection<bool>() { false, true, true, true, true, true };
        public TestWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {


            int value = 0;
            while (value<100)
            {
                await Task.Delay(50);
                this.Dispatcher.Invoke(() =>
                {
                    value++;
                    ring.Value++;
                });

            }

           
        }
    }
}
