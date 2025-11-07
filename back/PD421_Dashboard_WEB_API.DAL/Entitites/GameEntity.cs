namespace PD421_Dashboard_WEB_API.DAL.Entitites
{
    public class GameEntity : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }

        public ICollection<GenreEntity> Genres { get; set; } = [];
        public ICollection<GameImageEntity> Images { get; set; } = [];
    }
}
