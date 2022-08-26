using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using LeeTeke.WPFTest.ViewModels;
using LeeTeke.WPFTest.Models;

namespace LeeTeke.WPFTest.ViewModels
{
    internal class TestWindowViewModel : BindableBase
    {



        #region TestList
        private ObservableCollection<TestDataGridModel> _testList;
        /// <summary>
        /// TestList
        /// </summary>
        public ObservableCollection<TestDataGridModel> TestList
        {
            get => _testList;
            set => SetProperty(ref _testList, value);
        }
        #endregion



        /// <summary>
        /// 命令注释
        /// </summary>
        public DelegateCommand TragetCommand => new(TragetCommandExecute);
        /// <summary>
        /// 命令注释_Execute
        /// </summary>
        private void TragetCommandExecute()
        {
            TestList = new ObservableCollection<TestDataGridModel>();

            for (int i = 0; i < 1000; i++)
            {
                TestList.Add(new()
                {
                    No = i,
                    Id = Guid.NewGuid().ToString(),
                    CreateTIme = DateTime.Now,
                    Group = Guid.NewGuid().ToString(),
                    Intro = Guid.NewGuid().ToString(),
                    Name = Guid.NewGuid().ToString(),
                    StatusA = Guid.NewGuid().ToString(),
                    StatusB = Guid.NewGuid().ToString(),
                    StatusC = Guid.NewGuid().ToString(),
                    StatusD = Guid.NewGuid().ToString(),
                    StatusE = Guid.NewGuid().ToString(),
                    StatusF = Guid.NewGuid().ToString(),
                    TEnum = TestAEnum.A
                });
            }

        }



    }
}
