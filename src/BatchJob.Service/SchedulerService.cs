using AutoMapper;
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
    public class SchedulerService : ISchedulerService
    {
        public SchedulerRepository _schedulerRepository;

        public SchedulerService()
        {
            _schedulerRepository = new SchedulerRepository();
        }

        public SchedulerDto Create(SchedulerDto dto)
        {
            var entity = Mapper.Map<SchedulerEntity>(dto);
            entity = _schedulerRepository.CreateEntity(entity);
            dto = Mapper.Map<SchedulerDto>(entity);
            return dto;
        }

        public SchedulerDto Update(SchedulerDto dto)
        {
            if (dto.Id.HasValue)
            {
                var entity = _schedulerRepository.Find(dto.Id.Value);
                entity = Mapper.Map(dto, entity);
                entity = _schedulerRepository.ModifiedEntity(entity);
                dto = Mapper.Map<SchedulerDto>(entity);
                return dto;
            }
            return null;
        }

        public void Delete(int id)
        {
            _schedulerRepository.TryDelete(id);
        }

        public SchedulerDto GetScheduler(int id)
        {
            using (var ctx = new BatchJobDbContext())
            {
                return ctx.Schedulers.Where(a => a.Id == id).SelectDto().FirstOrDefault();
            }
        }

        public IList<SchedulerDto> GetSchedulers()
        {
            using (var ctx = new BatchJobDbContext())
            {
                return ctx.Schedulers.SelectDto().ToList();
            }
        }

        public void AddJobGroup(JobGroupDto dto)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var scheduler = ctx.Schedulers.Find(dto.SchedulerId);
                if (scheduler != null)
                {
                    scheduler.AddJobGroup(dto.Name);
                    ctx.SaveChanges();
                }
            }
        }

        public bool TryRemoveJobGroup(int id, out int? schedulerId)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var entity = ctx.JobGroups.Find(id);
                if (entity != null)
                {
                    ctx.JobGroups.Remove(entity);
                    ctx.SaveChanges();
                    schedulerId = entity.Scheduler.Id;
                    return true;
                }
                schedulerId = null;
                return false;
            }
        }

        public IList<JobGroupDto> GetJobGroups()
        {
            using (var ctx = new BatchJobDbContext())
            {
                return ctx.JobGroups.SelectDto().ToList();
            }
        }

        public IList<JobGroupDto> GetJobGroups(int schedulerId)
        {
            using (var ctx = new BatchJobDbContext())
            {
                return ctx.JobGroups.Where(a => a.Scheduler.Id == schedulerId).SelectDto().ToList();
            }
        }
    }
}
