using AuthenAuthorAccount.Authentication;
using HermesDataAccess;
using HermesDataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HermesLogic
{
    public class DependencyInjector
    {
        /// <summary>
        /// Registers all clases and returns container
        /// </summary>
        /// <returns>Container with registered clases</returns>
        public void RegisterClasses(IServiceCollection container)
        {
            var injectedClases = new Dictionary<Type, Type>();
            injectedClases = GetAssemblyClases(injectedClases, typeof(AuthenticationLogic));
            injectedClases = GetAssemblyClases(injectedClases, typeof(DependencyInjector));

            InjectSpecials(container);

            foreach (var classWithInterfacePair in injectedClases)
            {
                container.AddTransient(classWithInterfacePair.Key, classWithInterfacePair.Value);
            }
        }

        private void InjectSpecials(IServiceCollection container)
        {
            // Access http in library
            container.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // Data Access
            container.AddTransient<ISqlDb, SqlDb>();
            container.AddTransient<HermesDataAccess.Interfaces.ISession, Session>();
        }

        private Dictionary<Type, Type> GetAssemblyClases(Dictionary<Type, Type> injectionHolder, Type assemblyType)
        {
            var logicClasses = assemblyType.Assembly.GetExportedTypes() // to method
                .Where(type => type.Namespace != null &&
                           type.GetInterfaces().Length > 0 &&
                          !type.IsGenericType &&
                          !type.IsEnum &&
                          !type.IsAbstract);

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
    }
}