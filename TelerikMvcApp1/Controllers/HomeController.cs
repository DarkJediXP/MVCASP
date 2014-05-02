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
        public ActionResult AuthenticateUser(Core.User userToAdd, string password)
        {
            // gets the user name from the form in the index view
            string username = userToAdd.Username;
            ContentResult res = new ContentResult();

            //Checks to see if user is an admin and the password is test
            // if thsi is correct it will send the user to the chat view
            if (username == "Admin" && password == "test")
            {
                ViewBag.name = (string)username;
                return View("Chat", new {name = username });
            }
            else
            {
                //or it will return that the username does not exist in the database
                // or the password is incorrect
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


        // This is the controller for the Contact view
        public ActionResult SignUp()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        // This is the controller for the Chat() view
        public ActionResult Chat(string name)
        {
            ViewBag.Message = "Chat, please!";
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
