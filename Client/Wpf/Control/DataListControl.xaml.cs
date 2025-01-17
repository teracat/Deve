using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Deve.ClientApp.Wpf.Control
{
    /// <summary>
    /// Interaction logic for DataListControl.xaml
    /// https://stackoverflow.com/questions/29126224/how-do-i-bind-wpf-commands-between-a-usercontrol-and-a-parent-window
    /// </summary>
    public partial class DataListControl : UserControl
    {
        #region Static Properties
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(DataListControl), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(DataListControl), new FrameworkPropertyMetadata(OnIsBusyChanged));
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(nameof(Items), typeof(IEnumerable<ListData>), typeof(DataListControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(DataListControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register(nameof(AddCommand), typeof(ICommand), typeof(DataListControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty SearchCommandProperty = DependencyProperty.Register(nameof(SearchCommand), typeof(ICommand), typeof(DataListControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(nameof(SearchText), typeof(string), typeof(DataListControl), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ErrorTextProperty = DependencyProperty.Register(nameof(ErrorText), typeof(string), typeof(DataListControl), new FrameworkPropertyMetadata(OnErrorTextChanged));
        #endregion

        #region Properties
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public IEnumerable<ListData> Items
        {
            get => (IEnumerable<ListData>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        #endregion

        #region Constructor
        public DataListControl()
        {
            InitializeComponent();
            ItemTemplate = (DataTemplate)Application.Current.TryFindResource("DefaultDataListItemTemplate");
            UpdateIsBusy(false);
        }
        #endregion

        #region Methods
        private void UpdateIsBusy(bool isBusy)
        {
            if (isBusy)
            {
                // IsBusy
                uxAddButton.Visibility = Visibility.Collapsed;
                uxSearchButton.Visibility = Visibility.Collapsed;
                uxSearchTextBox.Visibility = Visibility.Collapsed;
                uxLoadingImage.Visibility = Visibility.Visible;
            }
            else
            {
                // IsIdle
                uxLoadingImage.Visibility = Visibility.Collapsed;
                uxSearchButton.Visibility = Visibility.Visible;
                uxSearchTextBox.Visibility = Visibility.Visible;
                uxAddButton.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Events
        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (DataListControl)d;
            control?.UpdateIsBusy((bool)e.NewValue);
        }

        private static void OnErrorTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (DataListControl)d;
            var errorText = (string)e.NewValue;
            control.uxErrorTextLabel.Content = errorText;
            if (string.IsNullOrWhiteSpace(errorText))
                control.uxErrorTextLabel.Visibility = Visibility.Collapsed;
            else
                control.uxErrorTextLabel.Visibility = Visibility.Visible;
        }

        private void OnSearchTextBoxKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && SearchCommand is not null && SearchCommand.CanExecute(this))
                SearchCommand.Execute(this);
        }
        #endregion
    }
}