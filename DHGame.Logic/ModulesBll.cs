using DHGame.Entity.DHWebSiteDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Logic
{
    public class ModulesBll : LogicBase
    {
        public List<Modules> GetAllModules()
        {
            return MyDb.Modules.AsNoTracking().OrderBy(c => c.ParentId).ThenBy(c => c.OrderNo).ToList();
        }
        public Modules Find(int id)
        {
            return MyDb.Modules.Find(id);
        }
        public int Edit(Modules modules)
        {
            var oldRole = MyDb.Modules.AsNoTracking().FirstOrDefault(p => p.Id == modules.Id);
            MyDb.Entry(modules).State = EntityState.Modified;
            return MyDb.SaveChanges();
        }
        public int Create(Modules modules)
        {
            MyDb.Modules.Add(modules);
            return MyDb.SaveChanges();
        }
        public int Delete(int id)
        {
            var modules = MyDb.Modules.FirstOrDefault(p => p.Id == id);
            if (modules != null)
            {
                MyDb.Modules.Remove(modules);
                return MyDb.SaveChanges();
            }
            return 0;
        }
    }
}
