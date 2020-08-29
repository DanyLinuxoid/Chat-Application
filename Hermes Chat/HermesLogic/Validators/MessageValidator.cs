using FluentValidation;
using HermesLogic.Interfaces;
using HermesModels.Chat;

namespace HermesLogic.Validators
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