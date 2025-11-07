namespace PD421_Dashboard_WEB_API.DAL.Entitites
{
    public class GenreEntity : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public string? NormalizedName { get; set; }

        public ICollection<GameEntity> Games { get; set; } = [];
    }
}
