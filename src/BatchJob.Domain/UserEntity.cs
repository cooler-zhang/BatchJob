using BatchJob.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class UserEntity : BaseEntity
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [MaxLength(100, ErrorMessage = "用户名长度不能超过100")]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "用户邮箱不能为空")]
        [MaxLength(100, ErrorMessage = "用户邮箱长度不能超过100")]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [MaxLength(100, ErrorMessage = "密码长度不能超过100")]
        public string Password { get; set; }

        public DateTime LastLoginDate { get; set; }

        public static UserEntity Create(string userName, string password, string email)
        {
            var manager = new UserEntity();
            manager.UserName = userName.Trim();
            manager.Email = email;
            manager.LastLoginDate = DateTime.Now;
            manager.Password = Encryption.UserPasswordEncryption(password.Trim());
            return DomainContext.Current.Set<UserEntity>().Add(manager);
        }

        public static bool ValidateUser(string userName, string password)
        {
            var manager = GetManagerByUserName(userName);
            if (manager == null || manager.Password != Encryption.UserPasswordEncryption(password))
            {
                return false;
            }
            manager.LastLoginDate = DateTime.Now;
            return true;
        }

        public bool ChangePassword(string originalPassword, string newPassword, out string errorMessage)
        {
            if (Encryption.UserPasswordEncryption(originalPassword) != Password)
            {
                errorMessage = "原始密码验证错误.";
                return false;
            }
            errorMessage = string.Empty;
            Password = Encryption.UserPasswordEncryption(newPassword);
            return true;
        }

        public static UserEntity GetManagerByUserName(string userName)
        {
            userName = userName.Trim();
            return DomainContext.Current.Set<UserEntity>().Where(a => a.UserName == userName || a.Email == userName).FirstOrDefault();
        }
    }
}
