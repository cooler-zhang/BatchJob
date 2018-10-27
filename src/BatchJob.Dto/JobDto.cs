using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Dto
{
    public class JobDto : BaseDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public DateTime? PreviousExecuteTime { get; set; }

        public DateTime? NextExecuteTime { get; set; }

        public bool IsRunning { get; set; }

        public string GroupName { get; set; }

        public int GroupId { get; set; }
    }
}
