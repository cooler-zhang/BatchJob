using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatchJob.WindowsService
{
    static class Program
    {
        static void Main(string[] args)
        {
            //注册Log4Net
            log4net.Config.XmlConfigurator.Configure();
            if (args.Length > 0 && args[0] == "-s")
            {
                var ServicesToRun = new ServiceBase[] { new HostService() };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Application.Run(new FormServiceManager());
            }
        }
    }
}
