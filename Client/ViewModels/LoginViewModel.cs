using ChatService.Client.Commands;
using ChatService.Client.Data;
using ChatService.Client.Service;
using ChatService.Client.Views;
using ChatService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatService.Client.ViewModels
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class LoginViewModel
    {
        public ICommand LoginCommand { get; }

        public Action CloseAction;
        public string UserName { get; set; }


        public List<User> Users { get; }

        public User SelectedUser { get; set; }

        /// <summary>
        /// Erstellt eine neue Instanz
        /// </summary>
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginExecute, LoginCanExecute);

            Users = new List<User>
            {
                new User { Nickname = Properties.Settings.Default.MyNickname, UserGuid = Properties.Settings.Default.MyGUID },
                new User { Nickname = "User1", UserGuid = "146f61f1-6dd0-471f-a8ed-264be2293630" },
                new User { Nickname = "User2", UserGuid = "195471a1-459a-4b26-9d10-14a885e50cba" },
                new User { Nickname = "User3", UserGuid = "1537974b-283b-44fa-8d2d-2e80ba4fc245" },
                new User { Nickname = "User4", UserGuid = "c86f038a-896e-432f-9e8d-d3ecf3a9aae2" }
            };

        }

        private bool LoginCanExecute(object obj)
        {
            return true; //UserName?.Length > 0;
        }

        private async void LoginExecute(object obj)
        {
            if (SelectedUser != null)
            {
                DataContext.Instance.Iam.Nickname = SelectedUser.Nickname;
                DataContext.Instance.Iam.UserGuid = SelectedUser.UserGuid;
            }
            else
            {
                ApiClientService apiClientService = new ApiClientService();
                await apiClientService.RegisterMe(UserName);
            }

            CloseAction();
        }
    }
}
