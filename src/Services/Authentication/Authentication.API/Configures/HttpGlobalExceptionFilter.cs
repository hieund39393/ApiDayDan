using EVN.Core.Common;
using EVN.Core.Exceptions;
using EVN.Core.Models;
using EVN.Core.Properties;
using EVN.Domain.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Linq;
using System.Net;

namespace Authentication.API.Configures
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        /// <summary>
        /// System exception
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var developerMessage = exception.Message + "\r\n" + exception.StackTrace;
            while (exception.InnerException != null)
            {
                developerMessage += "\r\n--------------------------------------------------\r\n";
                exception = exception.InnerException;
                developerMessage += (exception.Message + "\r\n" + exception.StackTrace);
            }

            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                developerMessage);

            if (context.ModelState.ErrorCount > 0)
            {
                var errors = context.ModelState.Where(v => v.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new JsonResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = EvnResources.MSG_INVALID_DATA,
                    Data = errors
                });
                context.ExceptionHandled = true;
                return;
            }

            var json = new JsonResponse
            {
                Message = context.Exception.Message
            };

            if (_env.EnvironmentName != "Production")
                json.DeveloperMessage = developerMessage;

            var userName = context.HttpContext.User.Identity.IsAuthenticated
                ? context.HttpContext.User.Identity.Name : "Guest"; //Gets user Name from user Identity 
            var ipAddress = HttpRequestUtilities.GetIpAddress(context.HttpContext);
            LogContext.PushProperty("UserName", userName);
            LogContext.PushProperty("IP", ipAddress);
            LogContext.PushProperty("LogEvent", null);
            // 400 Bad Request
            if (context.Exception.GetType() == typeof(EvnException))
            {
                var errorCode = (int?)exception.Data[EvnException.ErrorCode];
                if (errorCode != null)
                {
                    json.StatusCode = errorCode.Value;
                    context.HttpContext.Response.StatusCode = errorCode.Value;
                }
                else
                {
                    json.StatusCode = StatusCodes.Status400BadRequest;
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                context.Result = new BadRequestObjectResult(json);
                LogHelper.Logger.Warning(developerMessage);
            }
            // 404 Not Found
            else if (context.Exception.GetType() == typeof(NotFoundException))
            {
                json.StatusCode = StatusCodes.Status404NotFound;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new NotFoundObjectResult(json);
                LogHelper.Logger.Error(developerMessage);
            }
            // 500 Internal Server Error
            else
            {
                json.Message = EvnResources.MSG_SYSTEM_ERROR;
                json.StatusCode = StatusCodes.Status500InternalServerError;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new InternalServerErrorObjectResult(json);
                LogHelper.Logger.Error(developerMessage);
            }
            context.ExceptionHandled = true;
        }
    }
}
