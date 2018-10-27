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
    public class TriggerService : ITriggerService
    {
        public CronTriggerDto CreateCronTrigger(CronTriggerDto dto)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var entity = Mapper.Map<CronTriggerEntity>(dto);
                entity.Type = TriggerType.Cron;
                entity.SetJob(dto.JobId);
                entity = ctx.CronTriggers.Add(entity);
                ctx.SaveChanges();
                dto = Mapper.Map<CronTriggerDto>(entity);
                return dto;
            }
        }

        public CronTriggerDto UpdateCronTrigger(CronTriggerDto dto)
        {
            using (var ctx = new BatchJobDbContext())
            {
                if (dto.Id.HasValue)
                {
                    var entity = ctx.CronTriggers.Find(dto.Id.Value);
                    entity = Mapper.Map(dto, entity);
                    ctx.SaveChanges();
                    dto = Mapper.Map<CronTriggerDto>(entity);
                    return dto;
                }
                return null;
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new BatchJobDbContext(true))
            {
                var entity = ctx.CronTriggers.Find(id);
                if (entity != null)
                {
                    ctx.CronTriggers.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }

        public CronTriggerDto GetCronGrigger(int id)
        {
            using (var ctx = new BatchJobDbContext())
            {
                return ctx.CronTriggers.Where(a => a.Id == id).SelectDto().FirstOrDefault();
            }
        }

        public IList<TriggerDto> GetTriggers()
        {
            using (var ctx = new BatchJobDbContext())
            {
                return ctx.TriggerBases.SelectDto().ToList();
            }
        }

        public IList<CronTriggerDetailDto> GetCronTriggerDetails()
        {
            using (var ctx = new BatchJobDbContext())
            {
                var expression = from a in ctx.CronTriggers
                                 join b in ctx.Jobs on a.Job.Id equals b.Id
                                 join c in ctx.JobGroups on b.JobGroup.Id equals c.Id
                                 join d in ctx.Schedulers on c.Scheduler.Id equals d.Id
                                 where d.IsRunning && b.IsRunning
                                 select new CronTriggerDetailDto()
                                 {
                                     SchedulerId = d.Id,
                                     SchedulerName = d.Name,
                                     SchedulerPriority = d.Priority,
                                     SchedulerThreadPoolSize = d.ThreadPoolSize,
                                     JobGroupName = c.Name,
                                     JobId = b.Id,
                                     JobName = b.Name,
                                     JobCode = b.Code,
                                     CronExpression = a.CronExpression,
                                     Description = a.Description,
                                     Type = a.Type,
                                     TriggerCode = a.Code
                                 };
                return expression.ToList();
            }
        }
    }
}
