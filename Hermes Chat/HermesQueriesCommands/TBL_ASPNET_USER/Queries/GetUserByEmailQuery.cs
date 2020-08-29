using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;

namespace HermesQueriesCommands.TBL_ASPNET_USER.Queries
{
    public class GetUserByEmailQuery : CommonCommand, IQuery<ASPNET_USER>
    {
        private string _email { get; }

        public GetUserByEmailQuery(string email)
        {
            _email = email;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.ASPNET_USERS  
                WHERE Email = @Email";

        public ASPNET_USER Execute(ISession session)
        {
            return session.QueryFirstOrDefault<ASPNET_USER>(SqlQuery, parameters: new { Email =  _email });
        }

        public async ITask<ASPNET_USER> ExecuteAsync(ISession session)
        {
            var result = await session.QueryFirstOrDefaultAsync<ASPNET_USER>(SqlQuery, parameters: new { Email = _email });
            return result;
        }
    }
}