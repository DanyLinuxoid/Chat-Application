using Dapper;
using HermesDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HermesDataAccess
{
    public class Session : ISession
    {
        private string ConnectionString { get; } = "Server=(localdb)\\MSSQLLocalDB;Database=LocalHermesChatAlhambra;Trusted_Connection=True;MultipleActiveResultSets=true";

        public IEnumerable<T> Query<T>(string sql, object parameters = null) where T : class
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<T>(sql, parameters);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null) where T : class
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.QueryAsync<T>(sql, parameters);
            }
        }

        public T QueryFirstOrDefault<T>(string sql, object parameters = null) where T : class
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null) where T : class
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        public IExecutionResult Create(string sql, DynamicParameters parameters) 
        {
            return ExecuteCommand(sql, parameters);
        }

        public Task<IExecutionResult> CreateAsync(string sql, DynamicParameters parameters)
        {
            return ExecuteCommandAsync(sql, parameters);
        }

        public IExecutionResult Update(string sql, DynamicParameters parameters)
        {
            return ExecuteCommand(sql, parameters);
        }

        public Task<IExecutionResult> UpdateAsync(string sql, DynamicParameters parameters)
        {
            return ExecuteCommandAsync(sql, parameters);
        }

        public IExecutionResult Delete(string sql, DynamicParameters parameters) 
        {
            return ExecuteCommand(sql, parameters);
        }

        public Task<IExecutionResult> DeleteAsync(string sql, DynamicParameters parameters)
        {
            return ExecuteCommandAsync(sql, parameters);
        }

        private IExecutionResult ExecuteCommand(string sql, DynamicParameters parameters)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConnectionString))
                {
                    return new ExecutionResult()
                    {
                        Id = db.ExecuteScalar<int>(sql, new DynamicParameters(parameters)),
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

        private async Task<IExecutionResult> ExecuteCommandAsync(string sql, DynamicParameters parameters)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConnectionString))
                {
                    return new ExecutionResult()
                    {
                        Id = await db.ExecuteScalarAsync<int>(sql, new DynamicParameters(parameters)),
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