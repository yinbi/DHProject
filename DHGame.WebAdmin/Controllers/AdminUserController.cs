using DHGame.Entity.DHWebSiteDB;
using DHGame.Logic;
using DHGame.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace DHGame.WebAdmin.Controllers
{
    public class AdminUserController : BaseController
    {
        readonly AdminUserBll adminUserBll = new AdminUserBll();
        //
        // GET: /AdminUser/

        public ActionResult Index()
        {
            int pageNum = WebUtil.Get("pageNum", 1);
            int numPerPage=WebUtil.Get("numPerPage",PageSize);
            string orderField=WebUtil.Get("orderField","Id");
            bool isAsc = (WebUtil.Get("orderDirection", "asc") == "asc") ? true : false;
            Expression<Func<Admins, bool>> whereLambda = c => 1 == 1;
            Expression<Func<Admins, string>> orderByExpression = c => c.Id.ToString();
            var list = adminUserBll.PageList(pageNum, numPerPage, whereLambda, orderByExpression, isAsc, out Total);
            ViewBag.pageNum = pageNum;
            ViewBag.numPerPage = numPerPage;
            ViewBag.orderField = orderField;
            ViewBag.orderDirection = (isAsc == true ? "asc" : "desc");
            ViewBag.Total = Total;
            ViewBag.pagenumshown = Utility.Utils.GetPageCount(Total, numPerPage);
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            int pageNum = WebUtil.Get("pageNum", 1);
            int numPerPage = WebUtil.Get("numPerPage", PageSize);
            string orderField = WebUtil.Get("orderField", "Id");
            bool isAsc = (WebUtil.Get("orderDirection", "asc") == "asc") ? true : false;

            int roleid = WebUtil.Get("RoleId", 0);
            int enable = WebUtil.Get("Enable", -1);
            int selectUser = WebUtil.Get("selectUser", 0);
            string keywords = WebUtil.Get("keywords", "");

            Expression<Func<Admins, bool>> whereLambda = null;
            whereLambda = (c => (roleid == 0 || c.RoleId == roleid)
                //&& (enable == -1 ||Convert.ToInt16(c.Enable) == Convert.ToInt16(enable))
                &&(
                    string.IsNullOrEmpty(keywords)
                    || (selectUser==0 && c.LoginName==keywords)
                    || (selectUser==1 && c.RealName==keywords)
                   )
                );

            Expression<Func<Admins, string>> orderByExpression = c => c.Id.ToString();
            switch (orderField)
            {
                case "LoginName":
                    orderByExpression = c => c.LoginName;
                    break;
                case "RealName":
                    orderByExpression = c => c.RealName;
                    break;
                case "RoleId":
                    orderByExpression = c => c.RoleId.ToString();
                    break;
                case "Enable":
                    orderByExpression = c => c.Enable.ToString();
                    break;
                case "LastLoginTime":
                    orderByExpression = c => c.LastLoginTime.ToString();
                    break;
                case "LastLoginIp":
                    orderByExpression = c => c.LastLoginIp.ToString();
                    break;
                default:
                    break;
            }
            var list = adminUserBll.PageList(pageNum, numPerPage, whereLambda, orderByExpression, isAsc, out Total);
            ViewBag.pageNum = pageNum;
            ViewBag.numPerPage = numPerPage;
            ViewBag.orderField = orderField;
            ViewBag.orderDirection = (isAsc == true ? "asc" : "desc");
            ViewBag.Total = Total;
            ViewBag.pagenumshown = Utility.Utils.GetPageCount(Total, numPerPage);
            return View(list);
            //string str = "{\"statusCode\":\"200\", \"message\":\"操作成功\",\"navTabId\":\"\", \"rel\":\"\",  \"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
            //return Json(str);
        }

    }
}
