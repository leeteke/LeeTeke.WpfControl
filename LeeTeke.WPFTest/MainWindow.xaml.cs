using LeeTeke.WPFTest.Models;
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
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
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

 
   

        private void tage_MouseEnter(object sender, MouseEventArgs e)
        {
            //        tage.Width = 300;
        }

        private void tage_MouseLeave(object sender, MouseEventArgs e)
        {
            //   tage.Width = 45;
        }

        private void NotifyBanner_Clicked(object sender, WpfControl.NotifyBannerClickedEventArgs e)
        {

        }

        private void NotifyBanner_Closed(object sender, WpfControl.NotifyClosedEventArgs e)
        {

        }
    }
}
