using BatchJob.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Dto
{
    public class SchedulerDto : BaseDto
    {
        public string Name { get; set; }

        public bool IsRunning { get; set; }

        public SchedulerPriority Priority { get; set; }

        public int ThreadPoolSize { get; set; }
    }
}
