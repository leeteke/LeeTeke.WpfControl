using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class RegisterThemes : ResourceDictionary
    {
        public RegisterThemes()
        {
            Source = new Uri("pack://application:,,,/LeeTeke.WpfControl;component/Themes.xaml", UriKind.Absolute);
        }
    }
}
