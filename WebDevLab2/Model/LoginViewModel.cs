using System.ComponentModel.DataAnnotations;

namespace WebDevLab2.Model
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool rememberMe { get; set; }
    }
}
