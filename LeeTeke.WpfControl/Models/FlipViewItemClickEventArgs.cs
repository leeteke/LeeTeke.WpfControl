using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class FlipViewItemClickedEventArgs:RoutedEventArgs
    {
        public object Item { get;  }
        public FlipViewItemClickedEventArgs(object item,RoutedEvent routedEvent):base(routedEvent)
        {
            Item = item;
        }
    }

    public delegate void FlipViewItemClickedEventHandler(object sender, FlipViewItemClickedEventArgs e);
}
