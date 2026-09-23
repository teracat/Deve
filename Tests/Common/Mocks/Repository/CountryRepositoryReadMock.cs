using Moq;
using Deve.Customers.Entities;
using Deve.Repositories;

namespace Deve.Tests.Mocks.Repository;

internal class CountryRepositoryReadMock : Mock<IRepositoryRead<Country>>
{
    internal readonly IList<Country> _data =
    [
        new Country() { Id = TestsConstants.SpainCountryId, Name = "España", IsoCode = "ES" },
        new Country() { Id = TestsConstants.UsaCountryId, Name = "USA", IsoCode = "US" },
        new Country() { Id = TestsConstants.FranceCountryId, Name = "France", IsoCode = "FR" },
    ];

    public CountryRepositoryReadMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
    }
}
