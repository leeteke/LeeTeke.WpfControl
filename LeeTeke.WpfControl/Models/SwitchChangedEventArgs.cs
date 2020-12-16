using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeeTeke.WpfControl
{
    public class SwitchChangedEventArgs : EventArgs
    {
        public bool Switch { get; private set; }
        public SwitchChangedEventArgs(bool @switch) => Switch = @switch;

    }
}
