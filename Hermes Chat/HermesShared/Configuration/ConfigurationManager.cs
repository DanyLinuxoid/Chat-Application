using HermesShared.Caching;

namespace HermesShared.Configuration
{
    public static class ConfigurationManager
    {
        public static int DefaultUserSessionExpirationTimeInMinutes 
        {
            get
            {
                return 30; // Default value, is used for caching and for session.
            }
        }
    }
}