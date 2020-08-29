using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesDataAccess.Interfaces
{
    public interface ICommonCommandRepository
    {
        IExecutionResult Create(ISession session, Dictionary<string, object> parameters);

        Task<IExecutionResult> CreateAsync(ISession session, Dictionary<string, object> parameters);

        IExecutionResult Update(ISession session, Dictionary<string, object> parameters);

        Task<IExecutionResult> UpdateAsync(ISession session, Dictionary<string, object> parameters);

        IExecutionResult Delete(ISession session,  Dictionary<string, object> parameters);

        Task<IExecutionResult> DeleteAsync(ISession session,  Dictionary<string, object> parameters);
    }
}