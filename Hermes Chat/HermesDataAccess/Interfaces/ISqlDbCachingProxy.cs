using HermesDataAccess.Enums;
using MorseCode.ITask;
using System.Threading.Tasks;

namespace HermesDataAccess.Interfaces
{
    /// <summary>
    /// Proxy for database, with query caching functionality.
    /// </summary>
    public interface ISqlDbCachingProxy 
    {
        /// <summary>
        /// Query to database with caching functionality.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <returns>Entity that was queried from db or cache.</returns>
        T Query<T, Tb>(IQuery<T> sqlQuery, Tb id);

        /// <summary>
        /// Async query to database with caching functionality.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <returns>Entity that was queried from db or cache.</returns>
        ITask<T> QueryAsync<T, Tb>(IQuery<T> sqlQuery, Tb id);

        /// <summary>
        /// Delegates command to real object, as there is no caching possibility for commands.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="command">Command to execute.</param>
        /// <param name="commandType">Command type (create, update, delete).</param>
        /// <param name="model">Model that should be manipulated.</param>
        /// <returns>Execution result that contains id of manipulated element and error message (if error occured).</returns>
        IExecutionResult Command<T>(ICommonCommandRepository<T> command, CommandTypes commandType, T model) where T : class;

        /// <summary>
        /// Delegates command to real object, as there is no caching possibility for commands.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="command">Command to execute.</param>
        /// <param name="commandType">Command type (create, update, delete, async versions).</param>
        /// <param name="model">Model that should be manipulated.</param>
        /// <returns>Execution result that contains id of manipulated element and error message (if error occured).</returns>
        Task<IExecutionResult> CommandAsync<T>(ICommonCommandRepository<T> command, CommandTypesAsync commandType, T model) where T : class;

    }
}
