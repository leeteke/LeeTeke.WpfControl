﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class EffectManager
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
            DependencyProperty.RegisterAttached("Effect", typeof(Effect), typeof(EffectManager));
        #endregion



        #region Open
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
            DependencyProperty.RegisterAttached("Open", typeof(bool), typeof(EffectManager),new PropertyMetadata(OnOpenChanged));

        private static void OnOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element && e.NewValue is bool @open&& GetEffect(d) is Effect effect)
            {
                element.Effect = @open ? effect : null;
            }
        }
        #endregion

    }
}
