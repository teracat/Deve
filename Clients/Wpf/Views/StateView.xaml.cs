using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public partial class StateView : BaseEditView
    {
        #region Fields
        private readonly BaseEditViewModel _viewModel;
        #endregion

        #region Constructors
        public StateView(StateViewModel viewModel)
        {
            InitializeComponent();

            // When the data is loaded, set initial focus
            viewModel.LoadDataDoneAction = new Action(() => { uxName.Focus(); });

            // Set ViewModel
            ViewModel = _viewModel = viewModel;
        }
        #endregion

        #region Events
        private void OnNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                uxCountry.Focus();
            }
        }

        private void OnCountryKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                _ = _viewModel.DoSave();
            }
        }
        #endregion
    }
}