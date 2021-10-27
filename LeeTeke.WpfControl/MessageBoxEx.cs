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
        public static bool IsDefaultShowMsgCR = true;
        /// <summary>
        /// 显示并返回
        /// </summary>
        /// <param name="content"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool Show(string content, MessageStatus status = MessageStatus.None)
        {
            IMessageWin message;
            if (IsDefaultShowMsgCR)
            {
                message=new CRMessage();
            }
            else
            {
                message=new Message();
            }
            message.Content = content; 
            message.Status = status;
            switch (status)
            {
                case MessageStatus.None:
                    break;
                case MessageStatus.Question:
                    message.AddOptions("是", true);
                    message.AddOptions("否", false);
                    break;
                default:
                    message.AddOptions("确定", true);
                    break;
            }
            _ = message.ShowDialog();
            return message.Value == null ? false : (bool)message.Value;
        }

        public static bool Show(string title, string content, MessageStatus status = MessageStatus.None)
        {
            IMessageWin message;
            if (IsDefaultShowMsgCR)
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
                    message.AddOptions("是", true);
                    message.AddOptions("否", false);
                    break;
                default:
                    message.AddOptions("确定", true);
                    break;
            }

            _ = message.ShowDialog();
            return message.Value == null ? false : (bool)message.Value;
        }

        private IMessageWin _msg;

        public MessageBoxEx(CornerRadius cornerRadius = default)
        {
  
            if (IsDefaultShowMsgCR)
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
        public string Title
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
        public object Content
        {
            get => _msg.Content;
            set => _msg.Content = value;
        }


        
        /// <summary>
        /// 设置lodingVale 范围0-100;
        /// </summary>
        public double ProcessValue
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
        public void AddOptions(string name, object vale, CornerRadius btnCornerRadius = default) => _msg.AddOptions(name, vale, btnCornerRadius);

        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>

        public void SetSize(double width = 320, double height = 200) => _msg.SetSize(width, height);


        public bool? ShowDialog() => _msg.ShowDialog();

        public async Task<object> ShowDialogAsync()
        {

            var task = Task<object>.Factory.StartNew(() =>
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
