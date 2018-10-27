using BatchJob.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Dto
{
    public class TriggerDto : BaseDto
    {
        public string Description { get; set; }

        public TriggerType Type { get; set; }

        public int JobId { get; set; }

        public string JobName { get; set; }

        public string JobCode { get; set; }
    }
}
