using System;

namespace ghl.extension.QuartzSchedule
{
    public class JobScheduleSetting
    {
        public JobScheduleSetting(Type jobType, string cronExpression,bool withSecondInterval=false)
        {
            JobType = jobType;
            CronExpression = cronExpression;
            WithSecondInterval = withSecondInterval;
        }

        public Type JobType { get; }
        public string CronExpression { get; }
        public bool WithSecondInterval { get; } = false;

    }
}