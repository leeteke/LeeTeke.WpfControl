using LeeTeke.WpfControl.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shell;

namespace LeeTeke.WpfControl.Dependencies
{
    public class WindowShaowManager
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
                    var noCor = (CornerRadius)window.GetValue(WindowShaowManager.CornerRadiusProperty) == default;

                    if (Environment.OSVersion.Version.Major > 7 && noCor)
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
                        WindowChrome windowChrome = new WindowChrome
                        {
                            ResizeBorderThickness = new Thickness(11),
                            CornerRadius = new CornerRadius(0),
                            CaptionHeight = 0,
                            GlassFrameThickness = new Thickness(1)
                        };
                        window.SetValue(WindowChrome.WindowChromeProperty, windowChrome);
                        window.AllowsTransparency = true;
                        window.Background = null;
                        window.WindowStyle = WindowStyle.None;
                        var shaow = new Border
                        {
                            Name = "shaowborder",
                            Margin=new Thickness(8),
                            Effect = new DropShadowEffect() { BlurRadius = 8, Direction = 0, ShadowDepth = 0, Color = Colors.Black, Opacity = 0.4 }
                        };
                     
                       
                        window.Loaded += (we, ws) =>
                        {
                            ///获取之前的元素
                            var lastContent = window.Content as UIElement;
                            ///断开与之前元素的链接
                            window.Content = null;
                            if (noCor)
                            {
                                shaow.Child = lastContent;
                            }
                            else
                            {
                                var content = new ContentControl();
                                content.Content = lastContent;
                                MultiBinding multiBinding = new MultiBinding()
                                {
                                    Converter = new MultiValueToClipConverter(),
                                    Mode = BindingMode.OneWay
                                };
                                multiBinding.Bindings.Add(new Binding()
                                {
                                    Source = content,
                                    Path = new PropertyPath("ActualWidth")
                                });
                                multiBinding.Bindings.Add(new Binding()
                                {
                                    Source = content,
                                    Path = new PropertyPath("ActualHeight")
                                });
                                multiBinding.Bindings.Add(new Binding()
                                {
                                    Source = window,
                                    Path = new PropertyPath("(0)", new DependencyProperty[] { LeeTeke.WpfControl.Dependencies.WindowShaowManager.CornerRadiusProperty, }),
                                    Mode = BindingMode.OneWay
                                });
                                BindingOperations.SetBinding(content, ContentControl.ClipProperty, multiBinding);
                                shaow.CornerRadius = (CornerRadius)window.GetValue(WindowShaowManager.CornerRadiusProperty);
                                shaow.Child = content;
                            }
                            window.Content = shaow;
                        };
                        window.Activated += (we, ws) =>
                        {
                            ((DropShadowEffect)shaow.Effect).Opacity = 0.4;
                        };
                        window.Deactivated += (we, ws) =>
                        {
                            ((DropShadowEffect)shaow.Effect).Opacity = 0.2;
                        };


                    }

                }
                else
                {
                    if (Environment.OSVersion.Version.Major > 7 && (CornerRadius)window.GetValue(WindowShaowManager.CornerRadiusProperty) == default)
                    {
                        window.ClearValue(WindowChrome.WindowChromeProperty);
                    }
                    else
                    {
                        if (window.IsLoaded && window.Content is Border border && border.Name == "shaowborder")
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


        #region CornerRadius
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(WindowShaowManager));

        #endregion



    }
}
