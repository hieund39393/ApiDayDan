using EVN.Core.Resources.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using EVN.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EVN.Core.Models
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<ApiError> Errors { get; set; }

        public ApiErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ApiErrorResponse(ModelStateDictionary modelState)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
            Message = ErrorMessage.MSG_VALIDATION_FAILED;
            Errors = modelState.Keys
                .SelectMany(key =>
                    modelState[key].Errors.Select(x => new ApiError(key.ToCamelCase(), x.ErrorMessage)))
                .ToList();
        }        
    }

    /*
     * Object Result
     */

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ApiErrorResponse(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }

    public class ApiUnauthorizedResult : ObjectResult
    {
        public ApiUnauthorizedResult(int statusCode, string message)
            : base(new ApiErrorResponse(statusCode, message))
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }
    }

    public class ApiErrorResult : ObjectResult
    {
        public ApiErrorResult(int statusCode, string message = null)
            : base(new ApiErrorResponse(statusCode, message))
        {
            StatusCode = statusCode;
        }
    }

    public class ApiError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }

        public string Message { get; set; }

        public ApiError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
