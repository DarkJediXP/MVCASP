using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Core;

namespace TelerikMvcApp1.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main(string adminUser)
        {
            ViewBag.adminUser = adminUser;
            ContentResult res = new ContentResult();
            var db = ContextFactory.GetContextPerRequest();
            try
            {
                ViewBag.activeUserCount = (from u in db.Users
                                           where u.Active == 1
                                           select u).Count();
            }
            catch
            {
                res.Content = "error in count function";
                return res;
            }
            return View();
        }

        public ActionResult ControlPanel(string adminUsername)
        {
            var db = ContextFactory.GetContextPerRequest();
            var users = from u in db.Users select u;
            ViewBag.adminUser = adminUsername;
            ViewBag.users = users.ToArray();
            return View();
        }

        public ActionResult EditUser(long userid, string adminUser)
        {
            var db = ContextFactory.GetContextPerRequest();
            var user = (from u in db.Users where u.User_id == userid select u).Single();
            ViewBag.adminUser = adminUser;
            ViewBag.user = user;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveUser(long userid, string username, string password, string adminUser)
        {
            var db = ContextFactory.GetContextPerRequest();
            var user = (from u in db.Users where u.User_id == userid select u).Single();
            user.Username = username;
            user.Password = password;
            db.SaveChanges();
            return RedirectToAction("ControlPanel", "Admin", new { adminUsername = adminUser});
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteUser(long userid, string adminUser)
        {
            var db = ContextFactory.GetContextPerRequest();
            try
            {
                var user = (from u in db.Users where u.User_id == userid select u).Single();
                if (user.Username != "Admin@test.com")
                {
                    db.Delete(user);
                    db.SaveChanges();
                }
            }
            catch
            {
                ViewBag.adminUser = adminUser;
                return RedirectToAction("ControlPanel", "Admin", new { adminUsername = adminUser });
            }
            ViewBag.adminUser = adminUser;
            return RedirectToAction("ControlPanel", "Admin", new { adminUsername = adminUser });
        }
    }
}
