using FluentValidation;
using HermesLogic.Interfaces;
using HermesModels.Enums;
using HermesModels.MVC;

namespace HermesLogic.Validators
{
    public class LoginValidator : ApplicationValidator<LoginModel>
    {
        public LoginValidator(IUserManager userManager) : base(userManager) 
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(m => m.Username).NotEmpty().WithMessage("Username is mandatory information!");
            RuleFor(m => m.Password).NotEmpty().WithMessage("Password is mandatory information!");

            When(m => !string.IsNullOrEmpty(m.Username) && !string.IsNullOrEmpty(m.Password), () =>
            {
                RuleFor(x => x.Password).Must((o, credentials) =>
                {
                    return IsCorrectUsernameAndPassword(o.Username, o.Password);
                }).WithMessage("No user found with such credentials!");
            });
        }

        private bool IsCorrectUsernameAndPassword(string username, string password)
        {
            var user = _userManager.GetUser(new string[] { username }, UserRetrieveOption.GetByUsername);
            return user != null && _userManager.CredentialsManager.IsHashedPasswordEqual(password, user.Password);
        }
    }
}