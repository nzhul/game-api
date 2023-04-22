using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Server.Common.Errors;
using Server.Common.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Server.Common.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string responseBody;
            int statusCode;

            switch (ex)
            {
                case RestException re:
                    statusCode = (int)re.Code;
                    responseBody = JsonConvert.SerializeObject(new RestErrorResponseDto(re),
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    responseBody = JsonConvert.SerializeObject(new RestErrorResponseDto(context.Request.Path, ex),
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                    break;
            }

            if (context.Response.HasStarted)
            {
                _logger.LogError(ex, "Cannot write error payload since the response has already started. " +
                    "{Method} {Path} -> {StatusCode}", context.Request.Method, context.Request.Path, context.Response.StatusCode);
            }
            else
            {
                _logger.LogError(ex, "{Method} {Path} -> {StatusCode}", context.Request.Method, context.Request.Path, statusCode);
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(responseBody);
            }
        }
    }
}
