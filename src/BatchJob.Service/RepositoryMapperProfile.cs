using AutoMapper;
using BatchJob.Domain;
using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Service
{
    public class RepositoryMapperProfile : Profile
    {
        public RepositoryMapperProfile()
        {
            CreateMap<SchedulerEntity, SchedulerDto>().ReverseMap();
            CreateMap<JobGroupEntity, JobGroupDto>().ReverseMap();
            CreateMap<SchedulerEntity, JobDto>().ReverseMap();
            CreateMap<CronTriggerEntity, CronTriggerDto>().ReverseMap();
            CreateMap<ServiceEntity, ServiceDto>().ReverseMap();
            CreateMap<ServiceParameterEntity, ServiceDto.ServiceParameterDto>().ReverseMap();
        }
    }
}
