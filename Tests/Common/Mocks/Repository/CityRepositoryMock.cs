using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class CityRepositoryMock : Mock<IRepository<City>>
{
    internal readonly IList<City> _data =
    [
        new City() { Id = TestsConstants.SantpedorCityId, Name = "Santpedor", StateId = TestsConstants.BarcelonaStateId,  },
        new City() { Id = TestsConstants.BarcelonaCityId, Name = "Barcelona", StateId = TestsConstants.BarcelonaStateId },
        new City() { Id = TestsConstants.WashingtonDCCityId, Name = "Washington DC", StateId = TestsConstants.WashingtonStateId },
        new City() { Id = TestsConstants.RedmondCityId, Name = "Redmond", StateId = TestsConstants.WashingtonStateId },
    ];
    public CityRepositoryMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
        _ = Setup(d => d.AddAsync(It.IsAny<City>(), It.IsAny<CancellationToken>())).Returns<City, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<City>(), It.IsAny<CancellationToken>())).Returns<City, CancellationToken>((city, _) => Task.FromResult(city.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
