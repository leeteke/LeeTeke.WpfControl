using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class TextBoxExEventArgs : RoutedEventArgs
    {
        public string? Text { get;  }

        public TextBoxExEventArgs(string? text,RoutedEvent routedEvent):base(routedEvent)
        {
            Text = text;
        }

    }

    public delegate void TextBoxExEventHandler(object sender, TextBoxExEventArgs e);
}

