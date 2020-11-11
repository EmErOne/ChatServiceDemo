using ChatService.Shared.Models;
using ChatService.Shared.Models.Messages;
using System.Threading.Tasks;

namespace ChatService.Client.ViewModels
{
    public  interface IChatClient
    {
        Task NewMessageIncoming(Message message);

        void NewConversationIncoming(Conversation conversation);

        void UpdateMessageState(MessageStateModification messageStateModification);
    }
}