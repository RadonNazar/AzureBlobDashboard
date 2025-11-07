using PD421_Dashboard_WEB_API.BLL.Dtos.Genre;

namespace PD421_Dashboard_WEB_API.BLL.Services.Genre
{
    public interface IGenreService
    {
        Task<ServiceResponse> CreateAsync(CreateGenreDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateGenreDto dto);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetAllAsync();
    }
}
