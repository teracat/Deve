using System.Text;
using System.Text.Json;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api
{
    /// <summary>
    /// Api tests base class.
    /// </summary>
    public abstract class TestApiBase<TEntryPoint> where TEntryPoint : class
    {
        protected FixtureApiClients<TEntryPoint> Fixture { get; private set; }

        public TestApiBase(FixtureApiClients<TEntryPoint> fixture)
        {
            Fixture = fixture;
        }

        protected HttpContent ToHttpContent(object data)
        {
            var json = JsonSerializer.Serialize(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}