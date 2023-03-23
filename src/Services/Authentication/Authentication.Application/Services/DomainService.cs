using Microsoft.AspNetCore.Http;

namespace Authentication.Application.Services
{
    public interface IDomainService
    {
        string GetFullPath(string filePath);
        string GetDomain();
    }
    public class DomainService : IDomainService
    {
        private readonly IHttpContextAccessor _httpContext;
        public DomainService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetFullPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            return $"{GetDomain()}{filePath}";
        }

        public string GetDomain()
        {
            return $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/";
        }
    }
}
