using FluentValidation;
using HermesLogic.Base.UserManagement;

namespace HermesLogic.Base.Validator
{
    /// <summary>
    /// Base validator, wrapper for abstract validator.
    /// </summary>
    /// <typeparam name="T">Type of model to validate.</typeparam>
    public class ApplicationValidator<T> : AbstractValidator<T>, IApplicationValidator<T>
    {
        /// <summary>
        /// User management.
        /// </summary>
        protected readonly IUserManager _userManager;

        /// <summary>
        /// Base validator, wrapper for abstract validator.
        /// </summary>
        public ApplicationValidator(IUserManager userManager)
        {
            _userManager = userManager;
        }
    }
}