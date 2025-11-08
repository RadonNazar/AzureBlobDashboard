using Microsoft.AspNetCore.Http;

namespace PD421_Dashboard_WEB_API.BLL.Services.Storage
{
    public interface IStorageService
    {
        Task<string?> SaveImageAsync(IFormFile file, string folderPath);
        Task<IEnumerable<string>> SaveImagesAsync(IEnumerable<IFormFile> files, string folderPath);
        
        Task<bool> DeleteImageAsync(string blobPath); 
    }
}
