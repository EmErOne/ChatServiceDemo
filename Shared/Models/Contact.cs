using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Shared.Models
{
    /// <summary>
    /// Dient zum darstellen eines Kontaktes
    /// </summary>
    public class Contact
    {
        [Key]
        public string Guid { get; set; }
        public string Name { get; set; }
        public OnlineState State { get; set; }
        public DateTime LastTimeOnline { get; set; }
        public string ImageBase64 { get; set; }



        /// <summary>
        /// Erstellt eine neue Instanz
        /// </summary>
        public Contact()
        {

        }
    }
}
