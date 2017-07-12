using DHGame.Data;
using DHGame.Entity.DHWebSiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DHGame.Logic
{
    public class LogicBase
    {
        protected DHWebSiteDatabase MyDb = new DHWebSiteDatabase();
        protected string UserName = null;
        protected int UserId = 0;

        public LogicBase()
        {
            try
            {
                var currentUser = Utility.MyJson.Deserialize<CurrentUser>(HttpContext.Current.User.Identity.Name);
                UserName = currentUser.Name;
                UserId = currentUser.Id;
            }
            catch (Exception exception)
            {
                UserName = null;
                //GameCommon.Log.Log.WriteGlobalLog(exception);
            }

        }
    }
}
