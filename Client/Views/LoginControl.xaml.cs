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
    /// Interaktionslogik für LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        readonly IPresentationWindow presentationWindow;

        public LoginControl(IPresentationWindow presentationWindow)
        {
            this.presentationWindow = presentationWindow;
            InitializeComponent();

            LoginViewModel loginViewModel = new LoginViewModel();
            if (loginViewModel.CloseAction == null)
                loginViewModel.CloseAction = new Action(StartApp);

            this.DataContext = loginViewModel;
        }

        private void StartApp()
        {
            presentationWindow.ShowChatControl();
        }
    }
}
