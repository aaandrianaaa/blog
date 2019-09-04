using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Jobs
{
    public interface IUserFactory
    {
        IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler);
        void ReturnJob(IJob job);
    }

    public class UserFactory : IUserFactory
    {
        protected readonly IServiceProvider Container;

        public UserFactory(IServiceProvider container)
        {
            Container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return Container.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            
        }
    }
}
