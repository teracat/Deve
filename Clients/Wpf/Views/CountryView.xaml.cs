using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views;

internal sealed partial class CountryView : BaseEditView
{
    #region Fields
    private readonly CountryViewModel _viewModel;
    #endregion

    #region Constructors
    public CountryView(CountryViewModel viewModel)
    {
        InitializeComponent();

        // When the data is loaded, set initial focus
        viewModel.LoadDataDoneAction = new Action(() => { _ = uxName.Focus(); });

        // Set ViewModel
        ViewModel = _viewModel = viewModel;
    }
    #endregion

    #region Events
    private void OnNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Return)
        {
            _ = uxIsoCode.Focus();
        }
    }

    private void OnIsoCodeKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Return)
        {
            _ = _viewModel.DoSave();
        }
    }
    #endregion
}