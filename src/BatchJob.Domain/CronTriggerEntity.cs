using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class CronTriggerEntity : TriggerBaseEntity
    {
        [Required(ErrorMessage = "表达式必填"), MaxLength(50, ErrorMessage = "表达式长度不能超过50")]
        public string CronExpression { get; set; }
    }
}
