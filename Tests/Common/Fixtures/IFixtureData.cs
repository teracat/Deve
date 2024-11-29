namespace Deve.Tests
{
    public interface IFixtureData<TDataType> where TDataType : IDataCommon
    {
        TDataType Data { get; }
    }
}
