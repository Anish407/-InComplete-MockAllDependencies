using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public Mock<TService> MockServicesRegistrations<TService, TImpementation>(bool createMock = true)
            where TService : class
            where TImpementation : class, TService
        {
            Type serviceType = typeof(TService);
            Type implementationType = typeof(TImpementation);

            var contructorParameters = implementationType.GetConstructors().Max(i => i.GetParameters());
            var parameterTypes = contructorParameters.Select(i => i.ParameterType).ToArray();
            var registerTypes = Services.Select(i => i.ServiceType);
            serviceProvider = Services.BuildServiceProvider();

            var notRegisteredServices = parameterTypes.Where(i => !registerTypes.Contains(i));
            var notRegisteredMocks = notRegisteredServices.Select(i =>
            {
                var mockedTyped = typeof(Mock<>).MakeGenericType(i);
                //MethodInfo method = mockedTyped.GetType().GetMethod("Object");
                //object result = method.Invoke(mockedTyped, null);

                var instance = Activator.CreateInstance(mockedTyped);
                PropertyInfo? property = instance.GetType().GetPropertyUnambiguous("Object", BindingFlags.Public | BindingFlags.Instance );
                object result = property.GetValue(instance, null);

                return instance;
            });

            var x = notRegisteredMocks.Select(i => i).ToList();
            var regTypes = registerTypes.Select(i => (object)serviceProvider.GetRequiredService(i));
            x.AddRange(regTypes);

            List<object> objs = new List<object>();

            for (int i = 0; i < contructorParameters.Length; i++)
            {
                string name = contructorParameters[i].Name;
                var y = x.Where(item => item.GetType().Name == name).ToList();
                objs.Add(y);
            }

            var instance = Activator.CreateInstance(implementationType, x);
            Mock<TService> service = new Mock<TService>();

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

        public static PropertyInfo GetPropertyUnambiguous(this Type type, string name, BindingFlags flags)
{
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (name == null) throw new ArgumentNullException(nameof(name));

            flags |= BindingFlags.DeclaredOnly;

            while (type != null)
            {
                var property = type.GetProperty(name, flags);

                if (property != null)
                {
                    return property;
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}
