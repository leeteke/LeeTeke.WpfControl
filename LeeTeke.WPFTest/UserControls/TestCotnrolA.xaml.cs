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
using System.Windows.Threading;

namespace LeeTeke.WPFTest.UserControls
{
    /// <summary>
    /// TestCotnrolA.xaml 的交互逻辑
    /// </summary>
    public partial class TestCotnrolA : UserControl
    {
        DispatcherTimer _time;
        int _timeValue = 0;
        public TestCotnrolA()
        {
            InitializeComponent();
            Loaded += TestCotnrolA_Loaded;
        }

        private void TestCotnrolA_Loaded(object sender, RoutedEventArgs e)
        {
            if (_time == null)
            {
                _time = new DispatcherTimer();
                _time.Interval = TimeSpan.FromSeconds(1);
                _time.Tick += (_, _) =>
                {
                    txtTick.Text = (++_timeValue).ToString();
                };
                _time.Start();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoNextEvent?.Invoke(this, null);
        }


        public event EventHandler GoNextEvent;

        public void SetValue(string @value)
        {
            txtResult.Text = @value;
        }
    }
}
