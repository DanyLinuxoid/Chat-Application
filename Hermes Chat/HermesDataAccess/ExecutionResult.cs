using HermesDataAccess.Interfaces;

namespace HermesDataAccess
{
    public class ExecutionResult : IExecutionResult
    {
        public int Id { get; set; }

        public string ErrorMessage { get; set; }
    }
}
