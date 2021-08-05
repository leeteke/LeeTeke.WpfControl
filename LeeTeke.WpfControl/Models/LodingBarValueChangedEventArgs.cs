using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
   public class LodingBarValueChangedEventArgs:RoutedEventArgs
    {
        public double Value { get;  }

        public LodingBarValueChangedEventArgs(double value ,RoutedEvent routedEvent):base(routedEvent)
        {
            Value = value;
        }
    }

    public delegate void LodingBarValueChangedEventHandler(object sender, LodingBarValueChangedEventArgs e);

}
