using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChatService.Server.DataBase;
using ChatService.Shared.Models;
using ChatService.Shared.Models.Messages;
using Microsoft.AspNet.SignalR;

namespace ChatService.Server.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            var connectionGuid = Context.ConnectionId;
            var userGuid = Context.Headers.Get(Constants.UserGuid);

            var searchUser = ChatServerContext.GetUser(userGuid);
            if (searchUser != null)
            {
                searchUser.ConnectionGuid = connectionGuid;
                ChatServerContext.SaveChanges();
            }     

            return base.OnConnected();
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public void SendMessageTo(Message message)
        {
            Clients.Client(message.ToGuid).ReceivePrivateMessage(message);
        }
    }
}