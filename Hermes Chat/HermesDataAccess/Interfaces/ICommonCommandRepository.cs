using System.Threading.Tasks;

namespace HermesDataAccess.Interfaces
{
    /// <summary>
    /// Repository of CUD commands + async version.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    public interface ICommonCommandRepository<T> where T : class
    {
        /// <summary>
        /// Command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        IExecutionResult Create(ISession session, T dbo);

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to create.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        Task<IExecutionResult> CreateAsync(ISession session, T dbo);

        /// <summary>
        ///  Command to update record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        IExecutionResult Update(ISession session, T dbo);

        /// <summary>
        /// Async command to create record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to update.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        Task<IExecutionResult> UpdateAsync(ISession session, T dbo);

        /// <summary>
        /// Command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        IExecutionResult Delete(ISession session,  T dbo);

        /// <summary>
        /// Async command to delete record in database.
        /// </summary>
        /// <param name="session">Actual method container.</param>
        /// <param name="dbo">Model to delete.</param>
        /// <returns>Execution result which conatins created row id or error message if failed.</returns>
        Task<IExecutionResult> DeleteAsync(ISession session,  T dbo);
    }
}