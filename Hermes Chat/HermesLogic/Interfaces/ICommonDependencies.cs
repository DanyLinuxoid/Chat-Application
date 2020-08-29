using Microsoft.Extensions.Caching.Memory;

namespace HermesLogic.Interfaces
{
    public interface ICommonDependencies
    {
        IUserManager UserManager { get; }

        IMemoryCache MemoryCache { get; }

        IApplicationValidatorFactory ApplicationValidatorFactory { get; }
    }
}
