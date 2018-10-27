using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.ServiceInterface
{
    public interface IJobService
    {
        JobDto Create(JobDto dto);

        JobDto Update(JobDto dto);

        bool TryDelete(int jobId, out int? jobGroupId);

        JobDto GetJob(int id);

        IList<JobDto> GetJobs(int? jobGroupId);

        IList<ServiceDto> GetServices(int jobId);

        void AddService(ServiceDto service);

        bool TryRemoveService(int serviceId, out int? jobId);

        void ExcuteJob(string jobGroup,string jobCode, DateTime? nextExecuteTime);

        void RunJob(int jobId);

        void StopJob(int jobId);
    }
}
