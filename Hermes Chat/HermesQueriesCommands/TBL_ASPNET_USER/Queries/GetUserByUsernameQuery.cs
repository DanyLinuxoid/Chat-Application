using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;

namespace HermesQueriesCommands.TBL_ASPNET_USER.Queries
{
    public class GetUserByUsernameQuery : CommonCommand, IQuery<ASPNET_USER>
    {
        private readonly string _username;

        public GetUserByUsernameQuery(string username)
        {
            _username = username;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.ASPNET_USERS  
                WHERE Username = @Username";

        public ASPNET_USER Execute(ISession session)
        {
            return session.QueryFirstOrDefault<ASPNET_USER>(SqlQuery, parameters: new { Username = _username});
        }

        public async ITask<ASPNET_USER> ExecuteAsync(ISession session)
        {
            var result = await session.QueryFirstOrDefaultAsync<ASPNET_USER>(SqlQuery, parameters: new { Username = _username });
            return result;
        }
    }
}