using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LeeTeke.WpfControl.Commands
{
   public class WindowCommand
    {
        public static readonly ICommand MaximizeCommand = new MaximizeWindowCommand();
        public static readonly ICommand ShowSystemMenuCommand = new ShowSystemMenuCommand();
        public static readonly ICommand MinimizeCommand = new MinimizeWindowCommand();
        public static readonly ICommand RestoreCommand = new RestoreWindowCommand();
        public static readonly ICommand CloseCommand = new CloseWindowCommand();
    }


    public class MaximizeWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is DependencyObject dependencyObject)
            {
                if (Window.GetWindow(dependencyObject) is { } window)
                {
                    window.WindowState = WindowState.Maximized;
                }
            }
        }
    }

    public class ShowSystemMenuCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is DependencyObject dependencyObject)
            {
                if (Window.GetWindow(dependencyObject) is { } window)
                {
                    SystemCommands.ShowSystemMenu(window, new Point(0,0));
                }
            }
        }
    }

    public class MinimizeWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is DependencyObject dependencyObject)
            {
                if (Window.GetWindow(dependencyObject) is { } window)
                {
                    window.WindowState = WindowState.Minimized;
                }
            }
        }
    }

    public class RestoreWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is DependencyObject dependencyObject)
            {
                if (Window.GetWindow(dependencyObject) is { } window)
                {
                    window.WindowState = WindowState.Normal;
                }
            }
        }
    }

    public class CloseWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is DependencyObject dependencyObject)
            {
                if (Window.GetWindow(dependencyObject) is { } window)
                {
                    window.Close();
                }
            }
        }
    }


}
