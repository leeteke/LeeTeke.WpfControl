using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LeeTeke.WpfControl
{
    public class StaticMethods
    {
        /// <summary>
        /// 改变主题色
        /// </summary>
        /// <param name="color"></param>
        public static void SetThemeColor(Brush color)
        {
            Application.Current.Resources["LeeBrush_Main"] = color;
        }

        /// <summary>
        /// 改变主题色
        /// </summary>
        /// <param name="color"></param>
        public static void SetThemeColorDefault()
        {
            Application.Current.Resources["LeeBrush_Main"] = new SolidColorBrush(Color.FromRgb(1, 119, 25));
        }
        /// <summary>
        /// 改变默认文字颜色
        /// </summary>
        /// <param name="color"></param>
        public static void SetDefaultTextColor(Color color)
        {

            Application.Current.Resources["LeeBrush_TextColor"] = new SolidColorBrush(color);

        }

        /// <summary>
        /// 改变默认文字颜色
        /// </summary>
        /// <param name="color"></param>
        public static void SetDefaultTextColorDefault()
        {

            Application.Current.Resources["LeeBrush_TextColor"] = new SolidColorBrush(Color.FromRgb(51, 51, 51));

        }

        /// <summary>
        /// 改变页面背景色
        /// </summary>
        /// <param name="color"></param>
        public static void SetPageBackground(Brush color)
        {
            Application.Current.Resources["LeePage_Background"] = color;
        }

        public static void SetPageBackgroundDefault()
        {
            Application.Current.Resources["LeePage_Background"] = new SolidColorBrush(Colors.White);
        }


        public static void SetFocusVisual(Style focusVisual)
        {
            if (_defousVisual == null)
            {
                _defousVisual = Application.Current.Resources["LeeFocusVisual"] as Style;
            }
            Application.Current.Resources["LeeFocusVisual"] = focusVisual;

        }
        private static Style _defousVisual;
        public static void SetFocusVisualDefault()
        {
            Application.Current.Resources["LeeFocusVisual"] = _defousVisual;
        }

        /// <summary>
        /// 设置默认的阴影效果
        /// </summary>
        /// <param name="dropShadow"></param>
        public static void SetDefaultDropShadowEffect(DropShadowEffect dropShadow)
        {
            Application.Current.Resources["LeeShadow"] = dropShadow;
        }

        /// <summary>
        /// 颜色加深或者变浅
        /// 目前只支持 SolidColorBrush
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static Brush ChangeBrushDepth(Brush brush, float depth)
        {
            return brush switch
            {
                SolidColorBrush scbrush => scbrush.Color == Colors.Transparent ? (depth > 0 ? new SolidColorBrush(Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255)) : new SolidColorBrush(Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0))) : new SolidColorBrush(StaticMethods.ChangeColorDepth(scbrush.Color, depth)),
                _ => depth > 0 ? new SolidColorBrush(Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255)) : new SolidColorBrush(Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0)),
            };
        }


        /// <summary>
        /// 颜色加深或者变浅
        /// </summary>
        /// <param name="color"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static Color ChangeColorDepth(Color color, float depth)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;
            if (depth < 0)
            {
                depth = 1 + depth;
                red *= depth;
                green *= depth;
                blue *= depth;
            }
            else
            {
                red = (255 - red) * depth + red;
                green = (255 - green) * depth + green;
                blue = (255 - blue) * depth + blue;
            }
            if (red < 0) red = 0;
            if (red > 255) red = 255;
            if (green < 0) green = 0;
            if (green > 255) green = 255;
            if (blue < 0) blue = 0;
            if (blue > 255) blue = 255;
            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }

        /// <summary>
        /// 获取Clip (圆角矩形，圆形) 带board的
        /// https://wpfspark.wordpress.com/2011/06/08/clipborder-a-wpf-border-that-clips/
        /// </summary>
        /// <param name="baseRect"></param>
        /// <param name="borderThickness"></param>
        /// <param name="cornerRadius"></param>
        /// <returns></returns>
        public static Geometry GetRoundRectangle(Rect baseRect, Thickness borderThickness, CornerRadius cornerRadius)
        {
            // Normalizing the corner radius
            if (cornerRadius.TopLeft < double.Epsilon)
            {
                cornerRadius.TopLeft = 0.0;
            }

            if (cornerRadius.TopRight < double.Epsilon)
            {
                cornerRadius.TopRight = 0.0;
            }

            if (cornerRadius.BottomLeft < double.Epsilon)
            {
                cornerRadius.BottomLeft = 0.0;
            }

            if (cornerRadius.BottomRight < double.Epsilon)
            {
                cornerRadius.BottomRight = 0.0;
            }

            // Taking the border thickness into account
            var leftHalf = borderThickness.Left * 0.5;
            if (leftHalf < double.Epsilon)
            {
                leftHalf = 0.0;
            }

            var topHalf = borderThickness.Top * 0.5;
            if (topHalf < double.Epsilon)
            {
                topHalf = 0.0;
            }

            var rightHalf = borderThickness.Right * 0.5;
            if (rightHalf < double.Epsilon)
            {
                rightHalf = 0.0;
            }

            var bottomHalf = borderThickness.Bottom * 0.5;
            if (bottomHalf < double.Epsilon)
            {
                bottomHalf = 0.0;
            }

            // Create the rectangles for the corners that needs to be curved in the base rectangle 
            // TopLeft Rectangle 
            var topLeftRect = new Rect(
                baseRect.Location.X,
                baseRect.Location.Y,
                Math.Max(0.0, cornerRadius.TopLeft - leftHalf),
                Math.Max(0.0, cornerRadius.TopLeft - rightHalf));

            // TopRight Rectangle 
            var topRightRect = new Rect(
                baseRect.Location.X + baseRect.Width - cornerRadius.TopRight + rightHalf,
                baseRect.Location.Y,
                Math.Max(0.0, cornerRadius.TopRight - rightHalf),
                Math.Max(0.0, cornerRadius.TopRight - topHalf));

            // BottomRight Rectangle
            var bottomRightRect = new Rect(
                baseRect.Location.X + baseRect.Width - cornerRadius.BottomRight + rightHalf,
                baseRect.Location.Y + baseRect.Height - cornerRadius.BottomRight + bottomHalf,
                Math.Max(0.0, cornerRadius.BottomRight - rightHalf),
                Math.Max(0.0, cornerRadius.BottomRight - bottomHalf));

            // BottomLeft Rectangle 
            var bottomLeftRect = new Rect(
                baseRect.Location.X,
                baseRect.Location.Y + baseRect.Height - cornerRadius.BottomLeft + bottomHalf,
                Math.Max(0.0, cornerRadius.BottomLeft - leftHalf),
                Math.Max(0.0, cornerRadius.BottomLeft - bottomHalf));

            // Adjust the width of the TopLeft and TopRight rectangles so that they are proportional to the width of the baseRect 
            if (topLeftRect.Right > topRightRect.Left)
            {
                var newWidth = (topLeftRect.Width / (topLeftRect.Width + topRightRect.Width)) * baseRect.Width;
                topLeftRect = new Rect(topLeftRect.Location.X, topLeftRect.Location.Y, newWidth, topLeftRect.Height);
                topRightRect = new Rect(
                    baseRect.Left + newWidth,
                    topRightRect.Location.Y,
                    Math.Max(0.0, baseRect.Width - newWidth),
                    topRightRect.Height);
            }

            // Adjust the height of the TopRight and BottomRight rectangles so that they are proportional to the height of the baseRect
            if (topRightRect.Bottom > bottomRightRect.Top)
            {
                var newHeight = (topRightRect.Height / (topRightRect.Height + bottomRightRect.Height)) * baseRect.Height;
                topRightRect = new Rect(topRightRect.Location.X, topRightRect.Location.Y, topRightRect.Width, newHeight);
                bottomRightRect = new Rect(
                    bottomRightRect.Location.X,
                    baseRect.Top + newHeight,
                    bottomRightRect.Width,
                    Math.Max(0.0, baseRect.Height - newHeight));
            }

            // Adjust the width of the BottomLeft and BottomRight rectangles so that they are proportional to the width of the baseRect
            if (bottomRightRect.Left < bottomLeftRect.Right)
            {
                var newWidth = (bottomLeftRect.Width / (bottomLeftRect.Width + bottomRightRect.Width)) * baseRect.Width;
                bottomLeftRect = new Rect(bottomLeftRect.Location.X, bottomLeftRect.Location.Y, newWidth, bottomLeftRect.Height);
                bottomRightRect = new Rect(
                    baseRect.Left + newWidth,
                    bottomRightRect.Location.Y,
                    Math.Max(0.0, baseRect.Width - newWidth),
                    bottomRightRect.Height);
            }

            // Adjust the height of the TopLeft and BottomLeft rectangles so that they are proportional to the height of the baseRect
            if (bottomLeftRect.Top < topLeftRect.Bottom)
            {
                var newHeight = (topLeftRect.Height / (topLeftRect.Height + bottomLeftRect.Height)) * baseRect.Height;
                topLeftRect = new Rect(topLeftRect.Location.X, topLeftRect.Location.Y, topLeftRect.Width, newHeight);
                bottomLeftRect = new Rect(
                    bottomLeftRect.Location.X,
                    baseRect.Top + newHeight,
                    bottomLeftRect.Width,
                    Math.Max(0.0, baseRect.Height - newHeight));
            }

            var roundedRectGeometry = new StreamGeometry();

            using (var context = roundedRectGeometry.Open())
            {
                // Begin from the Bottom of the TopLeft Arc and proceed clockwise
                context.BeginFigure(topLeftRect.BottomLeft, true, true);

                // TopLeft Arc
                context.ArcTo(topLeftRect.TopRight, topLeftRect.Size, 0, false, SweepDirection.Clockwise, true, true);

                // Top Line
                context.LineTo(topRightRect.TopLeft, true, true);

                // TopRight Arc
                context.ArcTo(topRightRect.BottomRight, topRightRect.Size, 0, false, SweepDirection.Clockwise, true, true);

                // Right Line
                context.LineTo(bottomRightRect.TopRight, true, true);

                // BottomRight Arc
                context.ArcTo(bottomRightRect.BottomLeft, bottomRightRect.Size, 0, false, SweepDirection.Clockwise, true, true);

                // Bottom Line
                context.LineTo(bottomLeftRect.BottomRight, true, true);

                // BottomLeft Arc
                context.ArcTo(bottomLeftRect.TopLeft, bottomLeftRect.Size, 0, false, SweepDirection.Clockwise, true, true);
            }

            return roundedRectGeometry;
        }

        /// <summary>
        /// 获取Clip (圆角矩形，圆形) 
        /// https://wpfspark.wordpress.com/2011/06/08/clipborder-a-wpf-border-that-clips/
        /// </summary>
        /// <param name="baseRect"></param>
        /// <param name="borderThickness"></param>
        /// <param name="cornerRadius"></param>
        /// <returns></returns>
        public static Geometry GetRoundRectangle(Rect baseRect, CornerRadius cornerRadius)
        {
            // Normalizing the corner radius
            if (cornerRadius.TopLeft < double.Epsilon)
            {
                cornerRadius.TopLeft = 0.0;
            }

            if (cornerRadius.TopRight < double.Epsilon)
            {
                cornerRadius.TopRight = 0.0;
            }

            if (cornerRadius.BottomLeft < double.Epsilon)
            {
                cornerRadius.BottomLeft = 0.0;
            }

            if (cornerRadius.BottomRight < double.Epsilon)
            {
                cornerRadius.BottomRight = 0.0;
            }

            // Taking the border thickness into account
            //var leftHalf = borderThickness.Left * 0.5;
            //if (leftHalf < double.Epsilon)
            //{
            //    leftHalf = 0.0;
            //}

            //var topHalf = borderThickness.Top * 0.5;
            //if (topHalf < double.Epsilon)
            //{
            //    topHalf = 0.0;
            //}

            //var rightHalf = borderThickness.Right * 0.5;
            //if (rightHalf < double.Epsilon)
            //{
            //    rightHalf = 0.0;
            //}

            //var bottomHalf = borderThickness.Bottom * 0.5;
            //if (bottomHalf < double.Epsilon)
            //{
            //    bottomHalf = 0.0;
            //}

            // Create the rectangles for the corners that needs to be curved in the base rectangle 
            // TopLeft Rectangle 
            var topLeftRect = new Rect(
                baseRect.Location.X,
                baseRect.Location.Y,
                Math.Max(0.0, cornerRadius.TopLeft),
                Math.Max(0.0, cornerRadius.TopLeft));

            // TopRight Rectangle 
            var topRightRect = new Rect(
                baseRect.Location.X + baseRect.Width - cornerRadius.TopRight,
                baseRect.Location.Y,
                Math.Max(0.0, cornerRadius.TopRight),
                Math.Max(0.0, cornerRadius.TopRight));

            // BottomRight Rectangle
            var bottomRightRect = new Rect(
                baseRect.Location.X + baseRect.Width - cornerRadius.BottomRight,
                baseRect.Location.Y + baseRect.Height - cornerRadius.BottomRight,
                Math.Max(0.0, cornerRadius.BottomRight),
                Math.Max(0.0, cornerRadius.BottomRight));

            // BottomLeft Rectangle 
            var bottomLeftRect = new Rect(
                baseRect.Location.X,
                baseRect.Location.Y + baseRect.Height - cornerRadius.BottomLeft,
                Math.Max(0.0, cornerRadius.BottomLeft),
                Math.Max(0.0, cornerRadius.BottomLeft));

            // Adjust the width of the TopLeft and TopRight rectangles so that they are proportional to the width of the baseRect 
            if (topLeftRect.Right > topRightRect.Left)
            {
                var newWidth = (topLeftRect.Width / (topLeftRect.Width + topRightRect.Width)) * baseRect.Width;
                topLeftRect = new Rect(topLeftRect.Location.X, topLeftRect.Location.Y, newWidth, topLeftRect.Height);
                topRightRect = new Rect(
                    baseRect.Left + newWidth,
                    topRightRect.Location.Y,
                    Math.Max(0.0, baseRect.Width - newWidth),
                    topRightRect.Height);
            }

            // Adjust the height of the TopRight and BottomRight rectangles so that they are proportional to the height of the baseRect
            if (topRightRect.Bottom > bottomRightRect.Top)
            {
                var newHeight = (topRightRect.Height / (topRightRect.Height + bottomRightRect.Height)) * baseRect.Height;
                topRightRect = new Rect(topRightRect.Location.X, topRightRect.Location.Y, topRightRect.Width, newHeight);
                bottomRightRect = new Rect(
                    bottomRightRect.Location.X,
                    baseRect.Top + newHeight,
                    bottomRightRect.Width,
                    Math.Max(0.0, baseRect.Height - newHeight));
            }

            // Adjust the width of the BottomLeft and BottomRight rectangles so that they are proportional to the width of the baseRect
            if (bottomRightRect.Left < bottomLeftRect.Right)
            {
                var newWidth = (bottomLeftRect.Width / (bottomLeftRect.Width + bottomRightRect.Width)) * baseRect.Width;
                bottomLeftRect = new Rect(bottomLeftRect.Location.X, bottomLeftRect.Location.Y, newWidth, bottomLeftRect.Height);
                bottomRightRect = new Rect(
                    baseRect.Left + newWidth,
                    bottomRightRect.Location.Y,
                    Math.Max(0.0, baseRect.Width - newWidth),
                    bottomRightRect.Height);
            }

            // Adjust the height of the TopLeft and BottomLeft rectangles so that they are proportional to the height of the baseRect
            if (bottomLeftRect.Top < topLeftRect.Bottom)
            {
                var newHeight = (topLeftRect.Height / (topLeftRect.Height + bottomLeftRect.Height)) * baseRect.Height;
                topLeftRect = new Rect(topLeftRect.Location.X, topLeftRect.Location.Y, topLeftRect.Width, newHeight);
                bottomLeftRect = new Rect(
                    bottomLeftRect.Location.X,
                    baseRect.Top + newHeight,
                    bottomLeftRect.Width,
                    Math.Max(0.0, baseRect.Height - newHeight));
            }

            var roundedRectGeometry = new StreamGeometry();

            using (var context = roundedRectGeometry.Open())
            {
                // Begin from the Bottom of the TopLeft Arc and proceed clockwise
                context.BeginFigure(topLeftRect.BottomLeft, true, true);

                // TopLeft Arc
                context.ArcTo(topLeftRect.TopRight, topLeftRect.Size, 0, false, SweepDirection.Clockwise, true, true);

                // Top Line
                context.LineTo(topRightRect.TopLeft, true, true);

                // TopRight Arc
                context.ArcTo(topRightRect.BottomRight, topRightRect.Size, 0, false, SweepDirection.Clockwise, true, true);

                // Right Line
                context.LineTo(bottomRightRect.TopRight, true, true);

                // BottomRight Arc
                context.ArcTo(bottomRightRect.BottomLeft, bottomRightRect.Size, 0, false, SweepDirection.Clockwise, true, true);

                // Bottom Line
                context.LineTo(bottomLeftRect.BottomRight, true, true);

                // BottomLeft Arc
                context.ArcTo(bottomLeftRect.TopLeft, bottomLeftRect.Size, 0, false, SweepDirection.Clockwise, true, true);
            }

            return roundedRectGeometry;
        }


        /// <summary>
        /// ImageSource转换成Bitmap
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        // ImageSource --> Bitmap
        public static System.Drawing.Bitmap ImageSourceToBitmap(ImageSource imageSource)
        {
            try
            {
                var m = (System.Windows.Media.Imaging.BitmapSource)imageSource;

                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(m.PixelWidth, m.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb); // 坑点：选Format32bppRgb将不带透明度

                System.Drawing.Imaging.BitmapData data = bmp.LockBits(
                new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                m.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
                bmp.UnlockBits(data);
                return bmp;

            }
            catch (Exception)
            {

                return null;
            }
        }



        /// <summary>
        /// 寻找子控件
        /// </summary>
        /// <typeparam name="childItem"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        /// <summary>
        /// 是否在控件里面
        /// </summary>
        /// <param name="pardent"></param>
        /// <param name="child"></param>
        /// <param name="outLevel"></param>
        /// <returns></returns>
        public static bool IsInControl(DependencyObject pardent, DependencyObject child, int outLevel = 1)
        {
            var _p = VisualTreeHelper.GetParent(child);
            if (_p == null)
            {
                return false;
            }

            if (_p == pardent)
            {
                return true;
            }
            else
            {
                if (_p.GetType() == pardent.GetType())
                {
                    if (outLevel < 2)
                    {
                        return false;
                    }
                    else
                    {
                        return IsInControl(pardent, _p, outLevel - 1);
                    }
                }
                else
                {
                    return IsInControl(pardent, _p, outLevel);
                }

            }
        }


        internal static CornerRadius MessageBoxExBtnCR { get; private set; } = default;
        /// <summary>
        /// 设置MessageBox全局按钮
        /// </summary>
        /// <param name="cornerRadius"></param>
        public static void SetMessageboxExButtonCornerRadius(CornerRadius cornerRadius) => MessageBoxExBtnCR = cornerRadius;

        internal static CornerRadius MessageBoxExCR { get; private set; } = default;

        /// <summary>
        /// 设置MessageBox全局CR
        /// </summary>
        /// <param name="cornerRadius"></param>
        public static void SetMessageboxExCornerRadius(CornerRadius cornerRadius) => MessageBoxExCR = cornerRadius;

        ///建议使用此方法
        [DllImport("psapi.dll")]
        private static extern bool EmptyWorkingSet(IntPtr hProcess);
        /// <summary>      
        /// 释放内存      
        /// </summary>      
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                EmptyWorkingSet(System.Diagnostics.Process.GetCurrentProcess().Handle);
            }
        }

        /// <summary>      
        /// 优化其它程序内存内存      
        /// </summary>      
        public static void ClearMemory(IntPtr handel)
        {
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    EmptyWorkingSet(handel);
                }
            }
            catch
            {
            }
        }
    }
}
