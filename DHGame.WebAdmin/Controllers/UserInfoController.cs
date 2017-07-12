using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHGame.WebAdmin.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public ActionResult List()
        {
            return View();
        } 
        public ActionResult View1()
        {
            return View();
        }

    }
}
