using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesQueriesCommands.Commands
{
    /// <summary>
    /// Repository of CUD commands + async version.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    public class FILES_PHOTO_Commands : ICommonCommandRepository<FILES_PHOTO>
    {
        /// <summary>
        /// Dummy model with properties to use directly mapped names in sql.
        /// </summary>
        private FILES_PHOTO tbl { get; } = new FILES_PHOTO();

        /// <summary>
        /// Command to create record in database.
        /// </summary>
        private string _createCommand =>
            $@"INSERT INTO [dbo].[FILES_PHOTO]
                    (FILENAME,
                     DATA,
                     ASPNET_USER_ID,
                     IS_ACCOUNT_IMAGE,
                     IS_DEFAULT_IMAGE)
                OUTPUT INSERTED.Id
                VALUES(
                    @{nameof(tbl.FILENAME)}, 
                    @{nameof(tbl.DATA)},
                    @{nameof(tbl.ASPNET_USER_ID)},
                    @{nameof(tbl.IS_ACCOUNT_IMAGE)},
                    0)";

        /// <summary>
        /// Command to update record in database.
        /// </summary>
        private string _updateCommand =>
            $@"UPDATE dbo.FILES_PHOTO
                SET FILENAME = @{nameof(tbl.FILENAME)},
                       DATA = @{nameof(tbl.DATA)}
                OUTPUT INSERTED.Id
                WHERE ID = @{nameof(tbl.ID)}";

        /// <summary>
        /// Command to delete record from database.
        /// </summary>
        private string _deleteCommand =>
            $@"DELETE FROM dbo.FILES_PHOTO
                            WHERE ID = @{nameof(tbl.ID)}";

        /// <summary>
        /// Command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Create(ISession session, FILES_PHOTO dbo)
        {
            return session.Create(_createCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> CreateAsync(ISession session, FILES_PHOTO dbo)
        {
            return session.CreateAsync(_createCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        ///  Command to update record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Update(ISession session, FILES_PHOTO dbo)
        {
            return session.Update(_updateCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> UpdateAsync(ISession session, FILES_PHOTO dbo)
        {
            return session.UpdateAsync(_updateCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Delete(ISession session, FILES_PHOTO dbo)
        {
            return session.Delete(_deleteCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> DeleteAsync(ISession session, FILES_PHOTO dbo)
        {
            return session.DeleteAsync(_deleteCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Parameter mapper for table, later is used for dynamic parameter creation.
        /// </summary>
        /// <param name="table">Table to map parameters from/</param>
        /// <returns>Dictionary that contains parameter as key and it's value.</returns>
        private Dictionary<string, object> MapParameters(FILES_PHOTO table)
        {
            return new Dictionary<string, object>()
            {
                [nameof(table.ID)] = table.ID,
                [nameof(table.FILENAME)] = table.FILENAME,
                [nameof(table.DATA)] = table.DATA,
                [nameof(table.IS_ACCOUNT_IMAGE)] = table.IS_ACCOUNT_IMAGE,
                [nameof(table.ASPNET_USER_ID)] = table.ASPNET_USER_ID,
            };
        }
    }
}
