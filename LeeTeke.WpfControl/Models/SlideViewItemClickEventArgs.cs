using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class SlideViewItemClickedEventArgs:RoutedEventArgs
    {
        public object Item { get;  }
        public SlideViewItemClickedEventArgs(object item,RoutedEvent routedEvent):base(routedEvent)
        {
            Item = item;
        }
    }

    public delegate void SlideViewItemClickedEventHandler(object sender, SlideViewItemClickedEventArgs e);
}
