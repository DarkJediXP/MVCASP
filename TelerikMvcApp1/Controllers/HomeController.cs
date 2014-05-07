using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.SignalR;

using Core;

namespace TelerikMvcApp1.Controllers
{
    public class HomeController : Controller
    {
        // This is the controller for the index view
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        //just testing out controllers
        public ActionResult HelloWorld()
        {
            ViewBag.Message = "Hello World from Sean!";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AuthenticateUser(string username, string password)
        {
            ContentResult res = new ContentResult();

            var db = ContextFactory.GetContextPerRequest();
            try { 
                var user = (from u in db.Users
                            where u.Username == username &&
                                  u.Password == password
                            select u).Single();

                if (user != null && user.Username == "Admin@test.com" && user.Password == "test")
                {
                    ViewBag.adminUser= user;
                    return RedirectToAction("Main", "Admin", new { adminUser = username });
                }
                else
                {
                    ViewBag.user = user;
                    return View("Main");
                }
            }catch{
                res.Content = "No record exists for " + username + " or the password is incorrect";
                return res;
            }
        }

        public ActionResult AddUser()
        {
            return View(new Core.User());
        }

        // This is the controller for the About view
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult JoinUser(string username, string password)
        {
            var db = ContextFactory.GetContextPerRequest();
            Core.User user = new Core.User();
            user.Username = username;
            user.Password = password;
            user.Active = 1;
            db.Add(user);
            db.SaveChanges();
            return View("Index");
        }


        // This is the controller for the Chat() view
        public ActionResult Chat(string username)
        {
            ViewBag.Message = "Chat, please!";
            ViewBag.name = (string)username;
            return View();
        }

        public ActionResult EditUserInfo(long userid) 
        {
            var db = ContextFactory.GetContextPerRequest();
            var user = (from u in db.Users where u.User_id == userid select u).Single();
            ViewBag.user = user;
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveUserInfo(long userid, string username, string password) 
        {
            var db = ContextFactory.GetContextPerRequest();
            var user = (from u in db.Users where u.User_id == userid select u).Single();
            user.Username = username;
            user.Password = password;
            db.SaveChanges();
            ViewBag.user = user;
            return View("Main");
        }

        public ActionResult Main(string username)
        {
            ViewBag.name = (string)username;
            return View();
        }


        // This is the controller for the Shout view
        public ActionResult Shout()
        {
            ViewBag.Message = "Shout, please!";

            if (Request.Form["shouted"] != null)
            {
                // send a shout
                var message = Request.Form["message"].ToString();

                var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

                hub.Clients.All.broadcastMessage("SYSTEM", message);

                ViewBag.Message = "Message Sent";

                var db = ContextFactory.GetContextPerRequest();
                Chat newChat = new Core.Chat();
                newChat.Time = DateTime.Now;
                newChat.Name = "SYSTEM";
                newChat.Message = message;
                db.Add(newChat);
                db.SaveChanges();
            }


            return View();
        }
    }
}