using ChatService.Shared.Models.Messages;
using System.Windows;
using System.Windows.Controls;

namespace ChatService.Client.Views.MessageControls
{
    public class MessageStateSelector : DataTemplateSelector
    {
        public DataTemplate MessageOnServerTemplate { get; set; }
        public DataTemplate MessageReceivedTemplate { get; set; }
        public DataTemplate MessageReadedTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }
                       
            Message message = (Message)item;

            if(message.IsMyMessage == false)
            {
                return null;
            }

            switch (message.State)
            {
                case Shared.Models.MessageState.Readed:
                    return MessageReadedTemplate;
                case Shared.Models.MessageState.Received:
                    return MessageReceivedTemplate;
                case Shared.Models.MessageState.OnServer:
                    return MessageOnServerTemplate;
                case Shared.Models.MessageState.None:
                    return null;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
