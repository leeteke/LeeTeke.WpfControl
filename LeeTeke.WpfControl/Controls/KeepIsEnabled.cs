using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LeeTeke.WpfControl.Controls
{
    public class KeepIsEnabled : ContentControl
    {

        static KeepIsEnabled()
        {
            IsEnabledProperty.OverrideMetadata(typeof(KeepIsEnabled), new UIPropertyMetadata(true, (_, __) => { }, (_, x) => x));
        }
    }
}
