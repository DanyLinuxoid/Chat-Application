using HermesModels.Base;
using HermesModels.Interfaces;

namespace HermesModels.MVC
{
    /// <summary>
    /// Login model, that get's filled on login page.
    /// </summary>
    public class LoginModel : ModelBase, IValidationObject
    {
        /// <summary>
        /// Username that was chosen by user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Pssword, that was chosen by user.
        /// </summary>
        public string Password { get; set; }
    }
}