using HermesLogic.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HermesLogic.Base
{
    public class CommonDependencies : ICommonDependencies 
    {
        public IUserManager UserManager { get; }

        public IMemoryCache MemoryCache { get; }

        public IApplicationValidatorFactory ApplicationValidatorFactory { get; }

        public CommonDependencies
            (IUserManager userManager, IMemoryCache memoryCache, IApplicationValidatorFactory applicationValidatorFactory)
        {
            UserManager = userManager;
            MemoryCache = memoryCache;
            ApplicationValidatorFactory = applicationValidatorFactory;
        }
    }
}