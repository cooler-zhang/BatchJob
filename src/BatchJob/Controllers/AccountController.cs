using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BatchJob.Common;
using log4net;
using System.Web.Security;
using System.Web.Script.Serialization;
using BatchJob.Models;

namespace BatchJob.Controllers
{
    public class AccountController : ControllerBase
    {
        [ActionName("login"), AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [ActionName("login"), HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid && Membership.ValidateUser(model.Email, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码错误.");
                return View(model);
            }
        }

        [ActionName("register"), AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [ActionName("register"), HttpPost, AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Membership.CreateUser(model.Email, model.Password, model.Email);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ModelState.AddModelError("", "用户注册失败");
                }
            }
            return View(model);
        }

        [ActionName("log-off"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "home");
        }
    }
}