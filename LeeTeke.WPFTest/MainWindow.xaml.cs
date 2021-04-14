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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void test_SelectionChanged(object sender, object e)
        {

        }

        private void Test_IconCliecked(object sender, string e)
        {

        }

        private void WrapPanel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //test.Content = new WpfControl.NotifyBannerShowModel("你好", "1", new SolidColorBrush(Colors.Red), null, new System.Media.SoundPlayer(Properties.Resources._8400));
        }

        private void test_NotifyBannerClicked(object sender, RoutedEventArgs e)
        {

        }

        private void test_NotifyBannerClicked_1(object sender, WpfControl.NotifyBannerClickedEventArgs e)
        {

        }

        private void test_NotifyBannerClicked(object sender, WpfControl.NotifyBannerClickedEventArgs e)
        {

        }

        private void tage_MouseEnter(object sender, MouseEventArgs e)
        {
    //        tage.Width = 300;
        }

        private void tage_MouseLeave(object sender, MouseEventArgs e)
        {
         //   tage.Width = 45;
        }
    }
}
