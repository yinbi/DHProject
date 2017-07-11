using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHWebAdmin.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOn()
        {
            ViewBag.info = " 输入用户名密码登录. ";
            return View();
        }

    }
}
