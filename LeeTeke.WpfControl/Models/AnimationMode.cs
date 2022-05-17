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
    /// FromTop | FromBottom| ToTop | ToBottom
    /// FromLeft| FromRingt | ToLeft | ToRight
    /// Expansion | Shrink
    /// Grade 由大到小忽略
    /// </summary>
    [Flags]
    public enum AnimationMode
    {
        /// <summary>
        /// 渐显
        /// </summary>
        Fade=1,
        /// <summary>
        /// 渐隐
        /// </summary>
        FadeOut=2,
        /// <summary>
        /// 从上部开始
        /// </summary>
        FromTop=4,
        /// <summary>
        /// 去往上部
        /// </summary>
        ToTop=8,
        /// <summary>
        /// 从下部过来
        /// </summary>
        FromBottom=16,
        /// <summary>
        /// 去往下部
        /// </summary>
        ToBottom=32,
        /// <summary>
        /// 从左边过来
        /// </summary>
        FromLeft=64,
        /// <summary>
        /// 去往左边
        /// </summary>
        ToLeft=128,
        /// <summary>
        /// 从左边过来
        /// </summary>
        FromRight=256,
        /// <summary>
        /// 去网右边
        /// </summary>
        ToRight=512,
           
        /// <summary>
        /// 展开
        /// </summary>
        Expansion =1024,

        /// <summary>
        /// 缩放
        /// </summary>
        Shrink=2048,

        /// <summary>
        /// 所有效果从0%开始
        /// </summary>
        Grade_0=4096,
        /// <summary>
        /// 所有效果从10%开始
        /// </summary>
        Grade_10 =8192,
        /// <summary>
        /// 所有效果从20%开始
        /// </summary>
        Grade_20 =16384,
        /// <summary>
        /// 所有效果从30%开始
        /// </summary>
        Grade_30= 32768,
        /// <summary>
        /// 所有效果从400%开始
        /// </summary>
        Grade_40= 65536,
        /// <summary>
        /// 所有效果50%开始
        /// </summary>
        Grade_50 = 131072,
        /// <summary>
        /// 所有效果从60%开始
        /// </summary>
        Grade_60 = 262144,
        /// <summary>
        /// 所有效果从70%开始
        /// </summary>
        Grade_70 = 32768,
        /// <summary>
        /// 所有效果从80%开始
        /// </summary>
        Grade_80 = 524288,
        /// <summary>
        /// 所有效果从90%开始
        /// </summary>
        Grade_90 = 1048576,

        /// <summary>
        /// 效果不包含Fade
        /// </summary>
        Grabe_NoFade= 2097152,
    }
}
