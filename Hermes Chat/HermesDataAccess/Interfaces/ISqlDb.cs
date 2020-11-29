using HermesDataAccess.Enums;
using MorseCode.ITask;
using System.Threading.Tasks;

namespace HermesDataAccess.Interfaces
{
    /// <summary>
    /// Database operations first layer.
    /// </summary>
    public interface ISqlDb
    {
        /// <summary>
        /// Query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <returns>Entity that was queried.</returns>
        T Query<T>(IQuery<T> sqlQuery);

        /// <summary>
        /// Query to database with caching functionality.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <typeparam name="Tb">
        /// Type of identifier, works normally with string, numbers.
        /// </typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <returns>Entity that was queried from db or cache.</returns>
        T CacheNQuery<T, Tb>(IQuery<T> sqlQuery, Tb id);

        /// <summary>
        /// Async query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <param name="shouldCache">Determines if query should be cached.</param>
        /// <returns>Entity that was queried.</returns>
        ITask<T> QueryAsync<T>(IQuery<T> sqlQuery);

        /// <summary>
        /// Async query to database with caching functionality.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <typeparam name="Tb">
        /// Type of identifier, works normally with string, numbers.
        /// </typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <returns>Entity that was queried from db or cache.</returns>
        ITask<T> CacheNQueryAsync<T, Tb>(IQuery<T> sqlQuery, Tb id);

        /// <summary>
        /// Command to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="command">Command to execute.</param>
        /// <param name="commandType">Command type (create, update, delete, async versions).</param>
        /// <param name="model">Model that should be manipulated.</param>
        /// <returns>Execution result that contains id of manipulated element and error message (if error occured).</returns>
        IExecutionResult Command<T>(ICommonCommandRepository<T> command, CommandTypes commandType, T model) where T : class;

        /// <summary>
        /// Async command to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="command">Command to execute.</param>
        /// <param name="commandType">Command type (create, update, delete, async versions).</param>
        /// <param name="model">Model that should be manipulated.</param>
        /// <returns>Execution result that contains id of manipulated element and error message (if error occured).</returns>
        Task<IExecutionResult> CommandAsync<T>(ICommonCommandRepository<T> command, CommandTypesAsync commandType, T model) where T : class;
    }
}