using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;
using System.Collections.Generic;
using System.Linq;

namespace HermesQueriesCommands.Queries
{
    public class GetUserDetailsByUserIdsQuery : CommonQuery, IQuery<List<ACCOUNT_DETAILS>>
    {
        private List<long> _userIds { get; }

        public GetUserDetailsByUserIdsQuery(List<long> userIds)
        {
            _userIds = userIds;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.ACCOUNT_DETAILS
                WHERE ASPNET_USER_ID IN @_ids";

        public List<ACCOUNT_DETAILS> Execute(ISession session)
        {
            return session.Query<ACCOUNT_DETAILS>(SqlQuery, new { _ids = _userIds }).ToList();
        }

        public async ITask<List<ACCOUNT_DETAILS>> ExecuteAsync(ISession session)
        {
            return (await session.QueryAsync<ACCOUNT_DETAILS>(SqlQuery, parameters: new { _ids = _userIds })).ToList();
        }
    }
}