using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;

namespace HermesQueriesCommands.Queries
{
    public class GetUserDetailsByUserIdQuery : CommonQuery, IQuery<ACCOUNT_DETAILS>
    {
        private long _userId { get; }

        public GetUserDetailsByUserIdQuery(long userId)
        {
            _userId = userId;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.ACCOUNT_DETAILS
                WHERE ASPNET_USER_ID = @id";

        public ACCOUNT_DETAILS Execute(ISession session)
        {
            return session.QueryFirstOrDefault<ACCOUNT_DETAILS>(SqlQuery, new { id = _userId });
        }

        public async ITask<ACCOUNT_DETAILS> ExecuteAsync(ISession session)
        {
            return await session.QueryFirstOrDefaultAsync<ACCOUNT_DETAILS>(SqlQuery, parameters: new { id = _userId });
        }
    }
}
