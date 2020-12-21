using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl.Dependencies
{
    public class FocusFromEventManager : DependencyObject
    {
        #region Element


        public static UIElement GetElement(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(ElementProperty);
        }

        public static void SetElement(DependencyObject obj, UIElement value)
        {
            obj.SetValue(ElementProperty, value);
        }

        // Using a DependencyProperty as the backing store for Element.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementProperty =
            DependencyProperty.RegisterAttached("Element", typeof(UIElement), typeof(FocusFromEventManager), new PropertyMetadata(null, new PropertyChangedCallback(ElementChanged)));

        private static void ElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement em)
            {
                if (e.NewValue is UIElement element)
                {
                    if (GetEventName(d) != null)
                    {
                        new EventToFocusClass(em, element, GetEventName(d));
                    }
                }
            }
        }
        #endregion

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
            DependencyProperty.RegisterAttached("EventName", typeof(string), typeof(FocusFromEventManager));
        #endregion
    }

    class EventToFocusClass
    {
        private UIElement _em;
        public EventToFocusClass(UIElement em, UIElement obj, string eventName)
        {
            _em = em;
            EventHandler clickHandler = Test;
            Type type = obj.GetType();
            EventInfo @event = type.GetEvent(eventName);
            if (@event != null)
            {
                var pd = @event.GetAddMethod();
                @event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, clickHandler.Method));
            }
        }

        private void Test(object send, EventArgs e)
        {
            _em.Focus();
        }

    }
}
