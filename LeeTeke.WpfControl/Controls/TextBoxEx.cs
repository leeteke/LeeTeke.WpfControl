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
    /// 
    [TemplatePart(Name = ElementPassword, Type = typeof(PasswordBox))]
    [TemplatePart(Name = ElementMain, Type = typeof(TextBox))]
    [TemplatePart(Name = ElementICON, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ElementPlaceholder, Type = typeof(ContentPresenter))]
    public class TextBoxEx : System.Windows.Controls.Control
    {


        static TextBoxEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxEx), new FrameworkPropertyMetadata(typeof(TextBoxEx)));
        }

        #region consts

        private const string ElementPassword = "PART_Password";
        private const string ElementMain = "PART_Main";
        private const string ElementICON = "PART_ICON";
        private const string ElementPlaceholder = "PART_Placeholder";
        #endregion

        public string? SelectedText { get => Mode switch { TextMode.Password => null, _ => _textBox?.SelectedText }; }

        private PasswordBox? _password;
        private TextBox? _textBox;
        private ContentPresenter? _icon;
        private ContentPresenter? _placeholder;
        public TextBoxEx()
        {
            KeyDown += TextBoxEx_KeyDown;
            LostFocus += TextBoxEx_LostFocus;
            GotFocus += TextBoxEx_GotFocus;
        }

    
     


        #region 

        public override void OnApplyTemplate()
        {
            if (_icon != null)
                _icon.MouseUp -= Icon_MouseUp;

            if (_password != null)
                _password.PasswordChanged -= Password_PasswordChanged;

            if (_textBox != null)
            {
                _textBox.PreviewTextInput -= TextBox_PreviewTextInput;
                _textBox.TextChanged -= TextBox_TextChanged;
            }


            base.OnApplyTemplate();
            _password = GetTemplateChild(ElementPassword) as PasswordBox;
            _textBox = GetTemplateChild(ElementMain) as TextBox;
            _icon = GetTemplateChild(ElementICON) as ContentPresenter;
            _placeholder = GetTemplateChild(ElementPlaceholder) as ContentPresenter;
            if (_password != null)
            {
                _password.PasswordChanged += Password_PasswordChanged;
                _password.Password = Text;
            }

            if (_textBox != null)
            {
                _textBox.PreviewTextInput += TextBox_PreviewTextInput;
                _textBox.TextChanged += TextBox_TextChanged;
            }

            if (_icon != null)
            {
                _icon.MouseUp += Icon_MouseUp;
            }

            PlaceholderVisibly();

        }

        #endregion

        private void TextBoxEx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_placeholder != null && IsReadOnly == false)
            {
                _placeholder.Visibility = Visibility.Collapsed;
            }
        }

        private void TextBoxEx_LostFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderVisibly();
        }

        private void TextBoxEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
                return;
            switch (e.Key)
            {
                case Key.Enter:
                    if (!AcceptsReturn)
                    {
                        Keyboard.ClearFocus();
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (Mode)
            {

                case TextMode.Password:
                case TextMode.Number:
                case TextMode.IMEDispaly:
                    if (_password != null && _password.Password != Text)
                    {
                        _password.Password = Text;
                    }
                    break;
                default:
                    break;
            }

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Icon_MouseUp(object sender, MouseButtonEventArgs e)
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

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_password != null && Text != _password.Password && Mode == TextMode.Password)
            {
                Text = _password.Password;
            }
        }

        /// <summary>
        /// 控制显示
        /// </summary>
        private void PlaceholderVisibly()
        {
            if (_placeholder != null)
            {

                if ((_password?.IsFocused==true|| _textBox?.IsFocused==true) &&!IsReadOnly)
                {
                    _placeholder.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _placeholder.Visibility = string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
                }

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
        public string? Text
        {
            get { return (string?)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxEx), new FrameworkPropertyMetadata(default,  FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(TextChanged)) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged } );

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBoxEx ex)
            {
                ex.PlaceholderVisibly();
            }
        }

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


        #region ActiveBackground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush ActiveBackground
        {
            get { return (Brush)GetValue(ActiveBackgroundProperty); }
            set { SetValue(ActiveBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActiveBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveBackgroundProperty =
            DependencyProperty.Register("ActiveBackground", typeof(Brush), typeof(TextBoxEx));
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

        #region Placeholder
        /// <summary>
        /// 占位符号
        /// </summary>
        public object Placeholder
        {
            get { return (object)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(object), typeof(TextBoxEx));
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
        /// 请添加描述
        /// </summary>
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(TextBoxEx));
        #endregion


        #region IconFontFamily
        /// <summary>
        /// 请添加描述
        /// </summary>
        public FontFamily IconFontFamily
        {
            get { return (FontFamily)GetValue(IconFontFamilyProperty); }
            set { SetValue(IconFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontFamilyProperty =
            DependencyProperty.Register("IconFontFamily", typeof(FontFamily), typeof(TextBoxEx));
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


        #region IconFontSize
        /// <summary>
        /// 请添加描述
        /// </summary>
        public double IconFontSize
        {
            get { return (double)GetValue(IconFontSizeProperty); }
            set { SetValue(IconFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontSizeProperty =
            DependencyProperty.Register("IconFontSize", typeof(double), typeof(TextBoxEx));
        #endregion


        #region IconVerticalAlignment
        /// <summary>
        /// 请添加描述
        /// </summary>
        public VerticalAlignment IconVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(IconVerticalAlignmentProperty); }
            set { SetValue(IconVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconVerticalAlignmentProperty =
            DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment), typeof(TextBoxEx));
        #endregion


        #region IconHorizontalAlignment
        /// <summary>
        /// 请添加描述
        /// </summary>
        public HorizontalAlignment IconHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(IconHorizontalAlignmentProperty); }
            set { SetValue(IconHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconHorizontalAlignmentProperty =
            DependencyProperty.Register("IconHorizontalAlignment", typeof(HorizontalAlignment), typeof(TextBoxEx));
        #endregion

        #region IconForeground
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(Brush), typeof(TextBoxEx));
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

        #region EditBoxCursor
        /// <summary>
        /// 请添加描述
        /// </summary>
        public Cursor EditBoxCursor
        {
            get { return (Cursor)GetValue(EditBoxCursorProperty); }
            set { SetValue(EditBoxCursorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditBoxCursor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditBoxCursorProperty =
            DependencyProperty.Register("EditBoxCursor", typeof(Cursor), typeof(TextBoxEx));
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


        private void RaiseEntered(string? newValue)
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


        private void RaiseIconClicked(string? newValue)
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
                    _textBox?.SelectAll();
                    break;
                case TextMode.Password:
                    _password?.SelectAll();
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
                    _textBox?.Paste();
                    break;
                case TextMode.Password:
                    _password?.Paste();
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
                    _textBox?.Focus();
                    break;
                case TextMode.Password:
                    _password?.Focus();
                    break;
                default:
                    break;
            }

            base.OnGotFocus(e);
        }
        #endregion

    }
}
