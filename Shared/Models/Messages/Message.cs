using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ChatService.Shared.Models.Messages
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class Message 
    {
        [Key]
        public string MessageGuid { get; set; }
        public string FromGuid { get; set; }
        public string ToGuid { get; set; } 
        public MessageContent Content { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsMyMessage { get; set; }
        public User From { get; set; }
        public MessageState State { get; set; }     
    }
}
