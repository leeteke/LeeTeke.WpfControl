using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;

namespace LeeTeke.WpfControl.Dependencies
{
    public class EventToBoolManager : DependencyObject
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
            DependencyProperty.RegisterAttached("Element", typeof(UIElement), typeof(EventToBoolManager), new PropertyMetadata(null, new PropertyChangedCallback(ElementChanged)));

        private static void ElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement em)
            {
                if (e.NewValue is UIElement element)
                {
                    if (GetEventName(d)!=null&& GetBoolValue(d) != null)
                    {
                        new EventToBoolClass(GetBoolValue(d), em, element, GetEventName(d));
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
            DependencyProperty.RegisterAttached("EventName", typeof(string), typeof(EventToBoolManager));
        #endregion


        #region BoolValue


        public static DependencyProperty GetBoolValue(DependencyObject obj)
        {
            return (DependencyProperty)obj.GetValue(BoolValueProperty);
        }

        public static void SetBoolValue(DependencyObject obj, DependencyProperty value)
        {
            obj.SetValue(BoolValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for BoolValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoolValueProperty =
            DependencyProperty.RegisterAttached("BoolValue", typeof(DependencyProperty), typeof(EventToBoolManager));

        #endregion

    }

    class EventToBoolClass
    {
        private DependencyProperty _value;
        private UIElement _em;
        public EventToBoolClass(DependencyProperty value, UIElement em, UIElement obj, string eventName)
        {
            _value = value;
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
            if (_em.GetValue(_value) is bool result)
            {
                _em.SetValue(_value, !result);
            }

        }

    }
}
