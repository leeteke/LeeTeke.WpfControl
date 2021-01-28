using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace LeeTeke.WpfControl
{
    public class NotifyBannerShowModel
    {
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// 背景色无则使用控件默认
        /// </summary>
        public Brush Background { get; private set; }

        /// <summary>
        /// 内容，默认文本可以是FE
        /// </summary>
        public object Content { get; set; }
        /// <summary>
        /// 背景音，默认没有
        /// </summary>
        public SoundPlayer Sound { get; private set; }

        /// <summary>
        /// 持续时间，可为空，毫秒
        /// </summary>
        public int? Duration { get; private set; } 


        public NotifyBannerShowModel(object content)
        {
            Content = content;
        }

        public NotifyBannerShowModel(object content, Brush background)
        {
            Content = content;
            Background = background;
        }

        public NotifyBannerShowModel(object content, object value)
        {
            Content = content;
            Value = value;
        }

        public NotifyBannerShowModel(object content, object value, Brush background)
        {
            Content = content;
            Value = value;
            Background = background;
        }

        public NotifyBannerShowModel(object content, object value, Brush background, int duration, SoundPlayer sound)
        {
            Content = content;
            Value = value;
            Background = background;
            Sound = sound;
            Duration = duration;
        }

    }
}
