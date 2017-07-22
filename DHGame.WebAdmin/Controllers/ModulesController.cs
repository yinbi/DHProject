using DHGame.Entity.DHWebSiteDB;
using DHGame.Logic;
using DHGame.Utility;
using DHGame.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DHGame.WebAdmin.Controllers
{
    public class ModulesController : BaseController
    {
        readonly ModulesBll modulesBll = new ModulesBll();
        //
        // GET: /Modules/

        public ActionResult Index()
        {
            var list = modulesBll.GetAllModules();
            StringBuilder sb = new StringBuilder();
            string _html = CreateTreeGrid(list, 0, 0, sb);
            ViewBag.TreeHtml = _html;
            return View(list);
        }
        public ActionResult Create(int id)
        {
            var li = modulesBll.GetAllModules();
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value=\"0\">一级菜单</option>");
            string _html = CreateTreeSelect(li, 0, id, 0, sb);
            ViewBag.TreeSelectHtml = _html;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Modules modules)
        {
            if (ModelState.IsValid)
            {
                modulesBll.Create(modules);
                return RedirectToAction("Index", "Modules");

            }
            else
            {
                foreach (var s in ModelState.Values)
                {
                    foreach (var p in s.Errors)
                    {
                        ViewBag.info += p.ErrorMessage;
                        break;
                    }
                }
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            modulesBll.Delete(id);
            return RedirectToAction("Index", "Modules");
        }

        [HttpPost]
        public ActionResult Edit(Modules modules)
        {
            if (ModelState.IsValid)
            {
                modulesBll.Edit(modules);
            }
            else
            {
                foreach (var s in ModelState.Values)
                {
                    foreach (var p in s.Errors)
                    {
                        ViewBag.info += p.ErrorMessage;
                        return View(modules);
                    }
                }
            }
            return RedirectToAction("Index", "Modules");
        }
        public ActionResult Edit(int id)
        {
            var li = modulesBll.GetAllModules();
            Modules modules = li.Where(p => p.Id == id).FirstOrDefault();
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value=\"0\">一级菜单</option>");
            string _html = CreateTreeSelect(li, 0, modules.ParentId, 0, sb);
            ViewBag.TreeSelectHtml = _html;
            return View(modules);
        }
        [NonAction]
        private string CreateTreeSelect(List<Modules> list, int parentId, int curParentId, int level, StringBuilder sb)
        {
            var li = list.Where(p => p.ParentId == parentId).OrderBy(p => p.OrderNo).ToList();
            if (li.Count > 0)
            {
                foreach (var item in li)
                {
                    if (level == 0)
                    {
                        sb.Append("<option value=\"" + item.Id + "\" " + (item.Id == curParentId ? "selected=\"selected\"" : "") + ">" + item.Title + "</option>");
                    }
                    else
                    {
                        string _str = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        for (int i = 0; i < level; i++)
                        {
                            _str += _str;
                        }
                        sb.Append("<option value=\"" + item.Id + "\" " + (item.Id == curParentId ? "selected=\"selected\"" : "") + ">" + _str + "|-" + item.Title + "</option>");
                    }
                    int a = level + 1;
                    CreateTreeSelect(list, item.Id, curParentId, a, sb);
                }
            }
            return sb.ToString();
        }

        [NonAction]
        private string CreateTreeGrid(List<Modules> list, int parentid, int level, StringBuilder sb)
        {
            var li = list.Where(p => p.ParentId == parentid).OrderBy(p => p.OrderNo).ToList();
            if (li.Count > 0)
            {

                foreach (var item in li)
                {
                    int rowId = GetTipTopParentId(list, parentid);
                    sb.Append("<div rel=\"rowpid_" + rowId + "\">");
                    sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"dataTable\">");
                    sb.Append("<tr align=\"left\" class=\"dataTr\">");
                    sb.Append("<td width=\"5%\" height=\"36\" class=\"firstCol\"><input type=\"checkbox\" name=\"checkid[]\" id=\"checkid[]\" value=\"" + item.Id + "\" /></td>");
                    sb.Append("<td width=\"3%\">" + item.Id + " <input type=\"hidden\" name=\"id[]\" id=\"id[]\" value=\"" + item.Id + "\" /></td>");
                    if (level == 0)
                    {
                        sb.Append("<td width=\"22%\"><span class=\"minusSign\" id=\"rowid_" + item.Id + "\" onclick=\"DisplayRows(" + item.Id + ");\">" + item.Title + "</span></td>");
                    }
                    else
                    {
                        string _str = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        for (int i = 0; i < level; i++)
                        {
                            _str += _str;
                        }
                        sb.Append("<td width=\"22%\">" + _str + "<span class=\"subType\">" + item.Title + "</span></td>");
                    }
                    sb.Append("<td width=\"5%\">" + item.Controller + "</td>");
                    sb.Append("<td width=\"5%\">" + item.Action + "</td>");
                    sb.Append("<td width=\"20%\" align=\"center\">");
                    sb.Append("<a href=\"javascript:void(0)\" class=\"leftArrow\" title=\"提升排序\"></a>");
                    sb.Append("<input type=\"text\" name=\"orderid[]\" id=\"orderid[]\" class=\"inputls\" value=\"" + item.OrderNo + "\" />");
                    sb.Append("<a href=\"javascript:void(0)\" class=\"rightArrow\" title=\"下降排序\"></a>");
                    sb.Append("</td>");
                    sb.Append("<td width=\"32%\" class=\"action endCol\">");
                    sb.Append("<span><a href=\"/Modules/Create/" + item.Id + "\">添加子栏目</a></span> | ");
                    sb.Append("<span><a href=\"javascript:void(0)\" title=\"点击进行显示与隐藏操作\">" + (item.Nullity ? "隐藏" : "显示") + "</a></span> | ");
                    sb.Append("<span><a href=\"/Modules/Edit/" + item.Id + "\">修改</a></span> | ");
                    sb.Append("<span class=\"nb\"><a href=\"/Modules/Delete/" + item.Id + "\" onclick=\"if(confirm('确认删除吗？')==false)return false;\" >删除</a></span>"); //onclick=\"return ConfDel(2);\"  onclick=\"return Alert.confirm(\"确定删除吗?\", function () { return true; });\"   /Modules/Delete/" + item.Id + "
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    int a = level + 1;
                    CreateTreeGrid(list, item.Id, a, sb);
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

    }
}
