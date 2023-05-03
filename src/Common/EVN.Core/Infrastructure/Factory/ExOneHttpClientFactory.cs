using Microsoft.Extensions.Configuration;

namespace EVN.Core.Infrastructure.Factory
{
    public interface IExOneHttpClientFactory
    {
        HttpClient CreateClient();
    }
    public class ExOneHttpClientFactory : IExOneHttpClientFactory
    {
        private readonly IConfiguration _configuration;
        public ExOneHttpClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClient CreateClient()
        {
            var timeout = 1800;
            var client = new HttpClient();
            SetupClientDefaults(client, timeout);
            return client;
        }

        protected virtual void SetupClientDefaults(HttpClient client, int timeout)
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.MaxResponseContentBufferSize = int.MaxValue;
        }
    }
}
