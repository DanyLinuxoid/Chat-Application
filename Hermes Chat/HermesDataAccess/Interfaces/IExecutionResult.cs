namespace HermesDataAccess.Interfaces
{
    public interface IExecutionResult
    {
        long Id { get; set; }

        string ErrorMessage { get; set; }
    }
}