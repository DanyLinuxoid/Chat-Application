namespace HermesLogic.Base.UserManagement
{
    public interface ICredentialManager
    {
        string GetUserPasswordInHashedFormat(string password);

        bool IsHashedPasswordEqual(string unhashedPassword, string hashedPassword);
    }
}