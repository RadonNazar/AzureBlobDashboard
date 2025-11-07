using PD421_Dashboard_WEB_API.DAL.Entitites;

namespace PD421_Dashboard_WEB_API.DAL.Repositories.Genre
{
    public interface IGenreRepository
        : IGenericRepository<GenreEntity, string>
    {
        Task<bool> IsExistsAsync(string name);
    }
}
