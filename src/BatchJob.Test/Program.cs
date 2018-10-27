using BatchJob.Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //注册Log4Net
            log4net.Config.XmlConfigurator.Configure();
            //运行批处理
            var run = new BatchJobRunTime();
            run.ServiceRuntime();
        }
    }
}
