using AutoMapper;
using BatchJob.Common;
using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatchJob.Models
{
    public class TriggerViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "触发器描述"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "触发器类型")]
        public TriggerType Type { get; set; }

        public int JobId { get; set; }

        [Display(Name = "作业")]
        public string JobName { get; set; }

        public static TriggerViewModel Create(TriggerDto dto)
        {
            return Mapper.Map<TriggerViewModel>(dto);
        }

        public TriggerDto ToDto()
        {
            return Mapper.Map<TriggerDto>(this);
        }
    }

    public class CronTriggerViewModel: TriggerViewModel
    {
        [Display(Name = "Cron表达式"), StringLength(50, ErrorMessage = "表达式长度不能超过50")]
        public string CronExpression { get; set; }

        [Display(Name = "作业")]
        public IList<SelectListItem> Jobs { get; set; }

        public static CronTriggerViewModel Create(CronTriggerDto dto)
        {
            return Mapper.Map<CronTriggerViewModel>(dto);
        }

        public new CronTriggerDto ToDto()
        {
            return Mapper.Map<CronTriggerDto>(this);
        }
    }
}