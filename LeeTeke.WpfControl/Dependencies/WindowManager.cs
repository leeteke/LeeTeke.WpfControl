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
    public class WindowManager
    {

        #region Effect
        public static Effect GetEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(EffectProperty);
        }

        public static void SetEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(EffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for Effect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EffectProperty =
            DependencyProperty.RegisterAttached("Effect", typeof(Effect), typeof(WindowManager));
        #endregion

        #region BorderBrush
        public static Brush GetBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BorderBrushProperty);
        }

        public static void SetBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(WindowManager));
        #endregion

        #region BorderThickness
        public static Thickness GetBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(BorderThicknessProperty);
        }

        public static void SetBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(BorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for BorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.RegisterAttached("BorderThickness", typeof(Thickness), typeof(WindowManager));
        #endregion


        #region DeactivatedEffect
        public static Effect GetDeactivatedEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(DeactivatedEffectProperty);
        }

        public static void SetDeactivatedEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(DeactivatedEffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for DeactivatedEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeactivatedEffectProperty =
            DependencyProperty.RegisterAttached("DeactivatedEffect", typeof(Effect), typeof(WindowManager));
        #endregion


        #region DeactivatedBorderBrush
        public static Brush GetDeactivatedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(DeactivatedBorderBrushProperty);
        }

        public static void SetDeactivatedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(DeactivatedBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for DeactivatedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeactivatedBorderBrushProperty =
            DependencyProperty.RegisterAttached("DeactivatedBorderBrush", typeof(Brush), typeof(WindowManager));
        #endregion


        #region DeactivatedBorderThickness
        public static Thickness GetDeactivatedBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(DeactivatedBorderThicknessProperty);
        }

        public static void SetDeactivatedBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(DeactivatedBorderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for DeactivatedBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeactivatedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("DeactivatedBorderThickness", typeof(Thickness), typeof(WindowManager));
        #endregion

        #region IsEnabled
        /// <summary>
        /// 开关
        /// </summary>
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for Open.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(WindowManager), new PropertyMetadata(false, new PropertyChangedCallback(OnIsEnabledChanged)));

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Controls.Window && d is Window window && e.NewValue != e.OldValue && e.NewValue != null)
            {
                window.StateChanged -= NoCorWindow_StateChanged;
                window.Loaded -= Window_Loaded;

                window.Activated -= Window_Activated;

                window.Deactivated -= Window_Deactivated;

                window.StateChanged -= Window_StateChanged;
                if ((bool)e.NewValue)
                {
                    var noCor = (CornerRadius)window.GetValue(CornerRadiusManager.CornerRadiusProperty) == default;

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

                        window.StateChanged += NoCorWindow_StateChanged;

                    }
                    else
                    {
                        WindowChrome windowChrome = new WindowChrome
                        {
                            CaptionHeight = 0,
                            GlassFrameThickness = new Thickness(-1),
                        };
                        window.SetValue(WindowChrome.WindowChromeProperty, windowChrome);
                        window.Background = null;
                        window.WindowStyle = WindowStyle.None;
                        window.ResizeMode = ResizeMode.NoResize;

                        window.Loaded += Window_Loaded;

                        window.Activated += Window_Activated;

                        window.Deactivated += Window_Deactivated;

                    }

                }
                else
                {
                    if (Environment.OSVersion.Version.Major > 7 && (CornerRadius)window.GetValue(CornerRadiusManager.CornerRadiusProperty) == default)
                    {
                        window.ClearValue(WindowChrome.WindowChromeProperty);
                    }
                }

            }
        }


        #endregion

        #region 私有

        private static void NoCorWindow_StateChanged(object? sender, EventArgs e)
        {
            if (sender is Window window)
            {
                var border = Helper.FindVisualChild<Border>(window);
                if (border != null)
                {
                    if (window.WindowState == WindowState.Maximized)
                    {

                        border.Margin = new Thickness(7);

                    }
                    if (window.WindowState == WindowState.Normal)
                    {
                        border.Margin = new Thickness(0);
                    }
                }
            }
        }

        private static void Window_StateChanged(object? sender, EventArgs e)
        {
            if (sender is Window window && window.IsLoaded)
            {
                if (window.Content is Grid panel)
                {
                    if (window.WindowState == WindowState.Maximized)
                    {
                        var bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                        var taskbarRect = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                        var mar = new Thickness();
                        if (taskbarRect.X == 0)
                        {
                            mar.Left = 7;
                            mar.Right = bounds.Width - taskbarRect.Width + 7;
                        }
                        else
                        {
                            mar.Left = taskbarRect.X + 7;
                            mar.Right = 7;
                        }
                        if (taskbarRect.Y == 0)
                        {
                            mar.Top = 7;
                            mar.Bottom = bounds.Height - taskbarRect.Height + 7;
                        }
                        else
                        {
                            mar.Top = taskbarRect.Y + 7;
                            mar.Bottom = 7;
                        }
                        panel.Margin = mar;
                    }
                    if (window.WindowState == WindowState.Normal)
                    {
                        panel.Margin = new Thickness(7);
                    }
                }

            }


        }

        private static void Window_Deactivated(object? sender, EventArgs e)
        {
            if (sender is Window window && window.IsLoaded)
            {
                var border = Helper.FindVisualChild<Border>(window.Content as FrameworkElement, "PART_WinBGBorder");
                if (border != null)
                {
                    if (GetDeactivatedEffect(window) != null)
                    {
                        border.SetBinding(Border.EffectProperty, new Binding()
                        {
                            Source = window,
                            Path = new PropertyPath(WindowManager.DeactivatedEffectProperty),
                            Mode = BindingMode.OneWay
                        });
                    }

                    if (GetDeactivatedBorderBrush(window) != null)
                    {
                        border.SetBinding(Border.BorderBrushProperty, new Binding()
                        {
                            Source = window,
                            Path = new PropertyPath(WindowManager.DeactivatedBorderBrushProperty),
                            Mode = BindingMode.OneWay
                        });
                    }

                    if (GetDeactivatedBorderThickness(window) != default)
                    {
                        border.SetBinding(Border.BorderThicknessProperty, new Binding()
                        {
                            Source = window,
                            Path = new PropertyPath(WindowManager.DeactivatedBorderThicknessProperty),
                            Mode = BindingMode.OneWay
                        });
                    }
                }
            }
        }

        private static void Window_Activated(object? sender, EventArgs e)
        {

            if (sender is Window window && window.IsLoaded)
            {
                var border = Helper.FindVisualChild<Border>(window.Content as FrameworkElement, "PART_WinBGBorder");

                if (border != null)
                {
                    border.SetBinding(Border.EffectProperty, new Binding()
                    {
                        Source = window,
                        Path = new PropertyPath(WindowManager.EffectProperty),
                        Mode = BindingMode.OneWay
                    });

                    border.SetBinding(Border.BorderBrushProperty, new Binding()
                    {
                        Source = window,
                        Path = new PropertyPath(WindowManager.BorderBrushProperty),
                        Mode = BindingMode.OneWay
                    });

                    border.SetBinding(Border.BorderThicknessProperty, new Binding()
                    {
                        Source = window,
                        Path = new PropertyPath(WindowManager.BorderThicknessProperty),
                        Mode = BindingMode.OneWay
                    });
                }
            }
        }



        private static void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Window window && window.Content is FrameworkElement element)
            {
                var border = Helper.FindVisualChild<Border>(window);
                if (border != null)
                {
                    BindingOperations.ClearAllBindings(border);
                    border.Background = null;
                    border.BorderThickness = new Thickness(0);
                }

                var bgBorder = new Border() { Name = "PART_WinBGBorder" };
                bgBorder.SetBinding(Border.BorderBrushProperty, new Binding()
                {
                    Source = window,
                    Path = new PropertyPath(Window.BorderBrushProperty),
                    Mode = BindingMode.OneWay
                });

                bgBorder.SetBinding(Border.BorderThicknessProperty, new Binding()
                {
                    Source = window,
                    Path = new PropertyPath(Window.BorderThicknessProperty),
                    Mode = BindingMode.OneWay
                });

                bgBorder.SetBinding(Border.BackgroundProperty, new Binding()
                {
                    Source = window,
                    Path = new PropertyPath(Window.BackgroundProperty),
                    Mode = BindingMode.OneWay
                });

                bgBorder.SetBinding(Border.CornerRadiusProperty, new Binding()
                {
                    Source = window,
                    Path = new PropertyPath(CornerRadiusManager.CornerRadiusProperty),
                    Mode = BindingMode.OneWay
                });

                bgBorder.SetBinding(Border.EffectProperty, new Binding()
                {
                    Source = window,
                    Path = new PropertyPath(WindowManager.EffectProperty),
                    Mode = BindingMode.OneWay
                });

                var contentBorder = new Border() { Background = null, BorderBrush = null };
                contentBorder.SetBinding(Border.BorderThicknessProperty, new Binding()
                {
                    Source = window,
                    Path = new PropertyPath(Window.BorderThicknessProperty),
                    Mode = BindingMode.OneWay
                });

                var bgGrid = new Grid() { Margin = new Thickness(7) };
                bgGrid.Children.Add(bgBorder);
                bgGrid.Children.Add(contentBorder);
                window.Content = null;
                contentBorder.Child = element;
                window.Content = bgGrid;

                if (CornerRadiusManager.GetIsClip(window))
                {
                    MultiBinding multiBinding = new MultiBinding()
                    {
                        Converter = new MultiValueToClipConverter(),
                        Mode = BindingMode.OneWay
                    };
                    multiBinding.Bindings.Add(new Binding()
                    {
                        Source = element,
                        Path = new PropertyPath("ActualWidth")
                    });
                    multiBinding.Bindings.Add(new Binding()
                    {
                        Source = element,
                        Path = new PropertyPath("ActualHeight")
                    });
                    multiBinding.Bindings.Add(new Binding()
                    {
                        Source = bgBorder,
                        Path = new PropertyPath("(0)", new DependencyProperty[] { Border.CornerRadiusProperty, }),
                        Mode = BindingMode.OneWay
                    });
                    multiBinding.Bindings.Add(new Binding()
                    {
                        Source = bgBorder,
                        Path = new PropertyPath(Border.BorderBrushProperty),
                        Mode = BindingMode.OneWay
                    });

                    BindingOperations.SetBinding(element as FrameworkElement, FrameworkElement.ClipProperty, multiBinding);
                }
            }
        }

        #endregion

    }
}


