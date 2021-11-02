using LeeTeke.WpfControl;
using LeeTeke.WPFTest.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LeeTeke.WPFTest.ViewModels
{
    class TestPageViewModel : BindableBase
    {

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


        public DelegateCommand<object> TestCommand { get; set; }
        public TestPageViewModel()
        {
            TestCommand = new DelegateCommand<object>(TestCommandExecute);
            TestList = new ObservableCollection<TestListModel>()
            {
                new TestListModel(){  Title="测试1", Boolen=true, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200629/3e654b20-5b71-41d5-88a1-a8c814141030.jpg"))},
                new TestListModel(){  Title="测试2", Boolen=false, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200629/4dd8e4fd-c8a3-4591-8b5f-f1476f2bc129.jpg"))},
                new TestListModel(){  Title="测试3", Boolen=true, Image=new BitmapImage(new Uri("https://nie.res.netease.com/r/pic/20200317/16005c2f-d749-465f-a05d-3dcbba024f22.jpg"))},
                new TestListModel(){  Title="测试4", Boolen=true, Image=new BitmapImage(new Uri("https://pal7.cubejoy.com/images/apic/bvimg10.jpg"))},
                new TestListModel(){  Title="测试5", Boolen=true, Image=new BitmapImage(new Uri("https://pal7.cubejoy.com/images/apic/bvimg19.jpg"))},
                new TestListModel(){  Title="测试6", Boolen=true, Image=new BitmapImage(new Uri("https://pal7.cubejoy.com/images/apic/bvimg13.jpg"))},
                new TestListModel(){  Title="测试7", Boolen=true, Image=new BitmapImage(new Uri("https://pal7.cubejoy.com/images/apic/bvimg11.jpg"))},
                new TestListModel(){  Title="测试8", Boolen=true, Image=new BitmapImage(new Uri("https://pal7.cubejoy.com/images/apic/bvimg12.jpg"))},
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
            _ = MessageBoxEx.Show("1", "1", MessageStatus.Question);
            _ = MessageBoxEx.Show("1", "1", MessageStatus.Stop);
            _ = MessageBoxEx.Show("1", "1", MessageStatus.None);
            _ = MessageBoxEx.Show("1", "1", MessageStatus.OK);
            _ = MessageBoxEx.Show("1", "1", MessageStatus.Info);
            _ = MessageBoxEx.Show("1", "1", MessageStatus.Wating);
            _ = MessageBoxEx.Show("1", "1", MessageStatus.Error);
            _ = MessageBoxEx.Show("1", "1", MessageStatus.Warning);



            NotifyData = new NotifyBannerShowData()
            {
                Content = $"1",
                Status = NotifyStatus.Info
            };
            NotifyData = new NotifyBannerShowData()
            {
                Content = $"1",
                Status = NotifyStatus.Success
            };
            NotifyData = new NotifyBannerShowData()
            {
                Content = $"1",
                Status = NotifyStatus.Primary
            };
            NotifyData = new NotifyBannerShowData()
            {
                Content = $"1",
                Status = NotifyStatus.Error
            };
            NotifyData = new NotifyBannerShowData()
            {
                Content = $"1",
                Status = NotifyStatus.Warning
            };
         
            NotifyData = new NotifyBannerShowData()
            {
                Content = $"1",
                Status = NotifyStatus.None
            };

         
        }
    }
}
