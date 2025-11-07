using System.ComponentModel.DataAnnotations;

namespace PD421_Dashboard_WEB_API.BLL.Dtos.Genre
{
    public class UpdateGenreDto // PUT
    {
        [Required(ErrorMessage = "Поле Id є обов'язковим")]
        public required string Id { get; set; }
        [Required(ErrorMessage = "Поле Name є обов'язковим")]
        public required string Name { get; set; }
    }
}
