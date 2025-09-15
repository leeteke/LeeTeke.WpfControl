using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class RegisterColors : ResourceDictionary
    {
        public RegisterColors() 
        {
           Source = new Uri("pack://application:,,,/LeeTeke.WpfControl;component/Colors.xaml",UriKind.Absolute) ;
        }
    }
}
