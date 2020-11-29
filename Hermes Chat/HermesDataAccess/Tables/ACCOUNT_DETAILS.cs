using System;

namespace HermesDataAccess.Tables
{
    public class ACCOUNT_DETAILS
    {
        public long ID { get; set; }

        public string USERNAME { get; set; }

        public string PASSWORD_HASH { get; set; }

        public string EMAIL { get; set; }

        public bool IS_EMAIL_CONFIRMED { get; set; }

        public string PHONE_NUMBER { get; set; }

        public bool IS_PHONE_NUMBER_CONFIRMED { get; set; }

        public bool IS_TWO_FACTOR_ENABLED { get; set; }

        public string ABOUT_ME { get; set; }

        public long? PROFILE_PHOTO_ID { get; set; }

        public DateTime MODIFICATION_TIME { get; set; }

        public DateTime CREATION_TIME { get; set; }

        public long ASPNET_USER_ID { get; set; }
    }
}
