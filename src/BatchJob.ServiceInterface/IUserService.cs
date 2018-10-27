using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace BatchJob.ServiceInterface
{
    public interface IUserService
    {
        bool ValidateUser(string username, string password);

        bool ChangePassword(string username, string oldPassword, string newPassword);

        MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);

        MembershipUser GetUser(string username, bool userIsOnline);
    }
}
