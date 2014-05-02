using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;

using Microsoft.AspNet.SignalR;
namespace TelerikMvcApp1
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);

            var db = ContextFactory.GetContextPerRequest();
            Chat newChat = new Core.Chat();
            newChat.Time = DateTime.Now;
            newChat.Name = name;
            newChat.Message = message;
            db.Add(newChat);
            db.SaveChanges();
        }
    }
}