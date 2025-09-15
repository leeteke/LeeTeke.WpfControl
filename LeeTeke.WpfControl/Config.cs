using LeeTeke.WpfControl.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ThemeMode = LeeTeke.WpfControl.Models.ThemeMode;

namespace LeeTeke.WpfControl
{
    public static class Config
    {


        private static bool _initialized = false;
        static Config()
        {


        }


        public static void Initialize()
        {
            if (_initialized)
                return;
            _initialized = true;


            Application.Current.Resources["LeeBrush_SystemTheme"] = SystemParameters.WindowGlassBrush as SolidColorBrush;
            //注意程序关闭要释放
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged; 
            Application.Current.Exit += (_, _) => { SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged; };
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
     
     
            Application.Current.Resources["LeeBrush_SystemTheme"] = SystemParameters.WindowGlassBrush as SolidColorBrush;

            if (UseSystemThemeColor)
            {
                Application.Current.Resources["LeeBrush_Theme"] = SystemParameters.WindowGlassBrush as SolidColorBrush;
            }

            if (UseSystemThemeMode)
            {
                SystemThemeMode();
            }
        }


        #region Property

        private static ThemeMode _themeMode = ThemeMode.Light;
        /// <summary>
        /// 主题模式
        /// </summary>
        public static ThemeMode ThemeMode
        {
            get => _themeMode;
            set
            {
                //如果使用了系统主题模式则不会接收
                if (UseSystemThemeMode)
                    return;
                ChangeThemeMode(value);
            }
        }


        private static bool _openSystemThemeMode = false;

        /// <summary>
        /// 使用系统的主题模式
        /// 当系统主题模式发生更改时会自动出发模式变化
        /// </summary>
        public static bool UseSystemThemeMode
        {
            get => _openSystemThemeMode;
            set
            {
                _openSystemThemeMode = value;
                if (_openSystemThemeMode)
                {
                    SystemThemeMode();
                }
            }
        }


        private static bool _openSystemThemeColor = false;

        /// <summary>
        /// 使用系统的主题模式
        /// 当系统主题模式发生更改时会自动出发模式变化
        /// </summary>
        public static bool UseSystemThemeColor
        {
            get => _openSystemThemeColor;
            set
            {
                _openSystemThemeColor = value;
                if (_openSystemThemeColor)
                {
                    SetThemeColor(SystemParameters.WindowGlassBrush);
                }
                else
                {
                    SetThemeColor();
                }
            }
        }

        /// <summary>
        /// 默认圆角值
        /// </summary>
        public static CornerRadius DefaultCornerRadius
        {
            get => (CornerRadius)Application.Current.Resources["LeeValue_CornerRadius"];
            set => Application.Current.Resources["LeeValue_CornerRadius"] = value;
        }


        /// <summary>
        /// 滚动视图滑动效果开启？
        /// </summary>
        public static bool ScrollViewerSlideEnabled
        {
            get => (bool)Application.Current.Resources["LeeValue_ScrollViewerSlideEnabled"];
            set => Application.Current.Resources["LeeValue_ScrollViewerSlideEnabled"] = value;
        }

        /// <summary>
        /// 滚动视图的滑动效果
        /// </summary>
        public static IEasingFunction ScrollViewerSlideEasingFuncion
        {
            get => (IEasingFunction)Application.Current.Resources["LeeValue_ScrollViewerSlideEasingFuncion"];
            set => Application.Current.Resources["LeeValue_ScrollViewerSlideEasingFuncion"] = value;
        }

        /// <summary>
        /// 滚动视图的滑动时间
        /// </summary>
        public static Duration ScrollViewerSlideDuration
        {
            get => (Duration)Application.Current.Resources["LeeValue_ScrollViewerSlideDuration"];
            set => Application.Current.Resources["LeeValue_ScrollViewerSlideDuration"] = value;
        }


        /// <summary>
        /// MessageBoxEx的窗口圆角
        /// </summary>
        public static CornerRadius? MessageBoxExCornerRadius { get; set; }

        /// <summary>
        /// MessageBoxEx的按钮圆角
        /// </summary>
        public static CornerRadius? MessageBoxExButtonCornerRadius { get; set; }

        public static bool MessageBoxExShowCornerRadius { get; set; } = true;

        #endregion

        /// <summary>
        /// 改变主题色
        /// </summary>
        /// <param name="color">当color为空时则恢复为LeeColor_Theme</param>
        public static void SetThemeColor(Brush? color = null)
        {
            Application.Current.Resources["LeeBrush_Theme"] = color ?? new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Theme"]); ;
        }



        #region Event
        /// <summary>
        /// 颜色模式改变事件
        /// </summary>
        public static event EventHandler<ThemeMode>? ThemeModeChanged;
        #endregion


        #region Private

        /// <summary>
        /// 系统主题改变的模型
        /// </summary>
        private static void SystemThemeMode()
        {
            using var opt = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize\\");
            if (opt != null)
            {
                var optValue = opt.GetValue("AppsUseLightTheme");
                if (optValue != null)
                {
                    ChangeThemeMode(optValue.ToString() == "1" ? ThemeMode.Light : ThemeMode.Dark);
                }

            }
        }

        /// <summary>
        /// 改变颜色模式
        /// </summary>
        /// <param name="isLight"></param>
        private static void ChangeThemeMode(ThemeMode mode)
        {
            _themeMode = mode;
            if (mode == ThemeMode.Light)
            {
                #region Light
                Application.Current.Resources["LeeBrush_Background"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Background"]);
                Application.Current.Resources["LeeBrush_Background2"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Background2"]);
                Application.Current.Resources["LeeBrush_Background3"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Background3"]);
                Application.Current.Resources["LeeBrush_Forground"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Forground"]);
                Application.Current.Resources["LeeBrush_ForgroundActive"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_ForgroundActive"]);
                Application.Current.Resources["LeeBrush_Text"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Text"]);
                Application.Current.Resources["LeeBrush_Mark"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Mark"]);
                Application.Current.Resources["LeeBrush_BorderBrush"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_BorderBrush"]);
                Application.Current.Resources["LeeBrush_BorderBrushMouseOver"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_BorderBrushMouseOver"]);
                Application.Current.Resources["LeeBrush_White"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_White"]);
                Application.Current.Resources["LeeBrush_Black"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Black"]);
                Application.Current.Resources["LeeBrush_Gray50"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray50"]);
                Application.Current.Resources["LeeBrush_Gray100"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray100"]);
                Application.Current.Resources["LeeBrush_Gray200"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray200"]);
                Application.Current.Resources["LeeBrush_Gray300"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray300"]);
                Application.Current.Resources["LeeBrush_Gray400"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray400"]);
                Application.Current.Resources["LeeBrush_Gray500"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray500"]);
                Application.Current.Resources["LeeBrush_Gray600"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray600"]);
                Application.Current.Resources["LeeBrush_Gray900"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray900"]);
                Application.Current.Resources["LeeBrush_Gray950"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Light_Gray950"]);
                #endregion
            }
            else
            {

                #region Dak
                Application.Current.Resources["LeeBrush_Background"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Background"]);
                Application.Current.Resources["LeeBrush_Background2"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Background2"]);
                Application.Current.Resources["LeeBrush_Background3"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Background3"]);
                Application.Current.Resources["LeeBrush_Forground"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Forground"]);
                Application.Current.Resources["LeeBrush_ForgroundActive"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_ForgroundActive"]);
                Application.Current.Resources["LeeBrush_Text"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Text"]);
                Application.Current.Resources["LeeBrush_Mark"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Mark"]);
                Application.Current.Resources["LeeBrush_BorderBrush"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_BorderBrush"]);
                Application.Current.Resources["LeeBrush_BorderBrushMouseOver"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_BorderBrushMouseOver"]);
                Application.Current.Resources["LeeBrush_White"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_White"]);
                Application.Current.Resources["LeeBrush_Black"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Black"]);
                Application.Current.Resources["LeeBrush_Gray50"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray50"]);
                Application.Current.Resources["LeeBrush_Gray100"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray100"]);
                Application.Current.Resources["LeeBrush_Gray200"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray200"]);
                Application.Current.Resources["LeeBrush_Gray300"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray300"]);
                Application.Current.Resources["LeeBrush_Gray400"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray400"]);
                Application.Current.Resources["LeeBrush_Gray500"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray500"]);
                Application.Current.Resources["LeeBrush_Gray600"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray600"]);
                Application.Current.Resources["LeeBrush_Gray900"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray900"]);
                Application.Current.Resources["LeeBrush_Gray950"] = new SolidColorBrush((Color)Application.Current.Resources["LeeColor_Dark_Gray950"]);
                #endregion
            }

            ThemeModeChanged?.Invoke(null, mode);
        }


        #endregion


    }
}
