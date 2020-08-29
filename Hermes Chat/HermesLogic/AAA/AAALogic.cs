using AuthenAuthorAccount.Interfaces;
using HermesLogic.Base;
using HermesLogic.Interfaces;
using HermesModels.Enums;
using HermesModels.MVC;
using HermesModels.User;

namespace HermesLogic.AAA
{
    public class AAALogic : BaseLogic, IAAALogic
    {
        private readonly IAuthenticationLogic _authenticationLogic;

        public AAALogic(
            IAuthenticationLogic authenticationLogic,
            ICommonDependencies commonDependencies)
            : base(commonDependencies)
        {
            _authenticationLogic = authenticationLogic;
        }

        public bool? LoginUser(LoginModel loginModel)
        {
            if (!Validate(loginModel)) 
            {
                return false;
            }

            AspNetUser user = _commonDependencies.UserManager.GetUser(new string[] { loginModel.Username }, UserRetrieveOption.GetByUsername);
            return _authenticationLogic.LoginUser(user).Id != 0 ? true : (bool?)null;
        }

        public bool? RegisterUser(RegistrationModel registrationModel)
        {
            if (!Validate(registrationModel))
            {
                return false;
            }

            registrationModel.Password = _commonDependencies.UserManager.CredentialsManager.GetUserPasswordInHashedFormat(registrationModel.Password);
            return _authenticationLogic.RegisterNewUser(registrationModel).Id != 0 ? true : (bool?)null;
        }

        public bool? LogoutUser(string user)
        {
            return _authenticationLogic.LogoutUser
                (_commonDependencies.UserManager.GetUser(new string[] { user }, UserRetrieveOption.GetByUsername)).Id != 0;
        }
    }
}