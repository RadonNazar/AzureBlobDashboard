using System.ComponentModel.DataAnnotations;

namespace PD421_Dashboard_WEB_API.BLL.Dtos.Genre
{
    public class CreateGenreDto // POST
    {
        [Required(ErrorMessage = "Поле Name є обов'язковим")]
        public required string Name { get; set; }
    }
}
