using AutoMapper;
using BatchJob.Domain;
using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Repository
{
    public class SchedulerRepository : Repository<SchedulerEntity>
    {
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
    }
}
