using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ChatService.Shared.Models.Messages
{
    /// <summary>
    /// Dient zum Darstellen des Inhaltes einer Nachricht 
    /// </summary>
    
    public class MessageContent : IMessageContent
    {
        public string ContentString { get; set; }
        [Key]
        public MessageContentTyp ContentTyp { get; set; }
        public string FileName { get; set; }

        public MessageContent() { }

        public MessageContent(string text)
        {
            this.ContentString = text;
            this.ContentTyp = MessageContentTyp.Text;
        }

        public MessageContent(string filepath, MessageContentTyp contentTyp)
        {
            this.ContentString = CreateBase64(filepath);
            this.ContentTyp = contentTyp;
            this.FileName = Path.GetFileName(filepath);
        }

        private string CreateBase64(string path)
        {
            Byte[] bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }

        public string GetContentString()
        {
            return ContentString;
        }
    }
}
