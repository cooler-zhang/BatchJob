using BatchJob.Domain;
using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace BatchJob.Repository
{
    public static class E2DMapping
    {
        public static MembershipUser ToMembershipUser(this UserEntity entity, MembershipUser membershipUser = null)
        {
            membershipUser = membershipUser ?? new MembershipUser("UserProvider", entity.UserName, null, entity.Email, null, null, true, true, entity.CreatedDate,
                entity.LastLoginDate, entity.UpdatedDate, entity.UpdatedDate, entity.CreatedDate);
            return membershipUser;
        }

        public static IQueryable<SchedulerDto> SelectDto(this IQueryable<SchedulerEntity> expression)
        {
            return expression.Select(a => new SchedulerDto()
            {
                Id = a.Id,
                Name = a.Name,
                IsRunning = a.IsRunning,
                Priority = a.Priority,
                ThreadPoolSize = a.ThreadPoolSize
            });
        }

        public static IQueryable<TriggerDto> SelectDto(this IQueryable<TriggerBaseEntity> expression)
        {
            return expression.Select(a => new TriggerDto()
            {
                Id = a.Id,
                Type = a.Type,
                Description = a.Description,
                JobId = a.Job.Id,
                JobName = a.Job.Name,
                JobCode = a.Job.Code
            });
        }

        public static IQueryable<CronTriggerDto> SelectDto(this IQueryable<CronTriggerEntity> expression)
        {
            return expression.Select(a => new CronTriggerDto()
            {
                Id = a.Id,
                CronExpression = a.CronExpression,
                Description = a.Description,
                JobId = a.Job.Id,
                JobName = a.Job.Name,
                JobCode = a.Job.Code
            });
        }

        public static IQueryable<JobGroupDto> SelectDto(this IQueryable<JobGroupEntity> expression)
        {
            return expression.Select(a => new JobGroupDto()
            {
                Id = a.Id,
                Name = a.Name,
                SchedulerId = a.Scheduler.Id,
                SchedulerName = a.Scheduler.Name
            });
        }

        public static IQueryable<JobDto> SelectDto(this IQueryable<JobEntity> expression)
        {
            return expression.Select(a => new JobDto()
            {
                Id = a.Id,
                Name = a.Name,
                Code = a.Code,
                Description = a.Description,
                Comment = a.Comment,
                PreviousExecuteTime = a.PreviousExecuteTime,
                NextExecuteTime = a.NextExecuteTime,
                IsRunning = a.IsRunning,
                GroupId = a.JobGroup.Id,
                GroupName = a.JobGroup.Name
            });
        }

        public static IQueryable<ServiceDto> SelectDto(this IQueryable<ServiceEntity> expression)
        {
            return expression.Select(a => new ServiceDto()
            {
                Id = a.Id,
                ServiceAddress = a.ServiceAddress,
                MethodName = a.MethodName,
                OperationContractName = a.OperationContractName,
                JobId = a.Job.Id,
                JobName = a.Job.Name,
                Parameters = a.ServiceParameters.Select(b => new ServiceDto.ServiceParameterDto()
                {
                    Id = b.Id,
                    TypeName = b.TypeName,
                    Name = b.Name,
                    Value = b.Value
                }).ToList()
            });
        }
    }
}
