using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class PageIndexChangedEventArgs : RoutedEventArgs
    {
        public int Index { get;  }
        public PageIndexChangedEventArgs( int index, RoutedEvent @event):base(@event)
        {
            Index = index;
        }
    }

    public delegate  void PageIndexChangedEventHandler(object sender, PageIndexChangedEventArgs e);

}
