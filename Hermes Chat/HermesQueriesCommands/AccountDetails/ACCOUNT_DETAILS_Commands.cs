using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesQueriesCommands.Commands
{
    public class ACCOUNT_DETAILS_Commands : ICommonCommandRepository<ACCOUNT_DETAILS>
    {
        /// <summary>
        /// Dummy model with properties to use directly mapped names in sql.
        /// </summary>
        private ACCOUNT_DETAILS tbl { get; } = new ACCOUNT_DETAILS();

        /// <summary>
        /// Command to create record in database.
        /// </summary>
        private string _createCommand =>
            $@"INSERT INTO dbo.ACCOUNT_DETAILS
	                (USERNAME,
                    PASSWORD_HASH, 
                    EMAIL, 
                    IS_EMAIL_CONFIRMED, 
                    PHONE_NUMBER, 
                    IS_PHONE_NUMBER_CONFIRMED,
                    IS_TWO_FACTOR_ENABLED,
                    ABOUT_ME,
                    PROFILE_PHOTO_ID,
                    CREATION_TIME,
                    MODIFICATION_TIME,
                    ASPNET_USER_ID)
                OUTPUT INSERTED.Id
	            VALUES(
                     @{nameof(tbl.USERNAME)},
                     @{nameof(tbl.PASSWORD_HASH)},
                     @{nameof(tbl.EMAIL)},
                     @{nameof(tbl.IS_EMAIL_CONFIRMED)},
                     @{nameof(tbl.PHONE_NUMBER)},
                     @{nameof(tbl.IS_PHONE_NUMBER_CONFIRMED)},
                     @{nameof(tbl.IS_TWO_FACTOR_ENABLED)},
                     @{nameof(tbl.ABOUT_ME)},
                     @{nameof(tbl.PROFILE_PHOTO_ID)},
                     GETDATE(),
                     GETDATE(),
                     @{nameof(tbl.ASPNET_USER_ID)})";

        /// <summary>
        /// Command to update record in database.
        /// </summary>
        private string _updateCommand =>
            $@"UPDATE dbo.ACCOUNT_DETAILS
                SET USERNAME = ISNULL(@{nameof(tbl.USERNAME)}, {nameof(tbl.USERNAME)}),
                       PASSWORD_HASH = ISNULL(@{nameof(tbl.PASSWORD_HASH)}, {nameof(tbl.PASSWORD_HASH)}),
                       EMAIL = ISNULL(@{nameof(tbl.EMAIL)}, {nameof(tbl.EMAIL)}),
                       IS_EMAIL_CONFIRMED = ISNULL(@{nameof(tbl.IS_EMAIL_CONFIRMED)}, {nameof(tbl.IS_EMAIL_CONFIRMED)}),
                       PHONE_NUMBER = ISNULL(@{nameof(tbl.PHONE_NUMBER)}, {nameof(tbl.PHONE_NUMBER)}),
                       IS_PHONE_NUMBER_CONFIRMED = ISNULL(@{nameof(tbl.IS_PHONE_NUMBER_CONFIRMED)}, {nameof(tbl.IS_PHONE_NUMBER_CONFIRMED)}),
                       IS_TWO_FACTOR_ENABLED = ISNULL(@{nameof(tbl.IS_TWO_FACTOR_ENABLED)}, {nameof(tbl.IS_TWO_FACTOR_ENABLED)}),
                       ABOUT_ME = ISNULL(@{nameof(tbl.ABOUT_ME)}, {nameof(tbl.ABOUT_ME)}),
                       PROFILE_PHOTO_ID = ISNULL(@{nameof(tbl.PROFILE_PHOTO_ID)}, {nameof(tbl.PROFILE_PHOTO_ID)}),
                       MODIFICATION_TIME = GETDATE()
                OUTPUT INSERTED.Id
                WHERE ID = @{nameof(tbl.ID)}";

        /// <summary>
        /// Command to delete record from database.
        /// </summary>
        private string _deleteCommand =>
            $@"DELETE FROM dbo.ACCOUNT_DETAILS
                            WHERE ID = @{nameof(tbl.ID)}";

        /// <summary>
        /// Command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Create(ISession session, ACCOUNT_DETAILS dbo)
        {
            return session.Create(_createCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> CreateAsync(ISession session, ACCOUNT_DETAILS dbo)
        {
            return session.CreateAsync(_createCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        ///  Command to update record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Update(ISession session, ACCOUNT_DETAILS dbo)
        {
            return session.Update(_updateCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> UpdateAsync(ISession session, ACCOUNT_DETAILS dbo)
        {
            return session.UpdateAsync(_updateCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public IExecutionResult Delete(ISession session, ACCOUNT_DETAILS dbo)
        {
            return session.Delete(_deleteCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Async command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        public Task<IExecutionResult> DeleteAsync(ISession session, ACCOUNT_DETAILS dbo)
        {
            return session.DeleteAsync(_deleteCommand, CommandHelper.GetDynamicParameters(MapParameters(dbo)));
        }

        /// <summary>
        /// Parameter mapper for table, later is used for dynamic parameter creation.
        /// </summary>
        /// <param name="table">Table to map parameters from/</param>
        /// <returns>Dictionary that contains parameter as key and it's value.</returns>
        private Dictionary<string, object> MapParameters(ACCOUNT_DETAILS table)
        {
            return new Dictionary<string, object>()
            {
                [nameof(table.ID)] = table.ID,
                [nameof(table.ABOUT_ME)] = table.ABOUT_ME,
                [nameof(table.ASPNET_USER_ID)] = table.ASPNET_USER_ID,
                [nameof(table.EMAIL)] = table.EMAIL,
                [nameof(table.IS_EMAIL_CONFIRMED)] = table.IS_EMAIL_CONFIRMED,
                [nameof(table.IS_PHONE_NUMBER_CONFIRMED)] = table.IS_PHONE_NUMBER_CONFIRMED,
                [nameof(table.IS_TWO_FACTOR_ENABLED)] = table.IS_TWO_FACTOR_ENABLED,
                [nameof(table.MODIFICATION_TIME)] = table.MODIFICATION_TIME,
                [nameof(table.CREATION_TIME)] = table.CREATION_TIME,
                [nameof(table.PASSWORD_HASH)] = table.PASSWORD_HASH,
                [nameof(table.PHONE_NUMBER)] = table.PHONE_NUMBER,
                [nameof(table.PROFILE_PHOTO_ID)] = table.PROFILE_PHOTO_ID,
                [nameof(table.USERNAME)] = table.USERNAME,
            };
        }
    }
}
