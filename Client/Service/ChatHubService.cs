using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ChatService.Client.Data;
using ChatService.Client.ViewModels;
using ChatService.Shared.Models;
using ChatService.Shared.Models.Messages;
using Microsoft.AspNet.SignalR.Client;

namespace ChatService.Client.Service
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class ChatHubService
    {
        readonly IChatClient chatClient;
        public IHubProxy Proxy { get; set; }
        public HubConnection Connection { get; set; }
        public string ChatHubID { get; private set; }
        public bool Active { get; set; }

        /// <summary>
        /// Erstellt eine neue Instanz
        /// </summary>
        public ChatHubService(IChatClient chatViewModel)
        {
            chatClient = chatViewModel;
        }

        public async Task ConnectAsync()
        {
            await Task.Run(() => Connect());
        }

        public void Connect()
        {
           
            Connection = new HubConnection(Properties.Settings.Default.ServerURL);
            Proxy = Connection.CreateHubProxy(Constants.ChatHub);
            
            Proxy.On<Message>("ReceivePrivateMessage", (message) => ReceivePrivateMessage(message));
            Proxy.On<MessageStateModification>("ReceiveMessageStateModification", (messageModification) => ReceiveMessageStateModification(messageModification));

            Connection.Headers.Add(Constants.UserGuid, DataContext.Instance.Iam.UserGuid);
            Connection.Start();

            while (Connection.State == ConnectionState.Connecting)
            { }

            if (Connection.State == ConnectionState.Connected)
            {
                DataContext.Instance.Iam.ConnectionGuid = Connection.ConnectionId;               
            }
        }

        private void ReceiveMessageStateModification(MessageStateModification messageModification)
        {
            foreach (var conversation in DataContext.Instance.Conversations)
            {
                var message = conversation.Messages.FirstOrDefault(m => m.MessageGuid == messageModification.MessageGuid);
                if(message != null)
                {
                    message.State = messageModification.MessageState;
                    chatClient?.UpdateMessageState(messageModification);
                    break;
                }
            }            
        }

        private async Task ReceivePrivateMessage(Message message)
        {
            await MessageService.ReportMessageHasBeenReceived(message.MessageGuid);

            var conversation = DataContext.Instance.Conversations.FirstOrDefault(c => c.From?.UserGuid == message.FromGuid);
            if(conversation != null)
            {
                message.From = conversation.From;
                Application.Current.Dispatcher.Invoke( new Action(() =>  conversation.Messages.Add((message))));    
                chatClient?.NewMessageIncoming(message);                
            }
            else
            {
                Conversation newConversation = new Conversation
                {
                    From = message.From
                };
                message.From = newConversation.From;
                newConversation.Messages.Add((message));
                DataContext.Instance.Conversations.Add(newConversation);
                chatClient?.NewConversationIncoming(newConversation);               
            }            
        }    
    }
}
