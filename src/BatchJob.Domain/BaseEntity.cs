using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public virtual UserEntity CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual UserEntity UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
