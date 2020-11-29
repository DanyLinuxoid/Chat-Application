using FluentValidation;
using HermesLogic.Base.UserManagement;
using HermesLogic.Base.Validator;
using HermesModels.Chat;

namespace HermesLogic.Features.Chat.Validators
{
    public class MessageValidator : ApplicationValidator<MessageModel>
    {
        public MessageValidator(IUserManager userManager) : base(userManager)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(m => m.Text)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}