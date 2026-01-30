using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Customers.Clients;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class ClientDetailsViewModel : BaseDetailsViewModel
{
    #region Atributes
    [ObservableProperty]
    private ClientResponse? _client;
    #endregion

    #region Constructor
    public ClientDetailsViewModel(INavigationService navigationService, Data.IData data)
        : base(navigationService, data)
    {
    }
    #endregion

    #region Methods
    protected override async Task GetData()
    {
        var res = await Data.Customers.Clients.GetByIdAsync(Id);
        if (!res.Success)
        {
            ErrorText = Utils.ErrorsToString(res.Errors);
            return;
        }

        Client = res.Data;
    }
    #endregion
}