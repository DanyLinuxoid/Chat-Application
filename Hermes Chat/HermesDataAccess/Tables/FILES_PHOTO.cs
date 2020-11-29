namespace HermesDataAccess.Tables
{
    public class FILES_PHOTO
    {
        public long ID { get; set; }

        public string FILENAME { get; set; }

        public byte[] DATA { get; set; }

        public long ASPNET_USER_ID { get; set; }

        public bool IS_ACCOUNT_IMAGE { get; set; }

        public bool IS_DEFAULT_IMAGE { get; set; }
    }
}
