using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Jobs;

namespace WebApi.Extension
{
    public static class QuartzExtensions
    {
        public static void UseQuartz(this IServiceCollection services, params Type[] jobs)
        {
            services.AddSingleton<IUserFactory, UserFactory>();
            var descriptors = jobs.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton));

            foreach (var descriptor in descriptors)
            {
                services.Add(descriptor);
            }

            services.AddSingleton(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler();
                scheduler.Result.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });
        }
    }
}
