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
            var user = from u in db.Users select u;
            var count = (from u in db.Users select u).Count();
            ViewBag.users = Json(user.ToArray());
            ViewBag.count = Json(count);
            return View();
        }
    }
}
