using HermesDataAccess.Interfaces;

namespace HermesDataAccess
{
    public class ExecutionResult : IExecutionResult
    {
        public long Id { get; set; }

        public string ErrorMessage { get; set; }
    }
}
