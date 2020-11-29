using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;
using System.Collections.Generic;
using System.Linq;

namespace HermesQueriesCommands.Queries
{
    public class FilesPhotoGetByIdsQuery : CommonQuery, IQuery<List<FILES_PHOTO>>
    {
        private List<long> _ids { get; }

        public FilesPhotoGetByIdsQuery(List<long> ids)
        {
            _ids = ids;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.FILES_PHOTO
                WHERE ID IN @Ids";

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
