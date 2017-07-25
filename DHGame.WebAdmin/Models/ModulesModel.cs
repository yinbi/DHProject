﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DHGame.WebAdmin.Models
{
    public class ModulesModel
    {
        [Required]
        public int ParentId { get; set; }
        [Required(ErrorMessage = "菜单名称不能为空")]
        [MaxLength(30)]
        public string Title { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "控制器名称不能为空")]
        public string Style { get; set; }
        public string Controller { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "视图不能为空")]
        public string Action { get; set; }
        [Required(ErrorMessage = "排序号不能为空")]
        public int OrderNo { get; set; }
        [Required]
        public bool Nullity { get; set; }
    }
}