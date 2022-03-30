using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ghl.extension.DependencyInjection
{
    public static class DIExtiension
    {
        public static IHostBuilder UseDIModuleconfig(this IHostBuilder webHostBuilder)
        {
            return webHostBuilder.ConfigureServices((hostBuilder,services) =>
                {
                    var inheresObj = Assembly.GetEntryAssembly()
                        .GetTypes()
                        .Where(myType => myType.IsSubclassOf(typeof(DIModuleConfigure)));


                    foreach (var item in inheresObj)
                    {
                        // 忽略標示為 [Obsolete] 的模組
                        if (item.GetCustomAttributes<ObsoleteAttribute>().Any())
                        {
                            continue;
                        }

                        var instance = Activator.CreateInstance(item);
                        var mi = item.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                            .FirstOrDefault();
                        mi.Invoke(instance, new object[] {hostBuilder, services});
                    }
                }
            );
        }
        
        /// <summary>
        /// Console使用
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection UseDIModuleconfig(this IServiceCollection service, IConfiguration configuration)
        {
            var inheresObj = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(myType => myType.IsSubclassOf(typeof(ConsoleModuleConfigure)));

            foreach (var type in inheresObj)
            {
                // 忽略標示為 [Obsolete] 的模組
                if (type.GetCustomAttributes<ObsoleteAttribute>().Any())
                {
                    continue;
                }

                var module = Activator.CreateInstance(type) as ConsoleModuleConfigure;
  
                
                var instance = Activator.CreateInstance(type);
                var mi = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                    .FirstOrDefault();
                mi.Invoke(instance, new object[] {service, configuration});
            }

            return service;
        }

        //IServiceCollection
    }
}