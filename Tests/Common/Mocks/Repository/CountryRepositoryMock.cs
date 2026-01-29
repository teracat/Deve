using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class CountryRepositoryMock : Mock<IRepository<Country>>
{
    internal readonly IList<Country> _data =
    [
        new Country() { Id = TestsConstants.SpainCountryId, Name = "España", IsoCode = "ES" },
        new Country() { Id = TestsConstants.UsaCountryId, Name = "USA", IsoCode = "US" },
        new Country() { Id = TestsConstants.FranceCountryId, Name = "France", IsoCode = "FR" },
    ];

    public CountryRepositoryMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
        _ = Setup(d => d.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>())).Returns<Country, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>())).Returns<Country, CancellationToken>((country, _) => Task.FromResult(country.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
