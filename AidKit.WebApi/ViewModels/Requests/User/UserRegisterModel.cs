using System.ComponentModel.DataAnnotations;

namespace AidKit.WebApi.ViewModels.Requests.User
{
    public class UserRegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Логин должен быть указан.")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя должно быть указано.")]
        public string FullName { get; set; }
        
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пароль должен быть указан.")]
        public string Password { get; set; }
    }
}
