using DHGame.Entity.DHWebSiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Logic
{
    public class AdminsBll : LogicBase
    {
        public List<Admins> PageList(int pageIndex, int pageSize, out int records)
        {
            var query = MyDb.Admins.Include("Roles").AsNoTracking().Select(v => v);
            records = query.Count();
            return query.OrderByDescending(p => p.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).
                    ToList();
        }
        public Admins GetAdminUser(string loginName,string loginPwd)
        {
            return MyDb.Admins.FirstOrDefault(p => p.LoginName.Equals(loginName, StringComparison.Ordinal) && p.LoginPwd.Equals(loginPwd, StringComparison.Ordinal));
            //return MyDb.Admins.Include("Roles").FirstOrDefault(p => p.LoginName.Equals(loginName, StringComparison.Ordinal) && p.LoginPwd.Equals(loginPwd, StringComparison.Ordinal));
        }
    }
}
