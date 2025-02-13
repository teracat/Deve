using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Deve.Model;

namespace Deve.Api.Helpers
{
    /// <summary>
    /// Parse the custom Result and change the response StatusCode to match it.
    /// </summary>
    public class ResultResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Not needed
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is Result result)
            {
                var error = result.Errors?.FirstOrDefault();
                if (error is not null)
                {
                    var response = context.HttpContext.Response;
                    switch (error.Type)
                    {
                        case ResultErrorType.Unauthorized:
                            response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            break;
                        case ResultErrorType.MissingRequiredField:
                        case ResultErrorType.InvalidId:
                        case ResultErrorType.NotFound:
                        case ResultErrorType.DuplicatedValue:
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case ResultErrorType.NotAllowed:
                            response.StatusCode = (int)HttpStatusCode.Forbidden;
                            break;
                        case ResultErrorType.Locked:
                            response.StatusCode = (int)HttpStatusCode.Locked;
                            break;
                        case ResultErrorType.TooManyAttempts:
                            response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                            break;
                        case ResultErrorType.Unknown:
                        default:
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }
                }
            }
        }
    }
}