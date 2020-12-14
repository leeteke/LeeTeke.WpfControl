using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    /// TextBoxEx.xaml 的交互逻辑
    /// </summary>
    public partial class TextBoxEx : UserControl
    {

        public TextBoxEx()
        {
            InitializeComponent();
            main.DataContext = this;
            FontSize = 15;
            this.SetResourceReference(ToggleGroup.FocusVisualStyleProperty, "LeeFocusVisual");
            this.SetResourceReference(ToggleGroup.SnapsToDevicePixelsProperty, "LeeSnapsToDevicePixels");
        }


        public event RoutedEventHandler Enter
        {
            add { AddHandler(EnterEvent, value); }
            remove { RemoveHandler(EnterEvent, value); }
        }

        public static readonly RoutedEvent EnterEvent = EventManager.RegisterRoutedEvent(
        "Enter", RoutingStrategy.Bubble, typeof(EventHandler<TextBoxExEnterEventArgs>), typeof(TextBoxEx));


        #region 依赖


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

        #region BorderThickness
        /// <summary>
        /// BorderThickness
        /// </summary>
        public new Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderThickness.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(TextBoxEx), new PropertyMetadata(new Thickness(0, 0, 0, 1)));




        #endregion

   


        #region Icon

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(TextBoxEx), new PropertyMetadata("", new PropertyChangedCallback(IconChanged)));

        private static void IconChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBoxEx textBoxEx)
            {
                string value = (string)e.NewValue;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    textBoxEx.Icon = value;
                    textBoxEx.txtIcon.Margin = new Thickness(5, 0, 5, 0);
                }
                else
                {
                    textBoxEx.txtIcon.Margin = default;
                }

            }
        }



        #endregion

        #region IconDock
        /// <summary>
        /// Icon的位置
        /// </summary>
        public Dock IconDock
        {
            get { return (Dock)GetValue(IconDockProperty); }
            set { SetValue(IconDockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconDock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconDockProperty =
            DependencyProperty.Register("IconDock", typeof(Dock), typeof(TextBoxEx), new PropertyMetadata(Dock.Left, new PropertyChangedCallback(IconDockChanged)));

        private static void IconDockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBoxEx leeNotify && e.NewValue != null)
            {
         
                switch ((Dock)e.NewValue)
                {
                    case Dock.Left:
                    case Dock.Right:
                        leeNotify.separator.Width = 1;
                        leeNotify.separator.Height = double.NaN;
                    
                        leeNotify.separator.Margin = new Thickness(2, 4, 2, 4);
                        break;

                    case Dock.Top:
                    case Dock.Bottom:
                        leeNotify.separator.Height = 1;
                        leeNotify.separator.Width = double.NaN;
                        leeNotify.separator.Margin = new Thickness(4, 2, 4, 2);
                        break;
                    default:
                        break;
                }
            }
        }


        #endregion


        #region IconCanClick
        /// <summary>           
        /// Icon可以点击
        /// </summary>
        public bool IconCanClick
        {
            get { return (bool)GetValue(IconCanClickProperty); }
            set { SetValue(IconCanClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconCanClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconCanClickProperty =
            DependencyProperty.Register("IconCanClick", typeof(bool), typeof(TextBoxEx), new PropertyMetadata(false));

        #endregion


        #region SeparatorShow
        /// <summary>
        /// 分割线显示
        /// </summary>
        public Visibility SeparatorShow
        {
            get { return (Visibility)GetValue(SeparatorShowProperty); }
            set { SetValue(SeparatorShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeparatorShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorShowProperty =
            DependencyProperty.Register("SeparatorShow", typeof(Visibility), typeof(TextBoxEx), new PropertyMetadata(Visibility.Collapsed));

        #endregion

        #region IsPassword


        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);

            }
        }

        // Using a DependencyProperty as the backing store for IsPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPasswordProperty =
            DependencyProperty.Register("IsPassword", typeof(bool), typeof(TextBoxEx), new PropertyMetadata(false, new PropertyChangedCallback(IsPasswordChanged)));

        private static void IsPasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBoxEx textBoxEx)
            {
                bool value = (bool)e.NewValue;
                if (value)
                {
                    textBoxEx.passwordBox.Visibility = Visibility.Visible;
                    textBoxEx.textBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    textBoxEx.passwordBox.Visibility = Visibility.Collapsed;
                    textBoxEx.textBox.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion


        #region Content
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(TextBoxEx),new PropertyMetadata(null,new PropertyChangedCallback(ContentDo)));


        private static void ContentDo(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBoxEx textBoxEx)
            {
                string value = (string)e.NewValue;
                if (textBoxEx.IsPassword)
                {
                    textBoxEx.passwordBox.Password = value;
                    SetPasswordBoxSelection(textBoxEx.passwordBox);
                }
                if (!string.IsNullOrWhiteSpace(value))
                {
                    textBoxEx.txtPreview.Visibility = Visibility.Collapsed;
                }
                else
                {
                    textBoxEx.txtPreview.Visibility = Visibility.Visible;
                }
            }
        }

        private static void SetPasswordBoxSelection(PasswordBox passwordBox)
        {
            try
            {

                var select = passwordBox.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                select.Invoke(passwordBox, new object[] { passwordBox.Password.Length, 0 });
            }
            catch
            {


            }
        }

        #endregion

        #region PreviewText


        public string PreviewText
        {
            get { return (string)GetValue(PreviewTextProperty); }
            set { SetValue(PreviewTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreviewText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewTextProperty =
            DependencyProperty.Register("PreviewText", typeof(string), typeof(TextBoxEx), new PropertyMetadata(string.Empty));

    

        #endregion


        #region Padding
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Padding.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register("Padding", typeof(Thickness), typeof(TextBoxEx));


        #endregion



        #region VerticalContentAlignment
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalContentAlignment.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register("VerticalContentAlignment", typeof(VerticalAlignment), typeof(TextBoxEx),new PropertyMetadata(VerticalAlignment.Center));

        #endregion


        #region HorizontalContentAlignment
        /// <summary>
        /// 请填写描述
        /// </summary>
        public new HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalContentAlignment.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(TextBoxEx),new PropertyMetadata(HorizontalAlignment.Stretch));

        #endregion


        #endregion

        #region EnterCommand


        public ICommand EnterCommand
        {
            get { return (ICommand)GetValue(EnterCommandProperty); }
            set { SetValue(EnterCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnterCommandProperty =
            DependencyProperty.Register("EnterCommand", typeof(ICommand), typeof(TextBoxEx), new PropertyMetadata(default));



        #endregion

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPreview.Visibility = Visibility.Collapsed;
            if (IsPassword)
            {
                passwordBox.Focus();
            }
            else
            {
                textBox.Focus();
            }

            BorderBrush = Application.Current.Resources["LeeBrush_Main"] as SolidColorBrush;
            txtIcon.Foreground = Application.Current.Resources["LeeBrush_Main"] as SolidColorBrush;
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsPassword)
            {
                if (string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    txtPreview.Visibility = Visibility.Visible;
                }
                BorderBrush = Application.Current.Resources["LeeBrush_Gray153"] as SolidColorBrush;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    txtPreview.Visibility = Visibility.Visible;
                }
                BorderBrush = Application.Current.Resources["LeeBrush_Gray153"] as SolidColorBrush;
            }

            txtIcon.Foreground = Application.Current.Resources["LeeBrush_Gray153"] as SolidColorBrush;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!passwordBox.IsFocused && !textBox.IsFocused)
            {
                BorderBrush = Application.Current.Resources["LeeBrush_Main"] as SolidColorBrush;
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!passwordBox.IsFocused && !textBox.IsFocused)
            {
                BorderBrush = Application.Current.Resources["LeeBrush_Gray153"] as SolidColorBrush;
            }
        }

        private void TxtPreview_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtPreview.Visibility = Visibility.Collapsed;
            if (IsPassword)
            {
                passwordBox.Focus();
            }
            else
            {
                textBox.Focus();
            }
        }



        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pwd)
            {
                Content = pwd.Password;
            }
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterCommand?.Execute(Content);
                TextBoxExEnterEventArgs eventArgs = new TextBoxExEnterEventArgs(EnterEvent, this);
                RaiseEvent(eventArgs);
            }
        }



        private void iconGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IconCanClick)
            {
                iconGrid.Background = new SolidColorBrush(new Color() { R = 0, G = 0, B = 0, A = 20 });
            }
        }

        private void iconGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IconCanClick)
            {
                iconGrid.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void iconGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IconCanClick)
            {
                if (iconGrid.IsMouseOver)
                {
                    iconGrid.Background = new SolidColorBrush(new Color() { R = 0, G = 0, B = 0, A = 20 });
                }
                EnterCommand?.Execute(Content);
                TextBoxExEnterEventArgs eventArgs = new TextBoxExEnterEventArgs(EnterEvent, this);
                RaiseEvent(eventArgs);
            }
        }

        private void iconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IconCanClick)
            {
                iconGrid.Background = new SolidColorBrush(new Color() { R = 0, G = 0, B = 0, A = 40 });
            }
        }
    }

    public class TextBoxExEnterEventArgs : RoutedEventArgs
    {
        public TextBoxExEnterEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        { }
    }
}
