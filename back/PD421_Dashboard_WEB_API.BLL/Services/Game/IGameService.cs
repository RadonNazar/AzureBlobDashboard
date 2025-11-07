using PD421_Dashboard_WEB_API.BLL.Dtos.Game;

namespace PD421_Dashboard_WEB_API.BLL.Services.Game
{
    public interface IGameService
    {
        Task<ServiceResponse> CreateAsync(CreateGameDto dto, string imagesPath);
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByGenreAsync(string genre);
    }
}
