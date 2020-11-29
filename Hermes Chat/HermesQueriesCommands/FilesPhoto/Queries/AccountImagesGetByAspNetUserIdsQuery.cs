using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;
using System.Collections.Generic;
using System.Linq;

namespace HermesQueriesCommands.Queries
{
    public class AccountImagesGetByAspNetUserIdsQuery : CommonQuery, IQuery<List<FILES_PHOTO>>
    {
        private List<long> _ids { get; }

        public AccountImagesGetByAspNetUserIdsQuery(List<long> ids)
        {
            _ids = ids;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.FILES_PHOTO
                WHERE ASPNET_USER_ID IN @Ids
                AND IS_ACCOUNT_IMAGE = 1 
                OR IS_DEFAULT_IMAGE = 1";

        public List<FILES_PHOTO> Execute(ISession session)
        {
            return session.Query<FILES_PHOTO>(SqlQuery, parameters: new { Ids = _ids }).ToList();
        }

        public async ITask<List<FILES_PHOTO>> ExecuteAsync(ISession session)
        {
            return (await session.QueryAsync<FILES_PHOTO>(SqlQuery, parameters: new { Ids = _ids })).ToList();
        }
    }
}
