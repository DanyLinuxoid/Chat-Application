using HermesModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace HermesModels.MVC
{
    public class RegistrationModel : IValidationObject
    {
        [Display(Name = "New Username")]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Password confirmation")]
        public string ConfirmPassword { get; set; }
    }
}
