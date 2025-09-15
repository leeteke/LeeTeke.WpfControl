using LeeTeke.WpfControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl
{
    public class GridViewItemClickedEventArgs: RoutedEventArgs
    {

        public GridViewItem Item { get; }

        public GridViewItemClickedEventArgs(GridViewItem item, RoutedEvent @event) : base(@event)
        {
            Item = item;
        }
   
    }
    public delegate void GridViewItemClickedEventHandler(object sender, GridViewItemClickedEventArgs e);
}
