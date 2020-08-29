using FluentValidation;
using HermesLogic.Interfaces;

namespace HermesLogic.Validators
{
    public class ApplicationValidator<T> : AbstractValidator<T>, IApplicationValidator<T>
    {
        protected readonly IUserManager _userManager;

        public ApplicationValidator(IUserManager userManager)
        {
            _userManager = userManager;
        }
    }
}