using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
   public class NavigationItemSelectedEventArgs : RoutedEventArgs
    {

        public object Value { get; private set; }
        public NavigationItemSelectedEventArgs(object value, RoutedEvent routedEvent):base(routedEvent)
        {
            Value = value;
        }

    }

    public delegate void NavigationItemSelectedEventHandler(object sender, NavigationItemSelectedEventArgs e);
}
