using Microsoft.Extensions.Options;
using Deve.Api.Options;

namespace Deve.MODULE_NAME.FEATURE_PLURAL;

internal static class Data
{
    public static readonly List<FEATURE_SINGULAR> FEATURE_PLURAL =
    [
        new FEATURE_SINGULAR() { Id = Guid.NewGuid(), Name = "FEATURE_SINGULAR 1" },
        new FEATURE_SINGULAR() { Id = Guid.NewGuid(), Name = "FEATURE_SINGULAR 2" },
    ];
}
