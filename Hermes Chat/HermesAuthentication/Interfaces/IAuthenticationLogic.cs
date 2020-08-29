using HermesDataAccess.Interfaces;
using HermesModels.MVC;
using HermesModels.User;

namespace AuthenAuthorAccount.Interfaces
{
    public interface IAuthenticationLogic
    {
        IExecutionResult RegisterNewUser(RegistrationModel registrationModel);

        IExecutionResult LoginUser(AspNetUser aspNetUser);

        IExecutionResult LogoutUser(AspNetUser aspNetUser);
    }
}