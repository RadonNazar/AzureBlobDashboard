using Microsoft.EntityFrameworkCore;
using PD421_Dashboard_WEB_API.DAL.Entitites;

namespace PD421_Dashboard_WEB_API.DAL.Repositories.Genre
{
    public class GenreRepository
        : GenericRepository<GenreEntity, string>, IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
            : base(context) 
        {
            _context = context;
        }

        public async Task<bool> IsExistsAsync(string name)
        {
            return await _context.Genres
                .AnyAsync(g => g.NormalizedName == name.ToUpper());
        }
    }
}
