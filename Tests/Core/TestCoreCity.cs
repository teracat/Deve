using Deve.Internal;
using Deve.Core;

namespace Deve.Tests.Core
{
    public class TestCoreCity : TestCity<ICore>
    {
        protected override ICore CreateData() => TestsCoreHelpers.CreateCore();

        protected override IDataAll<City, City, CriteriaCity> GetDataAll(ICore core) => core.Cities;
    }
}