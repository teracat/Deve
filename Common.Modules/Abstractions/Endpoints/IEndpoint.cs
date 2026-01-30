using Microsoft.AspNetCore.Routing;

namespace Deve.Abstractions.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}
