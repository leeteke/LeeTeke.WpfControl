# WpfControl
一款类Win11风格的WPF主题控件库;  
支持暗黑/命令主题切换，可跟随系统；  
多个自定义控件；



``` xaml
<Application
    ...
    xmlns:lee="https://github.com/leeteke/LeeTeke.WpfControl">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <lee:RegisterColors /> 
                <lee:RegisterThemes />
                <lee:RegisterControls />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>

```
添加命名控件以使用自定义控件以及其它辅助工具方法

    xmlns:lee="https://github.com/leeteke/LeeTeke.WpfControl"
   
   

