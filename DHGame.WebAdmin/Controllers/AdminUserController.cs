﻿using DHGame.Config;
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
    public class AdminUserController : BaseController
    {
        readonly AdminUserBll adminUserBll = new AdminUserBll();
        readonly RolesBll rolesBll = new RolesBll();
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
            string _html = rolesBll.CreateAllRolsSelect(0);
            ViewBag.RolesSelectHtml = _html;
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

            WhereHelper<Admins> whereLambda = new WhereHelper<Admins>();
            if (roleid != 0)
            {
                whereLambda.Equal("RoleId", roleid, "and");
            }
            if (enable != -1)
            {
                whereLambda.Equal("Enable", (enable == 1), "and");
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                if (selectUser == 0)
                {
                    whereLambda.Equal("LoginName", keywords, "and");
                }
                if (selectUser == 1)
                {
                    whereLambda.Equal("RealName", keywords, "and");
                }
            }

            //Expression<Func<Admins, bool>> whereLambda = null;
            //whereLambda = (c => (roleid == 0 || c.RoleId == roleid)
            //    && (enable == -1 || c.Enable == (enable == 1))
            //    && (
            //        string.IsNullOrEmpty(keywords)
            //        || (selectUser == 0 && c.LoginName == keywords)
            //        || (selectUser == 1 && c.RealName == keywords)
            //       )
            //    );

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
            var list = adminUserBll.PageList(pageNum, numPerPage, whereLambda.GetExpression(), orderByExpression, isAsc, out Total);
            ViewBag.pageNum = pageNum;
            ViewBag.numPerPage = numPerPage;
            ViewBag.orderField = orderField;
            ViewBag.orderDirection = (isAsc == true ? "asc" : "desc");
            ViewBag.Total = Total;
            ViewBag.pagenumshown = Utility.Utils.GetPageCount(Total, numPerPage);
            string _html = rolesBll.CreateAllRolsSelect(roleid);
            ViewBag.RolesSelectHtml = _html;
            return View(list);
            //string str = "{\"statusCode\":\"200\", \"message\":\"操作成功\",\"navTabId\":\"\", \"rel\":\"\",  \"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
            //return Json(str);
        }

        public ActionResult Edit(int id)
        {
            Admins user = adminUserBll.Find(id);
            string _html = rolesBll.CreateAllRolsSelect(user.RoleId);
            ViewBag.RolesSelectHtml = _html;
            return View(user);
        }

        [HttpPost]
        public string Edit(Admins user)
        {
            if (ModelState.IsValid)
            {
                adminUserBll.Edit(user);
            }
            ReturnMsg msg = new ReturnMsg { statusCode = "200", message = "操作成功", callbackType = "closeCurrent" };
            return MyJson.Serialize<ReturnMsg>(msg);
        }
        public ActionResult Create()
        {
            string _html = rolesBll.CreateAllRolsSelect(0);
            ViewBag.RolesSelectHtml = _html;
            return View();
        }
        [HttpPost]
        public string Create(AdminUserModel model)
        {
            Admins adminUser = new Admins();
            adminUser.LoginName = model.LoginName;
            adminUser.LoginPwd = EncryptionHelper.EncryptText(model.LoginPwd);
            adminUser.RealName = model.RealName;
            adminUser.RoleId = model.RoleId;
            adminUser.Enable = model.Enable;
            adminUser.ErrNum = 0;
            adminUser.LastLoginIp = "";
            adminUser.LastLoginTime = DateTime.Now;
            adminUserBll.Create(adminUser);
            ReturnMsg msg = new ReturnMsg { statusCode = "200", message = "操作成功", callbackType = "closeCurrent" };
            return MyJson.Serialize<ReturnMsg>(msg);
        }
        

    }
}
