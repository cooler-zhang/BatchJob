using AutoMapper;
using BatchJob.Dto;
using BatchJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BatchJob
{
    public class ViewModelMapperProfile : Profile
    {
        public ViewModelMapperProfile()
        {
            CreateMap<SchedulerDto, SchedulerViewModel>().ReverseMap();
            CreateMap<JobGroupDto, JobGroupViewModel>().ReverseMap();
            CreateMap<JobDto, JobViewModel>().ReverseMap();
            CreateMap<TriggerDto, TriggerViewModel>().ReverseMap();
            CreateMap<CronTriggerDto, CronTriggerViewModel>().ReverseMap();
            CreateMap<ServiceDto, ServiceViewModel>()
                .ForMember(t => t.Parameters, opt => opt.MapFrom(b => string.Join("|", b.Parameters.Select(s => s.Name + ":" + s.Value).ToArray())))
                .ReverseMap();
        }
    }
}