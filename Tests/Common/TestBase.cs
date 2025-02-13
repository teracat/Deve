using Deve.Data;

namespace Deve.Tests
{
    public abstract class TestBase<TDataType> where TDataType : IDataCommon
    {
        protected IFixtureData<TDataType> Fixture { get; private set; }

        protected TestBase(IFixtureData<TDataType> fixture)
        {
            Fixture = fixture;
        }
    }
}