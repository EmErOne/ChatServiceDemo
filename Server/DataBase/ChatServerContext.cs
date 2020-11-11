using ChatService.Shared.Models;
using ChatService.Shared.Models.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChatService.Server.DataBase
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public static class ChatServerContext
    {
        private static string dbPath;
        public static List<User> Users { get; set; }
        public static List<Message> Messages { get; set; }

        internal static void Init()
        {
            dbPath = Path.Combine(Path.GetTempPath(), "ChatServerDB");

            Directory.CreateDirectory(dbPath);

            LoadContext();

            if (ChatServerContext.Users == null)
            {
                GenerateData();
            }
        }

        public static void SaveChanges()
        {
            Save(Users, "Users");
            Save(Messages, "Messages");
        }

        public static void LoadContext()
        {
            Users = Load<List<User>>("Users");
            Messages = Load<List<Message>>("Messages");
        }


        #region Speichern und Lesen

        public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public static void Save<T>(List<T> list, string name)
        {
            string xml = ToXML(list);
            File.WriteAllText($"{dbPath}\\{name}.db", xml);
        }


        public static T Load<T>(string name)
        {
            string path = $"{dbPath}\\{name}.db";

            if (File.Exists(path))
            {
                string xml = File.ReadAllText(path);
                return FromXML<T>(xml);
            }

            return default;
        }
        #endregion

        #region Erstellen für den Default fall

        private static void GenerateData()
        {
            Users = new List<User>();
            Messages = new List<Message>();

            User user1 = new User { Nickname = "User1" };
            User user2 = new User { Nickname = "User2" };
            User user3 = new User { Nickname = "User3" };
            User user4 = new User { Nickname = "User4" };

            Users.Add(user1);
            Users.Add(user2);
            Users.Add(user3);
            Users.Add(user4);

            Messages.Add(new Message
            {
                Content = new MessageContent("Hallo User1 wie geht es?"),
                DateTime = DateTime.Parse("18.12.2019 12:01:00"),
                FromGuid = user2.UserGuid,
                ToGuid = user1.UserGuid,
                MessageGuid = Guid.NewGuid().ToString(),
                From = user1,
                State = MessageState.Received
            });

            Messages.Add(new Message
            {
                Content = new MessageContent("Hallo User2 Mir geht es super!!!!"),
                DateTime = DateTime.Parse("18.12.2019 12:02:00"),
                FromGuid = user1.UserGuid,
                ToGuid = user2.UserGuid,
                MessageGuid = Guid.NewGuid().ToString(),
                From = user1,
                State = MessageState.Received
            });

            Messages.Add(new Message
            {
                Content = new MessageContent("Das ist ja tolll!"),
                DateTime = DateTime.Parse("18.12.2019 12:03:00"),
                FromGuid = user1.UserGuid,
                ToGuid = user2.UserGuid,
                MessageGuid = Guid.NewGuid().ToString(),
                From = user1,
                State = MessageState.Received
            });

            Messages.Add(new Message
            {
                Content = new MessageContent("Grüße"),
                DateTime = DateTime.Parse("18.12.2019 12:04:00"),
                FromGuid = user2.UserGuid,
                ToGuid = user1.UserGuid,
                MessageGuid = Guid.NewGuid().ToString(),
                From = user2,
                State = MessageState.Received
            });

            SaveChanges();
        }
        #endregion

        #region Methoden

        public static User GetUser(string userGuid)
        {
            return ChatServerContext.Users.FirstOrDefault(u => u.UserGuid == userGuid);
        }

        #endregion

    }
}
