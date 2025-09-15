using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
   public class NotifyClosedEventArgs: RoutedEventArgs
    {

        public object? Value { get;  }

        public NotifyClosedEventArgs(object? value ,RoutedEvent @event):base(@event)
        {
            Value = value;
        }

    }

    public delegate void NotifyClosedEventHandler(object sender, NotifyClosedEventArgs e);
}
