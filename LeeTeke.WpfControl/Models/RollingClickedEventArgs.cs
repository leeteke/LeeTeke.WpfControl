using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl.Models
{
    public class RollingClickedEventArgs : RoutedEventArgs
    {

        public object Content { get; private set; }
        public RollingClickedEventArgs(object value, RoutedEvent @event) : base(@event)
        {
            Content = value;
        }

    }


    public delegate void RollingClickedEventHandler(object sender, RollingClickedEventArgs e);



}
