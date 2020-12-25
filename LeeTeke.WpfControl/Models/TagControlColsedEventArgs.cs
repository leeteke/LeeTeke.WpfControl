using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class TagControlColsedEventArgs: RoutedEventArgs
    {

        public object Value { get; private set; }
        public TagControlColsedEventArgs(object value,RoutedEvent routedEvent):base(routedEvent)
        {
            Value = value;
        }
    }

    public delegate void TagControlColsedEventHandler(object sender, TagControlColsedEventArgs e);
}
