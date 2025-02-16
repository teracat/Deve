using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Data;

namespace Deve.Tests.Sdk.Fixtures
{
    public interface IFixtureSdkDataTypeBuilderExternal<TEntryPoint, TDataType> where TEntryPoint : class where TDataType : IDataCommon
    {
        TDataType CreateData(WebApplicationFactory<TEntryPoint> factory);
    }
}