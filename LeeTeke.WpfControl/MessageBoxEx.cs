using LeeTeke.WpfControl.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LeeTeke.WpfControl
{
    public class MessageBoxEx
    {

        /// <summary>
        /// 显示并返回
        /// </summary>
        /// <param name="content"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool Show(string content, MessageStatus status = MessageStatus.None)
        {
            Message message = new Message();
            message.textConetnt.Text = content;
            SetIcon(message, status);
            switch (status)
            {
                case MessageStatus.None:
                    break;
                case MessageStatus.Question:
                    message.AddButton("是", true);
                    message.AddButton("否", false);
                    break;
                default:
                    message.AddButton("确定", true);
                    break;
            }
            _ = message.ShowDialog();
            return message.Value == null ? false : (bool)message.Value;
        }

        public static bool Show(string title, string content, MessageStatus status = MessageStatus.None)
        {
            Message message = new Message();
            message.textTitle.Text = title;
            message.textTitle.Visibility = System.Windows.Visibility.Visible;
            message.textConetnt.Text = content;
            SetIcon(message, status);
            switch (status)
            {
                case MessageStatus.None:
                    break;
                   
                case MessageStatus.Question:
                    message.AddButton("是", true);
                    message.AddButton("否", false);
                    break;
                default:
                    message.AddButton("确定", true);
                    break;
            }

            _ = message.ShowDialog();
            return message.Value == null ? false : (bool)message.Value;
        }

        private Message _msg;
        public MessageBoxEx(CornerRadius cornerRadius = default)
        {
            _msg = new Message(cornerRadius);
        }
        /// <summary>
        /// 是否可以关闭
        /// </summary>
        public bool CanClose
        {
            get => _msg.CanClose;
            set => _msg.CanClose = value;
        }

        public bool ShowLoding
        {
            get => _msg.loding.Visibility == System.Windows.Visibility.Visible;
            set => _msg.loding.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            CanClose = true;
            _msg.Close();
            GC.Collect();
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get => _msg.textTitle.Text;
            set
            {
                _msg.textTitle.Text = value;
                _msg.textTitle.Visibility = string.IsNullOrWhiteSpace(value) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            }
        }


        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get => _msg.textConetnt.Text;
            set => _msg.textConetnt.Text = value;
        }

        /// <summary>
        /// 设置自定义内容
        /// </summary>
        /// <param name="element"></param>
        public void SetContent(UIElement element)
        {
            _msg.textConetnt.Visibility = Visibility.Collapsed;
            _msg.gridMain.Children.Add(element);
            System.Windows.Controls.Grid.SetColumn(element, 1);
            System.Windows.Controls.Grid.SetRow(element, 1);
        }


        /// <summary>
        /// lodingbar模式
        /// </summary>
        public LodingBarMode LodingBarModel
        {
            get => _msg.loding.Mode;
            set => _msg.loding.Mode = value;
        }

        /// <summary>
        /// 设置lodingVale 范围0-100;
        /// </summary>
        public double LodingBarValue { get => _msg.loding.Value; set => _msg.loding.Value = value; }
        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="icon"></param>
        public void SetIcon(MessageStatus icon) => SetIcon(_msg, icon);


        /// <summary>
        /// 设置选项
        /// </summary>
        /// <param name="name"></param>
        /// <param name="vale"></param>
        /// <param name="btnCornerRadius">圆角值</param>
        public void AddOptions(string name, object vale, CornerRadius btnCornerRadius = default) => _msg.AddButton(name, vale, btnCornerRadius);

        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(double width = 320, double height = 200)
        {
            _msg.Width = width;
            _msg.Height = height;
        }


        public async Task<object> ShowDialog()
        {

            var task = Task<object>.Factory.StartNew(() =>
             {
                 _msg.Dispatcher.Invoke(() =>
                 {
                     _ = _msg.ShowDialog();
                 });
                 return _msg.Value;
             });
            return await task;
        }

        public void Show()
        {
            _msg.Show();
        }

        private static void SetIcon(Message msg, MessageStatus icon)
        {
            switch (icon)
            {
                case MessageStatus.None:
                    msg.icon.Visibility = Visibility.Collapsed;
                    break;
                case MessageStatus.OK:
                    msg.icon.Visibility = Visibility.Visible;
                    msg.icon.Text = "\xEC61";
                    msg.icon.Foreground = Application.Current.Resources["LeeBrush_Success"] as SolidColorBrush;
                    break;
                case MessageStatus.Warning:
                    msg.icon.Visibility = Visibility.Visible;
                    msg.icon.Text = "\xE814";
                    msg.icon.Foreground = Application.Current.Resources["LeeBrush_Warning"] as SolidColorBrush;
                    break;
                case MessageStatus.Info:
                    msg.icon.Visibility = Visibility.Visible;
                    msg.icon.Text = "\xF167";
                    msg.icon.Foreground = Application.Current.Resources["LeeBrush_Info"] as SolidColorBrush;
                    break;
                case MessageStatus.Question:
                    msg.icon.Visibility = Visibility.Visible;
                    msg.icon.Text = "\xE9CE";
                    msg.icon.Foreground = Application.Current.Resources["LeeBrush_Question"] as SolidColorBrush;
                    break;
                case MessageStatus.Error:
                    msg.icon.Visibility = Visibility.Visible;
                    msg.icon.Text = "\xEB90";
                    msg.icon.Foreground = Application.Current.Resources["LeeBrush_Error"] as SolidColorBrush;
                    break;
                case MessageStatus.Stop:
                    msg.icon.Visibility = Visibility.Visible;
                    msg.icon.Text = "\xF140";
                    msg.icon.Foreground = Application.Current.Resources["LeeColor_Stop"] as SolidColorBrush;
                    break;
            
                default:
                    break;
            }

        }

    }

    public enum MessageStatus
    {
        None,
        OK,
        Warning,
        Info,
        Question,
        Error,
        Stop,
    }
}
