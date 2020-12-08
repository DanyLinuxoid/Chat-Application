using FluentValidation;
using HermesLogic.Base.UserManagement;
using HermesLogic.Base.Validator;
using HermesModels.MVC;

namespace HermesLogic.Features.AccountManagement.Validators
{
    public class UserProfileValidator : ApplicationValidator<UserProfileModel>
    {
        public UserProfileValidator(IUserManager userManager) : base(userManager)
        {
            var currentUser = _userManager.CurrentUser;

            RuleFor(m => m.UserName)
                .NotEmpty().WithMessage("Username is mandatory information")
                .Length(3, 24).WithMessage("Username length must be in range 3-24 characters!")
                .Must(BeNonExistingUsername).WithMessage("Username is already taken!")
                .When(m => m.UserName != currentUser.Username);

            RuleFor(m => m.Email)
                .NotEmpty().WithMessage("Email is mandatory information!")
                .Length(0, 32).WithMessage("Email length cannot be longer than 32 characters!")
                .EmailAddress().WithMessage("Bad email address format!")
                .Must(BeNonExistingEmail).WithMessage("Email is already taken!")
                .When(m => m.Email != currentUser.Email);

            RuleFor(m => m.PhoneNumber)
                .Matches(@"^\+\d{8,12}$").WithMessage("Bad phone number format!");

            RuleFor(m => m.AboutMe)
                .Length(0, 500).WithMessage("Information should not be longer than 500 symbols!");
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