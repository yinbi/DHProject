using DHGame.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Data
{
    public class AdminDatabase : MyDataBase
    {
        public AdminDatabase()
            : base(WebConfig.DHWebSiteDBConnStr)
        { }
    }
}
