using Microsoft.AspNetCore.Builder;

namespace Deve.Api;

internal enum ApiBuilderAppActionPriority
{
    High = 0,
    Normal = 1,
    Low = 2,
}

internal sealed class ApiBuilderAppAction
{
    public Action<WebApplication> Action { get; }

    public int Position { get; set; }

    public ApiBuilderAppActionPriority Priority { get; set; } = ApiBuilderAppActionPriority.Normal;

    public ApiBuilderAppAction(Action<WebApplication> action, int position)
    {
        Action = action;
        Position = position;
    }

    public ApiBuilderAppAction(Action<WebApplication> action, int position, ApiBuilderAppActionPriority proprity)
    {
        Action = action;
        Position = position;
        Priority = proprity;
    }
}
