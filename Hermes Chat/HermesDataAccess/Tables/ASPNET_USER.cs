using System;

namespace HermesDataAccess.Tables
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Readability")]
    public class ASPNET_USER
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string ModificationTime { get; set; }

        public string ConcurencyStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }

        public bool IsTwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public bool? IsLogged { get; set; }

        public DateTime CreationTime { get; set; }
    }
}