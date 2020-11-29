using System;

namespace HermesModels.User
{
    public class AspNetUser
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ModificationTime { get; set; }

        public bool IsLockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public int AccessFailedCount { get; set; }
    }
}