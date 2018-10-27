using BatchJob.Domain;
using BatchJob.Repository;
using BatchJob.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace BatchJob.Service
{
    public class UserService : IUserService
    {
        public bool ValidateUser(string username, string password)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var result = UserEntity.ValidateUser(username, password);
                ctx.SaveChanges();
                return result;
            }
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (var ctx = new BatchJobDbContext())
            {
                username = username.Trim();
                var manager = UserEntity.GetManagerByUserName(username);
                if (manager != null)
                {
                    string errorMessage = string.Empty;
                    return manager.ChangePassword(oldPassword, newPassword, out errorMessage);
                }
                return false;
            }
        }

        public MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var user = UserEntity.Create(username, password, email);
                ctx.SaveChanges();
                MembershipUser membershipUser = user.ToMembershipUser();
                status = MembershipCreateStatus.Success;
                return membershipUser;
            }
        }

        public MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var ctx = new BatchJobDbContext())
            {
                var manager = UserEntity.GetManagerByUserName(username);
                if (manager != null)
                {
                    return manager.ToMembershipUser();
                }
                return null;
            }
        }
    }
}
