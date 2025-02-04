using Deve.ClientApp.Wpf.ViewModels;

namespace Deve.ClientApp.Wpf.Views
{
    public partial class CountryView : BaseEditView
    {
        #region Fields
        private readonly CountryViewModel _viewModel;
        #endregion

        #region Constructors
        public CountryView(CountryViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = _viewModel = viewModel;
            uxName.Focus();
        }
        #endregion

        #region Events
        private void OnNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
                uxIsoCode.Focus();
        }

        private void OnIsoCodeKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
                _ = _viewModel.DoSave();
        }
        #endregion
    }
}
