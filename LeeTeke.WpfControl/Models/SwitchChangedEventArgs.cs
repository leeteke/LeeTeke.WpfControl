using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class SwitchChangedEventArgs : RoutedEventArgs
    {
        public bool Switch { get;  }
        public SwitchChangedEventArgs(bool @switch, RoutedEvent routedEvent) : base(routedEvent) => Switch = @switch;

    }

    public delegate void SwitchChangedEventHandler(object sender, SwitchChangedEventArgs e);
}
