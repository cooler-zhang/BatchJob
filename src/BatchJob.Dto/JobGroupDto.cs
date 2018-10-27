using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Dto
{
    public class JobGroupDto : BaseDto
    {
        public int SchedulerId { get; set; }

        public string SchedulerName { get; set; }

        public string Name { get; set; }
    }
}
