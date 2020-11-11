using ChatService.Server.Data;
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
using ChatService.Shared.Models.Messages;

namespace ChatService.Server.Controllers
{
    public class MessagesController : ApiController
    {
        readonly IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

        // GET: api/Messages/5
        public IEnumerable<Message> Get(string user)
        {
            var messages = ChatServerContext.Messages.Where(m => m.ToGuid == user).ToList();
            return messages;
        }

        // POST: api/Messages
        public void Post([FromBody]Message message)
        {
            DataContext.Instance.Context.Messages.Add(message);

            ChatServerContext.Messages.Add(message);
            ChatServerContext.SaveChanges();

            var messageState = new MessageStateModification { MessageGuid = message.MessageGuid, MessageState = MessageState.OnServer };
            hubContext.Clients.Client(message.From.ConnectionGuid).ReceiveMessageStateModification(messageState);

            var conGuid = ChatServerContext.Users.FirstOrDefault(u => u.UserGuid == message.ToGuid)?.ConnectionGuid;
            if (conGuid != null)
            {
                hubContext.Clients.Client(conGuid).ReceivePrivateMessage(message);
            }
        }

        // Put: api/Messages        
        public void Put([FromBody]MessageStateModification messageState)
        {
            var message = ChatServerContext.Messages.FirstOrDefault(m => m.MessageGuid == messageState.MessageGuid);
            if (message != null)
            {
                message.State = messageState.MessageState;
                hubContext.Clients.Client(message.From.ConnectionGuid).ReceiveMessageStateModification(messageState);
            }

            ChatServerContext.SaveChanges();
        }
    }
}
