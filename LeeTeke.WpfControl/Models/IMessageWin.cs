using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl.Models
{
    internal interface IMessageWin
    {
        Window Window { get; }

        object Value { get; set; }

        bool ShowLoding { get; set; }

        bool CanClose { get; set; }

        string Title { get; set; }

        object Content { get; set; }

        ProcessControlMode ProcessControlMode { get; set; }

        double ProcessControlValue { get; set; }

        MessageStatus Status { get; set; }

        void SetSize(double width = 320, double height = 200);

        void AddOptions(string name, object value, CornerRadius cornerRadius = default);

        void Show();

        bool? ShowDialog();

    }
}
