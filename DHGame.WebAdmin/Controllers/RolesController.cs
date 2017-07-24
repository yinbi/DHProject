using DHGame.Entity.DHWebSiteDB;
using DHGame.Logic;
using DHGame.Utility;
using DHGame.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace DHGame.WebAdmin.Controllers
{
    public class RolesController : BaseController
    {
        readonly RolesBll rolesBll = new RolesBll();
        readonly RolesPermissionBll rolePermissionsBll = new RolesPermissionBll();
        //
        // GET: /Roles/

        public ActionResult Index()
        {
            int pageNum = WebUtil.Get("pageNum", 1);
            int numPerPage = WebUtil.Get("numPerPage", PageSize);
            string orderField = WebUtil.Get("orderField", "Id");
            bool isAsc = (WebUtil.Get("orderDirection", "asc") == "asc") ? true : false;
            Expression<Func<Roles, bool>> whereLambda = c => 1 == 1;
            Expression<Func<Roles, string>> orderByExpression = c => c.Id.ToString();
            var list = rolesBll.PageList(pageNum, numPerPage, whereLambda, orderByExpression, isAsc, out Total);
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

            WhereHelper<Roles> whereLambda = new WhereHelper<Roles>();
            Expression<Func<Roles, string>> orderByExpression = c => c.Id.ToString();
            switch (orderField)
            {
                case "RoleName":
                    orderByExpression = c => c.RoleName;
                    break;
                case "RoleDesc":
                    orderByExpression = c => c.RoleDesc;
                    break;
                case "Nullity":
                    orderByExpression = c => c.Nullity.ToString();
                    break;
                case "CreateTime":
                    orderByExpression = c => c.CreateTime.ToString();
                    break;
                default:
                    break;
            }

            var list = rolesBll.PageList(pageNum, numPerPage, whereLambda.GetExpression(), orderByExpression, isAsc, out Total);
            ViewBag.pageNum = pageNum;
            ViewBag.numPerPage = numPerPage;
            ViewBag.orderField = orderField;
            ViewBag.orderDirection = (isAsc == true ? "asc" : "desc");
            ViewBag.Total = Total;
            ViewBag.pagenumshown = Utility.Utils.GetPageCount(Total, numPerPage);
            return View(list);
        }
        public ActionResult Edit(int id)
        {
            Roles role = rolesBll.Find(id);
            //role.RolePermissions = rolePermissionsBll.GetModuleIdByRoleId(role.Id);
            RolesModel model = new RolesModel();
            model.Id = role.Id;
            model.RoleName = role.RoleName;
            model.RoleDesc = role.RoleDesc;
            model.Nullity = role.Nullity;
            model.RolePermissions = rolePermissionsBll.GetModuleIdByRoleId(role.Id);
            return View(model);
        }
        [HttpPost]
        public string Edit(Roles role)
        {
            if (ModelState.IsValid)
            {
                rolesBll.Edit(role);
            }
            //ReturnMsg msg = new ReturnMsg { statusCode = "200", message = "操作成功", callbackType = "closeCurrent" };
            ReturnMsg msg = new ReturnMsg { statusCode = "200", message = "操作成功", navTabId = "RolesIndex", callbackType = "closeCurrent" };
            return MyJson.Serialize<ReturnMsg>(msg);
        }
        //[HttpPost]
        //public string Edit(Roles role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        rolesBll.Edit(role);
        //    }
        //    //ReturnMsg msg = new ReturnMsg { statusCode = "200", message = "操作成功", callbackType = "closeCurrent" };
        //    ReturnMsg msg = new ReturnMsg { statusCode = "200", message = "操作成功", navTabId = "RolesIndex", callbackType = "closeCurrent" };
        //    return MyJson.Serialize<ReturnMsg>(msg);
        //}
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public string Create(Roles role)
        {
            rolesBll.Create(role);
            ReturnMsg msg = new ReturnMsg { statusCode = "200", message = "操作成功", navTabId = "RolesIndex", callbackType = "closeCurrent" };
            return MyJson.Serialize<ReturnMsg>(msg);
        }
        public string Delete(int id)
        {
            rolesBll.Delete(id);
            ReturnMsg msg = new ReturnMsg { statusCode = "200", message = "操作成功", callbackType = "" };
            return MyJson.Serialize<ReturnMsg>(msg);
        }

    }
}
