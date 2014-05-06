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

        public ActionResult Main(string name)
        {
            ViewBag.name = (string)name;
            return View();
        }

        public ActionResult ControlPanel()
        {
            var db = ContextFactory.GetContextPerRequest();
            var users = from u in db.Users select u;
            ViewBag.users = users.ToArray();
            return View();
        }

        public ActionResult EditUser(long userid)
        {
            var db = ContextFactory.GetContextPerRequest();
            var user = (from u in db.Users where u.User_id == userid select u).Single();
            ViewBag.user = user;
            return View();
        }

        public ActionResult SaveUser(long userid, string username, string password)
        {
            var db = ContextFactory.GetContextPerRequest();
            var user = (from u in db.Users where u.User_id == userid select u).Single();
            user.Username = username;
            user.Password = password;
            db.SaveChanges();
            return RedirectToAction("ControlPanel", "Admin");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteUser(long userid)
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
                return RedirectToAction("ControlPanel", "Admin");
            }
            return RedirectToAction("ControlPanel", "Admin");
        }
    }
}
