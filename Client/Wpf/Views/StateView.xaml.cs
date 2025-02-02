using Deve.ClientApp.Wpf.ViewModels;

namespace Deve.ClientApp.Wpf.Views
{
    public partial class StateView : BaseEditView
    {
        #region Fields
        private readonly BaseEditViewModel _viewModel;
        #endregion

        #region Constructors
        public StateView(State state)
        {
            InitializeComponent();
            ViewModel = _viewModel = new StateViewModel(state);
            uxName.Focus();
        }
        #endregion

        #region Events
        private void OnNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
                uxCountry.Focus();
        }

        private void OnCountryKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
                _ = _viewModel.Save();
        }
        #endregion
    }
}
