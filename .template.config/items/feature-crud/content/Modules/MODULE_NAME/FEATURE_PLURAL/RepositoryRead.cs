using Microsoft.Extensions.Options;
using Deve.Options;

namespace Deve.MODULE_NAME.FEATURE_PLURAL;

internal sealed class RepositoryRead : IRepositoryRead<FEATURE_SINGULAR>
{
    // You should implement real repository logic here
    public RepositoryRead(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.MODULE_NAMEConnectionRead
        System.Diagnostics.Debug.WriteLine(options.Value.MODULE_NAMEConnectionRead);
    }

    public IQueryable<FEATURE_SINGULAR> GetAsQueryable() => Data.FEATURE_PLURAL.AsQueryable();
}
