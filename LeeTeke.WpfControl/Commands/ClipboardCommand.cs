using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LeeTeke.WpfControl.Commands
{
    public class ClipboardCommand
    {
        public static readonly ICommand CopyCommand = new ClipboardCopyValue();
    }

    public class ClipboardCopyValue : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                Clipboard.SetDataObject(parameter, true);
            }
            catch (Exception)
            {
            }
        }
    }

}
