using BatchJob.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Dto
{
    public class TriggerDetailDto: TriggerDto
    {
        public int SchedulerId { get; set; }

        public string SchedulerName { get; set; }

        public SchedulerPriority SchedulerPriority { get; set; }

        public int SchedulerThreadPoolSize { get; set; }

        public string JobGroupName { get; set; }

        public string TriggerCode { get; set; }
    }

    public class CronTriggerDetailDto : TriggerDetailDto
    {
        public string CronExpression { get; set; }
    }
}
