﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LeeTeke.WpfControl.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {

        }


        protected override void OnStartup(StartupEventArgs e)
        {
            LeeTeke.WpfControl.Config.Initialize();
            //Config.MessageBoxExShowCornerRadius = true;
            base.OnStartup(e);
      
        }
    }
}
