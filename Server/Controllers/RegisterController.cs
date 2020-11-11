using ChatService.Server.DataBase;
using ChatService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatService.Server.Controllers
{
    public class RegisterController : ApiController
    {

        // POST: api/Register
        public User Post([FromBody]string userName)
        {
            User output = new User
            {
                Nickname = userName,
                UserGuid = Guid.NewGuid().ToString()
            };

            ChatServerContext.Users.Add(output);
            ChatServerContext.SaveChanges();

            return output;
        }

        // PUT: api/Register/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Register/5
        //public void Delete(int id)
        //{
        //}
    }
}
