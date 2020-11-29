using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using MorseCode.ITask;

namespace HermesQueriesCommands.Queries
{
    public class FilePhotoGetByIdQuery : CommonQuery, IQuery<FILES_PHOTO>
    {
        private long _id { get; }

        public FilePhotoGetByIdQuery(long id)
        {
            _id = id;
        }

        public override string SqlQuery =>
            @"SELECT *
                FROM dbo.FILES_PHOTO
                WHERE ID = @Id";

        public FILES_PHOTO Execute(ISession session)
        {
            return session.QueryFirstOrDefault<FILES_PHOTO>(SqlQuery, parameters: new { Id = _id });
        }

        public async ITask<FILES_PHOTO> ExecuteAsync(ISession session)
        {
            return await session.QueryFirstOrDefaultAsync<FILES_PHOTO>(SqlQuery, parameters: new { Id = _id });
        }
    }
}
