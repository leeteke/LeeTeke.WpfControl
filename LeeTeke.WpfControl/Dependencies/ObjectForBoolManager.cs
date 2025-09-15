using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ObjectForBoolManager 
    {


        #region Property
        /// <summary>
        /// 属性名称
        /// </summary>
        public static DependencyProperty GetProperty(DependencyObject obj)
        {
            return (DependencyProperty)obj.GetValue(PropertyProperty);
        }

        public static void SetProperty(DependencyObject obj, DependencyProperty value)
        {
            obj.SetValue(PropertyProperty, value);
        }

        // Using a DependencyProperty as the backing store for Property.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyProperty =
            DependencyProperty.RegisterAttached("Property", typeof(DependencyProperty), typeof(ObjectForBoolManager));
        #endregion



        #region BoolValue
        /// <summary>
        /// BoolValue
        /// </summary>
        public static bool? GetBoolValue(DependencyObject obj)
        {
            return (bool?)obj.GetValue(BoolValueProperty);
        }

        public static void SetBoolValue(DependencyObject obj, bool? value)
        {
            obj.SetValue(BoolValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for BoolValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoolValueProperty =
            DependencyProperty.RegisterAttached("BoolValue", typeof(bool?), typeof(ObjectForBoolManager), new PropertyMetadata(null, new PropertyChangedCallback(BoolValueChanged)));

        private static void BoolValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                var value = (bool?)e.NewValue;
                switch (value)
                {
                    case true:
                        d.SetValue(GetProperty(d), GetTrueObject(d));
                        break;
                    case false:
                        d.SetValue(GetProperty(d), GetFalseObject(d));
                        break;
                    default:
                        d.SetValue(GetProperty(d), GetFalseObject(d));
                        break;
                }
            }
        }
        #endregion


        #region TrueObject
        /// <summary>
        /// TrueObject
        /// </summary>
        public static object GetTrueObject(DependencyObject obj)
        {
            return (object)obj.GetValue(TrueObjectProperty);
        }

        public static void SetTrueObject(DependencyObject obj, object value)
        {
            obj.SetValue(TrueObjectProperty, value);
        }

        // Using a DependencyProperty as the backing store for TrueObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrueObjectProperty =
            DependencyProperty.RegisterAttached("TrueObject", typeof(object), typeof(ObjectForBoolManager));
        #endregion


        #region FalseObject
        /// <summary>
        /// FalseObject
        /// </summary>
        public static object GetFalseObject(DependencyObject obj)
        {
            return (object)obj.GetValue(FalseObjectProperty);
        }

        public static void SetFalseObject(DependencyObject obj, object value)
        {
            obj.SetValue(FalseObjectProperty, value);
        }

        // Using a DependencyProperty as the backing store for FalseObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FalseObjectProperty =
            DependencyProperty.RegisterAttached("FalseObject", typeof(object), typeof(ObjectForBoolManager));
        #endregion


        #region NullObject
        /// <summary>
        /// NullObject
        /// </summary>
        public static object GetNullObject(DependencyObject obj)
        {
            return (object)obj.GetValue(NullObjectProperty);
        }

        public static void SetNullObject(DependencyObject obj, object value)
        {
            obj.SetValue(NullObjectProperty, value);
        }

        // Using a DependencyProperty as the backing store for NullObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NullObjectProperty =
            DependencyProperty.RegisterAttached("NullObject", typeof(object), typeof(ObjectForBoolManager));
        #endregion


    }
}
