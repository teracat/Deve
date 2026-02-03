using Moq;
using Deve.Repositories;
using Deve.MODULE_NAME.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class FEATURE_SINGULARRepositoryMock : Mock<IRepository<FEATURE_SINGULAR>>
{
    internal readonly IList<FEATURE_SINGULAR> _data =
    [
        new FEATURE_SINGULAR() { Id = TestsConstants.DefaultValidId, Name = "FEATURE_SINGULAR 1" },
        new FEATURE_SINGULAR() { Id = Guid.NewGuid(), Name = "FEATURE_SINGULAR 2" },
        new FEATURE_SINGULAR() { Id = Guid.NewGuid(), Name = "FEATURE_SINGULAR 3" },
    ];

    public FEATURE_SINGULARRepositoryMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
        _ = Setup(d => d.AddAsync(It.IsAny<FEATURE_SINGULAR>(), It.IsAny<CancellationToken>())).Returns<FEATURE_SINGULAR, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<FEATURE_SINGULAR>(), It.IsAny<CancellationToken>())).Returns<FEATURE_SINGULAR, CancellationToken>((entity, _) => Task.FromResult(entity.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
