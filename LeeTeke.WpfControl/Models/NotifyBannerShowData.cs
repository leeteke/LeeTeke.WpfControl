
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace LeeTeke.WpfControl
{
    public class NotifyBannerShowData
    {
        /// <summary>
        /// 值
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public NotifyStatus Status { get; set; }

        /// <summary>
        /// 内容，默认文本可以是FE
        /// </summary>
        public object? Content { get; set; }

        /// <summary>
        /// 背景音，默认没有
        /// </summary>
        public Stream? Sound { get; set; }

        /// <summary>
        /// 持续时间，可为空，毫秒
        /// </summary>
        public Duration? Duration { get; set; }

        public bool CanClick { get; set; }

        public Action? CloseAction { get; internal set; }

        public NotifyBannerShowData(object content, NotifyStatus status = NotifyStatus.Primary)
        {
            Content = content;
            Status = status;
        }

        public NotifyBannerShowData()
        {

        }

    }
}
