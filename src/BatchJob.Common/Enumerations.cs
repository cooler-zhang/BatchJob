using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Common
{
    public enum SchedulerStatus
    {
        Running = 0,
        Stoped = 1
    }

    public enum SchedulerPriority
    {
        Low = 0,
        Normal = 1,
        High = 2
    }

    public enum TriggerType
    {
        Cron = 0
    }

    //服务状态
    public enum ServiceStatus
    {
        [Description("已启动")]
        Started = 1,
        [Description("已停止")]
        Stopped = 2
    }
}
