using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;
using System.Collections.Generic;
using System.Linq;

namespace HermesQueriesCommands.TBL_ASPNET_USER.Queries
{
    public class GetAllLoggedUsersQuery : CommonCommand, IQuery<List<ASPNET_USER>>
    {
        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.ASPNET_USERS  
                WHERE IsLogged = 1";

        public List<ASPNET_USER> Execute(ISession session)
        {
            return session.Query<ASPNET_USER>(SqlQuery).ToList();
        }

        public async ITask<List<ASPNET_USER>> ExecuteAsync(ISession session)
        {
            var result = await session.QueryAsync<ASPNET_USER>(SqlQuery);
            return result.ToList();
        }
    }               
}