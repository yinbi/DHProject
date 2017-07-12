﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DHGame.WebAdmin.Models
{
    public class AccountModels
    {
    }
    public class LogOnModel
    {
        [Required(ErrorMessage = "用户名不能为空.")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空.")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "验证码不能为空.")]
        public string Yzm { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}