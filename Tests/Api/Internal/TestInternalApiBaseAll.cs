using Deve.Internal.Api;

namespace Deve.Tests.Api.Internal
{
    public abstract class TestInternalApiBaseAll<Model> : TestApiBaseAll<Program, Model> where Model: ModelId
    {
        public TestInternalApiBaseAll(FixtureApiInternal fixture)
            : base(fixture)
        {
        }
    }
}