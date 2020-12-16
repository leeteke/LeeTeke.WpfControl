using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeeTeke.WpfControl
{
    public class SlideViewItemClickEventArgs
    {
        public object Source { get; private set; }
        public SlideViewItemClickEventArgs(object source)
        {
            Source = source;
        }
    }
}
