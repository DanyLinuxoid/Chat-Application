using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesDataAccess.Interfaces
{
    /// <summary>
    /// Session with actual executable methods
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Simple query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sql">Sql query.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>IEnumerable sequence of elements.</returns>
        IEnumerable<T> Query<T>(string sql, object parameters = null);

        /// <summary>
        /// Simple async query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sql">Sql query.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>IEnumerable sequence of elements.</returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);

        /// <summary>
        /// Simple query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sql">Sql query.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>One element, or null if not found.</returns>
        T QueryFirstOrDefault<T>(string sql, object parameters = null);

        /// <summary>
        /// Simple async query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sql">Sql query.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>One element, or null if not found.</returns>
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null);

        /// <summary>
        /// Command to create object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        IExecutionResult Create(string sql, DynamicParameters parameters);

        /// <summary>
        /// Async command to create object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        Task<IExecutionResult> CreateAsync(string sql, DynamicParameters parameters);


        /// <summary>
        /// Command to update object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        IExecutionResult Update(string sql, DynamicParameters parameters);

        /// <summary>
        /// Async command to update object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        Task<IExecutionResult> UpdateAsync(string sql, DynamicParameters parameters);

        /// <summary>
        /// Command to delete object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        IExecutionResult Delete(string sql, DynamicParameters parameters);

        /// <summary>
        /// Async command to delete object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        Task<IExecutionResult> DeleteAsync(string sql, DynamicParameters parameters);
    }
}
