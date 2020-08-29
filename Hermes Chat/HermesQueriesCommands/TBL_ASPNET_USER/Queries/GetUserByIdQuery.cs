using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;

namespace HermesQueriesCommands.TBL_ASPNET_USER.Queries
{
    public class GetUserByIdQuery : CommonCommand, IQuery<ASPNET_USER>
    {
        private int _userId { get; }

        public GetUserByIdQuery(int userId)
        {
            _userId = userId;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.ASPNET_USERS  
                WHERE Id = @Id";

        public ASPNET_USER Execute(ISession session)
        {
            return session.QueryFirstOrDefault<ASPNET_USER>(SqlQuery, parameters: new { Id = _userId } );
        }

        public async ITask<ASPNET_USER> ExecuteAsync(ISession session)
        {
            var result = await session.QueryFirstOrDefaultAsync<ASPNET_USER>(SqlQuery, parameters: new { Id = _userId });
            return result;
        }
    }
}