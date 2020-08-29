namespace HermesDataAccess.Interfaces
{
    public interface IExecutionResult
    {
        int Id { get; set; }

        string ErrorMessage { get; set; }
    }
}