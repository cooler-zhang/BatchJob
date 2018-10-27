using BatchJob.Common;
using BatchJob.Repository;
using BatchJob.Service;
using BatchJob.ServiceInterface;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Quartz
{
    public class BatchJobRunTime
    {
        private ITriggerService _triggerRepository;
        public ITriggerService TriggerRepository
        {
            get
            {
                if (_triggerRepository == null)
                {
                    _triggerRepository = new TriggerService();
                }
                return _triggerRepository;
            }
        }

        public void ServiceRuntime()
        {
            var triggerDetails = TriggerRepository.GetCronTriggerDetails();
            var triggerDetailsSchedulerGroup = triggerDetails.GroupBy(a => new { a.SchedulerId, a.SchedulerName, a.SchedulerPriority, a.SchedulerThreadPoolSize }).ToDictionary(a => a.Key, a => a);
            //分组并创建调度器
            foreach (var schedulerGroupItem in triggerDetailsSchedulerGroup)
            {
                var scheduler = QuartzHelper.GetScheduler(schedulerGroupItem.Key.SchedulerName, schedulerGroupItem.Key.SchedulerThreadPoolSize, (int)schedulerGroupItem.Key.SchedulerPriority);
                foreach (var item in schedulerGroupItem.Value)
                {
                    //如果已经存在作业，需要先删除
                    var jobKey = new JobKey(item.JobCode, item.JobGroupName);
                    if (scheduler.CheckExists(jobKey))
                    {
                        scheduler.DeleteJob(jobKey);
                    }
                    var job = JobBuilder.Create<ExcuteJob>()
                               .WithIdentity(jobKey)
                               .Build();
                    //如果已经存在触发器，需要先删除
                    var triggerKey = new TriggerKey(item.TriggerCode);
                    if (scheduler.CheckExists(triggerKey))
                    {
                        scheduler.UnscheduleJob(triggerKey);
                    }
                    var trigger = TriggerBuilder.Create()
                                   .WithIdentity(triggerKey)
                                   .WithCronSchedule(item.CronExpression)
                                   .StartNow()
                                   .Build();
                    //调度作业
                    scheduler.ScheduleJob(job, trigger);
                }
            }
        }
    }
}
