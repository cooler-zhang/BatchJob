using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Common
{
    public static class QuartzHelper
    {
        /// <summary>
        /// 调度器字典, 存放当前已被实例化的调度器实例 
        /// 当选取调度器时, 先在字典中查找是否已经存在, 若存在直接获取, 否则, 新建调度器并存入字典中
        /// </summary>
        private static Dictionary<string, IScheduler> _schedulerList = new Dictionary<string, IScheduler>();

        /// <summary>
        /// 由调度器名称获取相应的调度器实例,若字典中存在,则直接返回,否则通过相应参数重新创建调度器,加入字典中并返回实例
        /// </summary>
        /// <param name="schedulerName">调度器名称</param>
        /// <returns>调度器实例</returns>
        public static IScheduler GetScheduler(string schedulerName, int schedulerThreadPoolSize, int schedulerPriority)
        {
            if (!_schedulerList.ContainsKey(schedulerName))
            {
                NameValueCollection schedulerProperties = InitialSchedulerProperties(schedulerName, schedulerThreadPoolSize, schedulerPriority);
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerProperties);
                _schedulerList[schedulerName] = schedulerFactory.GetScheduler();
            }
            IScheduler scheduler = _schedulerList[schedulerName];

            //add trigger listener
            if (scheduler.ListenerManager.GetTriggerListener(scheduler.SchedulerName) == null)
            {
                var listener = new LoggingHistoryTriggerListener(scheduler.SchedulerName);
                scheduler.ListenerManager.AddTriggerListener(listener);
            }

            //add job listener
            if (scheduler.ListenerManager.GetJobListener(scheduler.SchedulerName) == null)
            {
                var listener = new LoggingHistoryJobListener(scheduler.SchedulerName);
                scheduler.ListenerManager.AddJobListener(listener);
            }
            if (!scheduler.IsStarted && !scheduler.IsShutdown)
                scheduler.Start();

            return scheduler;
        }


        public static void ScheduleJobIfExists(this IScheduler scheduler, string jobName, string groupName, string triggerCode, string cronExpression,Type type)
        {
            //如果已经存在作业，需要先删除
            var jobKey = new JobKey(jobName, groupName);
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }
            var job = JobBuilder.Create(type)
                       .WithIdentity(jobKey)
                       .Build();
            //如果已经存在触发器，需要先删除
            var triggerKey = new TriggerKey(triggerCode);
            if (scheduler.CheckExists(triggerKey))
            {
                scheduler.UnscheduleJob(triggerKey);
            }
            var trigger = TriggerBuilder.Create()
                           .WithIdentity(triggerKey)
                           .WithCronSchedule(cronExpression)
                           .StartNow()
                           .Build();
            //调度作业
            scheduler.ScheduleJob(job, trigger);
        }

        public static void PauseJob(this IScheduler scheduler, string jobName, string groupName)
        {
            var jobKey = new JobKey(jobName, groupName);
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.PauseJob(jobKey);
            }
        }

        /// <summary>
        /// 初始化调度器参数集合
        /// </summary>
        /// <param name="scheduler">调度器实体对象</param>
        /// <returns>调度器参数集合</returns>
        private static NameValueCollection InitialSchedulerProperties(string schedulerName, int schedulerThreadPoolSize, int schedulerPriority)
        {
            var properties = new NameValueCollection();
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = schedulerThreadPoolSize.ToString();
            properties["quartz.threadPool.threadPriority"] = schedulerPriority.ToString();
            properties["quartz.jobStore.misfireThreshold"] = "10000";
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";//Quartz.Impl.AdoJobStore.JobStoreCMT Quartz
            properties["quartz.jobStore.useProperties"] = "true";
            properties["quartz.jobStore.dataSource"] = "default";
            properties["quartz.jobStore.clustered"] = "true";
            properties["quartz.jobStore.lockHandler.type"] = "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz";
            properties["quartz.dataSource.default.provider"] = "SqlServer-20";
            properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            properties["quartz.scheduler.instanceName"] = schedulerName;
            properties["quartz.scheduler.instanceId"] = "instance_one";
            properties["quartz.dataSource.default.connectionString"] = ConfigurationManager.ConnectionStrings["QuartzDBContext"].ConnectionString;
            return properties;
        }
    }
}
