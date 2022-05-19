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
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public object Content { get; set; }
        /// <summary>
        /// 控制关闭
        /// </summary>
        public bool BlockClose { get; set; }

        /// <summary>
        /// 内容大小
        /// </summary>
        public Size ContentSize { get; set; }

        /// <summary>
        /// 关闭Panle
        /// </summary>
        public Action ClosePanel { get; internal set; }
        /// <summary>
        /// 关闭回调
        /// 如果时通过ClosePanel则无回调
        /// 如果返回true则代表正常关闭
        /// 否则代表非正常关闭。例如当前显示委托关闭
        /// </summary>
        public Action<bool> CloseCallback { get; set; }

        /// <summary>
        /// 非公开属性
        /// </summary>
        internal bool IsActiveClose { get; set; }

    }
}
