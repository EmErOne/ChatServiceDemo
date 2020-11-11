using ChatService.Client.Commands;
using ChatService.Client.Data;
using ChatService.Client.Service;
using ChatService.Shared.Models;
using ChatService.Shared.Models.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatService.Client.ViewModels
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class ChatViewModel : ViewModelBase, IChatClient
    {
        readonly ChatHubService chatHubService;

        public ObservableCollection<Conversation> Conversations { get; set; }

        private User selectedContact;
        public User SelectedContact
        {
            get
            {
                return selectedContact;
            }
            set
            {
                if (value != selectedContact)
                {
                    selectedContact = value;
                    OnPropertyChanged();
                }
            }
        }


        private Conversation conversation = new Conversation();
        public Conversation Conversation
        {
            get { return conversation; }
            set
            {
                if (value != conversation)
                {
                    conversation = value;
                    OnPropertyChanged();
                }

            }
        }

        public ICommand SendCommand { get; }
        public ICommand SelectedItemChangedCommand { get; set; }

        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if (value != content)
                {
                    content = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Erstellt eine neue Instanz
        /// </summary>
        public ChatViewModel()
        {
            SendCommand = new RelayCommand(SendExecute, SendCanExecute);
            SelectedItemChangedCommand = new RelayCommand(SelectedItemChangedExecute);
            chatHubService = new ChatHubService(this);
        }

        private bool SendCanExecute(object obj)
        {
            return SelectedContact != null;
        }

        private async void SelectedItemChangedExecute(object obj)
        {
            if (obj is Conversation conversation)
            {
                SelectedContact = conversation.From;
                Conversation = conversation;

                var unReadedMessages = conversation.Messages.Where(m => m.State != MessageState.Readed);
                foreach (var message in unReadedMessages)
                {
                    await MessageService.ReportMessageHasBeenReaded(message.MessageGuid);
                }
            }
        }

        private async void SendExecute(object obj)
        {           
            MessageContent content;

            if (obj is string path)
            {
                content = new MessageContent(path, MessageContentTyp.Image);                                            
            }
            else
            {
                content = new MessageContent(Content);
                Content = string.Empty;
            }

            Message message = await MessageService.SendMessage(SelectedContact.UserGuid, content);
            message.IsMyMessage = true;
            Conversation.Messages.Add(message);
        }

        public async void Connect()
        {
            await chatHubService.ConnectAsync();

            if (chatHubService.Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
            {
                ApiClientService apiClientService = new ApiClientService();
                await apiClientService.PostMe();

                Conversations = new ObservableCollection<Conversation>(DataContext.Instance.Conversations);
                OnPropertyChanged(nameof(Conversations));
            }
            else
            {
                MessageBox.Show("Es konnte keine Verbindung zum ChatHub aufgebaut werden.");
            }
        }

        public async Task NewMessageIncoming(Message message)
        {
            if (message.FromGuid == SelectedContact?.UserGuid)
            {
                OnPropertyChanged(nameof(Conversation));
                OnPropertyChanged(nameof(Conversations));

                await MessageService.ReportMessageHasBeenReaded(message.MessageGuid);
            }
        }

        public void NewConversationIncoming(Conversation conversation)
        {
            Conversations = new ObservableCollection<Conversation>(DataContext.Instance.Conversations);
            OnPropertyChanged(nameof(Conversations));
        }

        public void UpdateMessageState(MessageStateModification messageStateModification)
        {           
                OnPropertyChanged(nameof(Conversation));
                OnPropertyChanged(nameof(Conversations));          
        }
    }
}
