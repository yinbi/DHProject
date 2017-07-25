using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DHGame.WebAdmin.Models
{
    public class AdminUserModel
    {
        [Required(ErrorMessage = "登录名不能为空.")]
        [Display(Name = "登录名")]
        [MaxLength(20)]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "登录密码不能为空.")]
        [Display(Name = "登录密码")]
        [MaxLength(34)]
        public string LoginPwd { get; set; }
        [Required(ErrorMessage = "真实姓名不能为空.")]
        [Display(Name = "真实姓名")]
        [MaxLength(20)]
        public string RealName { get; set; }
        public int RoleId { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string LastLoginIp { get; set; }
        public int ErrNum { get; set; }
        public bool Enable { get; set; }
    }
}