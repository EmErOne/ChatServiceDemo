using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Shared.Models
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class User
    {
        public string ConnectionGuid { get; set; }

        [Key]
        public string UserGuid { get; set; }

        public string Nickname { get; set; }

        public User()
        {
            UserGuid = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return Nickname;
        }
    }
}
