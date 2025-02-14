using Deve.Data;

namespace Deve.Tests
{
    public interface IFixtureData<out TDataType> where TDataType : IDataCommon
    {
        TDataType DataNoAuth { get; }
        TDataType DataValidAuth { get; }
    }
}