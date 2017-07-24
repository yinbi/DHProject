using DHGame.Entity.DHWebSiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DHGame.WebAdmin.Models
{
    public class RolesModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "角色名称不能为空.")]
        [Display(Name = "角色名称")]
        [MaxLength(30)]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "角色描述信息不能为空.")]
        [Display(Name = "描述")]
        [MaxLength(100)]
        public string RoleDesc { get; set; }

        public bool Nullity { get; set; }
        public List<RolesPermission> RolePermissions { get; set; }
    }
}