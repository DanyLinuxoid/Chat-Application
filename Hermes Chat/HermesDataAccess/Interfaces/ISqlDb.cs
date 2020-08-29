using HermesDataAccess.Enums;
using MorseCode.ITask;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesDataAccess.Interfaces
{
    public interface ISqlDb
    {
        T Query<T>(IQuery<T> sqlQuery);

        ITask<T> QueryAsync<T>(IQuery<T> sqlQuery);

        IExecutionResult Command(ICommonCommandRepository command, CommandTypes commandType, Dictionary<string, object> parameters);

        Task<IExecutionResult> CommandAsync(ICommonCommandRepository command, CommandTypes commandType, Dictionary<string, object> parameters);
    }
}