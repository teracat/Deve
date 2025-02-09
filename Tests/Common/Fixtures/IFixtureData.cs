using Deve.Data;

namespace Deve.Tests
{
    public interface IFixtureData<TDataType> where TDataType : IDataCommon
    {
        TDataType DataNoAuth { get; }
        TDataType DataValidAuth { get; }
    }
}