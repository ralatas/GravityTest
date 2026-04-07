using System;
using System.Collections.Generic;

namespace GravityTest.Scripts.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static void Register<TService>(TService service) where TService : class
        {
            Services[typeof(TService)] = service;
        }

        public static TService Resolve<TService>() where TService : class
        {
            if (Services.TryGetValue(typeof(TService), out var service))
            {
                return service as TService;
            }

            throw new InvalidOperationException($"Service of type {typeof(TService).Name} is not registered.");
        }
    }
}
