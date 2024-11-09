using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Deve.Common.Api
{
    /// <summary>
    /// Parse the custom Result and change the response StatusCode to match it.
    /// </summary>
    public class ResultResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
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
                        case ResultErrorType.DuplicatedValue:
                        case ResultErrorType.NotFound:
                        case ResultErrorType.InvalidId:
                        case ResultErrorType.NotAllowed:
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case ResultErrorType.Unknown:
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }
                }
            }
        }
    }
}