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
                    response.StatusCode = error.Type switch
                    {
                        ResultErrorType.Unauthorized => (int)HttpStatusCode.Unauthorized,
                        ResultErrorType.MissingRequiredField or ResultErrorType.InvalidId or ResultErrorType.NotFound or ResultErrorType.DuplicatedValue => (int)HttpStatusCode.BadRequest,
                        ResultErrorType.NotAllowed => (int)HttpStatusCode.Forbidden,
                        ResultErrorType.Locked => (int)HttpStatusCode.Locked,
                        ResultErrorType.TooManyAttempts => (int)HttpStatusCode.TooManyRequests,
                        ResultErrorType.Unknown => (int)HttpStatusCode.InternalServerError,
                        _ => (int)HttpStatusCode.InternalServerError,
                    };
                }
            }
        }
    }
}
