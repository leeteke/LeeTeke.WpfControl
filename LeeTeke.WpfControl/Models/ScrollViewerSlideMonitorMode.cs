using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeeTeke.WpfControl
{
    public enum ScrollViewerSlideMonitorMode
    {
        Auto,
        /// <summary>
        /// 两者都监听
        /// </summary>
        Both,
        /// <summary>
        /// 只管理
        /// </summary>
        OnlyHorizontal,
        /// <summary>
        /// 
        /// </summary>
        OnlyVertical,
    }
}
