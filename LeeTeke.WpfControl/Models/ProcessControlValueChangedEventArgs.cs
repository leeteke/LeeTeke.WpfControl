using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{

   public class ProcessControlValueChangedEventArgs:RoutedEventArgs
    {
        public double Value { get;  }

        public ProcessControlValueChangedEventArgs(double value ,RoutedEvent routedEvent):base(routedEvent)
        {
            Value = value;
        }
    }

    public delegate void ProcessControlValueChangedEventHandler(object sender, ProcessControlValueChangedEventArgs e);

}
