using System.ComponentModel.DataAnnotations;

namespace WebDevLab2.Model
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string password { get; set; }

        [Required]
        [Compare("password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string passwordconfirm { get; set; }

        [Display(Name = "isdeveloper")]
        public bool isdeveloper { get; set; }

        [Display(Name = "CompanyName")]
        public string company_name { get; set; }
    }
}
