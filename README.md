# WpfControl
WPF控件
资源引用使用方法

    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!--自定义控件需要的res-->
                <ResourceDictionary Source="pack://application:,,,/LeeTeke.WpfControl;component/Controls.xaml"/>
                <!--基础控件需要的res-->
                <ResourceDictionary Source="pack://application:,,,/LeeTeke.WpfControl;component/Themes.xaml"/>
                <!--所有控件需要的颜色res-->
                <ResourceDictionary Source="pack://application:,,,/LeeTeke.WpfControl;component/Colors.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
自定义控件引用方法

   xmlns:lee="clr-namespace:LeeTeke.WpfControl.Controls;assembly=LeeTeke.WpfControl"
   
   

