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
        public void UpdateRolesModules(List<RolesPermission> oldRolePer, List<RolesPermission> newRolePer)
        {
            foreach (var item in oldRolePer)
            {
                RolesPermission rp = newRolePer.Where(p => p.RoleId == item.RoleId && p.ModuleId == item.ModuleId).FirstOrDefault();
                if (rp == null)
                {
                    Delete(item.Id);
                }
                else
                {
                    if(rp.ActionValueSum!=item.ActionValueSum)
                    {
                        EditByRoleIdAndModulesId(rp);
                    }
                }
            }
            foreach (var item in newRolePer)
            {
                RolesPermission rp = oldRolePer.Where(p => p.RoleId == item.RoleId && p.ModuleId == item.ModuleId).FirstOrDefault();
                if (rp == null)
                {
                    Create(item);
                }
            }
        }
        public List<RolesPermission> GetCurRolePer()
        {
            return MyDb.RolesPermission.Include("Roles").Include("Modules").AsNoTracking().Where<RolesPermission>(p => p.RoleId == RoleId).ToList();
        }
        public int Delete(int id)
        {
            var role = MyDb.RolesPermission.FirstOrDefault(p => p.Id == id);
            if (role != null)
            {
                MyDb.RolesPermission.Remove(role);
                return MyDb.SaveChanges();
            }
            return 0;
        }
        public int EditByRoleIdAndModulesId(RolesPermission rolePer)
        {
            RolesPermission oldrolePer = MyDb.RolesPermission.AsNoTracking().FirstOrDefault(p => p.RoleId == rolePer.RoleId && p.ModuleId == rolePer.ModuleId);
            oldrolePer.ActionValueSum = rolePer.ActionValueSum;
            MyDb.Set<RolesPermission>().Attach(oldrolePer);
            MyDb.Entry<RolesPermission>(oldrolePer).State = EntityState.Modified;
            return MyDb.SaveChanges();
        }
        public int EditById(RolesPermission rolePer)
        {
            RolesPermission oldrolePer = MyDb.RolesPermission.AsNoTracking().FirstOrDefault(p => p.Id == rolePer.Id);
            oldrolePer.ActionValueSum = rolePer.ActionValueSum;
            MyDb.Set<RolesPermission>().Attach(oldrolePer);
            MyDb.Entry<RolesPermission>(oldrolePer).State = EntityState.Modified;
            return MyDb.SaveChanges();
        }
        public int Create(RolesPermission rolePer)
        {
            MyDb.RolesPermission.Add(rolePer);
            return MyDb.SaveChanges();
        }
    }
}
