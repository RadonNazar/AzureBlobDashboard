using PD421_Dashboard_WEB_API.DAL.Entitites;

namespace PD421_Dashboard_WEB_API.DAL.Repositories.Game
{
    public class GameRepository 
        : GenericRepository<GameEntity, string>, IGameRepository
    {
        public GameRepository(AppDbContext context)
            : base(context) { }

        public IQueryable<GameEntity> Games => GetAll();
    }
}
