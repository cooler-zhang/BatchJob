using System;
using System.Linq;
using Quartz;
using System.Diagnostics;
using BatchJob.Common;

namespace BatchJob.Common
{
    public class LoggingHistoryTriggerListener : ITriggerListener
    {
        public string Name { get; private set; }

        public LoggingHistoryTriggerListener(string name)
        {
            Name = name;
        }

        public bool VetoJobExecution(ITrigger trigger, IJobExecutionContext context)
        {
            return false;
        }

        public void TriggerFired(ITrigger trigger, IJobExecutionContext context)
        {
            Logger.Write(string.Join("触发器{0}点火.", trigger.Key.Name), LoggerLevel.Info);
        }

        public void TriggerMisfired(ITrigger trigger)
        {
            Logger.Write(string.Join("触发器{0}熄火.", trigger.Key.Name), LoggerLevel.Error);
        }

        public void TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode)
        {
            Logger.Write(string.Join("触发器{0}执行完成.", trigger.Key.Name), LoggerLevel.Info);
        }
    }
}