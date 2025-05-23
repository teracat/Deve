﻿using System.Net;
using Deve.Model;

namespace Deve.Api.Helpers
{
    /// <summary>
    /// If the response StatusCode is TooManyRequests, return the body with the error information to the client.
    /// </summary>
    public class TooManyRequestsMiddleware
    {
        private readonly RequestDelegate _next;

        public TooManyRequestsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.TooManyRequests)
            {
                string langCode = UtilsApi.GetLangCodeFromRequest(context.Request) ?? Constants.DefaultLangCode;

                var response = Utils.ResultError(langCode, ResultErrorType.TooManyAttempts);

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
