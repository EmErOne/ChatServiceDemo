using ChatService.Client.Data;
using ChatService.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatService.Client.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPresentationWindow
    {
        private ChatControl chatControl;
        private ChatViewModel chatViewModel;

        public MainWindow()
        {
            InitializeComponent();
            MasterContentControl.Content = new LoginControl(this);

            //using (var db = new ClientContext())
            //{
            //    if (db.Database.EnsureCreated())
            //    {

            //    }
            //}

        }

        public void ShowChatControl()
        {
            chatControl = new ChatControl();
            chatViewModel = new ChatViewModel();
            chatViewModel.Connect();
            chatControl.DataContext = chatViewModel;

            MasterContentControl.Content = chatControl;
        }
    }
}
