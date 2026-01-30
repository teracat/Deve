using ReactiveUI.SourceGenerators;
using Deve.Clients.Maui.Interfaces;
using Deve.Customers.Clients;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class ClientDetailsViewModel : BaseDetailsViewModel
{
    #region Atributes
    [Reactive]
    private ClientResponse? _client;
    #endregion

    #region Constructor
    public ClientDetailsViewModel(INavigationService navigationService, Data.IData data, ISchedulerProvider scheduler)
        : base(navigationService, data, scheduler)
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