using Moq;
using Deve.Repositories;
using Deve.Customers.Entities;

namespace Deve.Tests.Mocks.Repository;

internal class StateRepositoryMock : Mock<IRepository<State>>
{
    internal readonly IList<State> _data =
    [
        new State() { Id = TestsConstants.BarcelonaStateId, Name = "Barcelona", CountryId = TestsConstants.SpainCountryId },
        new State() { Id = TestsConstants.WashingtonStateId, Name = "Washington", CountryId = TestsConstants.UsaCountryId },
        new State() { Id = TestsConstants.MadridStateId, Name = "Madrid", CountryId = TestsConstants.SpainCountryId },
    ];

    public StateRepositoryMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
        _ = Setup(d => d.AddAsync(It.IsAny<State>(), It.IsAny<CancellationToken>())).Returns<State, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<State>(), It.IsAny<CancellationToken>())).Returns<State, CancellationToken>((state, _) => Task.FromResult(state.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
