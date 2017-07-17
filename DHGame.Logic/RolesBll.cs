using DHGame.Entity.DHWebSiteDB;
using System;
using System.Collections.Generic;
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
    }
}
