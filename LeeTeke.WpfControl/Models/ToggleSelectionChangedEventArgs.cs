using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class ToggleSelectionChangedEventArgs : RoutedEventArgs
    {
        public object Value { get;  }


        public ToggleSelectionChangedEventArgs(object value, RoutedEvent routedEvent) : base(routedEvent)
        {
            Value = value;
        }

    }

    public delegate void ToggleSelectionChangedEventHandler(object sender, ToggleSelectionChangedEventArgs e);
}

