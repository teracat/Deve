using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class CountryRepositoryWriteMock : Mock<IRepositoryWrite<Country>>
{
    public CountryRepositoryWriteMock()
    {
        _ = Setup(d => d.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>())).Returns<Country, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>())).Returns<Country, CancellationToken>((country, _) => Task.FromResult(country.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
