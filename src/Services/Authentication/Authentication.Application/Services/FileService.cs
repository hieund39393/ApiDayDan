using EVN.Core.Common;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Authentication.Application.Services
{
    public interface IFileService
    {
        Task<string> OnPostUploadAsync(IFormFile file);
    }

    public class FileService : IFileService
    {
        private readonly IDomainService _domainService;
        public FileService(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task<string> OnPostUploadAsync(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            var extension = Path.GetExtension(file.FileName);
            var fileName = Path.GetRandomFileName() + extension;
            var filePath = $"{AppConstants.Settings.StoredFilesPath}/{fileName}";
            await using (var stream = File.Create($"wwwroot//{filePath}"))
            {
                await file.CopyToAsync(stream);
            }

            var directory = _domainService.GetFullPath(filePath);
            return directory;
        }
    }
}
