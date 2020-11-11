using ChatService.Server.Data;
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
    public class ContactController : ApiController
    {
        // GET: api/Contact
        public IEnumerable<Contact> Get()
        {            
            return DataContext.Instance.Context.Contacts; 
        }

        // GET: api/Contact/5
        public Contact Get(string contactGuid)
        {
            var contact = DataContext.Instance.Context.Contacts.FirstOrDefault(c => c.Guid == contactGuid);
            return contact;
        }

        // POST: api/Contact
        public void Post([FromBody]Contact contact)
        {
            
        }

        // PUT: api/Contact/5
        public void Put(int id, [FromBody]Contact contact)
        {
        }

        // DELETE: api/Contact/5
        public void Delete(string contactGuid)
        {
        }
    }
}
