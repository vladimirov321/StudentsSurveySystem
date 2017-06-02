using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsSurveySystem.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Имейл")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Полето {0} e задължително")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомни този браузър?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Имейл")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Имейл")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        //Added properties to the ViewModel
        [Required(ErrorMessage = "Полето {0} e задължително")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Факултетният номер трябва да се състои от точно 6 цифри")]
        [Display(Name = "Факултетен номер")]
        public string FNumber { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Специалност")]
        public Specialty? Specialty { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Възраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [Display(Name = "Пол")]
        public Gender? Gender { get; set; }
        //

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [StringLength(100, ErrorMessage = "Паролата тръбва да бъде поне {2} символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърдете паролата")]
        [Compare("Password", ErrorMessage = "Потвърдената парола не съвпада с паролата.")] 
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Полето {0} e задължително")]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително")]
        [StringLength(100, ErrorMessage = "Паролата тръбва да бъде поне {2} символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърдете паролата")]
        [Compare("Password", ErrorMessage = "Потвърдената парола не съвпада с паролата.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Полето {0} e задължително")]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }
    }
}
