using DHGame.Entity.DHWebSiteDB;
using DHGame.Logic;
using DHGame.Utility;
using DHGame.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DHGame.WebAdmin.Controllers
{
    public class RolesController : BaseController
    {
        readonly RolesBll rolesBll = new RolesBll();
        readonly RolesPermissionBll rolePermissionsBll = new RolesPermissionBll();
        readonly ModulesBll modulesBll = new ModulesBll();
        private int _index = 0;
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
            StringBuilder sb = new StringBuilder();
            var modulesList = modulesBll.GetAllModules();
            string _html = CreateTreeGrid(modulesList, 0, 0, sb, model.RolePermissions, model.Id);
            ViewBag.TreeHtml = _html;
            return View(model);
        }
        [NonAction]
        private string CreateTreeGrid(List<Modules> modulesList, int parentid, int level, StringBuilder sb,List<RolesPermission> rolePerList,int roleId)
        {
            var li = modulesList.Where(p => p.ParentId == parentid).OrderBy(p => p.OrderNo).ToList();
            if (li.Count > 0)
            {

                foreach (var item in li)
                {
                    int actionSum = 0;
                    var rolePerModel = rolePerList.Where(p => p.RoleId == roleId && p.ModuleId == item.Id).FirstOrDefault();
                    if (rolePerModel != null)
                    {
                        actionSum = rolePerModel.ActionValueSum;
                    }
                    int rowId = GetTipTopParentId(modulesList, parentid);
                    sb.Append("<div rel=\"rowpid_" + rowId + "\">");
                    sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"dataTable\">");
                    sb.Append("<tr align=\"left\" class=\"dataTr\">");
                    sb.Append("<td width=\"3%\" height=\"32\"><input type=\"checkbox\" name=\"siteid[" + roleId + "]\" value=\"" + roleId + "\" onclick=\"SelRole(" + roleId + "," + item.Id + ",this);\" " + (actionSum == 0 ? "" : "checked") + "  /></td>");
                    if (level == 0)
                    {
                        sb.Append("<td align=\"left\" width=\"45%\"><span class=\"minusSign\" id=\"rowid_" + item.Id + "\" onclick=\"DisplayRows(" + item.Id + ");\">" + item.Title + "</span></td>");
                    }
                    else
                    {
                        string _str = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        for (int i = 0; i < level; i++)
                        {
                            _str += _str;
                        }
                        sb.Append("<td align=\"left\" width=\"45%\">" + _str + "<span class=\"subType\">" + item.Title + "</span></td>");
                    }
                    sb.Append("<td width=\"30%\" class=\"privTxt\">");

                    sb.Append("<span><input type=\"checkbox\" name=\"priv[" + roleId + "][" + item.Id + "][]\" value=\"1\" onclick=\"SetActionValueSum(" + roleId + "," + item.Id + ")\" " + ((actionSum & 1) == 1 ? "checked" : "") + " />查看</span> ");
                    sb.Append("<span><input type=\"checkbox\" name=\"priv[" + roleId + "][" + item.Id + "][]\" value=\"2\" onclick=\"SetActionValueSum(" + roleId + "," + item.Id + ")\" " + ((actionSum & 2) == 2 ? "checked" : "") + " />添加</span> ");
                    sb.Append("<span><input type=\"checkbox\" name=\"priv[" + roleId + "][" + item.Id + "][]\" value=\"4\" onclick=\"SetActionValueSum(" + roleId + "," + item.Id + ")\" " + ((actionSum & 4) == 4 ? "checked" : "") + " />修改</span> ");
                    sb.Append("<span><input type=\"checkbox\" name=\"priv[" + roleId + "][" + item.Id + "][]\" value=\"8\" onclick=\"SetActionValueSum(" + roleId + "," + item.Id + ")\" " + ((actionSum & 8) == 8 ? "checked" : "") + " />删除</span> ");
                    sb.Append("<span><input type=\"checkbox\" name=\"priv[" + roleId + "][" + item.Id + "][]\" value=\"16\" onclick=\"SetActionValueSum(" + roleId + "," + item.Id + ")\" " + ((actionSum & 16) == 16 ? "checked" : "") + " />下载</span> ");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<input type=\"hidden\" name=\"RolePermissions[" + _index + "].RoleId\" value=\"" + roleId + "\" />");
                    sb.Append("<input type=\"hidden\" name=\"RolePermissions[" + _index + "].ModuleId\" value=\"" + item.Id + "\" />");
                    sb.Append("<input type=\"hidden\" id=\"ActionValueSum_" + roleId + "_" + item.Id + "\" name=\"RolePermissions[" + _index + "].ActionValueSum\" value=\"" + actionSum + "\" />");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    ++_index;
                    int a = level + 1;
                    CreateTreeGrid(modulesList, item.Id, a, sb, rolePerList, roleId);
                }
            }
            return sb.ToString();
        }
        [NonAction]
        private int GetTipTopParentId(List<Modules> list, int parentId)
        {
            if (parentId == 0) return 0;
            if (list.Count != 0)
            {
                var model = list.Where(p => p.Id == parentId).FirstOrDefault();
                if (model.ParentId == 0)
                {
                    return model.Id;
                }
                else
                    return GetTipTopParentId(list, model.ParentId);
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        public string Edit(RolesModel role)
        {
            Roles model = new Roles();
            if (ModelState.IsValid)
            {
                model.Id = role.Id;
                model.RoleName = role.RoleName;
                model.RoleDesc = role.RoleDesc;
                model.Nullity = role.Nullity;
                rolesBll.Edit(model);
                if (role.RolePermissions != null && role.RolePermissions.Count > 0)
                {
                    List<RolesPermission> oldRolesAction = rolePermissionsBll.GetModuleIdByRoleId(role.Id);
                    List<RolesPermission> newRolesAction = role.RolePermissions.Where(p => p.ActionValueSum > 0).ToList();
                    //rolesBll.UpdateRolesModules(oldRolesAction, newRolesAction);
                    rolePermissionsBll.UpdateRolesModules(oldRolesAction, newRolesAction);
                }
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

        public ActionResult TopMenu()
        {
            List<RolesPermission> li = rolePermissionsBll.GetCurRolePer().Where(p => p.Modules.ParentId == 0).OrderBy(p => p.Modules.OrderNo).ToList();
            return View(li);
        }
        public ActionResult LeftMenu(int? id)
        {
            List<RolesPermission> li = rolePermissionsBll.GetCurRolePer();
            if (id == null)
            {
                RolesPermission topModel = li.Where(p => p.Modules.ParentId == 0).OrderBy(p => p.Modules.OrderNo).FirstOrDefault();
                if (topModel != null)
                {
                    id = topModel.Modules.Id;
                }
            }
            ViewBag.ModuleId = id;
            return View(li);
        }

    }
}
