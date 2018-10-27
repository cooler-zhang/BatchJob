using AutoMapper;
using AutoMapper.Configuration;
using BatchJob.Common;
using BatchJob.Dto;
using BatchJob.Models;
using BatchJob.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BatchJob
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //注册Log4Net
            log4net.Config.XmlConfigurator.Configure();
            //注册AutoMapper
            RegisterAutoMapper();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Logger.Write(Server.GetLastError());
        }

        private void RegisterAutoMapper()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<BatchJob.ViewModelMapperProfile>();
                cfg.AddProfile<BatchJob.Service.RepositoryMapperProfile>();
            });
        }
    }
}
