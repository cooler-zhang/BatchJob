using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using BatchJob.Common;

namespace BatchJob.WindowsService
{
    public partial class FormServiceManager : Form
    {
        private string serviceName = ConfigurationManager.AppSettings["ServiceName"];

        private ServiceStatus currentServiceStatus;
        public ServiceStatus CurrentServiceStatus
        {
            get { return currentServiceStatus; }
            set
            {
                currentServiceStatus = value;
                lblServiceStatus.Text = EnumHelper.GetEnumDescription(currentServiceStatus);
                pbxServiceStatus.Visible = true;
                if (currentServiceStatus == ServiceStatus.Started)
                {
                    pbxServiceStatus.Image = Resources.Stop;
                }
                else if (currentServiceStatus == ServiceStatus.Stopped)
                {
                    pbxServiceStatus.Image = Resources.Start;
                }
            }
        }

        public FormServiceManager()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (ServiceHelper.ServiceInstalled(serviceName))
            {
                btnInstallService.Enabled = false;
                btnUnInstallService.Enabled = true;
            }
            else
            {
                btnInstallService.Enabled = true;
                btnUnInstallService.Enabled = false;
            }
            RefreshStatus();
        }

        private void btnInstallService_Click(object sender, EventArgs e)
        {
            string serviceApplication = Application.ExecutablePath + " -s";
            try
            {
                bool result = ServiceHelper.InstallService(serviceApplication, serviceName, serviceName);
                if (result)
                {
                    lblServiceName.Text = serviceName;
                    pbxServiceStatus.Visible = true;
                    CurrentServiceStatus = ServiceStatus.Stopped;
                    btnInstallService.Enabled = false;
                    btnUnInstallService.Enabled = true;
                    RefreshStatus();
                }
                else
                {
                    MessageBox.Show("服务安装失败.");
                    pbxServiceStatus.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnUnInstallService_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = ServiceHelper.UnInstallService(serviceName);
                if (result)
                {
                    lblServiceName.Text = "暂无";
                    currentServiceStatus = ServiceStatus.Stopped;
                    btnInstallService.Enabled = true;
                    btnUnInstallService.Enabled = false;
                    pbxServiceStatus.Visible = false;
                }
                else
                {
                    MessageBox.Show("服务卸载失败.");
                    pbxServiceStatus.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void pbxServiceStatus_Click(object sender, EventArgs e)
        {
            if (CurrentServiceStatus == ServiceStatus.Started)
            {
                try
                {
                    bool result = ServiceHelper.StopService(serviceName);
                    if (result)
                    {
                        CurrentServiceStatus = ServiceStatus.Stopped;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (CurrentServiceStatus == ServiceStatus.Stopped)
            {
                try
                {
                    bool result = ServiceHelper.StartService(serviceName);
                    if (result)
                    {
                        CurrentServiceStatus = ServiceStatus.Started;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex.StackTrace + ex.Message);
                    throw ex;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            CurrentServiceStatus = ServiceHelper.ServiceIsRunning(serviceName) ? ServiceStatus.Started : ServiceStatus.Stopped;
        }
    }
}
