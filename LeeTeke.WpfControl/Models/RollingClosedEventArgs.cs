using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl.Models
{
 
    public class RollingClosedEventArgs : RoutedEventArgs
    {

        public object Content { get; private set; }
        public RollingClosedEventArgs(object value, RoutedEvent @event) : base(@event)
        {
            Content = value;
        }

    }


    public delegate void RollingClosedEventHandler(object sender, RollingClosedEventArgs e);
}
