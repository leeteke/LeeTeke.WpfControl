using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FontFamily = System.Windows.Media.FontFamily;

namespace LeeTeke.WpfControl.Dependencies
{
    internal class DependencyConst
    {
        /// <summary>
        /// FontSize
        /// 默认字体大小
        /// </summary>
        public const double FontSize = 12.0;

        /// <summary>
        /// FontFamily
        /// 默认字体
        /// </summary>
        public static readonly FontFamily FontFamily = new FontFamily("Microsoft YaHei UI");
    }
}
