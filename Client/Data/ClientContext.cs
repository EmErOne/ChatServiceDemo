using ChatService.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Client.Data
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class ClientContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
                  => options.UseSqlite("Data Source=Client.db");

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

    }
}
