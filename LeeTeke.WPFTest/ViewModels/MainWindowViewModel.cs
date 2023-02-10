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
using LeeTeke.WpfControl;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LeeTeke.WPFTest.ViewModels
{
    class MainWindowViewModel : BindableBase
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


        private double _testDouble;
        /// <summary>
        /// 请填写属性名
        /// </summary>
        public double TestDoubel
        {
            get { return _testDouble; }
            set
            {
                _testDouble = value;
                this.RaisePropertyChanged("TestDoubel");
            }
        }


        private LeeTeke.WpfControl.NotifyBannerShowData _NotifyData;
        /// <summary>
        /// 请填写属性名
        /// </summary>
        public LeeTeke.WpfControl.NotifyBannerShowData NotifyData
        {
            get { return _NotifyData; }
            set
            {
                _NotifyData = value;
                this.RaisePropertyChanged("NotifyData");
            }
        }



        #region 请填写属性名
        private Page _NativePage;
        /// <summary>
        /// 请填写属性名
        /// </summary>
        public Page NativePage
        {
            get => _NativePage;
            set => SetProperty(ref _NativePage, value);
        }
        #endregion


        #region 请填写属性名
        private ObservableCollection<TreeViewDataModel> _treeList;
        /// <summary>
        /// 请填写属性名
        /// </summary>
        public ObservableCollection<TreeViewDataModel> TreeList
        {
            get => _treeList;
            set => SetProperty(ref _treeList, value);
        }
        #endregion



        #endregion


        #region Command

        public DelegateCommand<object> TestCommand { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand ThemeChangedCommand => new(ThemeChangedCommandExecute);



        /// <summary>
        /// 命令注释
        /// </summary>
        public DelegateCommand PageChangedCommand => new(PageChangedCommandExecute);


        /// <summary>
        /// SlideChangedCommand
        /// </summary>
        public DelegateCommand SlideChangedCommand => new(SlideChangedCommandExecute);
  


        #endregion

        private Page _testPage= new TestPage();
        private bool _switch = false;
        private bool _page = false;
        private bool _slide = false;
        public MainWindowViewModel()
        {
            Init();

            TestCommand = new DelegateCommand<object>(TestCommandExecute);
        }

        /// <summary>
        /// SlideChangedCommand_Execute
        /// </summary>
        private void SlideChangedCommandExecute()
        {
            _slide = !_slide;

            Config.ScrollViewerSlideEnabled = _slide;
        }

        private void Init()
        {

            TestList = new ObservableCollection<TestListModel>()
            {
                new TestListModel(){  Title="测试1", Boolen=true, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200629/3e654b20-5b71-41d5-88a1-a8c814141030.jpg"))},
                new TestListModel(){  Title="测试2", Boolen=false, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200629/4dd8e4fd-c8a3-4591-8b5f-f1476f2bc129.jpg"))},
                new TestListModel(){  Title="测试3", Boolen=true, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200317/16005c2f-d749-465f-a05d-3dcbba024f22.jpg"))},
            };


            TreeList = new ObservableCollection<TreeViewDataModel>();

            TreeList.Add(new TreeViewDataModel()
            {
                Title = "测试1",
                Child = new ObservableCollection<TreeViewDataModel>()
                {
                    new TreeViewDataModel(){ Title="A",  Child=new ObservableCollection<TreeViewDataModel>() {
                        new TreeViewDataModel(){ Title="a" ,Child=new ObservableCollection<TreeViewDataModel>(){ new TreeViewDataModel() {  Title="1"} } },
                        new TreeViewDataModel(){ Title="b"}
                    } },
                       new TreeViewDataModel(){ Title="B",  Child=new ObservableCollection<TreeViewDataModel>() {
                           new TreeViewDataModel(){ Title="a"},
                        new TreeViewDataModel(){ Title="b"}
                    } },
                            new TreeViewDataModel(){ Title="C",  Child=new ObservableCollection<TreeViewDataModel>() {
                           new TreeViewDataModel(){ Title="a"},
                        new TreeViewDataModel(){ Title="b"}
                    } }
                }
            });
            TreeList.Add(new TreeViewDataModel()
            {
                Title = "测试2",
                Child = new ObservableCollection<TreeViewDataModel>()
                {
                          new TreeViewDataModel(){ Title="A",  Child=new ObservableCollection<TreeViewDataModel>() {
                        new TreeViewDataModel(){ Title="a"},
                        new TreeViewDataModel(){ Title="b"}
                    } },
                       new TreeViewDataModel(){ Title="B",  Child=new ObservableCollection<TreeViewDataModel>() {
                           new TreeViewDataModel(){ Title="a"},
                        new TreeViewDataModel(){ Title="b"}
                    } },
                            new TreeViewDataModel(){ Title="C",  Child=new ObservableCollection<TreeViewDataModel>() {
                           new TreeViewDataModel(){ Title="a"},
                        new TreeViewDataModel(){ Title="b"}
                    } }
                }
            });
            TreeList.Add(new TreeViewDataModel()
            {
                Title = "测试3",
                Child = new ObservableCollection<TreeViewDataModel>()
                {
                          new TreeViewDataModel(){ Title="A",  Child=new ObservableCollection<TreeViewDataModel>() {
                        new TreeViewDataModel(){ Title="a"},
                        new TreeViewDataModel(){ Title="b"}
                    } },
                       new TreeViewDataModel(){ Title="B",  Child=new ObservableCollection<TreeViewDataModel>() {
                           new TreeViewDataModel(){ Title="a"},
                        new TreeViewDataModel(){ Title="b"}
                    } },
                            new TreeViewDataModel(){ Title="C",  Child=new ObservableCollection<TreeViewDataModel>() {
                           new TreeViewDataModel(){ Title="a"},
                        new TreeViewDataModel(){ Title="b"}
                    } }
                }
            });

            SelectedData = TestList[1];

            SelectedList = new List<object>()
            {
                TestList[0],TestList[2]
            };

            Text = "123123123";
           
        }



        private async void TestCommandExecute(object obj)
        {
            MessageBoxEx.Show("saasddddddddddddddddddddddddddddddddddddddddaaaaaaaaaaaaaaaaaaaaaaaaaaa", MessageStatus.Error);

             MessageBoxEx msg = new MessageBoxEx()
            {
                Content = "正在等待服务器数据同步...",
                Title = "测试",
                ShowProcess = false,
                Status = MessageStatus.Wating,
            };

            msg.AddOptions("测试1",1);
            var reulst = msg.ShowDialogAsync();



            NotifyData = new NotifyBannerShowData()
            {
                Content = $"您选择了 {await reulst}",
                Status = NotifyStatus.Info
            };
        }

        private void ThemeChangedCommandExecute()
        {

            _switch = !_switch;

            Config.ThemeMode = _switch ? WpfControl.Models.ThemeMode.Light : WpfControl.Models.ThemeMode.Dark;
     
        }


        /// <summary>
        /// 命令注释_Execute
        /// </summary>
        private void PageChangedCommandExecute()
        {
            _page = !_page;
            if (_page)
            {
                NativePage = _testPage;
            }
            else
            {
                NativePage = null;
            }
        }

    }
}
