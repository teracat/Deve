using Moq;
using Deve.Repositories;
using Deve.MODULE_NAME.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class FEATURE_SINGULARRepositoryWriteMock : Mock<IRepositoryWrite<FEATURE_SINGULAR>>
{
    public FEATURE_SINGULARRepositoryWriteMock()
    {
        _ = Setup(d => d.AddAsync(It.IsAny<FEATURE_SINGULAR>(), It.IsAny<CancellationToken>())).Returns<FEATURE_SINGULAR, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<FEATURE_SINGULAR>(), It.IsAny<CancellationToken>())).Returns<FEATURE_SINGULAR, CancellationToken>((entity, _) => Task.FromResult(entity.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
