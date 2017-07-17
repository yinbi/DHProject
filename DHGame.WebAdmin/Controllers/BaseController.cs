using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHGame.WebAdmin.Controllers
{
    public class BaseController : Controller
    {
        protected int PageSize = 20;
        protected int Total = 0;


        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!System.Web.HttpContext.Current.Request.IsAuthenticated)
            {
                System.Web.HttpContext.Current.Response.Redirect("/Account/LogOn", true);
            }
            else
            {
                base.OnAuthorization(filterContext);
            }

        }


        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null) return;
            //GameCommon.Log.Log.WriteGlobalLog(filterContext.Exception);
            base.OnException(filterContext);
        }

        /// <summary>
        /// 页面信息提示，默认成功样式显示
        /// </summary>
        /// <param name="msg">提示信息</param>
        protected void Msg(string msg)
        {
            TempData["Hint"] = msg;
        }

        ///// <summary>
        ///// 页面信息提示,指定显示样式
        ///// </summary>
        ///// <param name="msg">提示信息</param>
        ///// <param name="style">样式枚举：GameCommon.Enum.Auth.ShowMsgStyle</param>
        //protected void Msg(string msg, ShowMsgStyle style)
        //{
        //    TempData["HintStyle"] = style;


        //    Msg(msg);
        //}

        //protected void Log(Log log)
        //{
        //    MyDb.Logs.Add(log);
        //    MyDb.SaveChanges();
        //}

        //protected void Lg(string entityName,AuthActions authActions,string oldv=null,string newv=null,string desc=null)
        //{
        //    var log = new Log
        //                  {
        //                      EntityName = entityName,
        //                      AuthActions = authActions,
        //                      OptDate = DateTime.Now,
        //                      OptAdmin = User.Identity.Name,
        //                      OldValue = oldv,
        //                      NewValue = newv,
        //                      Dscn = desc
        //                  };

        //    MyDb.Logs.Add(log);
        //    MyDb.SaveChanges();
        //}
        /// <summary>
        /// 增加操作的日志
        /// </summary>
        /// <param name="obj"></param>
        //protected void AddLg(object obj)
        //{
        //    var log = new Log
        //                  {
        //                      EntityName = obj.ToString(),
        //                      AuthActions = AuthActions.Add,
        //                      OptDate = DateTime.Now,
        //                      OptAdmin = User.Identity.Name,
        //                      OldValue = null,
        //                      NewValue = Object2Json(obj),
        //                      Dscn = null
        //    };

        //    MyDb.Logs.Add(log);
        //    MyDb.SaveChanges();
        //}


        //protected string Object2Json(Object obj)
        //{
        //    return new JavaScriptSerializer().Serialize(obj);
        //}

        protected override void Dispose(bool disposing)
        {
            //MyDb.Dispose();
            base.Dispose(disposing);
        }
    }
}
