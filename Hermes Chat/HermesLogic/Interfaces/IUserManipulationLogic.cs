using HermesModels.User;
using System.Collections.Generic;

namespace HermesLogic.Interfaces
{
    public interface IUserManipulationLogic
    {
        void DeleteUser(string username);

        void UpdateUserInformation(AspNetUser user, Dictionary<string, object> parameters);
    }
}