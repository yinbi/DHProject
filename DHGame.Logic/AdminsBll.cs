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
        public Admins GetAdminUser(string loginName,string loginPwd)
        {
            return MyDb.Admins.FirstOrDefault(p => p.LoginName.Equals(loginName, StringComparison.Ordinal) && p.LoginPwd.Equals(loginPwd, StringComparison.Ordinal));
        }
    }
}
