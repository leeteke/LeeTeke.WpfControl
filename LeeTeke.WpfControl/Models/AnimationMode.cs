using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeeTeke.WpfControl
{
    /// <summary>
    /// 当AnimationMode Flags包含已下冲突将忽略后者,其它则随意组合
    /// Fade | FadeOut
    /// BottomToTop | TopToBottom
    /// RightToLeft | LeftToRight
    /// Expansion | Shrink
    /// Grade 由大到小忽略
    /// </summary>
    [Flags]
    public enum AnimationMode
    {
        /// <summary>
        /// 渐显
        /// </summary>
        Fade=0,
        /// <summary>
        /// 渐隐
        /// </summary>
        FadeOut=1,
        /// <summary>
        /// 从下到上
        /// </summary>
        BottomToTop=2,
        /// <summary>
        /// 从上到下
        /// </summary>
        TopToBottom = 4,

        /// <summary>
        /// 从右到左
        /// </summary>
        RightToLeft = 8,

        /// <summary>
        /// 从从到
        /// </summary>
        LeftToRight = 16,

        /// <summary>
        /// 展开
        /// </summary>
        Expansion = 32,

        /// <summary>
        /// 缩放
        /// </summary>
        Shrink = 64,

        /// <summary>
        /// 所有效果从0%开始
        /// </summary>
        Grade_0=128,
        /// <summary>
        /// 所有效果从0%开始
        /// </summary>
        Grade_10 = 256,
        /// <summary>
        /// 所有效果从100%开始
        /// </summary>
        Grade_20 = 512,
        /// <summary>
        /// 所有效果从100%开始
        /// </summary>
        Grade_30 = 1024,
        /// <summary>
        /// 所有效果从100%开始
        /// </summary>
        Grade_40 = 2048,
        /// <summary>
        /// 所有效果从100%开始
        /// </summary>
        Grade_50 = 4096,
        /// <summary>
        /// 所有效果从100%开始
        /// </summary>
        Grade_60 = 8192,
        /// <summary>
        /// 所有效果从100%开始
        /// </summary>
        Grade_70 = 16384,
        /// <summary>
        /// 所有效果从100%开始
        /// </summary>
        Grade_80 = 32768,
        /// <summary>
        /// 所有效果从100%开始
        /// </summary>
        Grade_90 = 65536,
   
    }
}
