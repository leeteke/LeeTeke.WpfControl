using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using LeeTeke.WpfControl.Demo.TestDatas;

namespace LeeTeke.WpfControl.Demo.ViewModels
{
    partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        private TestEnum? _enumData=TestEnum.A;


        public MainViewModel()
        {
           
            
        }

    }
}
