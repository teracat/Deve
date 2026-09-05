using Moq;
using Deve.Repositories;
using Deve.MODULE_NAME.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class FEATURE_SINGULARRepositoryReadMock : Mock<IRepositoryRead<FEATURE_SINGULAR>>
{
    internal readonly IList<FEATURE_SINGULAR> _data =
    [
        new FEATURE_SINGULAR() { Id = TestsConstants.DefaultValidId, Name = "FEATURE_SINGULAR 1" },
        new FEATURE_SINGULAR() { Id = Guid.NewGuid(), Name = "FEATURE_SINGULAR 2" },
        new FEATURE_SINGULAR() { Id = Guid.NewGuid(), Name = "FEATURE_SINGULAR 3" },
    ];

    public FEATURE_SINGULARRepositoryReadMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
    }
}
