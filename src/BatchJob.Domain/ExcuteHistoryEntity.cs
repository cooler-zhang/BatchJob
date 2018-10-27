using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class ExcuteHistoryEntity : BaseEntity
    {
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 任务执行期间发生的描述信息
        /// </summary>
        public string Comment { get; set; }
    }
}
