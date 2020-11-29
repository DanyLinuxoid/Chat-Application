using AuthenAuthorAccount.Authentication;
using AuthenAuthorAccount.Cookies;
using AuthenAuthorAccount.Interfaces;
using HermesAuthentication.Session;
using HermesDataAccess;
using HermesDataAccess.Interfaces;
using HermesLogic.Base.UserManagement;
using HermesLogic.Features.Chat;
using HermesLogic.Files;
using HermesShared.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HermesWeb
{
    /// <summary>
    /// Dependency injection class.
    /// </summary>
    public static class DependencyInjector
    {
        /// <summary>
        /// Out of box service provider.
        /// </summary>
        private static IServiceProvider _serviceProvider { get; set; }

        /// <summary>
        /// Registers all clases and returns container
        /// </summary>
        /// <returns>Container with registered clases</returns>
        public static void RegisterClasses(IServiceCollection services)
        {
            var injectedClases = new Dictionary<Type, Type>();

            // Logic classes can be injected automatically.
            injectedClases = GetAssemblyClasesForInjection(injectedClases, typeof(ChatLogic));

            InjectManual(services);

            foreach (var classWithInterfacePair in injectedClases)
            {
                services.AddTransient(classWithInterfacePair.Key, classWithInterfacePair.Value);
            }
        }

        /// <summary>
        /// Sets provider.
        /// </summary>
        /// <param name="serviceProvider">Provider to set.</param>
        public static void SetProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Service locator.
        /// </summary>
        /// <typeparam name="T">Type of service.</typeparam>
        /// <returns>Requested service.</returns>
        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        /// <summary>
        /// Manual injection of services.
        /// </summary>
        /// <param name="container"></param>
        private static void InjectManual(IServiceCollection container)
        {
            // Access http in library
            container.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // Data Access
            container.AddTransient<ISqlDb, SqlDb>();
            container.AddTransient<HermesDataAccess.Interfaces.ISession, Session>();

            // Common dependencies / Shared
            container.AddTransient<ICacheManager, CacheManager>();
            container.AddTransient<ICacheNQuery, CacheNQuery>();
            container.AddTransient<IFileLogic, FileLogic>();

            // Authentication
            container.AddSingleton<ICookieLogic, CookieLogic>();
            container.AddSingleton<ISessionLogic, SessionLogic>();
            container.AddTransient<IAuthenticationLogic, AuthenticationLogic>();

            // User management
            container.AddSingleton<IUserManager, UserManager>();
        }

        /// <summary>
        /// Automatically gets classes from assembly by provided assemblyType, for DI.
        /// </summary>
        /// <param name="injectionHolder">Container with classes that will be injected later.</param>
        /// <param name="assemblyType">Assembly type from where to take classes.</param>
        /// <returns></returns>
        private static Dictionary<Type, Type> GetAssemblyClasesForInjection(Dictionary<Type, Type> injectionHolder, Type assemblyType)
        {
            var logicClasses = assemblyType.Assembly.GetExportedTypes() // to method
                .Where(type => type.Namespace != null &&
                           type.GetInterfaces().Length > 0 &&
                          !type.IsGenericType &&
                          !type.IsEnum &&
                          !GetTypesToIgnore().Contains(type));

            foreach (var type in logicClasses)
            {
                foreach (var interfce in type.GetInterfaces()) // to method
                {
                    if (!injectionHolder.ContainsKey(interfce))
                    {
                        injectionHolder.Add(interfce, type);
                    }
                }
            }

            return injectionHolder;
        }

        /// <summary>
        /// Gets types that should be ignored during automatic injection.
        /// </summary>
        /// <returns>List with types that should be ignored.</returns>
        private static List<Type> GetTypesToIgnore()
        {
            return new List<Type>()
            {
                typeof (UserManager),
            };
        }
    }
}