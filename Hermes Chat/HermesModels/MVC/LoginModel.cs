using HermesModels.Interfaces;

namespace HermesModels.MVC
{
    public class LoginModel : IValidationObject
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}