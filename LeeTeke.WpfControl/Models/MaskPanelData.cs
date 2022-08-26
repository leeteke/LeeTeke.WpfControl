using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class MaskPanelData
    {
        public string? Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public object? Content { get; set; }
        /// <summary>
        /// 控制关闭
        /// </summary>
        public bool BlockClose { get; set; }

        /// <summary>
        /// 内容大小
        /// </summary>
        public Size ContentSize { get; set; } = Size.Empty;

        /// <summary>
        /// 关闭回调
        /// 如果时通过ClosePanel则无回调
        /// 如果返回true则代表正常关闭
        /// 否则代表非正常关闭
        /// </summary>
        public Action<MaskPanelCloseStatus>? CloseCallback { internal get; set; }


        /// <summary>
        /// 视图显示动作
        /// 用来资源加载调整的
        /// true为视图显示完毕后，泛指开始动画显示后的
        /// false为视图不显示前，泛指结束动画之前的
        /// </summary>
        public Action <bool>? ViewDisplayAction { internal get; set; }

        /// <summary>
        /// 非公开属性
        /// </summary>
        internal bool IsActiveClose { get; set; }

        /// <summary>
        /// 关闭Panle
        /// </summary>
        internal Action? ClosePanel { get; set; }

        public void Close()
        {
            ClosePanel?.Invoke();
        }

    }
}
