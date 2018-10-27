using AutoMapper;
using BatchJob.Common;
using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BatchJob.Models
{
    public class SchedulerViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "调度器名称"), Required(ErrorMessage = "调度器名称必填"), StringLength(100, ErrorMessage = "调度器名称长度超过100")]
        public string Name { get; set; }

        [Display(Name = "运行状态")]
        public bool IsRunning { get; set; }

        [Display(Name = "优先级")]
        public SchedulerPriority Priority { get; set; }

        [Display(Name = "线程池大小")]
        public int ThreadPoolSize { get; set; }

        public static SchedulerViewModel Create(SchedulerDto dto)
        {
            return Mapper.Map<SchedulerViewModel>(dto);
        }

        public SchedulerDto ToDto()
        {
            return Mapper.Map<SchedulerDto>(this);
        }
    }
}