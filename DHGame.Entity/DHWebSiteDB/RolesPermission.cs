using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Entity.DHWebSiteDB
{
    [Table("RolesPermission")]
    public class RolesPermission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }
        public int ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public Modules Modules { get; set; }
        public int ActionValueSum { get; set; }
         [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
    }
}
