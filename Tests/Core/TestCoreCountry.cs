using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreCountry : TestCountry<ICore>
    {
        protected override ICore CreateData() => TestsCoreHelpers.CreateCore();

        protected override IDataAll<Country, Country, CriteriaCountry> GetDataAll(ICore core) => core.Countries;
    }
}