namespace Deve.Clients.Wpf.Interfaces;

internal interface INavigationAwareWithType<in TParamType> where TParamType : class
{
    void OnNavigatedToWithType(TParamType parameter);
}
