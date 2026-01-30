using Deve.Data;

namespace Deve.Tests;

public abstract class BaseTest<TDataType> where TDataType : IData
{
    protected IDataFixture<TDataType> Fixture { get; }

    protected BaseTest(IDataFixture<TDataType> fixture)
    {
        Fixture = fixture;
    }
}
