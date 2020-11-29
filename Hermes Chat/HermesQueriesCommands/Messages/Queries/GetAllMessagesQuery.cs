using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;
using System.Collections.Generic;
using System.Linq;

namespace HermesQueriesCommands.Queries
{
    public class GetAllMessagesQuery : CommonQuery, IQuery<List<MESSAGES>>
    {
        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.MESSAGES";

        public List<MESSAGES> Execute(ISession session)
        {
            return session.Query<MESSAGES>(SqlQuery).ToList();
        }

        public async ITask<List<MESSAGES>> ExecuteAsync(ISession session)
        {
            var result = await session.QueryAsync<MESSAGES>(SqlQuery);
            return result.ToList();
        }
    }
}