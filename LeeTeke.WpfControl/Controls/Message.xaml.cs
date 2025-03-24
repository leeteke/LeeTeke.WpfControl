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
    public partial class Message : IMessageWin
    {
        public object? Value { get; set; }
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


        public new object? Content
        {
            get => content.Content;
            set
            {
                if (value is string str)
                {
                    if (content.Content is TextBox tb)
                    {
                        tb.Text = str;
                    }
                    else
                    {
                        content.Content = new TextBox()
                        {
                            Text = str,
                            Background = null,
                            TextWrapping = TextWrapping.Wrap,
                            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                            BorderThickness = new Thickness(0),
                            IsReadOnly = true,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalContentAlignment = HorizontalAlignment.Left,
                        };
                    }
                }
                else
                {
                    content.Content = value;
                }

            }

        }
        public System.Windows.Window Window { get => this; }
        public bool ShowProcess
        {
            get => loading.Visibility == Visibility.Visible;
            set => loading.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }



        #region ProcessValue
        /// <summary>
        /// 请添加描述
        /// </summary>
        public int ProcessValue
        {
            get { return (int)GetValue(ProcessValueProperty); }
            set { SetValue(ProcessValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProcessValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProcessValueProperty =
            DependencyProperty.Register("ProcessValue", typeof(int), typeof(Message));
        #endregion

        private MessageStatus _status;
        public MessageStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                ring.Visibility = Visibility.Collapsed;
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
                    case MessageStatus.Wating:
                        icon.Visibility = Visibility.Collapsed;
                        ring.Visibility = Visibility.Visible;
                        break;
                    default:
                        icon.Visibility = Visibility.Collapsed;
                        ring.Visibility = Visibility.Collapsed;
                        break;
                }

            }
        }

        public Message()
        {
            this.Resources= Application.Current.Resources;
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            try
            {
                foreach (var item in Application.Current.Windows)
                {
                    if (item is System.Windows.Window win && win.IsActive)
                    {
                        Owner = win;
                        WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        break;
                    }
                }
            }
            catch
            {
            }

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

        public void AddOptions(string name, object? value, CornerRadius? cornerRadius = null)
        {
            var theButton = new Button()
            {
                Content = name,
                DataContext = value,
                MinWidth = 80,
                Margin = new Thickness(5, 5, 5, 0),
                Height = 30,
            };
             
            if (Config.MessageBoxExButtonCornerRadius != null)
            {
                Dependencies.CornerRadiusManager.SetCornerRadius(theButton, (CornerRadius)Config.MessageBoxExButtonCornerRadius);
            }

            if (cornerRadius != null)
            {
                Dependencies.CornerRadiusManager.SetCornerRadius(theButton, (CornerRadius)cornerRadius);
            }
            theButton.Click += TheButton_Click;
            btnPanle.Children.Add(theButton);
        }

        public void AddOptions(Button btn, object? value)
        {
            if (value != null)
                btn.DataContext = value;
            btn.Click += TheButton_Click;
            btnPanle.Children.Add(btn);
        }
    }
}
