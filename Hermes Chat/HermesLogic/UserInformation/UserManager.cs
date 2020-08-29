using HermesLogic.Interfaces;
using HermesModels.Enums;
using HermesModels.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.UserInformation
{
    public class UserManager : IUserManager
    {
        public ICredentialManager CredentialsManager { get; }
        private readonly IUserManipulationLogic _userManipulationLogic;
        private readonly IUserNonManipulativeLogic _userNonManipulationLogic;

        public UserManager(
            ICredentialManager credentialManager,
            IUserManipulationLogic userManipulationLogic,
            IUserNonManipulativeLogic userNonManipulationLogic)

        {
            CredentialsManager = credentialManager;
            _userManipulationLogic = userManipulationLogic;
            _userNonManipulationLogic = userNonManipulationLogic;
        }

        /// <summary>
        /// Tries to get user from memory cache, if fails, then retrieves from database
        /// </summary>
        /// <param name="username">Username of user.</param>
        /// <returns>User object with user information, or null if found nothing</returns> ///////////// UPDATE COMMENT TODO
        public AspNetUser GetUser(string[] args, UserRetrieveOption option)
        {
            switch (option)
            {
                case UserRetrieveOption.GetByUsername:
                    return _userNonManipulationLogic.GetUserByUsername(args[0]);
                case UserRetrieveOption.GetByEmail:
                    return _userNonManipulationLogic.GetUserByEmail(args[0]);
                default:
                    throw new ArgumentException($"{ option } is unknown or not implemented");
            }
        }

        public List<AspNetUser> GetAllLoggedUsers()
        {
            return _userNonManipulationLogic.GetAllLoggedUsers();
        }

        public Task<List<AspNetUser>> GetAllLoggedUsersAsync()
        {
            return _userNonManipulationLogic.GetAllLoggedUsersAsync();
        }

        public void DeleteUser(string username)
        {
            _userManipulationLogic.DeleteUser(username);
        }

        public void UpdateUserInformation(AspNetUser user, object parameters) 
        {
            //_userManipulationLogic.UpdateUserInformation(user, parameters); // REDO LATER
        }

        public bool IsEmailExisting(string email)
        {
            return _userNonManipulationLogic.IsUserWithSameEmailExisting(email);
        }

        public bool IsUsernameExisting(string username)
        {
            return _userNonManipulationLogic.IsUserWithSameUsernameExisting(username);
        }
    }
}