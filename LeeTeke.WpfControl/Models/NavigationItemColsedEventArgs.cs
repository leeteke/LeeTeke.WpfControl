using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class NavigationItemColsedEventArgs: RoutedEventArgs
    {

        public object Value { get;  }
        public NavigationItemColsedEventArgs(object value,RoutedEvent routedEvent):base(routedEvent)
        {
            Value = value;
        }
    }

    public delegate void NavigationItemColsedEventHandler(object sender, NavigationItemColsedEventArgs e);
}
