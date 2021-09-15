using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace LeeTeke.WPFTest.Models
{
    class TreeViewDataModel:BindableBase
    {
        public string Title { get; set; }

        #region Child
        private ObservableCollection<TreeViewDataModel> _Child;
        /// <summary>
        /// Child
        /// </summary>
        public ObservableCollection<TreeViewDataModel> Child
        {
            get => _Child;
            set => SetProperty(ref _Child, value);
        }
        #endregion

    }
}
