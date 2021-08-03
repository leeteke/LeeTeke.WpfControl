using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
   public  class TabViewItemSelectedEventArgs: RoutedEventArgs
    {
        public TabViewItemSelectedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        
        }


    }

    public delegate void TabViewItemSelectedEventHandler(object sender, TabViewItemSelectedEventArgs e);
}
