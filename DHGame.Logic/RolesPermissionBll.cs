using DHGame.Entity.DHWebSiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DHGame.Logic
{
    public class RolesPermissionBll : LogicBase
    {
        public List<RolesPermission> GetModuleIdByRoleId(int roleId)
        {
            return MyDb.RolesPermission.AsNoTracking().Where<RolesPermission>(p => p.RoleId == roleId).ToList();
        }
    }
}
