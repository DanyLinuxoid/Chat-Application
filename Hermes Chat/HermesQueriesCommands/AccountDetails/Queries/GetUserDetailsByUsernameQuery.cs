using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;

namespace HermesQueriesCommands.Queries
{
    public class GetUserDetailsByUsernameQuery : CommonQuery, IQuery<ACCOUNT_DETAILS>
    {
        private readonly string _username;

        public GetUserDetailsByUsernameQuery(string username)
        {
            _username = username;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.ACCOUNT_DETAILS
                WHERE Username = @Username";

        public ACCOUNT_DETAILS Execute(ISession session)
        {
            return session.QueryFirstOrDefault<ACCOUNT_DETAILS>(SqlQuery, parameters: new { Username = _username});
        }

        public async ITask<ACCOUNT_DETAILS> ExecuteAsync(ISession session)
        {
            return await session.QueryFirstOrDefaultAsync<ACCOUNT_DETAILS>(SqlQuery, parameters: new { Username = _username });
        }
    }
}