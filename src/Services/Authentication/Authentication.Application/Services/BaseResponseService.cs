using Authentication.Application.Model;
using EVN.Core.Infrastructure.Factory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Services
{
    public class BaseResponseService<T>
    {
        private readonly IExOneHttpClientFactory _httpClientFactory;

        public BaseResponseService(
            IExOneHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region Get from Url
        public async Task<List<T>> GetResponse(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add($"Authorization", $"Basic {GetSyncAuthentication()}");
            var dataResponse = await httpClient.GetAsync(url);
            var resultString = JsonConvert.DeserializeObject<ApiResultData<List<T>>>(await dataResponse.Content.ReadAsStringAsync());
            var data = resultString.GetData;
            return data;
        }



        public async Task<List<T>> GetResponseData(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add($"Authorization", $"Basic {GetSyncAuthentication()}");
            var dataResponse = await httpClient.GetAsync(url);
            var resultString = JsonConvert.DeserializeObject<ApiResultData<List<T>>>(await dataResponse.Content.ReadAsStringAsync());
            var data = resultString.GetData;
            return data;
        }

        public async Task<ApiResultData<T>> GetStringResponse(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add($"Authorization", $"Basic {GetSyncAuthentication()}");
            var dataResponse = await httpClient.GetAsync(url);
            return JsonConvert.DeserializeObject<ApiResultData<T>>(await dataResponse.Content.ReadAsStringAsync());
        }

        #endregion
        #region Post from URl

        public async Task<ApiResultData<T>> PostResponse(string url, Object request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add($"Authorization", $"Basic {GetSyncAuthentication()}");
            var dataResponse = await httpClient.PostAsync(url, stringContent);
            return JsonConvert.DeserializeObject<ApiResultData<T>>(await dataResponse.Content.ReadAsStringAsync());
        }
        #endregion

        public string GetSyncAuthentication()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"user:pass");
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
