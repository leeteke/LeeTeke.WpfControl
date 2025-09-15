using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class RegisterControls : ResourceDictionary
    {
        public RegisterControls()
        {
            Source = new Uri("pack://application:,,,/LeeTeke.WpfControl;component/Controls.xaml", UriKind.Absolute);
        }
    }
}
