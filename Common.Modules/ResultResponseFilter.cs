using System.Net;
using Microsoft.AspNetCore.Http;

namespace Deve.Modules;

/// <summary>
/// Parse the custom Result and change the response StatusCode to match it.
/// </summary>
public class ResultResponseFilter : IEndpointFilter
{
    private static int MapResultErrorTypeToHttpStatusCode(ResultErrorType type) => type switch
    {
        ResultErrorType.Unauthorized => (int)HttpStatusCode.Unauthorized,
        ResultErrorType.MissingRequiredField or
            ResultErrorType.InvalidId or
            ResultErrorType.NotFound or
            ResultErrorType.DuplicatedValue or
            ResultErrorType.FieldValueTooLong or
            ResultErrorType.FieldValueTooShort => (int)HttpStatusCode.BadRequest,
        ResultErrorType.NotAllowed => (int)HttpStatusCode.Forbidden,
        ResultErrorType.Locked => (int)HttpStatusCode.Locked,
        ResultErrorType.TooManyAttempts => (int)HttpStatusCode.TooManyRequests,
        ResultErrorType.Unknown => (int)HttpStatusCode.InternalServerError,
        _ => (int)HttpStatusCode.InternalServerError,
    };

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        // Execute the endpoint
        var objectResult = await next(context);

        // Inspect the returned result
        if (objectResult is Dto.Responses.Results.IResult result)
        {
            var error = result.Errors?.Count > 0 ? result.Errors[0] : null;
            if (error is not null)
            {
                var response = context.HttpContext.Response;
                response.StatusCode = MapResultErrorTypeToHttpStatusCode(error.Type);
            }
        }

        return objectResult;
    }
}
