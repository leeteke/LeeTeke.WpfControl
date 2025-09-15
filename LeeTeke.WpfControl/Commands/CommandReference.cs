using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LeeTeke.WpfControl.Commands
{
    public class CommandReference : Freezable, ICommand, ICommandSource
    {

        #region CommandParameter
        /// <summary>
        /// 请添加描述
        /// </summary>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(CommandReference));
        #endregion


        #region CommandTarget
        /// <summary>
        /// 请添加描述
        /// </summary>
        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(CommandReference));
        #endregion

        #region Command
        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandReference), new PropertyMetadata(OnCommandChanged));
        #endregion

        #region ICommand 

        public bool CanExecute(object? parameter)
        {
            if (Command != null)
                return Command.CanExecute(CommandParameter);
            return false;
        }

        public void Execute(object? parameter)
        {
            Command.Execute(CommandParameter);
        }

        public event EventHandler? CanExecuteChanged;

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CommandReference commandReference)
            {


                if (e.OldValue is ICommand oldCommand)
                {
                    oldCommand.CanExecuteChanged -= commandReference.CanExecuteChanged;
                }
                if (e.NewValue is ICommand newCommand)
                {
                    newCommand.CanExecuteChanged += commandReference.CanExecuteChanged;
                }
            }
        }

        #endregion

        #region Freezable

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
