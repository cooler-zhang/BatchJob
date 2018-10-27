using AutoMapper;
using BatchJob.Common;
using BatchJob.Domain;
using BatchJob.Dto;
using BatchJob.Repository;
using BatchJob.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Service
{
    public class JobService : IJobService
    {
        public JobDto Create(JobDto dto)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var jobGroup = ctx.JobGroups.Find(dto.GroupId);
                var isExists = jobGroup.Jobs.Any(a => a.Name == dto.Name);
                if (isExists)
                {
                    throw new BusinessException("当前工作组下存在重名作业.");
                }
                var entity = Mapper.Map<JobEntity>(dto);
                entity.JobGroup = jobGroup;
                entity = ctx.Jobs.Add(entity);
                ctx.SaveChanges();
                dto = Mapper.Map<JobDto>(entity);
                return dto;
            }
        }

        public JobDto Update(JobDto dto)
        {
            using (var ctx = new BatchJobDbContext())
            {
                if (dto.Id.HasValue)
                {
                    var entity = ctx.Jobs.Find(dto.Id.Value);
                    entity = Mapper.Map(dto, entity);
                    ctx.SaveChanges();
                    dto = Mapper.Map<JobDto>(entity);
                    return dto;
                }
                return null;
            }
        }

        public bool TryDelete(int jobId, out int? jobGroupId)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var entity = ctx.Jobs.Where(a => a.Id == jobId).FirstOrDefault();
                if (entity != null)
                {
                    ctx.Jobs.Remove(entity);
                    ctx.SaveChanges();
                    jobGroupId = entity.JobGroup.Id;
                    return true;
                }
                jobGroupId = null;
                return false;
            }
        }

        public JobDto GetJob(int id)
        {
            using (var ctx = new BatchJobDbContext())
            {
                return ctx.Jobs.Where(a => a.Id == id).SelectDto().FirstOrDefault();
            }
        }

        public IList<JobDto> GetJobs(int? jobGroupId)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var expression = ctx.Jobs.AsQueryable();
                if (jobGroupId.HasValue)
                {
                    expression = expression.Where(a => a.JobGroup.Id == jobGroupId.Value);
                }
                return expression.SelectDto().ToList();
            }
        }

        public IList<ServiceDto> GetServices(int jobId)
        {
            using (var ctx = new BatchJobDbContext())
            {
                return ctx.Services.Where(a => a.Job.Id == jobId).SelectDto().ToList();
            }
        }

        public void AddService(ServiceDto service)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var job = ctx.Jobs.Find(service.JobId);
                if (job != null)
                {
                    job.AddService(service);
                }
                ctx.SaveChanges();
            }
        }

        public bool TryRemoveService(int serviceId, out int? jobId)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var entity = ctx.Services.Find(serviceId);
                if (entity != null)
                {
                    jobId = entity.Job.Id;
                    if (entity.ServiceParameters != null)
                    {
                        entity.ServiceParameters.Clear();
                    }
                    ctx.Services.Remove(entity);
                    ctx.SaveChanges();
                    return true;
                }
                jobId = null;
                return false;
            }
        }

        public void ExcuteJob(string jobGroup, string jobCode, DateTime? nextExecuteTime)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var job = ctx.Jobs.Where(a => a.JobGroup.Name.Equals(jobGroup) && a.Code == jobCode).FirstOrDefault();
                if (job != null)
                {
                    job.Excute(nextExecuteTime);
                    ctx.SaveChanges();
                }
            }
        }

        public void RunJob(int jobId)
        {
            using (var ctx = new BatchJobDbContext())
            {
                try
                {
                    var job = ctx.Jobs.Find(jobId);
                    if (job != null)
                    {
                        if (job.Triggers == null || job.Triggers.Count <= 0)
                        {
                            throw new BusinessException("当前作业未配置触发器.");
                        }
                        var scheduler = QuartzHelper.GetScheduler(job.JobGroup.Scheduler.Name, job.JobGroup.Scheduler.ThreadPoolSize, (int)job.JobGroup.Scheduler.Priority);
                        foreach (var trigger in job.Triggers)
                        {
                            if (trigger.Type == TriggerType.Cron)
                            {
                                var cronTrigger = DomainContext.Current.Set<CronTriggerEntity>().Find(trigger.Id);
                                if (cronTrigger != null)
                                {
                                    scheduler.ScheduleJobIfExists(job.Code, job.JobGroup.Name, cronTrigger.Code, cronTrigger.CronExpression, typeof(ExcuteJob));
                                }
                            }
                        }
                        job.RunJob();
                        ctx.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void StopJob(int jobId)
        {
            using (var ctx = new BatchJobDbContext())
            {
                try
                {
                    var job = ctx.Jobs.Find(jobId);
                    if (job != null)
                    {
                        var scheduler = QuartzHelper.GetScheduler(job.JobGroup.Scheduler.Name, job.JobGroup.Scheduler.ThreadPoolSize, (int)job.JobGroup.Scheduler.Priority);
                        scheduler.PauseJob(job.Code, job.JobGroup.Name);
                        job.StopJob();
                        ctx.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
