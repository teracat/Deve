namespace Deve.Clients.Wpf.Interfaces
{
    public interface INavigationAwareWithType<in TParamType> where TParamType : class
    {
        void OnNavigatedToWithType(TParamType parameter);
    }
}
