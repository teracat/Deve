using Deve.ClientApp.Wpf.ViewModel;

namespace Deve.ClientApp.Wpf.Window
{
    public partial class CountryWindow : BaseEditWindow
    {
        #region Fields
        private readonly CountryViewModel _viewModel;
        #endregion

        #region Constructors
        public CountryWindow(Country country)
        {
            InitializeComponent();
            ViewModel = _viewModel = new CountryViewModel(this, country);
        }
        #endregion

        #region Overrides
        protected override void OnWindowLoaded()
        {
            base.OnWindowLoaded();
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
