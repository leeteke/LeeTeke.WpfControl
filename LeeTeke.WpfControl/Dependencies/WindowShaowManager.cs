using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shell;

namespace LeeTeke.WpfControl.Dependencies
{
    public class WindowShaowManager : DependencyObject
    {

        #region Open
        /// <summary>
        /// 开关
        /// </summary>
        public static bool GetOpen(DependencyObject obj)
        {
            return (bool)obj.GetValue(OpenProperty);
        }

        public static void SetOpen(DependencyObject obj, bool value)
        {
            obj.SetValue(OpenProperty, value);
        }

        // Using a DependencyProperty as the backing store for Open.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenProperty =
            DependencyProperty.RegisterAttached("Open", typeof(bool), typeof(WindowShaowManager), new PropertyMetadata(false, new PropertyChangedCallback(OpenChanged)));

        private static void OpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue != e.OldValue && e.NewValue != null)
            {
                if ((bool)e.NewValue)
                {
                    if (Environment.OSVersion.Version.Major > 7 && !(bool)window.GetValue(WindowShaowManager.UsingCornerRadiusProperty))
                    {
                        WindowChrome windowChrome = new WindowChrome
                        {
                            ResizeBorderThickness = new Thickness(3),
                            CornerRadius = new CornerRadius(0),
                            CaptionHeight = 0,
                            GlassFrameThickness = new Thickness(1)
                        };
                        window.SetValue(WindowChrome.WindowChromeProperty, windowChrome);
                    }
                    else
                    {
                        window.Background = null;
                        window.AllowsTransparency = true;
                        window.WindowStyle = WindowStyle.None;
                        window.Loaded += (we, ws) =>
                        {
                            var border = new Border
                            {
                                Name="shaowborder",
                                Margin = new Thickness(5),
                                Effect = new DropShadowEffect() { BlurRadius = 3, Direction = 0, ShadowDepth = 0, Color = (Color)ColorConverter.ConvertFromString("#7F1B1B1B") }
                            };
                            var last = window.Content as UIElement;
                            window.Content = null;
                            border.Child = last;
                            window.Content = border;
                        };

                    }
                   
                }
                else
                {
                    if (Environment.OSVersion.Version.Major > 7 && !(bool)window.GetValue(WindowShaowManager.UsingCornerRadiusProperty))
                    {
                        window.ClearValue(WindowChrome.WindowChromeProperty);
                    }
                    else
                    {
                        if (window.IsLoaded&&window.Content is Border border&&border.Name== "shaowborder")
                        {
                            var last = border.Child;
                            window.Content = null;
                            window.Content = last;
                        }
                    }
                }

            }
        }
        #endregion

        #region UsingCornerRadius
        /// <summary>
        /// 是否使用圆角
        /// </summary>
        public static bool GetUsingCornerRadius(DependencyObject obj)
        {
            return (bool)obj.GetValue(UsingCornerRadiusProperty);
        }

        public static void SetUsingCornerRadius(DependencyObject obj, bool value)
        {
            obj.SetValue(UsingCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for UsingCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsingCornerRadiusProperty =
            DependencyProperty.RegisterAttached("UsingCornerRadius", typeof(bool), typeof(WindowShaowManager));
        #endregion

    }
}
