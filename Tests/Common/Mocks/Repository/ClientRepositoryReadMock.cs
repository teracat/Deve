using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;
using Deve.Customers.Enums;

namespace Deve.Tests.Mocks.Repository;

internal class ClientRepositoryReadMock : Mock<IRepositoryRead<Client>>
{
    internal readonly IList<Client> _data =
    [
        new Client() { Id = TestsConstants.TeracatClientId, Name = "Jordi Badia", TradeName = "Teracat", Balance = 50, Status = ClientStatus.Active, TaxName = "Jordi Badia Santaulària", CityId = TestsConstants.SantpedorCityId },
        new Client() { Id = TestsConstants.MicrosoftClientId, Name = "Microsoft", TradeName = "Microsoft", Balance = 1000, Status = ClientStatus.Inactive, TaxName = "Microsoft Corporation", CityId = TestsConstants.RedmondCityId },
        new Client() { Id = TestsConstants.FakeCompanyClientId, Name = "Fake Company", TradeName = "Fake", Balance = 500, Status = ClientStatus.Active, TaxName = "Fake Corporation", CityId = TestsConstants.SantpedorCityId },
    ];

    public ClientRepositoryReadMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
    }
}
