using LeeTeke.WpfControl.Models;
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

namespace LeeTeke.WpfControl.Controls
{
    /// <summary>
    /// Message.xaml 的交互逻辑
    /// </summary>
    /// <summary>
    /// MessageBoxEx.xaml 的交互逻辑
    /// </summary>
    public partial class Message :  IMessageWin
    {
        public object Value { get; set; }
        private bool _canClose;
        private bool _isDialog = false;


        public bool CanClose
        {
            get => _canClose;
            set
            {
                _canClose = value;
                CloseButtonIsEnable = value;
            }
        }


        public new object Content
        {
            get => content.Content;
            set
            {
                content.Content = value;
                if (value is string str)
                {
                    content.VerticalAlignment = VerticalAlignment.Center;
                    content.HorizontalAlignment = HorizontalAlignment.Left;
                }
                else
                {
                    content.VerticalAlignment = VerticalAlignment.Stretch;
                    content.HorizontalAlignment = HorizontalAlignment.Stretch;
                }
            }

        }
        public System.Windows.Window Window { get => this; }
        public bool ShowLoding
        {
            get => loding.Visibility == Visibility.Visible;
            set => loding.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
        public ProcessControlMode ProcessControlMode
        {
            get => loding.Mode;
            set => loding.Mode = value;
        }
        public double ProcessControlValue
        {
            get => loding.Value;
            set => loding.Value = value;
        }
        private MessageStatus _status;
        public MessageStatus Status
        {
            get => _status;
            set
            {
                _status = value;

                switch (value)
                {
                    case MessageStatus.None:
                        icon.Visibility = Visibility.Collapsed;
                        break;
                    case MessageStatus.OK:
                        icon.Visibility = Visibility.Visible;
                        icon.Text = "\xEC61";
                        icon.Foreground = Application.Current.Resources["LeeBrush_Success"] as SolidColorBrush;
                        break;
                    case MessageStatus.Warning:
                        icon.Visibility = Visibility.Visible;
                        icon.Text = "\xE814";
                        icon.Foreground = Application.Current.Resources["LeeBrush_Warning"] as SolidColorBrush;
                        break;
                    case MessageStatus.Info:
                        icon.Visibility = Visibility.Visible;
                        icon.Text = "\xF167";
                        icon.Foreground = Application.Current.Resources["LeeBrush_Info"] as SolidColorBrush;
                        break;
                    case MessageStatus.Question:
                        icon.Visibility = Visibility.Visible;
                        icon.Text = "\xE9CE";
                        icon.Foreground = Application.Current.Resources["LeeBrush_Question"] as SolidColorBrush;
                        break;
                    case MessageStatus.Error:
                        icon.Visibility = Visibility.Visible;
                        icon.Text = "\xEB90";
                        icon.Foreground = Application.Current.Resources["LeeBrush_Error"] as SolidColorBrush;
                        break;
                    case MessageStatus.Stop:
                        icon.Visibility = Visibility.Visible;
                        icon.Text = "\xF140";
                        icon.Foreground = Application.Current.Resources["LeeColor_Stop"] as SolidColorBrush;
                        break;

                    default:
                        icon.Visibility = Visibility.Collapsed;
                        break;
                }

            }
        }

        public Message()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            CanClose = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        public new void Show()
        {
            _isDialog = false;
            base.Show();
        }
        public new bool? ShowDialog()
        {
            _isDialog = true;
            return base.ShowDialog();
        }

        private void TheButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Value = button.DataContext;

                if (Value is Action action)
                {
                    action.Invoke();
                }

                if (_isDialog)
                {
                    DialogResult = true;
                }

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CanClose;
        }



        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }

            }
            catch
            {

            }
        }

        public void SetSize(double width = 320, double height = 200)
        {
            Width = width;
            Height = height;
        }

        public void AddOptions(string name, object value, CornerRadius cornerRadius = default)
        {
            var theButton = new Button()
            {
                Content = name,
                DataContext = value,
                MinWidth = 80,
                Margin = new Thickness(5, 5, 5, 0),
                Height = 30,
            };

            if (StaticMethods.MessageBoxExBtnCR != default)
            {
                Dependencies.CornerRadiusManager.SetCornerRadius(theButton, StaticMethods.MessageBoxExBtnCR);
            }

            if (cornerRadius != default)
            {
                Dependencies.CornerRadiusManager.SetCornerRadius(theButton, cornerRadius);
            }
            theButton.Click += TheButton_Click;
            btnPanle.Children.Add(theButton);
        }
    }
}
