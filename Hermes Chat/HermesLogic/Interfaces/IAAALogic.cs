using HermesModels.MVC;

namespace HermesLogic.Interfaces
{
    public interface IAAALogic
    {
        bool? RegisterUser(RegistrationModel registrationModel);

        bool? LoginUser(LoginModel loginModel);

        bool? LogoutUser(string user);
    }
}