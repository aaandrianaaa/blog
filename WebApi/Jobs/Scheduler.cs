using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApi.Jobs
{
    public class Scheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<UserJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()  // create trigger
                .WithIdentity("trigger1", "group1")     // identify trigger with name and group
                .StartNow()                            // start right after the start of execution
                .WithSimpleSchedule(x => x            //customize the execution of the action
                    .WithIntervalInMinutes(1)          // after  1 day
                    .RepeatForever())                   // endless repetition
                .Build();                               // create trigger

            await scheduler.ScheduleJob(job, trigger);        //  start work
        }
    }
}
