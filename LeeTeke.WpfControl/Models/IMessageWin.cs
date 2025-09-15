using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeeTeke.WpfControl.Models
{
    internal interface IMessageWin
    {
        Window Window { get; }

        object? Value { get; set; }

        bool ShowProcess { get; set; }

        bool CanClose { get; set; }

        string? Title { get; set; }

        object? Content { get; set; }


        int ProcessValue { get; set; }

        MessageStatus Status { get; set; }

        void SetSize(double width = 320, double height = 200);

        void AddOptions(string name, object? value, CornerRadius? cornerRadius = null);
        void AddOptions(Button btn, object? value);
        void Show();

        bool? ShowDialog();

    }
}
