using ChatService.Client.Data;
using ChatService.Shared.Models;
using ChatService.Shared.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Client.Service
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class ApiClientService
    {
        private readonly HttpClient client;
        private readonly string url = Properties.Settings.Default.ServerURL;

        public ApiClientService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task PostMe()
        {
            User contact = DataContext.Instance.Iam; 
            HttpResponseMessage response = await client.PostAsJsonAsync(url + $"/api/Login", contact);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Der Kontakt konnte nicht erstellt werden.");
            }
        }

        public async Task<List<Contact>> GetMyContacts()
        {
            List<Contact> output = null;
            HttpResponseMessage response = await client.GetAsync(url + "/api/Contact");
            if (response.IsSuccessStatusCode)
            {
                ///Nuget Microsoft.AspNet.WebApi.Client
                output = await response.Content.ReadAsAsync<List<Contact>>();
            }

            var iam = output.FirstOrDefault(c => c.Guid == DataContext.Instance.Iam.UserGuid);
            if (iam != null)
            {
                output.Remove(iam);
            }
            DataContext.Instance.MyContacts = output;

            return output;
        }

        public async Task<List<Message>> GetMyMessages()
        {
            List<Message> output = null;
            HttpResponseMessage response = await client.GetAsync(url + "/api/Messages/" + DataContext.Instance.Iam.UserGuid);
            if (response.IsSuccessStatusCode)
            {
                ///Nuget Microsoft.AspNet.WebApi.Client
                output = await response.Content.ReadAsAsync<List<Message>>();
            }
            return output;
        }

        public async Task PostMessage(Message message)
        {           
            HttpResponseMessage response = await client.PostAsJsonAsync(url + $"/api/Messages", message);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Die Message konnte nicht erstellt werden.");
            }
        }

        public async Task PutMessageStateModification(MessageStateModification messageState)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(url + $"/api/Messages", messageState);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Die Message konnte nicht verädert werden.");
            }
        }


        public async Task RegisterMe(string userName)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(url + $"/api/Register", userName);
            if (response.IsSuccessStatusCode)
            {
                User user = await response.Content.ReadAsAsync<User>();

                if (user != null)
                {
                    Properties.Settings.Default.MyNickname = userName;
                    Properties.Settings.Default.MyGUID = user.UserGuid;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                throw new Exception("Es konnte keine Anmeldung durchgeführt werden.");
            }
        }
    }
}
