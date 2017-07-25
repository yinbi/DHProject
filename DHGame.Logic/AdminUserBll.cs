using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DHGame.Entity.DHWebSiteDB;
using System.Data.Entity;
using DHGame.Config;

namespace DHGame.Logic
{
    public class AdminUserBll : LogicBase
    {
        public List<Admins> PageList(int pageIndex, int pageSize, Expression<Func<Admins, bool>> whereLambda, Expression<Func<Admins, string>> orderByExpression, bool isAsc, out int records)
        {
            if (isAsc)
            {
                var query = MyDb.Admins.Include("Roles").AsNoTracking().Where(whereLambda).OrderBy(orderByExpression); 
                records = query.Count();
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).
                        ToList();
            }
            else
            {
                var query = MyDb.Admins.Include("Roles").AsNoTracking().Where(whereLambda).OrderByDescending(orderByExpression); 
                records = query.Count();
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).
                        ToList();
            }
            //var query = MyDb.Admins.Include("Roles").AsNoTracking().Select(v => v).OrderByDescending(orderByExpression);
            //records = query.Count();
            //return query.OrderByDescending(p => p.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).
            //        ToList();
        }
        public Admins GetAdminUser(string loginName,string loginPwd)
        {
            return MyDb.Admins.FirstOrDefault(p => p.LoginName.Equals(loginName, StringComparison.Ordinal) && p.LoginPwd.Equals(loginPwd, StringComparison.Ordinal));
            //return MyDb.Admins.Include("Roles").FirstOrDefault(p => p.LoginName.Equals(loginName, StringComparison.Ordinal) && p.LoginPwd.Equals(loginPwd, StringComparison.Ordinal));
        }
        public Admins Find(int id)
        {
            return MyDb.Admins.Find(id);
        }
        public int Edit(Admins admin)
        {
            Admins oldAdmin = MyDb.Admins.AsNoTracking().FirstOrDefault(p => p.Id == admin.Id);
            oldAdmin.LoginName = admin.LoginName;
            oldAdmin.RealName = admin.RealName;
            if(!string.IsNullOrEmpty(admin.LoginPwd))
            {
                oldAdmin.LoginPwd = EncryptionHelper.EncryptText(admin.LoginPwd);
            }
            oldAdmin.RoleId = admin.RoleId;
            oldAdmin.Enable = admin.Enable;
            MyDb.Set<Admins>().Attach(oldAdmin);
            MyDb.Entry<Admins>(oldAdmin).State = EntityState.Modified;
            return MyDb.SaveChanges();
        }
        public int Create(Admins model)
        {
            MyDb.Admins.Add(model);
            return MyDb.SaveChanges();
        }
    }
}
