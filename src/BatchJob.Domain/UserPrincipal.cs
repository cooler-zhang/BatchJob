using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BatchJob.Domain
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }

        public static IPrincipal Current
        {
            get
            {
                return HttpContext.Current != null ? HttpContext.Current.User : null;
            }
        }

        public static UserEntity CurrentUser
        {
            get
            {
                if (Current != null)
                {
                    string userName = Current.Identity.Name;
                    if (!string.IsNullOrWhiteSpace(userName))
                    {
                        return DomainContext.Current.Set<UserEntity>().Where(a => a.UserName == userName).FirstOrDefault();
                    }
                }
                return null;
            }
        }

        public static int CurrentUserId
        {
            get
            {
                return CurrentUser != null ? CurrentUser.Id : 0;
            }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
