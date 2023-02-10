using LeeTeke.WpfControl.Controls;
using LeeTeke.WpfControl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            IMessageWin message;
            if (Config.MessageBoxExShowCornerRadius)
            {
                message = new CRMessage();
            }
            else
            {
                message = new Message();
            }
            message.Content = content;
            message.Status = status;
            switch (status)
            {
                case MessageStatus.None:
                    break;
                case MessageStatus.Question:
                    message.AddOptions(new Button()
                    {
                        Background = Application.Current.Resources["LeeBrush_Theme"] as Brush,
                        Foreground = Application.Current.Resources["LeeBrush_ThemeForeground"] as Brush,
                        BorderThickness = new Thickness(0),
                        Content = "是",
                        MinWidth = 80,
                        Margin = new Thickness(5, 5, 5, 0),
                        Height = 30,
                    }, true);
                    message.AddOptions("否", false);
                    break;
                default:
                    message.AddOptions("确定", null);
                    break;
            }
            _ = message.ShowDialog();
            if (message.Value is bool @value)
            {
                return value;
            }
            else
            {
                return false;
            }
        }

        public static bool Show(string title, string content, MessageStatus status = MessageStatus.None)
        {
            IMessageWin message;
            if (Config.MessageBoxExShowCornerRadius)
            {
                message = new CRMessage();
            }
            else
            {
                message = new Message();
            }
            message.Title = title;
            message.Content = content;
            message.Status = status;
            switch (status)
            {
                case MessageStatus.None:
                    break;

                case MessageStatus.Question:
                    message.AddOptions(new Button()
                    {
                        Background = Application.Current.Resources["LeeBrush_Theme"] as Brush,
                        Foreground = Application.Current.Resources["LeeBrush_ThemeForeground"] as Brush,
                        BorderThickness = new Thickness(0),
                        Content = "是",
                        MinWidth = 80,
                        Margin = new Thickness(5, 5, 5, 0),
                        Height = 30,
                    }, true) ;
                    message.AddOptions("否", false);
                    break;
                default:
                    message.AddOptions("确定", null);
                    break;
            }

            _ = message.ShowDialog();

            if (message.Value is bool @value)
            {
                return value;
            }
            else
            {
                return false;
            }


        }

        private IMessageWin _msg;

        public MessageBoxEx(CornerRadius cornerRadius = default)
        {

            if (Config.MessageBoxExShowCornerRadius)
            {
                _msg = new CRMessage(cornerRadius);
            }
            else
            {
                _msg = new Message();
            }
        }
        /// <summary>
        /// 是否可以关闭
        /// </summary>
        public bool CanClose
        {
            get => _msg.CanClose;
            set => _msg.CanClose = value;
        }

        public bool ShowProcess
        {
            get => _msg.ShowProcess;
            set => _msg.ShowProcess = value;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            CanClose = true;
            _msg.Window.Close();
            GC.Collect();
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title
        {
            get => _msg.Title;
            set => _msg.Title = value;
        }

        public MessageStatus Status
        {
            get => _msg.Status;
            set => _msg.Status = value;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public object? Content
        {
            get => _msg.Content;
            set => _msg.Content = value;
        }



        /// <summary>
        /// 设置loadingVale 范围0-100;
        /// </summary>
        public int ProcessValue
        {
            get => _msg.ProcessValue;
            set => _msg.ProcessValue = value;
        }


        /// <summary>
        /// 设置选项
        /// </summary>
        /// <param name="name"></param>
        /// <param name="vale"></param>
        /// <param name="btnCornerRadius">圆角值</param>
        public void AddOptions(string name, object? value, CornerRadius? btnCornerRadius = default) => _msg.AddOptions(name, value, btnCornerRadius);
        /// <summary>
        /// 设置选项
        /// </summary>
        /// <param name="button"></param>
        /// <param name="value"></param>
        public void AddOptions(Button button, object? value) => _msg.AddOptions(button, value);

        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>

        public void SetSize(double width = 320, double height = 200) => _msg.SetSize(width, height);


        public bool? ShowDialog() => _msg.ShowDialog();

        public async Task<object?> ShowDialogAsync()
        {

            var task = Task<object?>.Factory.StartNew(() =>
             {
                 _msg.Window.Dispatcher.Invoke(() =>
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
        Wating,
    }
}
