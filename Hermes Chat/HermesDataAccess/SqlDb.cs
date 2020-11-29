using HermesDataAccess.Enums;
using HermesDataAccess.Interfaces;
using HermesShared.Caching;
using MorseCode.ITask;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HermesDataAccess
{
    /// <summary>
    /// Database operations first layer.
    /// </summary>
    public class SqlDb : ISqlDb
    {
        /// <summary>
        /// Contains actual called methods.
        /// </summary>
        private readonly ISession _session;

        /// <summary>
        /// Fuctionality to cache queries and retrieve them from cache.
        /// </summary>
        private readonly ICacheNQuery _cacheNQuery;

        /// <summary>
        /// Implementation of database operations.
        /// </summary>
        public SqlDb(
            ISession session,
            ICacheNQuery cacheNQuery)
        {
            _session = session;
            _cacheNQuery = cacheNQuery;
        }

        /// <summary>
        /// Query to database.
        /// </summary> 
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sqlQuery">Query to execute.</param>
        /// <returns>Entity that was queried.</returns>
        public T Query<T>(IQuery<T> sqlQuery)
        {
            return sqlQuery.Execute(_session);
        }

        /// <summary>
        /// Query to database with caching functionality.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <typeparam name="Tb">
        /// Type of identifier, works normally with string, numbers.
        /// </typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <returns>Entity that was queried from db or cache.</returns>
        public T CacheNQuery<T, Tb>(IQuery<T> sqlQuery, Tb id)
        {
            // 'sqlQuery.ToString()' formats to smth like 'Project.Folder.Query', we are getting name of path after last dot as cache key (query name).
            string cacheKey = string.Join(":", sqlQuery.ToString().Split('.').Last(), id.ToString());
            return _cacheNQuery.CacheNExecute(
                cacheKey,
                () => sqlQuery.Execute(_session));
        }

        /// <summary>
        /// Async query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <returns>Entity that was queried.</returns>
        public ITask<T> QueryAsync<T>(IQuery<T> sqlQuery)
        {
            return sqlQuery.ExecuteAsync(_session);
        }

        /// <summary>
        /// Async query to database with caching functionality.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <typeparam name="Tb">
        /// Type of identifier, works normally with string, numbers.
        /// </typeparam>
        /// <param name="sqlQuery">Query.</param>
        /// <returns>Entity that was queried from db or cache.</returns>
        public async ITask<T> CacheNQueryAsync<T, Tb>(IQuery<T> sqlQuery, Tb id)
        {
            // 'sqlQuery.ToString()' formats to smth like 'Project.Folder.Query', we are getting name of path after last dot as cache key (query name).
            string cacheKey = string.Join(":", sqlQuery.ToString().Split('.').Last(), id.ToString());
            return await _cacheNQuery.CacheNExecute(
                cacheKey,
                () => sqlQuery.ExecuteAsync(_session));
        }

        /// <summary>
        /// Command to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="command">Command to execute.</param>
        /// <param name="commandType">Command type (create, update, delete, async versions).</param>
        /// <param name="model">Model that should be manipulated.</param>
        /// <returns>Execution result that contains id of manipulated element and error message (if error occured).</returns>
        public IExecutionResult Command<T>(ICommonCommandRepository<T> command, CommandTypes commandType, T model) where T : class
        {
            switch(commandType)
            {
                case CommandTypes.Create:
                    return command.Create(_session, model);
                case CommandTypes.Update:
                    return command.Update(_session, model);
                case CommandTypes.Delete:
                    return command.Delete(_session, model);
                default:
                    throw new InvalidEnumArgumentException($"{ commandType } is unknown type");
            }
        }

        /// <summary>
        /// Async command to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="command">Command to execute.</param>
        /// <param name="commandType">Command type (create, update, delete, async versions).</param>
        /// <param name="model">Model that should be manipulated.</param>
        /// <returns>Execution result that contains id of manipulated element and error message (if error occured).</returns>
        public async Task<IExecutionResult> CommandAsync<T>(ICommonCommandRepository<T> command, CommandTypesAsync commandType, T model) where T : class
        {
            switch(commandType)
            {
                case CommandTypesAsync.CreateAsync:
                    return await command.CreateAsync(_session, model);
                case CommandTypesAsync.UpdateAsync:
                    return await command.UpdateAsync(_session, model);
                case CommandTypesAsync.DeleteAsync:
                    return await command.DeleteAsync(_session, model);
                default:
                    throw new InvalidEnumArgumentException($"{ commandType } is unknown type");
            }
        }
    }
}