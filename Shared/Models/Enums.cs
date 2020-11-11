using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Shared.Models
{
    public enum OnlineState
    {
        Available,
        Absent,
        Offline
    }

    public enum MessageState
    {
        Readed,
        Received,
        OnServer,
        None
    }

    public static class MessageStateConverter
    {
        public static MessageState Convert(string state)
        {
            MessageState output;

            switch (state)
            {
                case "WasRead": output = MessageState.Readed; break;
                case "Received": output = MessageState.Received; break;
                case "OnServer": output = MessageState.OnServer; break;
                case "None": output = MessageState.None; break;

                default:
                    throw new Exception($"MessageState ist nicht in dieser Form ({state}) hinterlegt.");
            }

            return output;
        }
    }

    public enum MessageContentTyp
    {
        Text,
        Sound,
        Image,
        Video,
        File
    }
}
