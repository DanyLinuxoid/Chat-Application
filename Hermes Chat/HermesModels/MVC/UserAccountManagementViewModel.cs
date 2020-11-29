using HermesModels.Interfaces;

namespace HermesModels.MVC
{
    public class UserAccountManagementViewModel : IValidationObject
    {
        public int UserId { get; set; }

        public UserProfileModel UserProfile { get; set; }
    }
}
