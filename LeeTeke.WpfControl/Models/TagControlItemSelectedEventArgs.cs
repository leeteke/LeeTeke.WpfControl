using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
   public  class TagControlItemSelectedEventArgs: RoutedEventArgs
    {
        public TagControlItemSelectedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        
        }


    }

    public delegate void TagControlItemSelectedEventHandler(object sender, TagControlItemSelectedEventArgs e);
}
