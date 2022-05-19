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
           var a=  new WpfControl.MaskPanelData()
            {
                Title = "你好a",
                ContentSize = new Size(300, 200),
                Content = new Grid()
                {
                    Background = new SolidColorBrush(Colors.Red),
                },
                CloseCallback = p =>
                {
                    LeeTeke.WpfControl.MessageBoxEx.Show("1");

                },

            };
            mask.ContentData = a;

            await Task.Delay(3000);
             a.ClosePanel?.Invoke();
            await Task.Delay(3000);

            mask.ContentData =  new WpfControl.MaskPanelData()
            {
                Title = "你好b",
                ContentSize = new Size(300, 200),
                Content = new Grid()
                {
                    Background = new SolidColorBrush(Colors.Yellow),
                },
                CloseCallback = p =>
                {
                    if (p)
                    {
                        LeeTeke.WpfControl.MessageBoxEx.Show("2");

                    }
                    else
                    {
                        LeeTeke.WpfControl.MessageBoxEx.Show("3");

                    }

                },

            };
            await Task.Delay(1000);
          //  a.ClosePanel();
        }
    }
}
