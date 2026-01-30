using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;
using Deve.Customers.Enums;

namespace Deve.Tests.Mocks.Repository;

internal class ClientRepositoryMock : Mock<IRepository<Client>>
{
    internal readonly IList<Client> _data =
    [
        new Client() { Id = TestsConstants.TeracatClientId, Name = "Jordi Badia", TradeName = "Teracat", Balance = 50, Status = ClientStatus.Active, TaxName = "Jordi Badia Santaulària", CityId = TestsConstants.SantpedorCityId },
        new Client() { Id = TestsConstants.MicrosoftClientId, Name = "Microsoft", TradeName = "Microsoft", Balance = 1000, Status = ClientStatus.Inactive, TaxName = "Microsoft Corporation", CityId = TestsConstants.RedmondCityId },
        new Client() { Id = TestsConstants.FakeCompanyClientId, Name = "Fake Company", TradeName = "Fake", Balance = 500, Status = ClientStatus.Active, TaxName = "Fake Corporation", CityId = TestsConstants.SantpedorCityId },
    ];

    public ClientRepositoryMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
        _ = Setup(d => d.AddAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>())).Returns<Client, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>())).Returns<Client, CancellationToken>((client, _) => Task.FromResult(client.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
