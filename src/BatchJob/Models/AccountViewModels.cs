using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BatchJob.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住密码?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "邮箱")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}长度不能低于{2}位.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "确认密码与密码不同.")]
        public string ConfirmPassword { get; set; }
    }
}
