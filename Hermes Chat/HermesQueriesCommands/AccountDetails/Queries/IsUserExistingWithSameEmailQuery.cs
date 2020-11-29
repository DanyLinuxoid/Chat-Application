using HermesDataAccess;
using HermesDataAccess.Interfaces;
using MorseCode.ITask;

namespace HermesQueriesCommands.Queries
{
    public class IsUserExistingWithSameEmailQuery : CommonQuery, IQuery<bool>
    {
        private string _email;

        public IsUserExistingWithSameEmailQuery(string email)
        {
            _email = email;
        }

        public override string SqlQuery =>
            @"SELECT 1
                FROM dbo.ACCOUNT_DETAILS ad
                WHERE EMAIL = @Email
                HAVING COUNT(*) > 0";

        public bool Execute(ISession session)
        {
            return session.QueryFirstOrDefault<bool>(SqlQuery, parameters: new { @Email = _email });
        }

        public async ITask<bool> ExecuteAsync(ISession session)
        {
            return await session.QueryFirstOrDefaultAsync<bool>(SqlQuery, parameters: new { @Email = _email });
        }
    }
}
