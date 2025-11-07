using PD421_Dashboard_WEB_API.DAL.Entitites;

namespace PD421_Dashboard_WEB_API.DAL.Repositories.Game
{
    public interface IGameRepository 
        : IGenericRepository<GameEntity, string>
    {
        IQueryable<GameEntity> Games { get; }
    }
}
