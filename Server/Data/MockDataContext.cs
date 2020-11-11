using ChatService.Shared.Models;
using ChatService.Shared.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Server.Data
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class MockDataContext : IDataContext
    {
        public List<Contact> Contacts { get; set; } =  new List<Contact>();

        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Erstellt eine neue Instanz
        /// </summary>
        public MockDataContext()
        {
            Contacts.Add(new Contact { Guid = "1", LastTimeOnline = DateTime.Parse("16.12.2019 10:00:00"), Name = "Hans", State = OnlineState.Offline });
            Contacts.Add(new Contact { Guid = "2", LastTimeOnline = DateTime.Parse("16.12.2019 11:00:00"), Name = "Peter", State = OnlineState.Available });
            Contacts.Add(new Contact { Guid = "3", LastTimeOnline = DateTime.Parse("16.12.2019 10:30:00"), Name = "Moritz", State = OnlineState.Available });
            Contacts.Add(new Contact { Guid = "4", LastTimeOnline = DateTime.Parse("16.12.2019 11:30:00"), Name = "Linda", State = OnlineState.Absent });
        }
    }
}
