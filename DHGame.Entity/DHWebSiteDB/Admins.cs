using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Entity.DHWebSiteDB
{
    /// <summary>
    /// 后台管理员
    /// </summary>
    public class Admins
    {
        [Key]
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string RealName { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string LastLoginIp { get; set; }
        public int ErrNum { get; set; }
        public bool Enable { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
