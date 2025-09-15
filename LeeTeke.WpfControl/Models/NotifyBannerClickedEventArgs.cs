using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class NotifyBannerClickedEventArgs : RoutedEventArgs
    {
        public object? Value { get;  }

        public NotifyBannerClickedEventArgs(object? value, RoutedEvent @event) : base(@event)
        {
            Value = value;
        }

    }

    public delegate void NotifyBannerClickedEventHandler(object sender, NotifyBannerClickedEventArgs e);
}
