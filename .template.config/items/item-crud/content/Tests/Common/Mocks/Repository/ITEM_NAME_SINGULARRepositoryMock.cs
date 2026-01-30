using Moq;
using Deve.Repositories;
using Deve.MODULE_NAME.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class ITEM_NAME_SINGULARRepositoryMock : Mock<IRepository<ITEM_NAME_SINGULAR>>
{
    internal readonly IList<ITEM_NAME_SINGULAR> _data =
    [
        new ITEM_NAME_SINGULAR() { Id = TestsConstants.DefaultValidId, Name = "ITEM_NAME_SINGULAR 1" },
        new ITEM_NAME_SINGULAR() { Id = Guid.NewGuid(), Name = "ITEM_NAME_SINGULAR 2" },
        new ITEM_NAME_SINGULAR() { Id = Guid.NewGuid(), Name = "ITEM_NAME_SINGULAR 3" },
    ];

    public ITEM_NAME_SINGULARRepositoryMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
        _ = Setup(d => d.AddAsync(It.IsAny<ITEM_NAME_SINGULAR>(), It.IsAny<CancellationToken>())).Returns<ITEM_NAME_SINGULAR, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<ITEM_NAME_SINGULAR>(), It.IsAny<CancellationToken>())).Returns<ITEM_NAME_SINGULAR, CancellationToken>((entity, _) => Task.FromResult(entity.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
