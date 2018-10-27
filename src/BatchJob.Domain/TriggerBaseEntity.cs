using BatchJob.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class TriggerBaseEntity : BaseEntity
    {
        [Required(ErrorMessage = "编码必填"), MaxLength(100, ErrorMessage = "长度不能超过100")]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        [MaxLength(200, ErrorMessage = "描述长度不能超过200")]
        public string Description { get; set; }

        public TriggerType Type { get; set; }

        public virtual JobEntity Job { get; set; }

        public TriggerBaseEntity()
        {
            Code = Guid.NewGuid().ToString();
        }

        public void SetJob(int id)
        {
            if (Job == null || Job.Id != id)
            {
                Job = DomainContext.Current.Set<JobEntity>().Find(id);
            }
        }
    }
}
