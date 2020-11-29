using FluentValidation;
using HermesDataAccess.Interfaces;
using HermesLogic.Base.UserManagement;
using HermesLogic.Base.Validator;
using HermesLogic.Mappers.DboToDto;
using HermesModels.MVC;
using HermesModels.User;
using HermesQueriesCommands.Queries;

namespace HermesLogic.Features.Authentication.Validators
{
    /// <summary>
    /// Login model validation.
    /// </summary>
    public class LoginValidator : ApplicationValidator<LoginModel>
    {
        /// <summary>
        /// Sql database.
        /// </summary>
        private readonly ISqlDb _sqlDb;

        /// <summary>
        /// Login model validation.
        /// </summary>
        public LoginValidator(
            IUserManager userManager,
            ISqlDb sqlDb) : base(userManager) 
        {
            _sqlDb = sqlDb;
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

        /// <summary>
        /// Checks if user by provided username exists and if password is equal to provided.
        /// </summary>
        /// <param name="username">Provided username from model..</param>
        /// <param name="password">Provided password from model.</param>
        /// <returns>True if username and password is correct, false otherwise.</returns>
        private bool IsCorrectUsernameAndPassword(string username, string password)
        {
            var userDetails = GetUserAccountDetailsByUsername(username);
            return userDetails != null && _userManager.CredentialsManager.IsHashedPasswordEqual(password, userDetails.PasswordHash);
        }

        /// <summary>
        /// Gets user account details by provided username.
        /// </summary>
        /// <param name="username">Username of user to get account details for.</param>
        /// <returns>Account details for requested user or null if no user found.</returns>
        private AccountDetails GetUserAccountDetailsByUsername(string username)
        {
            return _sqlDb.CacheNQuery(new GetUserDetailsByUsernameQuery(username), username).ToAccountDetailsDto();
        }
    }
}