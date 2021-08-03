using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class TabViewItemClosedEventArgs : RoutedEventArgs
    {
        public TabViewItemClosedMode ClosedMode { get; private set; }
        public TabViewItemClosedEventArgs(TabViewItemClosedMode mode,RoutedEvent routedEvent):base(routedEvent)
        {
            ClosedMode = mode;
        }
    }

    public delegate void TabViewItemClosedEventHandler(object sender, TabViewItemClosedEventArgs e);
}
