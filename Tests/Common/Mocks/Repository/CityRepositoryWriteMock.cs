using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class CityRepositoryWriteMock : Mock<IRepositoryWrite<City>>
{
    public CityRepositoryWriteMock()
    {
        _ = Setup(d => d.AddAsync(It.IsAny<City>(), It.IsAny<CancellationToken>())).Returns<City, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<City>(), It.IsAny<CancellationToken>())).Returns<City, CancellationToken>((city, _) => Task.FromResult(city.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
