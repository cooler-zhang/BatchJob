using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class JobGroupEntity : BaseEntity
    {
        [Required(ErrorMessage = "作业组名称不能为空")]
        [MaxLength(100, ErrorMessage = "作业组名称长度不能超过100")]
        public string Name { get; set; }

        public virtual SchedulerEntity Scheduler { get; set; }

        public virtual ICollection<JobEntity> Jobs { get; set; }

        public JobGroupEntity()
        {
        }

        public JobGroupEntity(string name)
        {
            this.Name = name;
        }

        public void RemoveJob(int id)
        {
            var job = Jobs.FirstOrDefault(a => a.Id == id);
            if (job != null)
            {
                Jobs.Remove(job);
            }
        }
    }
}
