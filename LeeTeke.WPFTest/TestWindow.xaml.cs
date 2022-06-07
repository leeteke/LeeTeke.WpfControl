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
            var a = new WpfControl.MaskPanelData()
            {
                Title = "控件A",
                ContentSize = new Size(300, 200),

            };
            var aControl = new TestCotnrolA();
            aControl.GoNextEvent += (ao, ae) =>
            {
                a.Close();
            };

            a.Content = aControl;
            a.CloseCallback = p =>
            {
                if (p == WpfControl.MaskPanelCloseStatus.Self)
                {

                    string result = null;
                    var b = new WpfControl.MaskPanelData()
                    {
                        Title = "控件b",
                        ContentSize = new Size(300, 200),
                    };

                    var bControl = new TestControlB();

                    bControl.ReturnEvent += (bo, be) =>
                    {
                        b.Close();
                        result = be;
                    };

                    b.Content = bControl;

                    b.CloseCallback = bp =>
                    {
                        mask.ContentData = a;
                        if (bp == WpfControl.MaskPanelCloseStatus.Self)
                        {
                            aControl.SetValue(result);
                        }
                    };

                    mask.ContentData = b;
                }
            };

            mask.ContentData = a;
        }
    }
}
