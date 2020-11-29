using HermesDataAccess;
using HermesDataAccess.Interfaces;
using MorseCode.ITask;

namespace HermesQueriesCommands.Queries
{
    public class IsUserExistingWithSameUsernameQuery : CommonQuery, IQuery<bool>
    {
        private string _username;

        public IsUserExistingWithSameUsernameQuery(string username)
        {
            _username = username;
        }

        public override string SqlQuery =>
            @"SELECT 1
                FROM dbo.ACCOUNT_DETAILS
                WHERE USERNAME = @Username
                HAVING COUNT(*) > 0";

        public bool Execute(ISession session)
        {
            return session.QueryFirstOrDefault<bool>(SqlQuery, parameters: new { @Username = _username });
        }

        public async ITask<bool> ExecuteAsync(ISession session)
        {
            return await session.QueryFirstOrDefaultAsync<bool>(SqlQuery, parameters: new { @Username = _username });
        }
    }
}