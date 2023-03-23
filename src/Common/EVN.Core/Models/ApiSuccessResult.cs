using EVN.Core.SeedWork.ExtendEntities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace EVN.Core.Models
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        [DataMember]
        public T Data { get; set; }

        public ApiSuccessResult() 
        {
            StatusCode = StatusCodes.Status200OK;
        }

        public ApiSuccessResult(T data)
        {
            Data = data;
            StatusCode = StatusCodes.Status200OK;
        }

        public ApiSuccessResult(T data, string message)
        {
            Data = data;
            StatusCode = StatusCodes.Status200OK;
            Message = message;
        }

        public ApiSuccessResult(T data, PagingSP paging)
        {
            Data = data;
            Paging = paging;
            StatusCode = StatusCodes.Status200OK;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PagingSP Paging { get; set; }
    }
}
