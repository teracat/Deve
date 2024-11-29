namespace Deve.Tests
{
    public interface IFixtureDataLogged<TDataType> : IFixtureData<TDataType> where TDataType : IDataCommon
    {
    }
}
