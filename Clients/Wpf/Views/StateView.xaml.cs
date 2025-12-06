using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public partial class StateView : BaseEditView
    {
        #region Constructors
        public StateView(StateViewModel viewModel)
        {
            InitializeComponent();

            // When the data is loaded, set initial focus
            viewModel.LoadDataDoneAction = new Action(() => { _ = uxName.Focus(); });

            // Set ViewModel
            ViewModel = viewModel;
        }
        #endregion

        #region Events
        private void OnNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                _ = uxCountry.Focus();
            }
        }
        #endregion
    }
}