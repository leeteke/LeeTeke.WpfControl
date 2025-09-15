using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class NotifyClickedEventArgs : RoutedEventArgs
    {
        public object? Value { get; }
        public NotifyClickedEventArgs(object? value, RoutedEvent @event) : base(@event)
        {
            Value = value;
        }
    }

    public delegate void NotifyClickedEventHandler(object sender, NotifyClickedEventArgs e);
}
