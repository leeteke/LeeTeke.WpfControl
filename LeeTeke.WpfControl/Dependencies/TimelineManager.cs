using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace LeeTeke.WpfControl.Dependencies
{
   public class TimelineManager 
    {
        #region EventName


        public static string GetEventName(DependencyObject obj)
        {
            return (string)obj.GetValue(EventNameProperty);
        }

        public static void SetEventName(DependencyObject obj, string value)
        {
            obj.SetValue(EventNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for EventName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventNameProperty =
            DependencyProperty.RegisterAttached("EventName", typeof(string), typeof(TimelineManager));
        #endregion



        #region Command
        /// <summary>
        /// Command
        /// </summary>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(TimelineManager), new PropertyMetadata(null, CommandChanged));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Timeline em)
            {
                if (e.NewValue is ICommand element && e.NewValue != e.OldValue)
                {
                    if (GetEventName(d) != null)
                    {
                         new TimelineHelpClass(em, GetEventName(d), element);
                    }
                }
            }
        }
        #endregion

    }


    class TimelineHelpClass
    {
        ICommand _command;
        private Timeline _em;
        public TimelineHelpClass(Timeline em, string eventName, ICommand command)
        {
            _command = command;
            _em = em;
            EventHandler clickHandler = Test;
            Type type = em.GetType();
            System.Reflection.EventInfo @event = type.GetEvent(eventName);
            if (@event != null)
            {
                var pd = @event.GetAddMethod();
                @event.AddEventHandler(em, Delegate.CreateDelegate(@event.EventHandlerType, this, clickHandler.Method));
            }
        }

        private void Test(object send, EventArgs e)
        {
            _command?.Execute(_em);
        }

    }
}
