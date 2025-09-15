using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{

   public class ProgressControlValueChangedEventArgs:RoutedEventArgs
    {
        public double Value { get;  }

        public ProgressControlValueChangedEventArgs(double value ,RoutedEvent routedEvent):base(routedEvent)
        {
            Value = value;
        }
    }

    public delegate void ProgressControlValueChangedEventHandler(object sender, ProgressControlValueChangedEventArgs e);

}
