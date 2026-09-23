using Microsoft.Extensions.Options;
using Deve.Options;

namespace Deve.Customers.States;

internal sealed class RepositoryRead : IRepositoryRead<State>
{
    // You should implement real repository logic here
    public RepositoryRead(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.CustomersConnectionRead
        System.Diagnostics.Debug.WriteLine(options.Value.CustomersConnectionRead);
    }

    public IQueryable<State> GetAsQueryable() => Data.States.AsQueryable();
}