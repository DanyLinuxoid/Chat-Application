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
    public class ASPNET_USERS_Commands : ICommonCommandRepository<ASPNET_USER>
    {
        /// <summary>
        /// Dummy model with properties to use directly mapped names in sql.
        /// </summary>
        private ASPNET_USER tbl { get; } = new ASPNET_USER();

        /// <summary>
        /// Command to create record in database.
        /// </summary>
        private string _createCommand =>
            $@"INSERT INTO dbo.ASPNET_USERS 
	                (IS_LOCKOUT_ENABLED, 
                    LOCKOUT_END, 
                    ACCESS_FAILED_COUNT, 
                    CREATION_TIME, 
                    MODIFICATION_TIME)
                OUTPUT INSERTED.Id
	            VALUES(
	                @{nameof(tbl.IS_LOCKOUT_ENABLED)}, 
                    @{nameof(tbl.LOCKOUT_END)}, 
                    @{nameof(tbl.ACCESS_FAILED_COUNT)}, 
                    GETDATE(), 
                    GETDATE())";

        /// <summary>
        /// Command to update record in database.
        /// </summary>
        private string _updateCommand =>
            $@"UPDATE dbo.ASPNET_USERS 
                SET IS_LOCKOUT_ENABLED = ISNULL(@{nameof(tbl.IS_LOCKOUT_ENABLED)}, IS_LOCKOUT_ENABLED),
                       LOCKOUT_END = ISNULL(@{nameof(tbl.LOCKOUT_END)}, LOCKOUT_END),
                       ACCESS_FAILED_COUNT = ISNULL(@{nameof(tbl.ACCESS_FAILED_COUNT)}, 0),
                       MODIFICATION_TIME = GETDATE()
                OUTPUT INSERTED.Id
                WHERE ID = @{nameof(tbl.ID)}";

        /// <summary>
        /// Command to delete record from database.
        /// </summary>
        private string _deleteCommand =>
            $@"DELETE FROM dbo.ASPNET_USERS
                            WHERE ID = @{nameof(tbl.ID)}";

        /// <summary>
        /// Command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Create(ISession session, ASPNET_USER dbo)
        {
            return session.Create(_createCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> CreateAsync(ISession session, ASPNET_USER dbo)
        {
            return session.CreateAsync(_createCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        ///  Command to update record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Update(ISession session, ASPNET_USER dbo)
        {
            return session.Update(_updateCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> UpdateAsync(ISession session, ASPNET_USER dbo)
        {
            return session.UpdateAsync(_updateCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Delete(ISession session, ASPNET_USER dbo)
        {
            return session.Delete(_deleteCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> DeleteAsync(ISession session, ASPNET_USER dbo)
        {
            return session.DeleteAsync(_deleteCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Parameter mapper for table, later is used for dynamic parameter creation.
        /// </summary>
        /// <param name="table">Table to map parameters from/</param>
        /// <returns>Dictionary that contains parameter as key and it's value.</returns>
        private Dictionary<string, object> MapParameters(ASPNET_USER table)
        {
            return new Dictionary<string, object>()
            {
                [nameof(table.ID)] = table.ID,
                [nameof(table.LOCKOUT_END)] = table.LOCKOUT_END,
                [nameof(table.IS_LOCKOUT_ENABLED)] = table.IS_LOCKOUT_ENABLED,
                [nameof(table.ACCESS_FAILED_COUNT)] = table.ACCESS_FAILED_COUNT,
                [nameof(table.CREATION_TIME)] = table.CREATION_TIME,
                [nameof(table.MODIFICATION_TIME)] = table.MODIFICATION_TIME,
            };
        }
    }
}