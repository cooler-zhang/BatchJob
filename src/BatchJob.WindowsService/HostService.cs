using BatchJob.Common;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BatchJob.Quartz;

namespace BatchJob.WindowsService
{
    public partial class HostService : ServiceBase
    {
        public HostService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Write("-------HostService---------Start", LoggerLevel.Info);
            //运行批处理
            var run = new BatchJobRunTime();
            run.ServiceRuntime();
        }

        protected override void OnStop()
        {
        }
    }
}
