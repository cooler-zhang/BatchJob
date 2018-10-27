using BatchJob.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class SchedulerEntity : BaseEntity
    {
        /// <summary>
        /// 调度器名称
        /// </summary>
        [Required(ErrorMessage = "调度器名称不能为空")]
        [MaxLength(100, ErrorMessage = "调度器名称长度不能超过100")]
        public string Name { get; set; }

        /// <summary>
        /// 调度器当前状态
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// 调度器优先级
        /// </summary>
        public SchedulerPriority Priority { get; set; }

        /// <summary>
        /// 调度器线程池大小
        /// </summary>
        public int ThreadPoolSize { get; set; }

        public virtual ICollection<JobGroupEntity> JobGroups { get; set; }

        public JobGroupEntity AddJobGroup(string name)
        {
            if (JobGroups == null)
            {
                JobGroups = new List<JobGroupEntity>();
            }
            var jobGroup = new JobGroupEntity(name);
            JobGroups.Add(jobGroup);
            return jobGroup;
        }
    }
}
