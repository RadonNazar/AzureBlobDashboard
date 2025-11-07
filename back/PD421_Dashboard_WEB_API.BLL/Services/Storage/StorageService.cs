using Microsoft.AspNetCore.Http;

namespace PD421_Dashboard_WEB_API.BLL.Services.Storage
{
    public class StorageService : IStorageService
    {
        public async Task<string?> SaveImageAsync(IFormFile file, string folderPath)
        {
            try
            {
                var types = file.ContentType.Split('/');
                if (types.Length != 2 || types[0] != "image")
                {
                    return null;
                }

                string extension = Path.GetExtension(file.FileName);
                string imageName = $"{Guid.NewGuid()}{extension}";
                string imagePath = Path.Combine(folderPath, imageName);

                using (var stream = File.Create(imagePath))
                {
                    await file.CopyToAsync(stream);
                }

                return imageName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<string>> SaveImagesAsync(IEnumerable<IFormFile> files, string folderPath)
        {
            var tasks = files.Select(file => SaveImageAsync(file, folderPath));
            var results = await Task.WhenAll(tasks);
            return results.Where(res => res != null)!;
        }
    }
}
