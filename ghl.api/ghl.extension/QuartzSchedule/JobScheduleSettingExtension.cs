using System;
using Quartz;

namespace ghl.extension.QuartzSchedule
{
    public static class JobsExt
    {
        public static ITrigger CreateTrigger(this JobScheduleSetting schedule)
        {
            if (schedule.WithSecondInterval)
            {
                var sec = Int32.Parse(schedule.CronExpression);
                return TriggerBuilder
                    .Create()
                    .WithIdentity($"{schedule.JobType.FullName}.trigger")
                    .WithSimpleSchedule(s=>
                        s.WithInterval(TimeSpan.FromSeconds(sec))
                        .RepeatForever())
                    .WithDescription("per second")
                    .Build();
            }

            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();


        }
        
        
        public static IJobDetail CreateJob(this JobScheduleSetting schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }
    }
}