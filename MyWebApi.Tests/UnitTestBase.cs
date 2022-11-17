using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApi.Tests
{
    public class UnitTestBase
    {
        public ServiceCollection Services { get; set; } = new();

        public UnitTestBase()
        {
            serviceProvider = Services.BuildServiceProvider();
        }

        public IServiceProvider serviceProvider { get; set; }

        public TImpementation MockServicesRegistrations<TService, TImpementation>(bool createMock = true)
            where TService : class
            where TImpementation:class,TService
        {
            Type serviceType = typeof(TService);
            Type implementationType = typeof(TImpementation);

            var contructorParameters = implementationType.GetConstructors().Max(i => i.GetParameters());
            var parameterTypes = contructorParameters.Select(i => i.ParameterType).ToArray();
            var registerTypes = Services.Select(i => i.ServiceType);

            var notRegisteredServices = parameterTypes.Where(i => !registerTypes.Contains(i));
            //var notRegisteredMocks = notRegisteredServices.Select(i =>
            //{

            //    var mock = new Mock();
            //});

            TImpementation service = Activator.CreateInstance<TImpementation>();

            return service;
        }

    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection MockServicesRegistratons<TService, TImpementation>(this IServiceCollection services)
        {
            Type serviceType = typeof(TService);
            Type implementationType = typeof(TService);

            var contructorParameters = serviceType.GetConstructors().Max(i => i.GetParameters());




            return services;
        }
    }
}
