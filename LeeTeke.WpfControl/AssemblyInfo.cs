using System.Windows;
using System.Windows.Markup;
using LeeTeke.WpfControl;
using LeeTeke.WpfControl.Dependencies;
using LeeTeke.WpfControl.Controls;
using LeeTeke.WpfControl.Commands;
using LeeTeke.WpfControl.Converters;
using LeeTeke.WpfControl.Effects;
using LeeTeke.WpfControl.Models;
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsPrefix("https://github.com/leeteke/LeeTeke.WpfControl", "lee")]
[assembly: XmlnsDefinition("https://github.com/leeteke/LeeTeke.WpfControl", "LeeTeke.WpfControl")]
[assembly: XmlnsDefinition("https://github.com/leeteke/LeeTeke.WpfControl", "LeeTeke.WpfControl.Dependencies")]
[assembly: XmlnsDefinition("https://github.com/leeteke/LeeTeke.WpfControl", "LeeTeke.WpfControl.Controls")]
[assembly: XmlnsDefinition("https://github.com/leeteke/LeeTeke.WpfControl", "LeeTeke.WpfControl.Commands")]
[assembly: XmlnsDefinition("https://github.com/leeteke/LeeTeke.WpfControl", "LeeTeke.WpfControl.Effects")]
[assembly: XmlnsDefinition("https://github.com/leeteke/LeeTeke.WpfControl", "LeeTeke.WpfControl.Models")]
[assembly: XmlnsDefinition("https://github.com/leeteke/LeeTeke.WpfControl", "LeeTeke.WpfControl.Converters")]