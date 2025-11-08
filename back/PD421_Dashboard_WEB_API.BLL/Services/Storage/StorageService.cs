using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace PD421_Dashboard_WEB_API.BLL.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public StorageService(BlobContainerClient containerClient)
        {
            _containerClient = containerClient;
        }

        public async Task<string?> SaveImageAsync(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            
            if (!file.ContentType.StartsWith("image/"))
            {
                 return null;
            }

            try
            {
                string extension = Path.GetExtension(file.FileName);
                string blobName = $"{folderPath}/{Guid.NewGuid()}{extension}";
                
                BlobClient blobClient = _containerClient.GetBlobClient(blobName);

                var httpHeaders = new Azure.Storage.Blobs.Models.BlobHttpHeaders { ContentType = file.ContentType };

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, httpHeaders);
                }

                return blobName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving image to Azure: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<string>> SaveImagesAsync(IEnumerable<IFormFile> files, string folderPath)
        {
            var tasks = files.Select(file => SaveImageAsync(file, folderPath));
            var results = await Task.WhenAll(tasks);
            return results.Where(res => res != null)!;
        }

        public async Task<bool> DeleteImageAsync(string blobPath)
        {
            if (string.IsNullOrEmpty(blobPath))
            {
                return false;
            }

            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(blobPath);

                return await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting image from Azure: {ex.Message}");
                return false;
            }
        }
    }
}
