using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesQueriesCommands.TBL_ASPNET_USER
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Readability")]
    public class ASPNET_USER_Commands : ICommonCommandRepository
    {
        private Dictionary<string, object> _defaultParameters = new Dictionary<string, object>()
        {
            ["Id"] = null,
            ["UserName"] = null,
            ["Email"] = null,
            ["PasswordHash"] = null,
            ["PhoneNumber"] = null,
            ["TwoFactorEnabled"] = null,
            ["IsLogged"] = null,
        };

        private string _createAspNetUserCommand =>
            @"INSERT INTO dbo.ASPNET_USERS 
	                (UserName, Email, PasswordHash, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, IsLogged, LockoutEnabled, AccessFailedCount, CreationTime, ModificationTime)
                OUTPUT INSERTED.Id
	            VALUES 
	                (@Username, @Email, @PasswordHash, 0, '0', 0, 0, 0, 0, 0, GETDATE(), GETDATE())";

        private string _updateAspNetUserCommand =>
            @"UPDATE dbo.ASPNET_USERS 
                SET UserName = ISNULL(@Username, UserName),
                       Email = ISNULL(@Email, Email),
                       PasswordHash = ISNULL(@PasswordHash, PasswordHash),
                       PhoneNumber = ISNULL(@PhoneNumber, PhoneNumber),
                       TwoFactorEnabled = ISNULL(@TwoFactorEnabled, TwoFactorEnabled), 
                       IsLogged = ISNULL(@IsLogged, IsLogged),
                       ModificationTime = GETDATE()
                OUTPUT INSERTED.Id
                WHERE Id = @Id";

        private string _deleteAspNetUserCommand =>
            @"DELETE FROM dbo.ASPNET_USERS
                            WHERE UserName = @Username";

        public IExecutionResult Create(ISession session, Dictionary<string, object> parameters)
        {
            return session.Create(_createAspNetUserCommand, CommandHelper.GetDynamicParameters(parameters, _defaultParameters));
        }

        public Task<IExecutionResult> CreateAsync(ISession session, Dictionary<string, object> parameters)
        {
            return session.CreateAsync(_createAspNetUserCommand, CommandHelper.GetDynamicParameters(parameters, _defaultParameters));
        }

        public IExecutionResult Update(ISession session, Dictionary<string, object> parameters)
        {
            return session.Update(_updateAspNetUserCommand, CommandHelper.GetDynamicParameters(parameters, _defaultParameters));
        }

        public Task<IExecutionResult> UpdateAsync(ISession session, Dictionary<string, object> parameters)
        {
            return session.UpdateAsync(_updateAspNetUserCommand, CommandHelper.GetDynamicParameters(parameters, _defaultParameters));
        }

        public IExecutionResult Delete(ISession session, Dictionary<string, object> parameters)
        {
            return session.Delete(_deleteAspNetUserCommand, CommandHelper.GetDynamicParameters(parameters, _defaultParameters));
        }

        public Task<IExecutionResult> DeleteAsync(ISession session, Dictionary<string, object> parameters)
        {
            return session.DeleteAsync(_deleteAspNetUserCommand, CommandHelper.GetDynamicParameters(parameters, _defaultParameters));
        }
    }
}