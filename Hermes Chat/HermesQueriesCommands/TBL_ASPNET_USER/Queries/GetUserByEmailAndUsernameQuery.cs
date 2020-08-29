using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;

namespace HermesQueriesCommands.TBL_ASPNET_USER.Queries
{
    public class GetUserByEmailAndUsernameQuery : CommonCommand, IQuery<ASPNET_USER>
    {
        private readonly string _userName;
        private readonly string _email;

        public GetUserByEmailAndUsernameQuery(string userName, string email)
        {
            _userName = userName;
            _email = email;
        }

        public override string SqlQuery =>
            @"SELECT 1
                FROM dbo.ASPNET_USERS  
                WHERE Email = @Email
                    AND UserName = @Username;";

        public ASPNET_USER Execute(ISession session)
        {
            return session.QueryFirstOrDefault<ASPNET_USER>(SqlQuery, new { Username = _userName, Email = _email });
        }

        public async ITask<ASPNET_USER> ExecuteAsync(ISession session)
        {
            var result = await session.QueryFirstOrDefaultAsync<ASPNET_USER>(SqlQuery, new { Username = _userName, Email = _email });
            return result;
        }
    }
}