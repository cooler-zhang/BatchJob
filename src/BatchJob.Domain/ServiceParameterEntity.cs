using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class ServiceParameterEntity : BaseEntity
    {
        [Required(ErrorMessage = "方法参数类型不能为空")]
        [MaxLength(500, ErrorMessage = "方法参数类型长度不能超过500")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "方法参数名称不能为空")]
        [MaxLength(200, ErrorMessage = "方法参数名称长度不能超过200")]
        public string Name { get; set; }

        [Required(ErrorMessage = "方法参数值不能为空")]
        [MaxLength(500, ErrorMessage = "方法参数值长度不能超过500")]
        public string Value { get; set; }
    }
}
