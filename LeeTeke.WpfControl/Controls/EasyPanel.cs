using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LeeTeke.WpfControl.Controls
{
    /// <summary>
    /// 简单的Panel用于代替不需要格式化的Grid
    /// </summary>
    public class EasyPanel:Panel
    {

        public EasyPanel()
        {
            SetResourceReference(EasyPanel.SnapsToDevicePixelsProperty, "LeeSnapsToDevicePixels");
     
        }
        protected override Size MeasureOverride(Size constraint)
        {
            var maxSize = new Size();

            foreach (UIElement child in InternalChildren)
            {
                if (child != null)
                {
                    child.Measure(constraint);
                    maxSize.Width = Math.Max(maxSize.Width, child.DesiredSize.Width);
                    maxSize.Height = Math.Max(maxSize.Height, child.DesiredSize.Height);
                }
            }

            return maxSize;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child?.Arrange(new Rect(arrangeSize));
            }
            return arrangeSize;
        }
    }
}
