﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DHGame.Data;
using DHGame.Entity.DHWebSiteDB;
using System.Web;

namespace DHGame.Logic
{
    public class LogicBase
    {
        protected DHWebSiteDatabase MyDb = new DHWebSiteDatabase();
        protected string UserName = null;
        protected int UserId = 0;
        protected int RoleId = 0;

        public LogicBase()
        {
            try
            {
                var currentUser = Utility.MyJson.Deserialize<CurrentUser>(HttpContext.Current.User.Identity.Name);
                UserName = currentUser.Name;
                UserId = currentUser.Id;
                RoleId = currentUser.RoleId;
            }
            catch (Exception exception)
            {
                UserName = null;
                //GameCommon.Log.Log.WriteGlobalLog(exception);
            }

        }
    }
}
