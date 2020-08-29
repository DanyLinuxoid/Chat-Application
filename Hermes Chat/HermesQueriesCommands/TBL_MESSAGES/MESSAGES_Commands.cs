using HermesDataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesQueriesCommands.TBL_MESSAGES
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Readability")]
    public class MESSAGES_Commands : ICommonCommandRepository
    {
        private Dictionary<string, object> _defaultParameters = new Dictionary<string, object>()
        {
            ["UserName"] = null,
            ["Text"] = null,
            ["CreationTime"] = null,
            ["UserId"] = null,
        };

        private string _createMessageCommand =>
            @"INSERT INTO dbo.MESSAGES 
                (UserName, Text, CreationTime, UserId)
                OUTPUT INSERTED.Id
                VALUES 
                (@Username, @Text, @CreationTime, @UserId)";

        public IExecutionResult Create(ISession session, Dictionary<string, object> parameters)
        {
            return session.Create(_createMessageCommand, CommandHelper.GetDynamicParameters(parameters, _defaultParameters));
        }

        public Task<IExecutionResult> CreateAsync(ISession session, Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        public IExecutionResult Delete(ISession session, Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IExecutionResult> DeleteAsync(ISession session, Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        public IExecutionResult Update(ISession session, Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IExecutionResult> UpdateAsync(ISession session, Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}