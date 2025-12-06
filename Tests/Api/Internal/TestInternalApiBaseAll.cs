using Deve.Internal.Api;
using Deve.Model;
using Deve.Tests.Api.Internal.Fixture;

namespace Deve.Tests.Api.Internal
{
    public abstract class TestInternalApiBaseAll<Model> : TestApiBaseAll<Program, Model> where Model : ModelId
    {
        protected TestInternalApiBaseAll(FixtureApiInternal fixture)
            : base(fixture)
        {
        }
    }
}