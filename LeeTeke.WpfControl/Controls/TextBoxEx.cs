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

        public string SelectedText { get => Mode switch { TextMode.Password => null, _ => _textBox.SelectedText }; }

        static TextBoxEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxEx), new FrameworkPropertyMetadata(typeof(TextBoxEx)));
        }


        private PasswordBox _password;
        private TextBox _textBox;
        private Border _border;
        public TextBoxEx()
        {
            KeyDown += TextBoxEx_KeyDown;
        }


        #region 

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _password = this.Template.FindName("PART_Password", this) as PasswordBox;
            _textBox = this.Template.FindName("PART_Main", this) as TextBox;
            _border = this.Template.FindName("PART_ICON", this) as Border;

            if (_password != null)
            {
                _password.PasswordChanged += _password_PasswordChanged;
                _password.Password = Text;
            }

            if (_textBox != null)
            {
                _textBox.PreviewTextInput += _textBox_PreviewTextInput;
                _textBox.TextChanged += _textBox_TextChanged;
            }

            if (_border != null)
            {
                _border.MouseDown += _border_MouseDown;
            }


        }

        #endregion

        private void TextBoxEx_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    if (!AcceptsReturn)
                    {
                        RaiseEntered(Text);
                        try
                        {
                            EnterCommand?.Execute(Text);
                        }
                        catch
                        {

                        }
                    }
                    break;
                case Key.Escape:

                    if (EscToClear)
                    {
                        Clear();
                    }
                    break;
                default:
                    break;
            }

        }

        private void _textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (Mode)
            {

                case TextMode.Password:
                case TextMode.Number:
                case TextMode.IMEDispaly:
                    if (_password.Password != Text)
                    {
                        _password.Password = Text;
                    }
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
                RaiseIconClicked(Text);
                try
                {
                    IconCommand?.Execute(Text);
                }
                catch
                {
                }
            }
        }

        private void _password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Text != _password.Password && Mode == TextMode.Password)
            {
                Text = _password.Password;
            }
        }




        #region 依赖属性


        #region IsReadOnly
        /// <summary>
        /// 请填写描述
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(TextBoxEx));

        #endregion

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
            DependencyProperty.Register("Mode", typeof(TextMode), typeof(TextBoxEx));

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
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxEx), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

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


        #region IsClip
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsClip
        {
            get { return (bool)GetValue(IsClipProperty); }
            set { SetValue(IsClipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClipProperty =
            DependencyProperty.Register("IsClip", typeof(bool), typeof(TextBoxEx));
        #endregion



        #region TextWrapping
        /// <summary>
        /// 请添加描述
        /// </summary>
        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextWrapping.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(TextBoxEx));
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

        #region AcceptsTab
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool AcceptsTab
        {
            get { return (bool)GetValue(AcceptsTabProperty); }
            set { SetValue(AcceptsTabProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AcceptsTab.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AcceptsTabProperty =
            DependencyProperty.Register("AcceptsTab", typeof(bool), typeof(TextBoxEx));
        #endregion


        #region CaretBrush
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush CaretBrush
        {
            get { return (Brush)GetValue(CaretBrushProperty); }
            set { SetValue(CaretBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaretBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaretBrushProperty =
            DependencyProperty.Register("CaretBrush", typeof(Brush), typeof(TextBoxEx));
        #endregion

        #region IsInactiveSelectionHighlightEnabled
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool IsInactiveSelectionHighlightEnabled
        {
            get { return (bool)GetValue(IsInactiveSelectionHighlightEnabledProperty); }
            set { SetValue(IsInactiveSelectionHighlightEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsInactiveSelectionHighlightEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsInactiveSelectionHighlightEnabledProperty =
            DependencyProperty.Register("IsInactiveSelectionHighlightEnabled", typeof(bool), typeof(TextBoxEx));
        #endregion


        #region SelectionBrush
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush SelectionBrush
        {
            get { return (Brush)GetValue(SelectionBrushProperty); }
            set { SetValue(SelectionBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionBrushProperty =
            DependencyProperty.Register("SelectionBrush", typeof(Brush), typeof(TextBoxEx));
        #endregion

        #region SelectionOpacity
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double SelectionOpacity
        {
            get { return (double)GetValue(SelectionOpacityProperty); }
            set { SetValue(SelectionOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionOpacityProperty =
            DependencyProperty.Register("SelectionOpacity", typeof(double), typeof(TextBoxEx));
        #endregion

        #region SelectionTextBrush
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush SelectionTextBrush
        {
            get { return (Brush)GetValue(SelectionTextBrushProperty); }
            set { SetValue(SelectionTextBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionTextBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionTextBrushProperty =
            DependencyProperty.Register("SelectionTextBrush", typeof(Brush), typeof(TextBoxEx));
        #endregion


        #region CharacterCasing
        /// <summary>
        /// 请添加描述
        /// </summary>
        public CharacterCasing CharacterCasing
        {
            get { return (CharacterCasing)GetValue(CharacterCasingProperty); }
            set { SetValue(CharacterCasingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharacterCasing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharacterCasingProperty =
            DependencyProperty.Register("CharacterCasing", typeof(CharacterCasing), typeof(TextBoxEx));
        #endregion

        #region AutoWordSelection
        /// <summary>
        /// 请添加描述
        /// </summary>
        public bool AutoWordSelection
        {
            get { return (bool)GetValue(AutoWordSelectionProperty); }
            set { SetValue(AutoWordSelectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoWordSelection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoWordSelectionProperty =
            DependencyProperty.Register("AutoWordSelection", typeof(bool), typeof(TextBoxEx));
        #endregion


        #region MaxLength
        /// <summary>
        /// 请添加描述
        /// </summary>
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBoxEx));
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

        #region IconMargin
        /// <summary>
        /// 请填写描述
        /// </summary>
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(TextBoxEx));

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

        #region EscToClear
        /// <summary>
        /// 清理
        /// </summary>
        public bool EscToClear
        {
            get { return (bool)GetValue(EscToClearProperty); }
            set { SetValue(EscToClearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EscToClear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EscToClearProperty =
            DependencyProperty.Register("EscToClear", typeof(bool), typeof(TextBoxEx));

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

        #region RouteEvent


        #region Entered
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TextBoxExEventHandler Entered
        {
            add { AddHandler(EnteredEvent, value); }
            remove { RemoveHandler(EnteredEvent, value); }
        }

        public static readonly RoutedEvent EnteredEvent = EventManager.RegisterRoutedEvent(
        "Entered", RoutingStrategy.Bubble, typeof(TextBoxExEventHandler), typeof(TextBoxEx));


        private void RaiseEntered(string newValue)
        {
            var arg = new TextBoxExEventArgs(newValue, EnteredEvent);
            RaiseEvent(arg);
        }

        #endregion


        #region IconClicked
        /// <summary>
        /// 请填写描述
        /// </summary>
        public event TextBoxExEventHandler IconClicked
        {
            add { AddHandler(IconClickedEvent, value); }
            remove { RemoveHandler(IconClickedEvent, value); }
        }

        public static readonly RoutedEvent IconClickedEvent = EventManager.RegisterRoutedEvent(
        "IconClicked", RoutingStrategy.Bubble, typeof(TextBoxExEventHandler), typeof(TextBoxEx));


        private void RaiseIconClicked(string newValue)
        {
            var arg = new TextBoxExEventArgs(newValue, IconClickedEvent);
            RaiseEvent(arg);
        }

        #endregion


        #endregion

        #region Public


        public void SelectAll()
        {
            switch (Mode)
            {
                case TextMode.General:
                case TextMode.Number:
                case TextMode.IMEDispaly:
                    _textBox.SelectAll();
                    break;
                case TextMode.Password:
                    _password.SelectAll();
                    break;
                default:
                    break;
            }

        }

        public void Paste()
        {
            switch (Mode)
            {
                case TextMode.General:
                case TextMode.Number:
                case TextMode.IMEDispaly:
                    _textBox.Paste();
                    break;
                case TextMode.Password:
                    _password.Paste();
                    break;
                default:
                    break;
            }

        }

        public void Clear()
        {
            Text = string.Empty;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            switch (Mode)
            {
                case TextMode.General:
                case TextMode.IMEDispaly:
                case TextMode.Number:
                    _textBox.Focus();
                    break;
                case TextMode.Password:
                    _password.Focus();
                    break;
                default:
                    break;
            }

            base.OnGotFocus(e);
        }
        #endregion

    }
}
