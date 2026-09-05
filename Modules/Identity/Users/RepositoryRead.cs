using Microsoft.Extensions.Options;
using Deve.Options;

namespace Deve.Identity.Users;

internal sealed class RepositoryRead : IRepositoryRead<User>
{
    // You should implement real repository logic here
    public RepositoryRead(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.IdentityConnectionRead
        System.Diagnostics.Debug.WriteLine(options.Value.IdentityConnectionRead);
    }

    public IQueryable<User> GetAsQueryable() => Data.Users.AsQueryable();
}