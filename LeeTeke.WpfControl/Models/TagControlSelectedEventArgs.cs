using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
   public class TagControlSelectedEventArgs:RoutedEventArgs
    {

        public object Value { get; private set; }
        public TagControlSelectedEventArgs(object value, RoutedEvent routedEvent):base(routedEvent)
        {
            Value = value;
        }

    }

    public delegate void TagControlSelectedEventHandler(object sender, TagControlSelectedEventArgs e);
}
