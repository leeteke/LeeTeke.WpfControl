using System;
using System.Collections.Generic;
using System.Text;

namespace LeeTeke.WpfControl
{
    public class BalloonTipModel
    {
        /// <summary>
        /// 毫秒
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = null!;
        /// <summary>
        /// Icon
        /// </summary>
        public ToolTipIcon Icon { get; set; }

    }
}
