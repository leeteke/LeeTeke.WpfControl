# WpfControl
一款类Win11风格的WPF主题控件库;  
支持暗黑/命令主题切换，可跟随系统；  
多个自定义控件；



``` xaml
<Application
    ...>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/LeeTeke.WpfControl;component/Colors.xaml" />
                <ResourceDictionary Source="pack://application:,,,/LeeTeke.WpfControl;component/Themes.xaml" />
                <ResourceDictionary Source="pack://application:,,,/LeeTeke.WpfControl;component/Controls.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>

```
添加命名控件以使用自定义控件以及其它辅助工具方法

    xmlns:lee="https://github.com/leeteke/LeeTeke.WpfControl"
   
开启主题跟随主题的系统主题的方法

    在 app.cs 文件中
```csharp

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

         protected override void OnStartup(StartupEventArgs e)
        {
            //初始化
            LeeTeke.WpfControl.Config.Initialize();
            //使用系统主题
            LeeTeke.WpfControl.Config.UseSystemThemeMode = true;
            //使用系统主题色
            LeeTeke.WpfControl.Config.UseSystemThemeColor = true;
            //当系统主题切换时通知
            LeeTeke.WpfControl.Config.ThemeModeChanged += Config_ThemeModeChanged;
            base.OnStartup(e);
        }

        private void Config_ThemeModeChanged(object? sender, LeeTeke.WpfControl.Models.ThemeMode e)
        {
            if (e == LeeTeke.WpfControl.Models.ThemeMode.Light)
            {
                //配置自己的颜色方法
            }
            else
            { 
                //配置自己的颜色方法
            }
        }

    }

```
   

