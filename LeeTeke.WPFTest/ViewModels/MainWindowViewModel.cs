using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using LeeTeke.WPFTest.Models;
using System.Windows.Media.Imaging;

namespace LeeTeke.WPFTest.ViewModels
{
    class MainWindowViewModel:BindableBase
    {

        #region 属性

  

        private ObservableCollection<TestListModel> _TestList;
        /// <summary>
        /// 请填写属性名
        /// </summary>
        public ObservableCollection<TestListModel> TestList
        {
            get { return _TestList; }
            set
            {
                _TestList = value;
                this.RaisePropertyChanged("TestList");
            }
        }

        private TestListModel _SelectedData;
        /// <summary>
        /// 请填写属性名
        /// </summary>
        public TestListModel SelectedData
        {
            get { return _SelectedData; }
            set
            {
                _SelectedData = value;
                this.RaisePropertyChanged("SelectedData");
            }
        }


        private List<object> _SelectedList;
        /// <summary>
        /// 请填写属性名
        /// </summary>
        public List<object> SelectedList
        {
            get { return _SelectedList; }
            set
            {
                _SelectedList = value;
                this.RaisePropertyChanged("SelectedList");
            }
        }

        private string _text;
        /// <summary>
        /// 请填写属性名
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                this.RaisePropertyChanged("Text");
            }
        }



        #endregion


        #region Command

        public DelegateCommand<object> TestCommand { get; set; }


        #endregion


        public MainWindowViewModel()
        {
            Init();

            TestCommand = new DelegateCommand<object>(TestCommandExecute);
        }

   

        private void Init()
        {

            TestList = new ObservableCollection<TestListModel>()
            {
                new TestListModel(){  Title="测试1", Boolen=true, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200629/3e654b20-5b71-41d5-88a1-a8c814141030.jpg"))},
                new TestListModel(){  Title="测试2", Boolen=false, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200629/4dd8e4fd-c8a3-4591-8b5f-f1476f2bc129.jpg"))},
                new TestListModel(){  Title="测试3", Boolen=true, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200317/16005c2f-d749-465f-a05d-3dcbba024f22.jpg"))},
            };

            SelectedData = TestList[1];

            SelectedList = new List<object>()
            {
                TestList[0],TestList[2]
            };

            Text = "123123123";
        }



        private void TestCommandExecute(object obj)
        {

            var msgEx = new LeeTeke.WpfControl.MessageBoxEx() {  Title="测试"};
            msgEx.SetContent(new System.Windows.Controls.Button() { Content = "你好" });

            msgEx.Show();

        }

    }
}
