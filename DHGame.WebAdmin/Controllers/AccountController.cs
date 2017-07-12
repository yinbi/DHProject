using DHGame.Config;
using DHGame.Entity.DHWebSiteDB;
using DHGame.Logic;
using DHGame.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DHGame.WebAdmin.Controllers
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
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            var encPwc = EncryptionHelper.EncryptText(model.Password);

            //var dhWebSiteDatabase = new DHWebSiteDatabase();
            var adminBll = new AdminsBll();

            if (Session["validCode"] == null)
            {
                ViewBag.info = "对不起，验证码超时，请重试！";
                return View(model);
            }

            if (model.Yzm == null)
            {
                model.Yzm = Request.Form["Yzm"];
            }

            if (Session["validCode"].ToString().Trim().ToLower() != model.Yzm.ToLower().Trim())
            {
                ViewBag.info = "对不起，验证码不正确，请重新输入！";
                return View(model);
            }


            if (ModelState.IsValid)
            {
                //var user =dhWebSiteDatabase.Admins.FirstOrDefault(p => p.LoginName.Equals(model.UserName, StringComparison.Ordinal) && p.LoginPwd.Equals(encPwc, StringComparison.Ordinal));
                var user = adminBll.GetAdminUser(model.UserName, encPwc);

                if (user != null)
                {
                    //var userRoleBll = new Logic.UserRoleBll();
                    var currentUser = new CurrentUser
                    {
                        Id = user.Id,
                        Name = user.LoginName,//user.AdmName,
                        RoleId = user.RoleId// = userRoleBll.UserRoleIds(user.Id)
                    };

                    //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    FormsAuthentication.SetAuthCookie(Utility.MyJson.Serialize(currentUser), model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.info = " 用户名或者密码不正确.";
                }
            }
            else
            {
                foreach (var s in ModelState.Values)
                {
                    foreach (var p in s.Errors)
                    {
                        ViewBag.info += p.ErrorMessage;
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}