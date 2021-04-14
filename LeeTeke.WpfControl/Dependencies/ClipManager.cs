using LeeTeke.WpfControl.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace LeeTeke.WpfControl.Dependencies
{
    public class ClipManager 
    {

        public static object GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, object value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(object), typeof(ClipManager), new PropertyMetadata(null, new PropertyChangedCallback(CornerRadiusChanged)));

        private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {

                if (e.NewValue is not CornerRadius cornerRadius)
                {
                    cornerRadius = new CornerRadius(0);
                    var values = e.NewValue.ToString().Split(',');
                    switch (values.Length)
                    {
                        case 1:
                            if (double.TryParse(values[0], out double all))
                            {
                                cornerRadius = new CornerRadius(all);

                            }
                            else if (values[0] == "Round")
                            {
                              //  cornerRadius = new CornerRadius(element.ActualWidth, element.ActualWidth, element.ActualWidth, element.ActualWidth);
                                element.Loaded += (eo, es) =>
                                {

                                   SetCornerRadius(element,  new CornerRadius(element.ActualWidth, element.ActualWidth, element.ActualWidth, element.ActualWidth));

                                };
                            }
                            break;
                        case 4:
                            if (double.TryParse(values[0], out double topLeft) && double.TryParse(values[1], out double topRight) && double.TryParse(values[2], out double buttomRight) && double.TryParse(values[3], out double buttomLeft))
                            {
                                cornerRadius = new CornerRadius(topLeft, topRight, buttomRight, buttomLeft);
                            }
                            break;
                        default:
                            return;
                    }
                    SetCornerRadius(element, cornerRadius);
                    return;
                }
                else
                {
                    if (e.OldValue is not CornerRadius oldCR)
                    {
                        SetCornerRadius(element, cornerRadius);
                        return;
                    }
             
                }


                MultiBinding multiBinding = new MultiBinding()
                {
                    Converter = new MultiValueToClipConverter(),
                    Mode= BindingMode.OneWay
                };
                multiBinding.Bindings.Add(new Binding()
                {
                    Source = element,
                    Path = new PropertyPath("ActualWidth")
                });
                multiBinding.Bindings.Add(new Binding()
                {
                    Source = element,
                    Path = new PropertyPath("ActualHeight")
                });
                multiBinding.Bindings.Add(new Binding()
                {
                    Source = element,
                    Path = new PropertyPath("(0)", new DependencyProperty[] { LeeTeke.WpfControl.Dependencies.ClipManager.CornerRadiusProperty, }),
                    Mode = BindingMode.OneWay
                });

                BindingOperations.SetBinding(element, FrameworkElement.ClipProperty, multiBinding);



            }
        }

    }
}
