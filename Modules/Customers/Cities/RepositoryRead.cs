using Microsoft.Extensions.Options;
using Deve.Options;

namespace Deve.Customers.Cities;

internal sealed class RepositoryRead : IRepositoryRead<City>
{
    // You should implement real repository logic here
    public RepositoryRead(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.CustomersConnectionRead
        System.Diagnostics.Debug.WriteLine(options.Value.CustomersConnectionRead);
    }

    public IQueryable<City> GetAsQueryable() => Data.Cities.AsQueryable();
}