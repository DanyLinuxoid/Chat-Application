using MorseCode.ITask;

namespace HermesDataAccess.Interfaces
{
    public interface IQuery<out T> 
    {
        T Execute(ISession session);

        ITask<T> ExecuteAsync(ISession session);
    }
}