using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.ServiceInterface
{
    public interface ISchedulerService
    {
        SchedulerDto Create(SchedulerDto dto);

        SchedulerDto Update(SchedulerDto dto);

        void Delete(int id);

        SchedulerDto GetScheduler(int id);

        IList<SchedulerDto> GetSchedulers();

        IList<JobGroupDto> GetJobGroups();

        IList<JobGroupDto> GetJobGroups(int schedulerId);

        void AddJobGroup(JobGroupDto dto);

        bool TryRemoveJobGroup(int id, out int? schedulerId);
    }
}
