using LeeTeke.WpfControl.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Converters
{
    internal class TreeViewItemSeparateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            double left = 0.0;
            UIElement? element = value as TreeViewItem;
            while (element != null && element.GetType() != typeof(TreeView))
            {
                element = (UIElement)VisualTreeHelper.GetParent(element);

                switch (element)
                {
                    case TreeViewItem item:
                        var size = TreeViewItemManager.GetSwitchWidth(item);
                        left += size;
                        break;
                    default:
                        break;
                }
                      
                
            }
            return left;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
