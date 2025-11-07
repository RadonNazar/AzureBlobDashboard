using System.ComponentModel.DataAnnotations;

namespace PD421_Dashboard_WEB_API.BLL.Dtos.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Поле 'login' є обов'язковим")]
        public required string Login { get; set; }
        [Required(ErrorMessage = "Поле 'password' є обов'язковим")]
        [MinLength(6, ErrorMessage = "Пароль повинен містити мінімум 6 символів")]
        public required string Password { get; set; }
    }
}
