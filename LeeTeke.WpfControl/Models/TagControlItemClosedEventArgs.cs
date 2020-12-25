using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class TagControlItemClosedEventArgs : RoutedEventArgs
    {
        public TagControlItemClosedMode ClosedMode { get; private set; }
        public TagControlItemClosedEventArgs(TagControlItemClosedMode mode,RoutedEvent routedEvent):base(routedEvent)
        {
            ClosedMode = mode;
        }
    }

    public delegate void TagControlItemClosedEventHandler(object sender, TagControlItemClosedEventArgs e);
}
