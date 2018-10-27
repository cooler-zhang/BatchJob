using BatchJob.Common;
using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class JobEntity : BaseEntity
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "作业名称长度不能超过100")]
        [Required(ErrorMessage = "作业名称不能为空")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "作业编码长度不能超过100")]
        [Required(ErrorMessage = "作业编码不能为空")]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        /// <summary>
        /// 任务所在的任务组
        /// </summary>
        public virtual JobGroupEntity JobGroup { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200, ErrorMessage = "描述长度不能超过200")]
        public string Description { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// 任务上次执行时间
        /// </summary>
        public DateTime? PreviousExecuteTime { get; set; }

        /// <summary>
        /// 任务下次执行时间
        /// </summary>
        public DateTime? NextExecuteTime { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public bool IsRunning { get; private set; }

        public virtual ICollection<TriggerBaseEntity> Triggers { get; set; }

        public virtual ICollection<ExcuteHistoryEntity> ExcuteHistories { get; set; }

        public virtual ICollection<ServiceEntity> Services { get; set; }

        /// <summary>
        /// 执行
        /// </summary>
        public void Excute(DateTime? nextExecuteTime)
        {
            NextExecuteTime = nextExecuteTime;
            PreviousExecuteTime = DateTime.Now;
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                //按操作创建顺序逐个调用
                foreach (ServiceEntity service in Services.OrderBy(a => a.Id).ToList())
                {
                    service.Call();
                }
                Comment = string.Format("作业执行成功 结束时间:{0}, 耗时：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), watch.Elapsed.ToString());
                //记录执行历史
                if (ExcuteHistories == null)
                {
                    ExcuteHistories = new List<ExcuteHistoryEntity>();
                }
                ExcuteHistories.Add(new ExcuteHistoryEntity()
                {
                    BeginTime = PreviousExecuteTime.Value,
                    EndTime = DateTime.Now,
                    Comment = Comment
                });
            }
            catch (Exception ex)
            {
                Comment = string.Format("作业执行异常 StackTrace:{0} ErrorMessage:{1}", ex.StackTrace, ex.Message);
            }
        }

        public void AddService(ServiceDto dto)
        {
            if (dto == null)
            {
                return;
            }
            if (Services == null)
            {
                Services = new List<ServiceEntity>();
            }
            var service = new ServiceEntity();
            service.ServiceAddress = dto.ServiceAddress;
            service.MethodName = dto.MethodName;
            service.OperationContractName = dto.OperationContractName;
            if (dto.Parameters != null)
            {
                service.ServiceParameters = new List<ServiceParameterEntity>();
                foreach (var parameter in dto.Parameters)
                {
                    service.ServiceParameters.Add(new ServiceParameterEntity()
                    {
                        TypeName = parameter.TypeName,
                        Name = parameter.Name,
                        Value = parameter.Value
                    });
                }
            }
            Services.Add(service);
        }

        public void RunJob()
        {
            IsRunning = true;
        }

        public void StopJob()
        {
            IsRunning = false;
        }
    }
}
