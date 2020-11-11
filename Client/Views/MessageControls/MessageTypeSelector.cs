using ChatService.Shared.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatService.Client.Views.MessageControls
{
    public class MessageTypeSelector : DataTemplateSelector
    {
        public DataTemplate TextMessageTemplate { get; set; }
        public DataTemplate SoundMessageTemplate { get; set; }
        public DataTemplate ImageMessageTemplate { get; set; }
        public DataTemplate VideoMessageTemplate { get; set; }
        public DataTemplate FileMessageTemplate { get; set; }
        

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            MessageContent data = (MessageContent)item;
            switch (data.ContentTyp)
            {
                case Shared.Models.MessageContentTyp.Text:
                    return TextMessageTemplate;
                case Shared.Models.MessageContentTyp.Sound:
                    return SoundMessageTemplate;
                case Shared.Models.MessageContentTyp.Image:
                    return ImageMessageTemplate;
                case Shared.Models.MessageContentTyp.Video:
                    return VideoMessageTemplate;
                case Shared.Models.MessageContentTyp.File:
                    return FileMessageTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
