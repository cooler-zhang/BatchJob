using AutoMapper;
using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatchJob.Models
{
    public class JobGroupViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "调度器Id")]
        public int SchedulerId { get; set; }

        [Display(Name = "调度器名称")]
        public string SchedulerName { get; set; }

        [Display(Name = "作业组名称"), Required(ErrorMessage = "作业组名称必填"), StringLength(100, ErrorMessage = "作业组名称长度超过100")]
        public string Name { get; set; }

        public static JobGroupViewModel Create(JobGroupDto dto)
        {
            return Mapper.Map<JobGroupViewModel>(dto);
        }

        public JobGroupDto ToDto()
        {
            return Mapper.Map<JobGroupDto>(this);
        }
    }

    public class JobViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "作业名称"), Required(ErrorMessage = "作业名称必填"), StringLength(100, ErrorMessage = "作业名称长度超过100")]
        public string Name { get; set; }

        [Display(Name = "作业编码"), Required(ErrorMessage = "作业编码必填"), StringLength(100, ErrorMessage = "作业编码长度超过100")]
        public string Code { get; set; }

        [Display(Name = "作业描述"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "作业执行备注"), DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Display(Name = "上次执行时间")]
        public DateTime? PreviousExecuteTime { get; set; }

        [Display(Name = "下次执行时间")]
        public DateTime? NextExecuteTime { get; set; }

        [Display(Name = "是否运行")]
        public bool IsRunning { get; set; }

        [Display(Name = "作业组")]
        public string GroupName { get; set; }

        public IList<SelectListItem> JobGroups { get; set; }

        public int GroupId { get; set; }

        public static JobViewModel Create(JobDto dto)
        {
            return Mapper.Map<JobViewModel>(dto);
        }

        public JobDto ToDto()
        {
            return Mapper.Map<JobDto>(this);
        }
    }
}