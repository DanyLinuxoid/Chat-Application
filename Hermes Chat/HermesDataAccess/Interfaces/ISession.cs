using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesDataAccess.Interfaces
{
    public interface ISession
    {
        IEnumerable<T> Query<T>(string sql, object parameters = null) where T : class;

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null) where T : class;

        T QueryFirstOrDefault<T>(string sql, object parameters = null) where T : class;

        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null) where T : class;

        IExecutionResult Create(string sql, DynamicParameters parameters);

        Task<IExecutionResult> CreateAsync(string sql, DynamicParameters parameters);

        IExecutionResult Update(string sql, DynamicParameters parameters);

        Task<IExecutionResult> UpdateAsync(string sql, DynamicParameters parameters);

        IExecutionResult Delete(string sql, DynamicParameters parameters);

        Task<IExecutionResult> DeleteAsync(string sql, DynamicParameters parameters);
    }
}
