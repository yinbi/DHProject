using DHGame.Entity.DHWebSiteDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Logic
{
    public class RolesBll : LogicBase
    {
        public List<Roles> PageList(int pageIndex, int pageSize, Expression<Func<Roles, bool>> whereLambda, Expression<Func<Roles, string>> orderByExpression, bool isAsc, out int records)
        {
            if (isAsc)
            {
                var query = MyDb.Roles.AsNoTracking().Where(whereLambda).OrderBy(orderByExpression);
                records = query.Count();
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).
                        ToList();
            }
            else
            {
                var query = MyDb.Roles.AsNoTracking().Where(whereLambda).OrderByDescending(orderByExpression);
                records = query.Count();
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).
                        ToList();
            }
        }
        public Roles Find(int id)
        {
            return MyDb.Roles.Find(id);
        }
        public int Edit(Roles role)
        {
            var oldRole = MyDb.Roles.AsNoTracking().FirstOrDefault(p => p.Id == role.Id);
            MyDb.Entry(role).State = EntityState.Modified;
            return MyDb.SaveChanges();
        }
        public int Create(Roles role)
        {
            MyDb.Roles.Add(role);
            return MyDb.SaveChanges();
        }
        public int Delete(int id)
        {
            var role = MyDb.Roles.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (role != null)
            {
                MyDb.Roles.Remove(role);
                return MyDb.SaveChanges();
            }
            return 0;
        }
    }
}
