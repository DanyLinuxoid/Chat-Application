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
    public class MESSAGES_Commands : ICommonCommandRepository<MESSAGES>
    {
        /// <summary>
        /// Dummy model with properties to use directly mapped names in sql.
        /// </summary>
        private MESSAGES tbl { get; } = new MESSAGES();

        /// <summary>
        /// Command to create record in database.
        /// </summary>
        private string _createCommand =>
            $@"INSERT INTO dbo.MESSAGES 
                    (USERNAME, TEXT, CREATION_TIME, ASPNET_USER_ID)
                OUTPUT INSERTED.Id
                VALUES(
                    @{nameof(tbl.USERNAME)}, 
                    @{nameof(tbl.TEXT)}, 
                    @{nameof(tbl.CREATION_TIME)},   
                    @{nameof(tbl.ASPNET_USER_ID)})";

        /// <summary>
        /// Command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Create(ISession session, MESSAGES dbo)
        {
            return session.Create(_createCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> CreateAsync(ISession session, MESSAGES dbo)
        {
            return session.CreateAsync(_createCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        ///  Command to update record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Update(ISession session, MESSAGES dbo)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> UpdateAsync(ISession session, MESSAGES dbo)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Delete(ISession session, MESSAGES dbo)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Async command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> DeleteAsync(ISession session, MESSAGES dbo)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Parameter mapper for table, later is used for dynamic parameter creation.
        /// </summary>
        /// <param name="table">Table to map parameters from/</param>
        /// <returns>Dictionary that contains parameter as key and it's value.</returns>
        private Dictionary<string, object> MapParameters(MESSAGES table)
        {
            return new Dictionary<string, object>()
            {
                [nameof(table.ID)] = table.ID,
                [nameof(table.USERNAME)] = table.USERNAME,
                [nameof(table.TEXT)] = table.TEXT,
                [nameof(table.CREATION_TIME)] = table.CREATION_TIME,
                [nameof(table.ASPNET_USER_ID)] = table.ASPNET_USER_ID,
            };
        }
    }
}