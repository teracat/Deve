using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class CityRepositoryReadMock : Mock<IRepositoryRead<City>>
{
    internal readonly IList<City> _data =
    [
        new City() { Id = TestsConstants.SantpedorCityId, Name = "Santpedor", StateId = TestsConstants.BarcelonaStateId,  },
        new City() { Id = TestsConstants.BarcelonaCityId, Name = "Barcelona", StateId = TestsConstants.BarcelonaStateId },
        new City() { Id = TestsConstants.WashingtonDCCityId, Name = "Washington DC", StateId = TestsConstants.WashingtonStateId },
        new City() { Id = TestsConstants.RedmondCityId, Name = "Redmond", StateId = TestsConstants.WashingtonStateId },
    ];

    public CityRepositoryReadMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
    }
}
