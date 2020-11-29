using HermesLogic.Base.UserManagement;
using HermesLogic.Base.Validator;
using HermesModels.MVC;

namespace HermesLogic.Features.AccountManagement.Validators
{
    public class AccountManagementValidator : ApplicationValidator<UserAccountManagementViewModel>
    {
        public AccountManagementValidator(IUserManager userManager) : base(userManager)
        {
            RuleFor(x => x.UserProfile).SetValidator(new UserProfileValidator(userManager));
        }
    }
}
