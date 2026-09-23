using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class StateRepositoryWriteMock : Mock<IRepositoryWrite<State>>
{
    public StateRepositoryWriteMock()
    {
        _ = Setup(d => d.AddAsync(It.IsAny<State>(), It.IsAny<CancellationToken>())).Returns<State, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<State>(), It.IsAny<CancellationToken>())).Returns<State, CancellationToken>((state, _) => Task.FromResult(state.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
