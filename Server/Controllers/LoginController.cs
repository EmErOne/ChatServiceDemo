using ChatService.Server.DataBase;
using ChatService.Server.Hubs;
using ChatService.Shared.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatService.Server.Controllers
{
    public class LoginController : ApiController
    {
 
        // POST: api/Login
        public void Post([FromBody]User user)
        {
            var searchUser = ChatServerContext.GetUser(user.UserGuid);
            if (searchUser == null)
            {
                ChatServerContext.Users.Add(user);
                ChatServerContext.SaveChanges();
            }

            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.Client(user.ConnectionGuid).Hello();
        }

        //// PUT: api/Login/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Login/5
        //public void Delete(int id)
        //{
        //}
    }
}
