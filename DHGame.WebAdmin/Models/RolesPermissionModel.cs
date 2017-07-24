using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGame.WebAdmin.Models
{
    public class RolesPermissionModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public int ActionValueSum { get; set; }
    }
}