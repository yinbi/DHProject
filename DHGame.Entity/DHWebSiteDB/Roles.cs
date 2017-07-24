using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Entity.DHWebSiteDB
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string RoleName { get; set; }
        [MaxLength(100)]
        public string RoleDesc { get; set; }
        public bool Nullity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
//        public List<RolesPermission> RolePermissions { get; set; }
    }
}
