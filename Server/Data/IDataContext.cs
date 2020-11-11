using ChatService.Shared.Models;
using ChatService.Shared.Models.Messages;
using System.Collections.Generic;

namespace ChatService.Server.Data
{
    public interface IDataContext
    {
        List<Contact> Contacts { get; set; }
        List<Message> Messages { get; set; }
    }
}