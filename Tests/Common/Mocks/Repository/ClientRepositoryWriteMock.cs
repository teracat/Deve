using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class ClientRepositoryWriteMock : Mock<IRepositoryWrite<Client>>
{
    public ClientRepositoryWriteMock()
    {
        _ = Setup(d => d.AddAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>())).Returns<Client, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>())).Returns<Client, CancellationToken>((client, _) => Task.FromResult(client.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
