
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace ghl.extension.QuartzSchedule
{
    public static class QuartzExtension
    {
        public static IServiceCollection UseQuartzFactory(this  IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, SingletonJobFactory>()
                .AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            return services;
        }
    }
}