using HermesModels.Enums;
using HermesModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Interfaces
{
    public interface IUserManager
    {
        ICredentialManager CredentialsManager { get; }

        AspNetUser GetUser(string[] args, UserRetrieveOption option);

        void UpdateUserInformation(AspNetUser user, object parameters);

        void DeleteUser(string username);

        List<AspNetUser> GetAllLoggedUsers();

        Task<List<AspNetUser>> GetAllLoggedUsersAsync();

        bool IsEmailExisting(string email);

       bool IsUsernameExisting(string username);
    }
}