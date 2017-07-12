using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHGame.Config;
using System.Data.Entity;
using DHGame.Entity.DHWebSiteDB;

namespace DHGame.Data
{
    public class DHWebSiteDatabase : MyDataBase
    {
        public DHWebSiteDatabase()
            : base(WebConfig.DHWebSiteDBConnStr)
        { }

        public DbSet<Admins> Admins { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolesPermission> RolesPermission { get; set; }

    }
}
