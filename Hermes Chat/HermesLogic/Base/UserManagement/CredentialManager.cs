namespace HermesLogic.Base.UserManagement
{
    public class CredentialManager : ICredentialManager
    {
        public string GetUserPasswordInHashedFormat(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 14);
        }

        public bool IsHashedPasswordEqual(string unhashedPassword ,string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(unhashedPassword, hashedPassword);
        }
    }
}