using BatchJob.Common;
using BatchJob.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatchJob.Controllers
{
    [AuthorizeEx]
    public abstract class ControllerBase : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null)
            {
                Logger.Write(filterContext.Exception);
                if (filterContext.Exception is BusinessException)
                {
                    Logger.Write(filterContext.Exception.Message, LoggerLevel.Warn);
                    filterContext.ExceptionHandled = true;
                    ModelState.AddModelError("", filterContext.Exception.Message);
                    if (HttpContext.Items.Contains("IsChildForm") && HttpContext.Items["IsChildForm"] != null
                        && Convert.ToBoolean(HttpContext.Items["IsChildForm"]))
                    {
                        ViewBag.ErrorMessage = filterContext.Exception.Message;
                        filterContext.Result = View("Error");
                    }
                    else
                    {
                        filterContext.Result = View();
                    }
                }
                else
                {
                    base.OnException(filterContext);
                }
            }
            else
            {
                base.OnException(filterContext);
            }
        }

        protected override HttpNotFoundResult HttpNotFound(string statusDescription)
        {
            Logger.Write(statusDescription);
            return base.HttpNotFound(statusDescription);
        }

        protected new ViewResult View()
        {
            var actionName = ProcessActionName();
            return base.View(actionName);
        }

        protected new ViewResult View(string viewName)
        {
            return base.View(viewName);
        }

        protected new ViewResult View(object model)
        {
            var actionName = ProcessActionName();
            return base.View(actionName, model);
        }

        protected new ViewResult View(string viewName, object model)
        {
            return base.View(viewName, model);
        }

        private string ProcessActionName()
        {
            var actionName = RouteData.Values["action"].ToString();
            var actionNameItems = actionName.Split('-');
            string newActionName = string.Empty;
            foreach (var actionNameItem in actionNameItems)
            {
                newActionName += actionNameItem.Substring(0, 1).ToUpper() + actionNameItem.Substring(1);
            }
            return newActionName;
        }
    }
}