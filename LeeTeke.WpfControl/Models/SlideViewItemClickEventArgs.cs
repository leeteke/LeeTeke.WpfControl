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
        public object Source { get;  }
        public SlideViewItemClickedEventArgs(object source,RoutedEvent routedEvent):base(routedEvent)
        {
            Source = source;
        }
    }

    public delegate void SlideViewItemClickedEventHandler(object sender, SlideViewItemClickedEventArgs e);
}
