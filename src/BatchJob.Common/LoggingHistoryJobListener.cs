using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using BatchJob.Common;

namespace BatchJob.Common
{
    public class LoggingHistoryJobListener : IJobListener
    {
        public string Name { get; private set; }

        public LoggingHistoryJobListener(string name)
        {
            Name = name;
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            Logger.Write(string.Join("作业{0}准备执行.", context.JobDetail.Key.Name), LoggerLevel.Info);
        }

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            Logger.Write(string.Join("作业{0}未启动.", context.JobDetail.Key.Name), LoggerLevel.Error);
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            Logger.Write(string.Join("作业{0}执行完成.", context.JobDetail.Key.Name), LoggerLevel.Info);
        }
    }
}