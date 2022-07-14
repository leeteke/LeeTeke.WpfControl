using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LeeTeke.WPFTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            #region 全局默认属性
            //字体相关设置
          //  TextOptions.TextFormattingModeProperty.OverrideMetadata(typeof(UIElement), new FrameworkPropertyMetadata(TextFormattingMode.Display, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
            //字体相关设置
         //   TextOptions.TextRenderingModeProperty.OverrideMetadata(typeof(UIElement), new FrameworkPropertyMetadata(TextRenderingMode.ClearType, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
            #endregion

            LeeTeke.WpfControl.StaticMethods.SetScrollViewerSlide(true);
            LeeTeke.WpfControl.MessageBoxEx.IsDefaultShowMsgCR = false;

        }
    }
}
