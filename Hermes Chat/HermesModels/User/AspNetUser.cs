using HermesModels.Interfaces;
using System;

namespace HermesModels.User
{
    public class AspNetUser : IUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreationTime { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }

        public bool IsTwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public bool? IsLogged { get; set; }
    }
}