using HermesModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Interfaces
{
    public interface IUserNonManipulativeLogic
    {
        AspNetUser GetUserByUsername(string username);

        AspNetUser GetUserByEmail(string email);

        bool IsUserWithSameEmailExisting(string email);

        bool IsUserWithSameUsernameExisting(string username);

        List<AspNetUser> GetAllLoggedUsers();

        Task<List<AspNetUser>> GetAllLoggedUsersAsync();
    }
}
