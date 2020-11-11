using ChatService.Client.Data;
using ChatService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatService.Shared.Models.Messages;
using System.IO;

namespace ChatService.Client.Service
{
    /// <summary>
    /// Dient zum Interagieren einer Nachricht
    /// </summary>
    public static class MessageService
    {
        private static readonly ApiClientService apiClientService = new ApiClientService();

        /// <summary>
        /// Versendet eine Text-Nachricht
        /// </summary>
        public static async Task SendTextMessage(String toGuid, String content)
        {
            await SendMessage(toGuid, new MessageContent(content));
        }

        /// <summary>
        /// Versendet eine Nachricht
        /// </summary>
        public static async Task<Message> SendMessage(String toGuid, MessageContent content)
        {
            Message message = new Message
            {
                ToGuid = toGuid,
                FromGuid = DataContext.Instance.Iam.UserGuid,
                Content = content,
                DateTime = DateTime.Now,
                MessageGuid = Guid.NewGuid().ToString(),
                From = DataContext.Instance.Iam,
                State = MessageState.None
            };

            await apiClientService.PostMessage(message);
            return message;
        }

        /// <summary>
        /// Öffnet eine Nachricht und stellt diese dar
        /// </summary>
        /// <param name="content"></param>
        public static void OpenMessage(MessageContent content)
        {
            if (content.ContentTyp != MessageContentTyp.Text)
            {
                string path = Path.Combine(Path.GetTempPath(), content.FileName);

                Byte[] bytes = Convert.FromBase64String(content.ContentString);
                File.WriteAllBytes(path, bytes);
                System.Diagnostics.Process.Start(path);
            }
        }

        /// <summary>
        /// Meldet das eine Nachricht empfangen wurde
        /// </summary>
        /// <param name="messageGuid"></param>
        /// <returns></returns>
        public static async Task ReportMessageHasBeenReceived(string messageGuid)
        {
            MessageStateModification messageStateModification = new MessageStateModification
            {
                MessageGuid = messageGuid,
                MessageState = MessageState.Received
            };

            await apiClientService.PutMessageStateModification(messageStateModification);
        }

        /// <summary>
        /// Meldet das eine Nachricht gelesen wurde.
        /// </summary>
        /// <param name="messageGuid"></param>
        /// <param name="messageState"></param>
        /// <returns></returns>
        public static async Task ReportMessageHasBeenReaded(string messageGuid)
        {
            MessageStateModification messageStateModification = new MessageStateModification
            {
                MessageGuid = messageGuid,
                MessageState = MessageState.Readed
            };

            await apiClientService.PutMessageStateModification(messageStateModification);
        }
    }
}
