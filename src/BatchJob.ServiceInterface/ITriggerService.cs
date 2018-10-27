using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.ServiceInterface
{
    public interface ITriggerService
    {
        CronTriggerDto CreateCronTrigger(CronTriggerDto dto);

        CronTriggerDto UpdateCronTrigger(CronTriggerDto dto);

        void Delete(int id);

        CronTriggerDto GetCronGrigger(int id);

        IList<TriggerDto> GetTriggers();

        IList<CronTriggerDetailDto> GetCronTriggerDetails();
    }
}
