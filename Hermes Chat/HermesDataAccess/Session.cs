using Dapper;
using HermesDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HermesDataAccess
{
    /// <summary>
    /// Session with actual executable methods
    /// </summary>
    public class Session : ISession
    {
        /// <summary>
        /// Connection string to db.
        /// </summary>
        private string ConnectionString { get; } = "Server=(localdb)\\MSSQLLocalDB;Database=LocalHermesChatAlhambra;Trusted_Connection=True;MultipleActiveResultSets=true";

        /// <summary>
        /// Simple query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sql">Sql query.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>IEnumerable sequence of elements.</returns>
        public IEnumerable<T> Query<T>(string sql, object parameters = null) 
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<T>(sql, parameters);
            }
        }

        /// <summary>
        /// Simple async query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sql">Sql query.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>IEnumerable sequence of elements.</returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null) 
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.QueryAsync<T>(sql, parameters);
            }
        }

        /// <summary>
        /// Simple query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sql">Sql query.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>One element, or null if not found.</returns>
        public T QueryFirstOrDefault<T>(string sql, object parameters = null) 
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        /// <summary>
        /// Simple async query to database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="sql">Sql query.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>One element, or null if not found.</returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null) 
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        /// <summary>
        /// Command to create object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        public IExecutionResult Create(string sql, DynamicParameters parameters) 
        {
            return ExecuteCommand(sql, parameters);
        }

        /// <summary>
        /// Async command to create object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        public Task<IExecutionResult> CreateAsync(string sql, DynamicParameters parameters)
        {
            return ExecuteCommandAsync(sql, parameters);
        }

        /// <summary>
        /// Command to update object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        public IExecutionResult Update(string sql, DynamicParameters parameters)
        {
            return ExecuteCommand(sql, parameters);
        }

        /// <summary>
        /// Async command to update object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        public Task<IExecutionResult> UpdateAsync(string sql, DynamicParameters parameters)
        {
            return ExecuteCommandAsync(sql, parameters);
        }

        /// <summary>
        /// Command to delete object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        public IExecutionResult Delete(string sql, DynamicParameters parameters) 
        {
            return ExecuteCommand(sql, parameters);
        }

        /// <summary>
        /// Async command to delete object in database.
        /// </summary>
        /// <param name="sql">Command to execute.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Execution result object which contains inserted id, or error message if errro occured.</returns>
        public Task<IExecutionResult> DeleteAsync(string sql, DynamicParameters parameters)
        {
            return ExecuteCommandAsync(sql, parameters);
        }

        /// <summary>
        /// Centrilized method to execute CUD commands.
        /// </summary>
        /// <param name="sql">Sql of command to execute.</param>
        /// <param name="parameters">Dynamic parameters for command.</param>
        /// <returns>Result of execution which contains id of processed object or error message if something went wrong.</returns>
        private IExecutionResult ExecuteCommand(string sql, DynamicParameters parameters)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConnectionString))
                {
                    return new ExecutionResult()
                    {
                        Id = db.ExecuteScalar<long>(
                            sql: sql,
                            param: new DynamicParameters(parameters)),
                    };
                }
            }
            catch(Exception ex)
            {
                return new ExecutionResult()
                {
                    ErrorMessage = ex.Message,
                };
            }
        }

        /// <summary>
        /// Centrilized method to execute CUD commands.
        /// </summary>
        /// <param name="sql">Sql of command to execute.</param>
        /// <param name="parameters">Dynamic parameters for command.</param>
        /// <returns>Result of execution which contains id of processed object or error message if something went wrong.</returns>
        private async Task<IExecutionResult> ExecuteCommandAsync(string sql, DynamicParameters parameters)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConnectionString))
                {
                    return new ExecutionResult()
                    {
                        Id = await db.ExecuteScalarAsync<long>(
                            sql: sql,
                            param: new DynamicParameters(parameters)),
                    };
                }
            }
            catch(Exception ex)
            {
                return new ExecutionResult()
                {
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}