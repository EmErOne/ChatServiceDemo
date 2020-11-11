using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatService.Shared.Models.Messages;

namespace ChatService.Shared.Models
{
    /// <summary>
    /// Dient zum 
    /// </summary>    
    public class Conversation
    {
        public int ID { get; set; }
        public User From { get; set; }
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();

        public string LastMessageContent
        {
            get
            {
                return GetLastTextMessage();
            }
        }

        public string GetLastTextMessage()
        {
            string output = String.Empty;

            for(int i = Messages.Count -1; i >= 0; i--)
            {
                if(Messages[i].Content is MessageContent message && message.ContentTyp == MessageContentTyp.Text)
                {
                    output = message.ContentString;
                    break;
                }
            }

            return output;
        }

        public string LastMessageTime
        {
            get
            {
                var time = Messages[Messages.Count - 1].DateTime;
                string first;
                string second;
                if (time.Day == DateTime.Now.Day && time.Month == DateTime.Now.Month && time.Year == DateTime.Now.Year)
                {
                    int hh = time.Hour;
                    int mm = time.Minute;

                    if (hh < 10)
                    {
                        first = "0" + hh.ToString();
                    }
                    else
                    {
                        first = hh.ToString();
                    }

                    if (mm < 10)
                    {
                        second = ":0" + mm.ToString();
                    }
                    else
                    {
                        second = ":" + mm.ToString();
                    }

                }
                else
                {

                    int dd = time.Day;
                    int MM = time.Month;

                    if (dd < 10)
                    {
                        first = "0" + dd.ToString();
                    }
                    else
                    {
                        first = dd.ToString();
                    }

                    if (MM < 10)
                    {
                        second = ".0" + MM.ToString();
                    }
                    else
                    {
                        second = "." + MM.ToString();
                    }

                }

                return $"{first}{second}";
            }
        }
    }
}
