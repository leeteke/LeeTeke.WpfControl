using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;

namespace LeeTeke.WpfControl
{
    public static class Helper
    {

        /// <summary>
        /// 颜色加深或者变浅
        /// 目前只支持 SolidColorBrush
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static Brush ChangeBrushDepth(Brush brush, float depth)
        {
            if (brush is SolidColorBrush scbrush)
            {
                return scbrush.Color == Colors.Transparent ? (depth > 0 ? new SolidColorBrush(Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255)) : new SolidColorBrush(Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0))) : new SolidColorBrush(Helper.ChangeColorDepth(scbrush.Color, depth));
            }
            else
            {
                return depth > 0 ? new SolidColorBrush(Color.FromArgb((byte)(255 * depth), (byte)255, (byte)255, (byte)255)) : new SolidColorBrush(Color.FromArgb((byte)(255 * Math.Abs(depth)), (byte)0, (byte)0, (byte)0));
            }
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
        public static Geometry GetRoundRectangle(Rect baseRect, Thickness thickness, CornerRadius cornerRadius)
        {
            // Normalizing the corner radius
            if (cornerRadius.TopLeft < Double.Epsilon)
                cornerRadius.TopLeft = 0.0;
            if (cornerRadius.TopRight < Double.Epsilon)
                cornerRadius.TopRight = 0.0;
            if (cornerRadius.BottomLeft < Double.Epsilon)
                cornerRadius.BottomLeft = 0.0;
            if (cornerRadius.BottomRight < Double.Epsilon)
                cornerRadius.BottomRight = 0.0;

            // Taking the border thickness into account
            double leftHalf = thickness.Left * 0.5;
            if (leftHalf < Double.Epsilon)
                leftHalf = 0.0;
            double topHalf = thickness.Top * 0.5;
            if (topHalf < Double.Epsilon)
                topHalf = 0.0;
            double rightHalf = thickness.Right * 0.5;
            if (rightHalf < Double.Epsilon)
                rightHalf = 0.0;
            double bottomHalf = thickness.Bottom * 0.5;
            if (bottomHalf < Double.Epsilon)
                bottomHalf = 0.0;

            // Create the rectangles for the corners that needs to be curved in the base rectangle 
            // TopLeft Rectangle 
            Rect topLeftRect = new Rect(baseRect.Location.X,
                                        baseRect.Location.Y,
                                        Math.Max(0.0, cornerRadius.TopLeft - leftHalf),
                                        Math.Max(0.0, cornerRadius.TopLeft - rightHalf));
            // TopRight Rectangle 
            Rect topRightRect = new Rect(baseRect.Location.X + baseRect.Width - cornerRadius.TopRight + rightHalf,
                                         baseRect.Location.Y,
                                         Math.Max(0.0, cornerRadius.TopRight - rightHalf),
                                         Math.Max(0.0, cornerRadius.TopRight - topHalf));
            // BottomRight Rectangle
            Rect bottomRightRect = new Rect(baseRect.Location.X + baseRect.Width - cornerRadius.BottomRight + rightHalf,
                                            baseRect.Location.Y + baseRect.Height - cornerRadius.BottomRight + bottomHalf,
                                            Math.Max(0.0, cornerRadius.BottomRight - rightHalf),
                                            Math.Max(0.0, cornerRadius.BottomRight - bottomHalf));
            // BottomLeft Rectangle 
            Rect bottomLeftRect = new Rect(baseRect.Location.X,
                                           baseRect.Location.Y + baseRect.Height - cornerRadius.BottomLeft + bottomHalf,
                                           Math.Max(0.0, cornerRadius.BottomLeft - leftHalf),
                                           Math.Max(0.0, cornerRadius.BottomLeft - bottomHalf));

            // Adjust the width of the TopLeft and TopRight rectangles so that they are proportional to the width of the baseRect 
            if (topLeftRect.Right > topRightRect.Left)
            {
                double newWidth = (topLeftRect.Width / (topLeftRect.Width + topRightRect.Width)) * baseRect.Width;
                topLeftRect = new Rect(topLeftRect.Location.X, topLeftRect.Location.Y, newWidth, topLeftRect.Height);
                topRightRect = new Rect(baseRect.Left + newWidth, topRightRect.Location.Y, Math.Max(0.0, baseRect.Width - newWidth), topRightRect.Height);
            }

            // Adjust the height of the TopRight and BottomRight rectangles so that they are proportional to the height of the baseRect
            if (topRightRect.Bottom > bottomRightRect.Top)
            {
                double newHeight = (topRightRect.Height / (topRightRect.Height + bottomRightRect.Height)) * baseRect.Height;
                topRightRect = new Rect(topRightRect.Location.X, topRightRect.Location.Y, topRightRect.Width, newHeight);
                bottomRightRect = new Rect(bottomRightRect.Location.X, baseRect.Top + newHeight, bottomRightRect.Width, Math.Max(0.0, baseRect.Height - newHeight));
            }

            // Adjust the width of the BottomLeft and BottomRight rectangles so that they are proportional to the width of the baseRect
            if (bottomRightRect.Left < bottomLeftRect.Right)
            {
                double newWidth = (bottomLeftRect.Width / (bottomLeftRect.Width + bottomRightRect.Width)) * baseRect.Width;
                bottomLeftRect = new Rect(bottomLeftRect.Location.X, bottomLeftRect.Location.Y, newWidth, bottomLeftRect.Height);
                bottomRightRect = new Rect(baseRect.Left + newWidth, bottomRightRect.Location.Y, Math.Max(0.0, baseRect.Width - newWidth), bottomRightRect.Height);
            }

            // Adjust the height of the TopLeft and BottomLeft rectangles so that they are proportional to the height of the baseRect
            if (bottomLeftRect.Top < topLeftRect.Bottom)
            {
                double newHeight = (topLeftRect.Height / (topLeftRect.Height + bottomLeftRect.Height)) * baseRect.Height;
                topLeftRect = new Rect(topLeftRect.Location.X, topLeftRect.Location.Y, topLeftRect.Width, newHeight);
                bottomLeftRect = new Rect(bottomLeftRect.Location.X, baseRect.Top + newHeight, bottomLeftRect.Width, Math.Max(0.0, baseRect.Height - newHeight));
            }

            StreamGeometry roundedRectGeometry = new StreamGeometry();

            using (StreamGeometryContext context = roundedRectGeometry.Open())
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
        public static System.Drawing.Bitmap? ImageSourceToBitmap(ImageSource imageSource)
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
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T? FindVisualChild<T>(DependencyObject? obj) where T : DependencyObject
        {
            var childIndex = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childIndex; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is not null and T)
                    return (T)child;
                else
                {
                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        /// <summary>
        /// 寻找子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static (DependencyObject? parent, T? child) FindVisualFistChildAndParent<T>(DependencyObject? obj) where T : DependencyObject
        {
            if (obj == null)
                return (null, null);
            var childIndex = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childIndex; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (obj, child as T);
                else
                {
                    var cp = FindVisualFistChildAndParent<T>(child);
                    if (cp.parent != null)
                        return cp;
                }
            }
            return (null, null);
        }

        /// <summary>
        /// 寻找子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T? FindVisualChild<T>(DependencyObject? obj, string name) where T : DependencyObject
        {
            var childIndex = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childIndex; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child == null)
                    continue;
                if (child.GetValue(FrameworkElement.NameProperty) as string == name && child is T t)
                    return t;

                child = FindVisualChild<T>(child, name);
                if (child != null)
                    return (T)child;
            }
            return null;
        }

        /// <summary>
        /// 寻找ItemsControl
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemsControl"></param>
        /// <returns></returns>
        public static T[]? FindItemsControlChilds<T>(DependencyObject itemsControl) where T : DependencyObject
        {
            try
            {

                var pc = FindVisualFistChildAndParent<T>(itemsControl);
                if (pc.parent != null && pc.child != null)
                {
                    if (pc.parent is Panel panel)
                    {
                        List<T> reuslt = new List<T>(panel.Children.Count);
                        foreach (var item in panel.Children)
                        {
                            if (item is T chlid)
                            {
                                reuslt.Add(chlid);
                            }
                        }
                        return reuslt.ToArray();
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// 寻找子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T? FindVisualParent<T>(DependencyObject? obj) where T : DependencyObject
        {
            try
            {
                if (obj == null)
                    return default;

                var p = VisualTreeHelper.GetParent(obj);
                if (p == null)
                {
                    return default;
                }

                if (p is T t)
                {
                    return t;
                }
                else
                {
                    return FindVisualParent<T>(p);
                }
            }
            catch
            {
                return default;

            }
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


        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static ValueConverModel ValueConver(string value)
        {
            if (double.TryParse(value, out double result))
            {
                return new ValueConverModel { Clear = false, Result = result };

            }

            if (value == "N")
            {
                return new ValueConverModel { Clear = true, Result = 0 };
            }
            else
            {
                return new ValueConverModel { Clear = false, Result = 0.0 };
            }

        }

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

        #region 获取坐标帮助


        [StructLayout(LayoutKind.Sequential)]
        private struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Win32Point pt);

        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hwnd, ref Win32Point pt);
        /// <summary>
        /// Returns the mouse cursor location.  This method is necessary during 
        /// a drag-drop operation because the WPF mechanisms for retrieving the
        /// cursor coordinates are unreliable.
        /// </summary>
        /// <param name="relativeTo">The Visual to which the mouse coordinates will be relative.</param>
        public static Point GetMousePosition(Visual relativeTo)
        {
            Win32Point mouse = new Win32Point();
            GetCursorPos(ref mouse);

            // Using PointFromScreen instead of Dan Crevier's code (commented out below)
            // is a bug fix created by William J. Roberts.  Read his comments about the fix
            // here: http://www.codeproject.com/useritems/ListViewDragDropManager.asp?msg=1911611#xx1911611xx
            return relativeTo.PointFromScreen(new Point((double)mouse.X, (double)mouse.Y));

            #region Commented Out
            //System.Windows.Interop.HwndSource presentationSource =
            //    (System.Windows.Interop.HwndSource)PresentationSource.FromVisual( relativeTo );
            //ScreenToClient( presentationSource.Handle, ref mouse );
            //GeneralTransform transform = relativeTo.TransformToAncestor( presentationSource.RootVisual );
            //Point offset = transform.Transform( new Point( 0, 0 ) );
            //return new Point( mouse.X - offset.X, mouse.Y - offset.Y );
            #endregion // Commented Out
        }
        #endregion

    }
}
