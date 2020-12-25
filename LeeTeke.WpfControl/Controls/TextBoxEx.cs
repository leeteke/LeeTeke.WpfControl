using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeeTeke.WpfControl.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LeeTeke.WpfControl.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LeeTeke.WpfControl.Controls;assembly=LeeTeke.WpfControl.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:TextBoxEx/>
    ///
    /// </summary>
    public class TextBoxEx : Control
    {
        static TextBoxEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxEx), new FrameworkPropertyMetadata(typeof(TextBoxEx)));
        }


        private PasswordBox _password;
        private TextBox _textBox;
        private Border _border;
        public TextBoxEx()
        {
            Loaded += TextBoxEx_Loaded;
        }

        private void TextBoxEx_Loaded(object sender, RoutedEventArgs e)
        {
            _password = this.Template.FindName("PART_Password", this) as PasswordBox;
            _textBox = this.Template.FindName("PART_Main", this) as TextBox;
            _border = this.Template.FindName("PART_ICON", this) as Border;
            if (_password != null)
            {
                _password.PasswordChanged += _password_PasswordChanged;
            }

            if (_textBox != null)
            {
                _textBox.PreviewTextInput += _textBox_PreviewTextInput;
                _textBox.TextChanged += _textBox_TextChanged;
            }

            if (_border!=null)
            {
                _border.MouseDown += _border_MouseDown;
            }
        }

        private void _textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           switch (Mode)
            {
            
                case TextMode.Number:
                case TextMode.IMEDispaly:
                    _password.Password = Text;
                    break;
                default:
                    break;
            }
          
        }

        private void _textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
         
            switch (Mode)
            {
                case TextMode.Number:
                    e.Handled = new Regex("[^0-9.-]+").IsMatch(e.Text);
                    return;
                default:
                    return;
            }

        }

        private void _border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IconCanClick)
            {
                try
                {
                    IconCommand?.Execute(Text);
                }
                catch
                {
                }
                IconCliecked?.Invoke(this, Text);
            }
        }

        private void _password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Text != _password.Password&&Mode== TextMode.Password)
            {
                Text = _password.Password;
            }
        }

                


        #region 依赖属性


        #region Mode
        /// <summary>
        /// 请填写描述
        /// </summary>
        public TextMode Mode
        {
            get { return (TextMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(TextMode), typeof(TextBoxEx), new PropertyMetadata(TextMode.General));

        #endregion


        #region Text
        /// <summary>
        /// 请填写描述
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxEx));

        #endregion


        #region TextAlignment
        /// <summary>
        /// 请填写描述
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(TextBoxEx));

        #endregion


        #region CornerRadius
        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TextBoxEx));

        #endregion

        #region PasswordChar
        /// <summary>
        /// 请填写描述
        /// </summary>
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PasswordChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(TextBoxEx));

        #endregion

        #region PreviewText
        /// <summary>
        /// 请填写描述
        /// </summary>
        public string PreviewText
        {
            get { return (string)GetValue(PreviewTextProperty); }
            set { SetValue(PreviewTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreviewText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewTextProperty =
            DependencyProperty.Register("PreviewText", typeof(string), typeof(TextBoxEx));
        #endregion

        #region AcceptsReturn
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool AcceptsReturn
        {
            get { return (bool)GetValue(AcceptsReturnProperty); }
            set { SetValue(AcceptsReturnProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AcceptsReturn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AcceptsReturnProperty =
            DependencyProperty.Register("AcceptsReturn", typeof(bool), typeof(TextBoxEx));

        #endregion

        #region Icon
        /// <summary>
        /// 请填写描述
        /// </summary>
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(TextBoxEx));

        #endregion


        #region IconOutSize
        /// <summary>
        /// 请填写描述
        /// </summary>
        public double IconOutSize
        {
            get { return (double)GetValue(IconOutSizeProperty); }
            set { SetValue(IconOutSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconOutSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconOutSizeProperty =
            DependencyProperty.Register("IconOutSize", typeof(double), typeof(TextBoxEx));

        #endregion


        #region IconSize
        /// <summary>
        /// 请填写描述
        /// </summary>
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(TextBoxEx));

        #endregion

        #region IconFill
        /// <summary>
        /// 请填写描述
        /// </summary>
        public Brush IconFill
        {
            get { return (Brush)GetValue(IconFillProperty); }
            set { SetValue(IconFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFillProperty =
            DependencyProperty.Register("IconFill", typeof(Brush), typeof(TextBoxEx));

        #endregion

        #region IconVisible
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool IconVisible
        {
            get { return (bool)GetValue(IconVisibleProperty); }
            set { SetValue(IconVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconVisibleProperty =
            DependencyProperty.Register("IconVisible", typeof(bool), typeof(TextBoxEx));

        #endregion

        #region IconCanClick
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool IconCanClick
        {
            get { return (bool)GetValue(IconCanClickProperty); }
            set { SetValue(IconCanClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconCanClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconCanClickProperty =
            DependencyProperty.Register("IconCanClick", typeof(bool), typeof(TextBoxEx));

        #endregion

        #region IconDock
        /// <summary>
        /// IconDock
        /// </summary>
        public Dock IconDock
        {
            get { return (Dock)GetValue(IconDockProperty); }
            set { SetValue(IconDockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconDock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconDockProperty =
            DependencyProperty.Register("IconDock", typeof(Dock), typeof(TextBoxEx));

        #endregion

        #region SplitterVisible
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool SplitterVisible
        {
            get { return (bool)GetValue(SplitterVisibleProperty); }
            set { SetValue(SplitterVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SplitterVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitterVisibleProperty =
            DependencyProperty.Register("SplitterVisible", typeof(bool), typeof(TextBoxEx));

        #endregion

        #region SplitterFill
        /// <summary>
        /// 请填写描述
        /// </summary>
        public Brush SplitterFill
        {
            get { return (Brush)GetValue(SplitterFillProperty); }
            set { SetValue(SplitterFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SplitterFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitterFillProperty =
            DependencyProperty.Register("SplitterFill", typeof(Brush), typeof(TextBoxEx));

        #endregion

        #region SplitterMargin
        /// <summary>
        /// 请填写描述
        /// </summary>
        public Thickness SplitterMargin
        {
            get { return (Thickness)GetValue(SplitterMarginProperty); }
            set { SetValue(SplitterMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SplitterMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitterMarginProperty =
            DependencyProperty.Register("SplitterMargin", typeof(Thickness), typeof(TextBoxEx));

        #endregion

        #region SplitterSize
        /// <summary>
        /// 请填写描述
        /// </summary>
        public double SplitterSize
        {
            get { return (double)GetValue(SplitterSizeProperty); }
            set { SetValue(SplitterSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SplitterSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitterSizeProperty =
            DependencyProperty.Register("SplitterSize", typeof(double), typeof(TextBoxEx));

        #endregion



        #endregion

        #region Command


        #region EnterCommand
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ICommand EnterCommand
        {
            get { return (ICommand)GetValue(EnterCommandProperty); }
            set { SetValue(EnterCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnterCommandProperty =
            DependencyProperty.Register("EnterCommand", typeof(ICommand), typeof(TextBoxEx));


        #region IconCommand
        /// <summary>
        /// 请填写描述
        /// </summary>
        public ICommand IconCommand
        {
            get { return (ICommand)GetValue(IconCommandProperty); }
            set { SetValue(IconCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconCommandProperty =
            DependencyProperty.Register("IconCommand", typeof(ICommand), typeof(TextBoxEx));

        #endregion

        #endregion


        #endregion

        #region Event
        /// <summary>
        /// 回车事件
        /// </summary>
        public event EventHandler<string> EnterEvent;

        public event EventHandler<string> IconCliecked;

        #endregion


        #region 私有逻辑

        #endregion
    }
}
