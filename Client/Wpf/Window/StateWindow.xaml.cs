using Deve.ClientApp.Wpf.ViewModel;

namespace Deve.ClientApp.Wpf.Window
{
    public partial class StateWindow : BaseEditWindow
    {
        #region Fields
        private readonly BaseEditViewModel _viewModel;
        #endregion

        #region Constructors
        public StateWindow(State state)
        {
            InitializeComponent();
            ViewModel = _viewModel = new StateViewModel(this, state);
        }
        #endregion

        #region Overrides
        protected override void OnWindowLoaded()
        {
            uxName.Focus();
            base.OnWindowLoaded();
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
                _ = _viewModel.DoSave();
        }
        #endregion
    }
}
