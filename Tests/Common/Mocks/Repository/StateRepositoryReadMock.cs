using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class StateRepositoryReadMock : Mock<IRepositoryRead<State>>
{
    internal readonly IList<State> _data =
    [
        new State() { Id = TestsConstants.BarcelonaStateId, Name = "Barcelona", CountryId = TestsConstants.SpainCountryId },
        new State() { Id = TestsConstants.WashingtonStateId, Name = "Washington", CountryId = TestsConstants.UsaCountryId },
        new State() { Id = TestsConstants.MadridStateId, Name = "Madrid", CountryId = TestsConstants.SpainCountryId },
    ];

    public StateRepositoryReadMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
    }
}
