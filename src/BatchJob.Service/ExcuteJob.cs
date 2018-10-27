using BatchJob.Repository;
using BatchJob.ServiceInterface;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Service
{
    public class ExcuteJob : IJob
    {
        public IJobService JobService;

        public ExcuteJob()
        {
            JobService = new JobService();
        }

        public void Execute(IJobExecutionContext context)
        {
            var jobGroup = context.JobDetail.Key.Group;
            var jobCode = context.JobDetail.Key.Name;
            DateTime? nextExcuteDate = null;
            if (context.Trigger.GetNextFireTimeUtc().HasValue)
            {
                nextExcuteDate = Convert.ToDateTime(context.Trigger.GetNextFireTimeUtc().Value.ToLocalTime().ToString());
            }
            JobService.ExcuteJob(jobGroup, jobCode, nextExcuteDate);
        }
    }
}
