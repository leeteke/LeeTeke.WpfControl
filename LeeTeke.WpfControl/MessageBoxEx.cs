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
        public static bool Show(string content, MessageBoxExEnum status)
        {
            Message message = new Message();
            message.textConetnt.Text = content;
            switch (status)
            {
                case MessageBoxExEnum.None:
                    break;
                case MessageBoxExEnum.OK:
                    message.icon.Text = "\xEC61";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 124, 187, 0));
                    message.AddButton("确定", true);
                    break;
                case MessageBoxExEnum.Warning:
                    message.icon.Text = "\xE814";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 153, 51));
                    message.AddButton("确定", true);
                    message.AddButton("取消", false);
                    break;
                case MessageBoxExEnum.Information:
                    message.icon.Text = "\xF167";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 1, 119, 215));
                    message.AddButton("确定", true);
                    break;
                case MessageBoxExEnum.Question:
                    message.icon.Text = "\xE9CE";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 1, 119, 215));
                    message.AddButton("是", true);
                    message.AddButton("否", false);
                    break;
                case MessageBoxExEnum.Erro:
                    message.icon.Text = "\xEB90";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 246, 83, 20));
                    message.AddButton("确定", true);
                    break;
                case MessageBoxExEnum.Stop:
                    message.icon.Text = "\xF140";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 235, 60, 0));
                    message.AddButton("确定", true);
                    break;
                case MessageBoxExEnum.Wating:
                    message.icon.Text = "\xEC32";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 165, 103, 63));
                    message.AddButton("确定", true);
                    break;
                default:
                    break;
            }

            _ = message.ShowDialog();
            return message.Value == null ? false : (bool)message.Value;
        }

        public static bool Show(string title, string content, MessageBoxExEnum status)
        {
            Message message = new Message();
            message.textTitle.Text = title;
            message.textTitle.Visibility = System.Windows.Visibility.Visible;
            message.textConetnt.Text = content;
            switch (status)
            {
                case MessageBoxExEnum.None:
                    break;
                case MessageBoxExEnum.OK:
                    message.icon.Text = "\xEC61";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 124, 187, 0));
                    message.AddButton("确定", true);
                    break;
                case MessageBoxExEnum.Warning:
                    message.icon.Text = "\xE814";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 153, 51));
                    message.AddButton("确定", true);
                    message.AddButton("取消", false);
                    break;
                case MessageBoxExEnum.Information:
                    message.icon.Text = "\xF167";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 1, 119, 215));
                    message.AddButton("确定", true);
                    break;
                case MessageBoxExEnum.Question:
                    message.icon.Text = "\xE9CE";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 1, 119, 215));
                    message.AddButton("是", true);
                    message.AddButton("否", false);
                    break;
                case MessageBoxExEnum.Erro:
                    message.icon.Text = "\xEB90";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 246, 83, 20));
                    message.AddButton("确定", true);
                    break;
                case MessageBoxExEnum.Stop:
                    message.icon.Text = "\xF140";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 235, 60, 0));
                    message.AddButton("确定", true);
                    break;
                case MessageBoxExEnum.Wating:
                    message.icon.Text = "\xEC32";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 165, 103, 63));
                    message.AddButton("确定", true);
                    break;
                default:
                    break;
            }
            _ = message.ShowDialog();
            return message.Value == null ? false : (bool)message.Value;
        }

        private Message message;
        public MessageBoxEx()
        {
            message = new Message();
        }
        /// <summary>
        /// 是否可以关闭
        /// </summary>
        public bool CanClose
        {
            get => message.CanClose;
            set => message.CanClose = value;
        }

        public bool ShowLoding
        {
            get => message.loding.Visibility == System.Windows.Visibility.Visible;
            set => message.loding.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            CanClose = true;
            message.Close();
            GC.Collect();
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get => message.textTitle.Text;
            set
            {
                message.textTitle.Text = value;
                message.textTitle.Visibility = string.IsNullOrWhiteSpace(value) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            }
        }


        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get => message.textConetnt.Text;
            set => message.textConetnt.Text = value;
        }

        /// <summary>
        /// lodingbar模式
        /// </summary>
        public LodingBarMode LodingBarModel
        {
            get => message.loding.Mode;
            set => message.loding.Mode = value;
        }

        /// <summary>
        /// 设置lodingVale 范围0-100;
        /// </summary>
        public double LodingBarValue { get => message.loding.Value; set => message.loding.Value=value; }
        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="icon"></param>
        public void SetIcon(MessageBoxExEnum icon)
        {
            switch (icon)
            {
                case MessageBoxExEnum.None:
                    break;
                case MessageBoxExEnum.OK:
                    message.icon.Text = "\xEC61";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 124, 187, 0));
                    break;
                case MessageBoxExEnum.Warning:
                    message.icon.Text = "\xE814";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 153, 51));
                    break;
                case MessageBoxExEnum.Information:
                    message.icon.Text = "\xF167";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 1, 119, 215));
                    break;
                case MessageBoxExEnum.Question:
                    message.icon.Text = "\xE9CE";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 1, 119, 215));
                    break;
                case MessageBoxExEnum.Erro:
                    message.icon.Text = "\xEB90";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 246, 83, 20));
                    break;
                case MessageBoxExEnum.Stop:
                    message.icon.Text = "\xF140";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 235, 60, 0));
                    break;
                case MessageBoxExEnum.Wating:
                    message.icon.Text = "\xEC32";
                    message.icon.Foreground = new SolidColorBrush(Color.FromArgb(255, 165, 103, 63));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 设置选项
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddOptions(string name, object value) => message.AddButton(name, value);

        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(double width = 320, double height = 200)
        {
            message.Width = width;
            message.Height = height;
        }


        public async Task<object> ShowDialog()
        {
            var task = Task<object>.Factory.StartNew(() =>
             {
                 message.Dispatcher.Invoke(() =>
                 {
                     _ = message.ShowDialog();
                 });
                 return message.Value;
             });
            return await task;
        }

        public void Show()
        {
            message.Show();
        }

    }

    public enum MessageBoxExEnum
    {
        None,
        OK,
        Warning,
        Information,
        Question,
        Erro,
        Stop,
        Wating,
    }
}
