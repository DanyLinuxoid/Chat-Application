using FluentValidation;
using HermesLogic.Base.UserManagement;
using HermesLogic.Base.Validator;
using HermesModels.MVC;
using System.Text.RegularExpressions;

namespace HermesLogic.Features.Authentication.Validators
{
    public class RegistrationValidator : ApplicationValidator<RegistrationModel>
    {
        public RegistrationValidator(IUserManager userManager) : base(userManager)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(m => m.UserName)
                .NotEmpty().WithMessage("Username is mandatory information!")
                .Length(3, 24).WithMessage("Username length must be in range 3-24 characters!")
                .Must(BeNonExistingUsername).WithMessage("Username is already taken!");

            RuleFor(m => m.Email)
                .NotEmpty().WithMessage("Email is mandatory information!")
                .Length(0, 32).WithMessage("Email length cannot be longer than 32 characters!")
                .EmailAddress().WithMessage("Bad email address format!")
                .Must(BeNonExistingEmail).WithMessage("Email is already taken!");

            RuleFor(m => m.Password)
                .NotEmpty().WithMessage("Password is mandatory information!")
                .Length(8, 32).WithMessage("Password length must be in range 8-32 characters!");

            RuleFor(m => m.ConfirmPassword)
                .NotEmpty().WithMessage("Password confirmation is mandatory information!")
                .Equal(m => m.Password).WithMessage("Password and Password confirmation don't match!");
        }

        private bool BeNonExistingUsername(string username)
        {
            return !_userManager.IsUsernameExisting(username);
        }

        private bool BeNonExistingEmail(string email)
        {
            return !_userManager.IsEmailExisting(email);
        }
    }
}