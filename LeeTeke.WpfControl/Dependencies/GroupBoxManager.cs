using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl.Dependencies
{
    public class GroupBoxManager 
    {

        #region HeaderPadding
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static Thickness GetHeaderPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HeaderPaddingProperty);
        }

        public static void SetHeaderPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HeaderPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderPaddingProperty =
            DependencyProperty.RegisterAttached("HeaderPadding", typeof(Thickness), typeof(GroupBoxManager));

        #endregion


        #region HeaderForeground
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static Brush GetHeaderForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderForegroundProperty);
        }

        public static void SetHeaderForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.RegisterAttached("HeaderForeground", typeof(Brush), typeof(GroupBoxManager));
        #endregion



        #region HeaderFontWeight
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static FontWeight GetHeaderFontWeight(DependencyObject obj)
        {
            return (FontWeight)obj.GetValue(HeaderFontWeightProperty);
        }

        public static void SetHeaderFontWeight(DependencyObject obj, FontWeight value)
        {
            obj.SetValue(HeaderFontWeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFontWeightProperty =
            DependencyProperty.RegisterAttached("HeaderFontWeight", typeof(FontWeight), typeof(GroupBoxManager));
        #endregion


        #region HeaderFontSize
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static double GetHeaderFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(HeaderFontSizeProperty);
        }

        public static void SetHeaderFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(HeaderFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(GroupBoxManager),new PropertyMetadata(DependencyConst.FontSize));
        #endregion



        #region BorderMode
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static GroupBorderMode GetBorderMode(DependencyObject obj)
        {
            return (GroupBorderMode)obj.GetValue(BorderModeProperty);
        }

        public static void SetBorderMode(DependencyObject obj, GroupBorderMode value)
        {
            obj.SetValue(BorderModeProperty, value);
        }

        // Using a DependencyProperty as the backing store for BorderMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderModeProperty =
            DependencyProperty.RegisterAttached("BorderMode", typeof(GroupBorderMode), typeof(GroupBoxManager));
        #endregion



        #region SplitLineVisibility
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static Visibility GetSplitLineVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(SplitLineVisibilityProperty);
        }

        public static void SetSplitLineVisibility(DependencyObject obj, Visibility value)
        {
            obj.SetValue(SplitLineVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for SplitLineVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitLineVisibilityProperty =
            DependencyProperty.RegisterAttached("SplitLineVisibility", typeof(Visibility), typeof(GroupBoxManager));
        #endregion


        #region SplitLineSize
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static double GetSplitLineSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SplitLineSizeProperty);
        }

        public static void SetSplitLineSize(DependencyObject obj, double value)
        {
            obj.SetValue(SplitLineSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SplitLineSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitLineSizeProperty =
            DependencyProperty.RegisterAttached("SplitLineSize", typeof(double), typeof(GroupBoxManager));
        #endregion


        #region SplitLineBrush
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static Brush GetSplitLineBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SplitLineBrushProperty);
        }

        public static void SetSplitLineBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(SplitLineBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for SplitLineBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitLineBrushProperty =
            DependencyProperty.RegisterAttached("SplitLineBrush", typeof(Brush), typeof(GroupBoxManager));
        #endregion


        #region SplitLineMargin
        /// <summary>
        /// 请填写描述
        /// </summary>
        public static Thickness GetSplitLineMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SplitLineMarginProperty);
        }

        public static void SetSplitLineMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SplitLineMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for SplitLineMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitLineMarginProperty =
            DependencyProperty.RegisterAttached("SplitLineMargin", typeof(Thickness), typeof(GroupBoxManager));
        #endregion




        #region ContentEffect
        public static Effect GetContentEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(ContentEffectProperty);
        }

        public static void SetContentEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(ContentEffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for ContentEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentEffectProperty =
            DependencyProperty.RegisterAttached("ContentEffect", typeof(Effect), typeof(GroupBoxManager));
        #endregion



    }

    public enum GroupBorderMode
    {
        External,
        Internal
    }
}
