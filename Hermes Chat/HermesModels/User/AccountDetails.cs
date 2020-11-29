using System;

namespace HermesModels.User
{
    public class AccountDetails
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }

        public bool IsTwoFactorEnabled { get; set; }

        public string AboutMe { get; set; }

        public long? ProfilePhotoId { get; set; }

        public DateTime ModificationTime { get; set; }

        public long AspnetUserId { get; set; }
    }
}
