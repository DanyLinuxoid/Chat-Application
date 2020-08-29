using HermesDataAccess.Enums;
using HermesDataAccess.Interfaces;
using MorseCode.ITask;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HermesDataAccess
{
    public class SqlDb : ISqlDb
    {
        private readonly ISession _session;

        public SqlDb(ISession session)
        {
            _session = session;
        }

        public T Query<T>(IQuery<T> sqlQuery)
        {
            return sqlQuery.Execute(_session);
        }

        public ITask<T> QueryAsync<T>(IQuery<T> sqlQuery)
        {
            return sqlQuery.ExecuteAsync(_session);
        }

        public IExecutionResult Command(ICommonCommandRepository command, CommandTypes commandType, Dictionary<string, object> parameters) 
        {
            switch(commandType)
            {
                case CommandTypes.Create:
                    return command.Create(_session, parameters);
                case CommandTypes.Update:
                    return command.Update(_session, parameters);
                case CommandTypes.Delete:
                    return command.Delete(_session, parameters);
                default:
                    throw new InvalidEnumArgumentException($"{ commandType } is unknown type");
            }
        }

        public Task<IExecutionResult> CommandAsync(ICommonCommandRepository command, CommandTypes commandType, Dictionary<string, object> parameters) 
        {
            switch(commandType)
            {
                case CommandTypes.CreateAsync:
                    return command.CreateAsync(_session, parameters);
                case CommandTypes.UpdateAsync:
                    return command.UpdateAsync(_session, parameters);
                case CommandTypes.DeleteAsync:
                    return command.DeleteAsync(_session, parameters);
                default:
                    throw new InvalidEnumArgumentException($"{ commandType } is unknown type");
            }
        }
    }
}