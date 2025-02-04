namespace Deve.ClientApp.Wpf.Interfaces
{
    public interface INavigationAwareWithType<TParamType> where TParamType : class
    {
        void OnNavigatedToWithType(TParamType parameter);
    }
}
