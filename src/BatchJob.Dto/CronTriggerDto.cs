using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Dto
{
    public class CronTriggerDto : TriggerDto
    {
        public string CronExpression { get; set; }
    }
}
