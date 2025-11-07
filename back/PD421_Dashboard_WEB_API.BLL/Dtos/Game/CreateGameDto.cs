using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PD421_Dashboard_WEB_API.BLL.Dtos.Game
{
    public class CreateGameDto
    {
        [Required(ErrorMessage = "Поле 'Name' є обов'язковим")]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }

        [Required(ErrorMessage = "Поле 'Main Image' є обов'язковим")]
        public required IFormFile MainImage { get; set; }
        public List<IFormFile> Images { get; set; } = [];
    }
}
